using AutoMapper;
using Backend.Model.Pharmacies;
using Backend.Service.Pharmacies;
using IntegrationAdapters.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationAdapters.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PharmaciesController : ControllerBase
    {
        private readonly IPharmacyService _phamracyService;
        private readonly IMapper _mapper;

        public PharmaciesController(IPharmacyService service, IMapper mapper)
        {
            _phamracyService = service;
            _mapper = mapper;
        }

        //GET api/pharmacies
        [HttpGet]
        public ActionResult<IEnumerable<PharmacyReadDto>> GetAllPharmacies()
        {
            var pharmacyItems = _phamracyService.GetAllPharmacies();

            return Ok(_mapper.Map<IEnumerable<PharmacyReadDto>>(pharmacyItems));
        }

        //GET api/pharmacies/{id}
        [HttpGet("{id}", Name = "GetPharmacyById")]
        public ActionResult<PharmacyReadDto> GetPharmacyById(int id)
        {
            var pharmacyItem = _phamracyService.GetPharmacyById(id);
            if (pharmacyItem != null)
            {
                return Ok(_mapper.Map<PharmacyReadDto>(pharmacyItem));
            }

            return NotFound();
        }

        //POST api/pharmacies
        [HttpPost]
        public ActionResult<PharmacyReadDto> CreatePharmacy(PharmacyCreateDto pharmacyCreateDto)
        {
            var pharmacyModel = _mapper.Map<Pharmacy>(pharmacyCreateDto);
            _phamracyService.CreatePharmacy(pharmacyModel);

            var pharmacyReadDto = _mapper.Map<PharmacyReadDto>(pharmacyModel);

            return CreatedAtRoute(nameof(GetPharmacyById), new { Id = pharmacyReadDto.Id }, pharmacyReadDto);
        }

        //PUT api/pharmacies/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePharmacy(int id, PharmacyUpdateDto pharmacyUpdateDto)
        {
            var pharmacyModelFromRepo = _phamracyService.GetPharmacyById(id);
            if (pharmacyModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(pharmacyUpdateDto, pharmacyModelFromRepo);
            _phamracyService.UpdatePharmacy(pharmacyModelFromRepo);

            return NoContent();
        }

        //DELETE api/pharmacies/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePharmacy(int id)
        {
            var pharmacyModelFromRepo = _phamracyService.GetPharmacyById(id);
            if (pharmacyModelFromRepo == null)
            {
                return NotFound();
            }
            _phamracyService.DeletePharmacy(pharmacyModelFromRepo);

            return NoContent();
        }
    }
}

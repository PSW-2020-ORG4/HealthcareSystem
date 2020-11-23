using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service.DrugAndTherapy;
using Backend.Service.ExaminationAndPatientCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TherapyController : ControllerBase
    {
        private readonly ITherapyService _therapyService;
        private readonly IExaminationService _examinationService;
        public TherapyController(ITherapyService therapyService, IExaminationService examinationService)
        {
            _therapyService = therapyService;
            _examinationService = examinationService;
        }
        /// <summary>
        /// /getting examinations linked to a certain patient
        /// </summary>
       
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpPost("{idExamination}")]
        public ActionResult GetTherapyByPatientSearch(int idExamination, TherapyDTO therapyDTO)
        {
            try
            {
                Therapy therapy = TherapyMapper.TherapyDTOToTherapy(therapyDTO);
                Examination examination = _examinationService.GetExaminationById(idExamination);
                List<Therapy> therapies = examination.Therapies.ToList();
                List <TherapyDTO> therapyDTOs = new List<TherapyDTO>();
                foreach (Therapy t in therapies)
                {
                    //_therapyService.GetTherapyByPatientSearch(therapy).ForEach(t => therapyDTOs.Add(TherapyMapper.TherapyToTherapyDTO(t)));
                    therapyDTOs.Add(TherapyMapper.TherapyToTherapyDTO(t));
                }
                //Therapy t=_therapyService.GetTherapyByPatientSearch(therapy);
                //TherapyDTO dto = TherapyMapper.TherapyToTherapyDTO(t);
                return Ok(therapyDTOs);
                
             }
            catch (Exception)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// /getting examinations linked to a certain drug
        /// </summary>
        /// <param name="idDrug">id of a wanted drug</param>
        /// <returns></returns>
        [HttpGet("{idDrug}")]
        public ActionResult GetTherapyByDrug(int idDrug)
        {
            try
            {
                List<TherapyDTO> therapyDTOs = new List<TherapyDTO>();
                _therapyService.GetTherapyByDrug(idDrug).ForEach(therapy => therapyDTOs.Add(TherapyMapper.TherapyToTherapyDTO(therapy)));
                return Ok(therapyDTOs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}

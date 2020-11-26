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
        /// /getting therapies linked to a certain examination for basic search
        /// </summary>
        /// <param name="idExamination"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="doctorSurname"></param>
        /// <param name="drugName"></param>
        /// <returns>list od Therapy DTOs</returns>
        [HttpGet("{idExamination}/{startDate}/{endDate}/{doctorSurname}/{drugName}")]
        public ActionResult GetTherapiesByExaminationSearch(int idExamination, string startDate, string endDate, string doctorSurname, string drugName)
        {
            try
            {
               Examination examination = _examinationService.GetExaminationById(idExamination);
                List<Therapy> therapies = examination.Therapies.ToList();
                List<TherapyDTO> therapyDTOs = new List<TherapyDTO>();
                _therapyService.GetTherapiesByExaminationSearch(therapies, startDate, endDate, doctorSurname, drugName).ForEach(therapy => therapyDTOs.Add(TherapyMapper.TherapyToTherapyDTO(therapy)));
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

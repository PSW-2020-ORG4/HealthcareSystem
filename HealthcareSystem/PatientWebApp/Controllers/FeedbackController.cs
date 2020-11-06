﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service.NotificationSurveyAndFeedback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.Adapters;
using PatientWebApp.DTOs;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(int id)
        {
            try
            {
                Feedback feedback = _feedbackService.GetFeedbackById(id);
                return Ok(FeedbackAdapter.FeedbackToFeedbackDTO(feedback));
            }
            catch (Exception) {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult AddFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                Feedback feedback = FeedbackAdapter.FeedbackDTOToFeedback(feedbackDTO);
                _feedbackService.AddFeedback(feedback);
                return Ok();
            } catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("published-feedbacks")]
        public ActionResult getPublishedFeedbacks()
        {
            try
            {
                List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
                _feedbackService.GetPublishedFeedbacks().ForEach(feedback => feedbackDTOs.Add(FeedbackAdapter.FeedbackToFeedbackDTO(feedback)));
                return Ok(feedbackDTOs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
		
		[HttpGet("unpublished-feedbacks")]
        public ActionResult getUnpublishedFeedbacks()
        {
            try
            {
                List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
                _feedbackService.GetUnpublishedFeedbacks().ForEach(feedback => feedbackDTOs.Add(FeedbackAdapter.FeedbackToFeedbackDTO(feedback)));
                return Ok(feedbackDTOs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult PublishFeedback(int id)
        {
            try
            {
                _feedbackService.PublishFeedback(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}

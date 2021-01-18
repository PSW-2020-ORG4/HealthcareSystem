using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.DTOs;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet]
        public ActionResult Add(StatisticEventDTO statisticEventDTO)
        {
            return RequestAdapter.SendRequestWithBody("http://localhost:58828", "/api/event", statisticEventDTO);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet]
        public ActionResult GetAll()
        {
            return RequestAdapter.SendRequestWithoutBody("http://localhost:58828", "/api/event", Method.GET);
        }

    }
}

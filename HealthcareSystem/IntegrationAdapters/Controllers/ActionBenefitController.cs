using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapters.Controllers
{
    public class ActionBenefitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IActionBenefitService _actionBenefitService;

        public ActionBenefitController(IActionBenefitService actionBenefitService)
        {
            _actionBenefitService = actionBenefitService;
        }

        public IActionResult Index()
        {
            return View(_actionBenefitService.GetAllActionsBenefits());
        }

        public IActionResult Details(int id)
        {
            return View(_actionBenefitService.GetActionBenefitById(id));
        }

        [Route("ActionBenefit/MakePublic/{id}/{isPublic}")]
        public IActionResult MakePublic(int id, bool isPublic)
        {
            _actionBenefitService.MakePublic(id, isPublic);
            return RedirectToAction("Details", new { id = id });
        }
    }
}

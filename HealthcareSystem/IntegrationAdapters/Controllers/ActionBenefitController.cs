using IntegrationAdapters.MicroserviceComunicator;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntegrationAdapters.Controllers
{
    public class ActionBenefitController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IActionBenefitService _actionBenefitService;

        public ActionBenefitController(IActionBenefitService actionBenefitService)
        {
            _actionBenefitService = actionBenefitService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _actionBenefitService.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _actionBenefitService.Get(id));
        }

        [Route("ActionBenefit/MakePublic/{id}/{isPublic}")]
        public async Task<IActionResult> MakePublic(int id, bool isPublic)
        {
            await _actionBenefitService.SetPublic(id, isPublic);
            return RedirectToAction("Details", new { id = id });
        }
    }
}

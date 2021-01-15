using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingService.Model.GraphicalEditor;
using EventSourcingService.Service.GraphicalEditor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingService.Controllers.GraphicalEditor
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapObjectSelectionController : ControllerBase
    {
        private readonly IMapObjectSelectionService _mapObjectSelectionService;

        public MapObjectSelectionController(IMapObjectSelectionService mapObjectSelectionService)
        {
            _mapObjectSelectionService = mapObjectSelectionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var mapObjectSelectionService = _mapObjectSelectionService.GetAll();
            return Ok(mapObjectSelectionService);
        }

        [HttpPost]
        public ActionResult Add(MapObjectSelectionEvent mapObjectSelectionEvent)
        {
            _mapObjectSelectionService.Add(mapObjectSelectionEvent);
            return NoContent();
        }
    }
}
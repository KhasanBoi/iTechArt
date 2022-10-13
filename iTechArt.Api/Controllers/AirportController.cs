﻿using iTechArt.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportsService _airportsService;
        public AirportController(IAirportsService airportsService)
        {
            _airportsService = airportsService;
        }

        [HttpPost("import")]
        public IActionResult ImportAirportExcel(IFormFile file)
        {
            if (file != null && (file.ContentType.EndsWith("xlsx") || file.ContentType.Contains("officedocument.spreadsheetml.sheet")))
            {
                return Ok(_airportsService.ImportAirportExcel());
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("export")]
        public IActionResult ExportAirportExcel()
        {
            return Ok(_airportsService.ExportAirportExcel());
        }
    }
}

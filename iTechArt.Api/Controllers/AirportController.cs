﻿using iTechArt.Api.Constants;
using iTechArt.Domain.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Api.Controllers
{
    [Route(RouteConstants.AIRPORT)]
    [ApiController]
    public sealed class AirportController : ControllerBase
    {
        private readonly IAirportsService _airportsService;

        public AirportController(IAirportsService airportsService)
        {
            _airportsService = airportsService;
        }

        /// <summary>
        /// Controller of Importing airport data
        /// </summary>
        /// <param name="file"></param>
        [HttpPost(ApiConstants.IMPORT), Obsolete]
        public async Task<IActionResult> ImportAirportExcel(IFormFile file)
        {
            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);

                if (FileConstants.Extensions.Contains(fileExtension))
                {
                    await _airportsService.ImportAirportFile(file);
                    return Ok();
                }

                return BadRequest("Invalid file format!");
            }
            else
            {
                return BadRequest("Invalid file format!");
            }
        }

        /// <summary>
        /// Controller of Exporting airport data
        /// </summary>
        [HttpGet("get_all")]
        public async Task<IActionResult> ExportAirportExcel()
        {
            return Ok( await _airportsService.ExportAirportExcel());
        }

        [HttpPost(ApiConstants.IMPORTEXCEL)]
        public async Task<IActionResult> ImportExcelFile(IFormFile file)
        {
            await _airportsService.AirportExcelParser(file);
            return Ok();
        }
        [HttpPost(ApiConstants.IMPORTCSV)]
        public async Task<IActionResult> ImportCsvFile(IFormFile file)
        {
            await _airportsService.AirportCSVParser(file);
            return Ok();
        }
        [HttpPost(ApiConstants.IMPORTXML)]
        public async Task<IActionResult> ImportXmlFile(IFormFile file)
        {
            await _airportsService.AirportXMLParser(file);
            return Ok();
        }
    }
}

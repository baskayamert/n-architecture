using Business.Abstract;
using Business.ActionFilters.Validation;
using Core.CrossCuttingConcerns.Authorization;
using Entities.Concrete;
using Entities.DTOs.PatientDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static Business.ValidationRules.FluentValidation.PatientValidator;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        
        [HttpGet("getall")]
        [Auth("Role")]
        public IActionResult GetAll()
        {
            var result = _patientService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(ValidationFilterAttribute<PatientRegisterDto, PatientRegisterValidator>))]
        public IActionResult Add(PatientRegisterDto patient)
        {
            patient.Birthdate = patient.Birthdate.ToUniversalTime();

            var result = _patientService.Add(patient);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _patientService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        [ServiceFilter(typeof(ValidationFilterAttribute<PatientUpdateDto, PatientUpdateValidator>))]
        public IActionResult Update(PatientUpdateDto patient)
        {
            var result = _patientService.Update(patient);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        [ServiceFilter(typeof(ValidationFilterAttribute<PatientDeletionDto, PatientDeletionValidator>))]

        public IActionResult Delete(PatientDeletionDto patient)
        {
            var result = _patientService.Delete(patient);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("filterbyage")]
        public IActionResult FilterByAge([FromQuery] int startAge, [FromQuery] int endAge)
        {
            var result = _patientService.FilterByAge(startAge, endAge);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("patientsbyhospitalid")]
        public IActionResult GetPatientsByHospitalId(int hospitalId)
        {
            var result = _patientService.GetPatientsByHospitalId(hospitalId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}

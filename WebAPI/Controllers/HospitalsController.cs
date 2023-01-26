using Business.Abstract;
using Business.ActionFilters.Validation;
using Entities.Concrete;
using Entities.DTOs.HospitalDtos;
using Entities.DTOs.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Business.ValidationRules.FluentValidation.HospitalValidator;
using static Business.ValidationRules.FluentValidation.UserValidator;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        IHospitalService _hospitalService;

        public HospitalsController(IHospitalService hospitalService)
        {
            _hospitalService= hospitalService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll() 
        {
            var result = _hospitalService.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _hospitalService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [ServiceFilter(typeof(ValidationFilterAttribute<HospitalRegisterDto, HospitalRegisterValidator>))]
        public IActionResult Add([FromBody] HospitalRegisterDto hospital)
        {
            var result = _hospitalService.Add(hospital);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        [ServiceFilter(typeof(ValidationFilterAttribute<HospitalUpdateDto, HospitalUpdateValidator>))]
        public IActionResult Update(HospitalUpdateDto hospital)
        {
            var result = _hospitalService.Update(hospital);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        [ServiceFilter(typeof(ValidationFilterAttribute<HospitalDeletionDto, HospitalDeletionValidator>))]
        public IActionResult Delete(HospitalDeletionDto hospital)
        {
            var result = _hospitalService.Delete(hospital);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}

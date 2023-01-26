using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.PatientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPatientService
    {
        IDataResult<List<PatientDetailDto>> GetAll();
        IResult Add(PatientRegisterDto patient);
        IDataResult<PatientDetailDto> GetById(int patientId);
        IResult Update(PatientUpdateDto patient);
        IResult Delete(PatientDeletionDto patient);
        IDataResult<List<PatientListDto>> FilterByAge(int startAge, int endAge);
        IDataResult<List<PatientDetailDto>> GetPatientsByHospitalId(int hospitalId);


    }
}

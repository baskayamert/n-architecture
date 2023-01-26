using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.HospitalDtos;
using Entities.DTOs.PatientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IHospitalService
    {
        IDataResult<List<HospitalDetailDto>> GetAll();
        IDataResult<HospitalDetailDto> GetById(int hospitalId);
        IResult Add(HospitalRegisterDto hospital);
        IResult Update(HospitalUpdateDto hospital);
        IResult Delete(HospitalDeletionDto hospital);

    }
}

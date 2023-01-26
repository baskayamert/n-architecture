using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.HospitalDtos;
using Entities.DTOs.PatientDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.ValidationRules.FluentValidation.HospitalValidator;

namespace Business.Concrete
{
    public class HospitalManager : IHospitalService
    {
        IHospitalDal _hospitalDal;
        IPatientService _patientService;
        IMapper _mapper; 

        public HospitalManager(IHospitalDal hospitalDal, IPatientService patientService, IMapper mapper)
        {
            _hospitalDal = hospitalDal;
            _patientService = patientService;
            _mapper = mapper;
        }

        public IResult Add(HospitalRegisterDto hospital)
        {
            var newHospital = _mapper.Map<Hospital>(hospital);


            _hospitalDal.Add(newHospital);

            return new SuccessResult("The hospital was successfully added.");
        }

        public IResult Delete(HospitalDeletionDto hospital)
        {
            var deletedHospital = _mapper.Map<Hospital>(hospital);

            _hospitalDal.Delete(deletedHospital);

            return new SuccessResult("The hospital was successfully deleted.");
        }

        public IDataResult<List<HospitalDetailDto>> GetAll()
        {
            var hospitals = _mapper.Map<List<HospitalDetailDto>>(_hospitalDal.GetAll());

            return new SuccessDataResult<List<HospitalDetailDto>>(hospitals, "Hospitals were successfully listed.");
        }

        public IDataResult<HospitalDetailDto> GetById(int hospitalId)
        {
            var childPatients = _mapper.Map<List<Patient>>(_patientService.FilterByAge(0, 18).Data);
            var patients = _mapper.Map<List<Patient>>(_patientService.GetPatientsByHospitalId(hospitalId).Data);

            var hospital = _mapper.Map<HospitalDetailDto>(_hospitalDal.Get(h => h.Id == hospitalId));

            hospital.ChildPatients = childPatients;
            hospital.Patients= patients;

            return new SuccessDataResult<HospitalDetailDto>(hospital);
        }

        public IResult Update(HospitalUpdateDto hospital)
        {
            var updatedHospital = _mapper.Map<Hospital>(hospital);

            _hospitalDal.Update(updatedHospital);

            return new SuccessResult("The hospital was successfully upadated.");
        }
    }
}

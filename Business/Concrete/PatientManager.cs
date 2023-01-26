using AutoMapper;
using Business.Abstract;
using Business.Extensions;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Authorization;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.PatientDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PatientManager : IPatientService
    {
        IPatientDal _patientDal;
        IMapper _mapper;

        public PatientManager(IPatientDal patientDal, IMapper mapper)
        {
            _patientDal = patientDal;
            _mapper = mapper;
        }

        public IResult Add(PatientRegisterDto patient)
        {

            var newPatient = _mapper.Map<Patient>(patient);

            _patientDal.Add(newPatient);

            return new SuccessResult("The patient was successfully added.");
        }

        public IResult Delete(PatientDeletionDto patient)
        {

            var deletedPatient = _mapper.Map<Patient>(patient);

            _patientDal.Delete(deletedPatient);

            return new SuccessResult("The patient was successfuly deleted.");
        }
        
        public IDataResult<List<PatientDetailDto>> GetAll()
        {
            var patients = _mapper.Map<List<PatientDetailDto>>(_patientDal.GetAll());

            return new SuccessDataResult<List<PatientDetailDto>>(patients, "Patients were listed.");
        }

        public IDataResult<List<PatientListDto>> FilterByAge(int startAge, int endAge)
        {
            var patients = _patientDal.AsNoTracking(
                patient => 
                patient.Birthdate.Year <= DateTime.UtcNow.AddYears(startAge * -1).Year   
                && patient.Birthdate.Year >= DateTime.UtcNow.AddYears(endAge * -1).Year
                );
            return new SuccessDataResult<List<PatientListDto>>(_mapper.Map<List<PatientListDto>>(patients), "Patients were listed.");
        }

        public IDataResult<PatientDetailDto> GetById(int patientId)
        {
            var patient = _mapper.Map<PatientDetailDto>(_patientDal.Get(p => p.Id == patientId));
            patient.Age = (DateTime.Now.Year - patient.Birthdate.Year == 0 && DateTime.Now.Month - patient.Birthdate.Month == 0 && DateTime.Now.Day - patient.Birthdate.Day == 0) ? (DateTime.Now.Year - patient.Birthdate.Year) : (DateTime.Now.Year - patient.Birthdate.Year - 1);

            return new SuccessDataResult<PatientDetailDto>(patient);
        }

        public IResult Update(PatientUpdateDto patient)
        {
            var updatedPatient = _mapper.Map<Patient>(patient);

            _patientDal.Update(updatedPatient);

            return new SuccessResult("The patient was successfuly updated.");
        }

        public IDataResult<List<PatientDetailDto>> GetPatientsByHospitalId(int hospitalId)
        {
            var patientsByHospitalId = _mapper.Map<List<PatientDetailDto>>(_patientDal.GetAll(p => p.HospitalId == hospitalId));
            return new SuccessDataResult<List<PatientDetailDto>>(patientsByHospitalId);
        }
    }
}

using AutoMapper;
using Business.Extensions;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs.HospitalDtos;
using Entities.DTOs.PatientDtos;
using Entities.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AutoMapper
    {
        public class PatientProfile: Profile
        {
            public PatientProfile()
            {
                CreateMap<PatientDetailDto, Patient>();
                CreateMap<Patient, PatientDetailDto>();

                CreateMap<PatientRegisterDto, Patient>()
                    .ForMember(dest => dest.Id, src => src.Ignore())
                    .ForMember(dest => dest.LatestVisitedDepartment, src => src.Ignore())
                    .ForMember(dest => dest.LatestHospitalNo, src => src.Ignore())
                    .ForMember(dest => dest.Hospital, src => src.Ignore());
                CreateMap<Patient, PatientRegisterDto>();

                CreateMap<PatientDeletionDto, Patient>()
                    .ForMember(dest => dest.LatestVisitedDepartment, src => src.Ignore())
                    .ForMember(dest => dest.HospitalId, src => src.Ignore())
                    .ForMember(dest => dest.LatestHospitalNo, src => src.Ignore())
                    .ForMember(dest => dest.Birthdate, src => src.Ignore())
                    .ForMember(dest => dest.NationalityNumber, src => src.Ignore())
                    .ForMember(dest => dest.Hospital, src => src.Ignore())
                    .ForMember(dest => dest.Name, src => src.Ignore());
                CreateMap<Patient, PatientDeletionDto>();

                CreateMap<Patient, PatientListDto>()
                    .ForMember(dest => dest.Age, src => src.MapFrom(p => p.Birthdate.ToAge()));
                CreateMap<PatientListDto, Patient>();

                CreateMap<PatientUpdateDto, Patient>()
                    .ForMember(dest => dest.Hospital, src => src.Ignore());
                CreateMap<Patient, PatientUpdateDto>();
            }

            
        }

        public class HospitalProfile: Profile
        {
            public HospitalProfile()
            {
                CreateMap<HospitalDetailDto, Hospital>();
                CreateMap<Hospital, HospitalDetailDto>();

                CreateMap<HospitalRegisterDto, Hospital>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.Patients, src => src.Ignore());

                CreateMap<Hospital, HospitalRegisterDto>();

                CreateMap<HospitalDeletionDto, Hospital>()
                    .ForMember(dest => dest.Address, src => src.Ignore())
                    .ForMember(dest => dest.Name, src => src.Ignore())
                    .ForMember(dest => dest.Patients, src => src.Ignore())
                    .ForMember(dest => dest.Lat, src => src.Ignore())
                    .ForMember(dest => dest.Long, src => src.Ignore());

                CreateMap<Hospital, HospitalDeletionDto>();

                CreateMap<HospitalUpdateDto, Hospital>()
                    .ForMember(dest => dest.Patients, src => src.Ignore());
                CreateMap<Hospital, HospitalUpdateDto>();
            }

        }

        public class UserProfile: Profile
        {
            public UserProfile()
            {
                CreateMap<UserDetailDto, User>()
                    .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                    .ForMember(dest => dest.PasswordSalt, src => src.Ignore());
                CreateMap<User, UserDetailDto>().ForMember(dest => dest.Password, src => src.Ignore());

                CreateMap<UserDeletionDto, User>()
                    .ForMember(dest => dest.UserType, src => src.Ignore())
                    .ForMember(dest => dest.Username, src => src.Ignore())
                    .ForMember(dest => dest.Email, src => src.Ignore())
                    .ForMember(dest => dest.PasswordSalt, src => src.Ignore())
                    .ForMember(dest => dest.PasswordHash, src => src.Ignore());
                    
                CreateMap<User, UserDeletionDto>();

                CreateMap<UserRegisterDto, User>()
                    .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                    .ForMember(dest => dest.PasswordSalt, src => src.Ignore());
                CreateMap<User, UserRegisterDto>()
                    .ForMember(dest => dest.Password, src => src.Ignore());

                CreateMap<UserUpdateDto, User>()
                    .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                    .ForMember(dest => dest.PasswordSalt, src => src.Ignore());

                CreateMap<User, UserLoginDto>().ForMember(dest => dest.Password, src => src.Ignore());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.PatientDtos
{
    public class PatientRegisterDto
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string NationalityNumber { get; set; }
        public int HospitalId { get; set; }

    }
}

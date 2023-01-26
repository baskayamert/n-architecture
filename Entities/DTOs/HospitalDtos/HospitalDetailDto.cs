using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.HospitalDtos
{
    public class HospitalDetailDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Address { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public List<Patient> ChildPatients { get; set; }
    }

}

using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Patient:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int HospitalId { get; set; }
        public string NationalityNumber { get; set; }
        public int? LatestHospitalNo { get; set; }
        public string? LatestVisitedDepartment { get; set; }
        public Hospital Hospital { get; set; }
    }
}

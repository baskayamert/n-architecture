using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public static class DateTimeExtensions
    {

        public static int ToAge(this DateTime birthDate)
        {
            var diff = DateTime.UtcNow - birthDate;
            return (new DateTime(1, 1, 1) + diff).Year - 1;
        }

    }
}

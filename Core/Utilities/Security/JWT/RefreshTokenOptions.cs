using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class RefreshTokenOptions : TokenOptions
    {
        public int RefreshTokenExpiration { get; set; }
        
    }
}

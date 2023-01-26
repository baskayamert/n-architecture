using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Authorization;

public class AuthAttribute : TypeFilterAttribute
{
    public AuthAttribute(string actionName) : base(typeof(AuthorizeAction))
    {
        Arguments = new object[]
        {
                actionName
        };
    }
}


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Authorization;
public class AuthorizeAction : IAuthorizationFilter
{
    private readonly string _actionName;

    public AuthorizeAction(string actionName)
    {
        _actionName = actionName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {   
        switch (_actionName)
        {
            case "Role":
                var currentUser = context.HttpContext.User;

                if (currentUser.HasClaim(c => c.Type == "UserType"))
                {

                    string userType = currentUser.Claims.FirstOrDefault(c => c.Type == "UserType").Value;

                    if (userType != "Admin")
                    {
                        context.HttpContext.Response.StatusCode = 401;
                        context.Result = new JsonResult("Permission denied!");
                    }
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new JsonResult("Forbidden area!");

                }
                break;
        }


    }
}
﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WriteAPI.ControllerConfig;

namespace WriteAPI.Filters;

public class ValidationRequireUserIdFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isValid = context.HttpContext.User.IsValidUserId();
        if (!isValid)
        {
            context.Result = new BadRequestObjectResult("Invalid user id");
        }
    }
}
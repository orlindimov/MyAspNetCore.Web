﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCore.Web.Models;

namespace MyAspNetCore.Web.Filters
{
    public class CustomExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled= true;

            var error=context.Exception.Message;

            context.Result= new RedirectToActionResult("Error", "Home", new ErrorViewModel()
            { Errors = new List<string>() { $"({error})" } });
        }
    }
}

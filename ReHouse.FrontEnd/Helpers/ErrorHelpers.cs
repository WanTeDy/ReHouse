using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Helpers
{
    public static class ErrorHelpers
    {
        public static void AddModelErrors(ModelStateDictionary modelState, IDictionary<String,String> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}
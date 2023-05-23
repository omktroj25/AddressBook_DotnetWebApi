using System.Diagnostics.CodeAnalysis;
using Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AddressBookApi
{
    /* The ModelStateActionFilter class checks if the model state is valid and throws an error if it is
    not. */
    [ExcludeFromCodeCoverage]
    public class ModelStateActionFilter : IActionFilter
    {
        /// <summary>
        /// This function checks if the model state is valid and throws an error with a message and list
        /// of errors if it is not.
        /// </summary>
        /// <param name="ActionExecutedContext">ActionExecutedContext is a class in ASP.NET Core that
        /// provides context information for an action method after it has been executed. It contains
        /// information about the HTTP request and response, the action result, and any exceptions that
        /// were thrown during the execution of the action.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var errors = new List<object>();
                foreach(var model in context.ModelState.Values)
                {
                    foreach(var error in model.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                throw new ErrorException(400,"Bad request","Please check the input name.",errors);
                      
            }
        }

        /// <summary>
        /// This is a method in C# that is executed before an action method in a controller is executed.
        /// </summary>
        /// <param name="ActionExecutingContext">ActionExecutingContext is a class in ASP.NET Core that
        /// represents the context in which an action method is being executed. It contains information
        /// about the current HTTP request, the action being executed, and the action's parameters. The
        /// OnActionExecuting method is a filter method that is called before the action method
        /// is</param>

        [ExcludeFromCodeCoverage]
        public void OnActionExecuting(ActionExecutingContext context)
        {
        
        }
    }
}
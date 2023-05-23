using System.Diagnostics.CodeAnalysis;

namespace Exception
{
    [ExcludeFromCodeCoverage]
    public class ErrorException : System.Exception
    {
        /* These are properties of the `ErrorException` class that allow for storing and accessing
        information about an error. */
        public int StatusCode{get;set;}
        public string Messages{get;set;}
        public string Description{get;set;}
        public object Error {get; set;}

       /* This is a constructor method for the `ErrorException` class that takes in four parameters:
       `code`, `message`, `description`, and `error`. It sets the values of the `StatusCode`,
       `Messages`, `Description`, and `Error` properties of the `ErrorException` object using the
       values passed in as parameters. The `: base()` part of the constructor calls the base
       constructor of the `System.Exception` class. */
        public ErrorException(int code, string message, string description, object error) : base()
        {
            StatusCode = code;
            Messages = message;
            Description = description;
            Error = error;
        }

    }
 
}

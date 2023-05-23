using System.Diagnostics.CodeAnalysis;

namespace Exception
{
    public class BaseCustomException : System.Exception
    {
        /* These are properties of the `BaseCustomException` class. They define the status code,
        message, and description of the exception. The `get;set;` syntax indicates that these
        properties have both a getter and a setter method, allowing them to be accessed and modified
        from outside the class. */
        public int StatusCode{get;set;}
        public string Messages{get;set;}
        public string Description{get;set;}

        /* This is a constructor method for the `BaseCustomException` class. It takes three parameters:
        `code`, `message`, and `description`, which are used to set the values of the `StatusCode`,
        `Messages`, and `Description` properties of the exception object. The `: base()` syntax
        calls the constructor of the base class (`System.Exception`) with no arguments. */
        public BaseCustomException(int code, string message, string description) : base()
        {
            StatusCode = code;
            Messages = message;
            Description = description;
        }

    }
    
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static void Main(string[] args)
        {
        
        }
    }
}
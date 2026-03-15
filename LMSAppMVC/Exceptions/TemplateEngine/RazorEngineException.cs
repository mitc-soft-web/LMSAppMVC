namespace LMSAppMVC.Exceptions.TemplateEngine
{
    public class RazorEngineException : Exception
    {
#pragma warning disable CS0114
        public string Message { get; set; }
#pragma warning restore CS0114

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public RazorEngineException(string message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            : base(message) { }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public RazorEngineException(string message, Exception innerException)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            : base(message, innerException) { }
    }
}

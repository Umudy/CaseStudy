using System;

namespace TemplateCore.Domain.Exceptions
{
    public class TemplateCoreException : Exception
    {
        public TemplateCoreException(string message) : base(message)
        {
        }
    }

    public class TemplateCoreSecurityException : Exception
    {

    }
}

using System;

namespace KaliGasService.Core.Application.CQRS
{
    public class Error
    {
        public const string UnexpectedErrorCode = "UnexpectedError";

        public string Code { get; }
        public string Message { get; }

        public static Error Create(string code, string message)
        {
            return new Error(code, message);
        }

        public static Error CreateUnexpectedError(string message)
        {
            return new Error(UnexpectedErrorCode, message);
        }

        public Error(string code, string message)
        {
            ThrowsArgumentExceptionIfFieldNotField(code, nameof(Code));
            ThrowsArgumentExceptionIfFieldNotField(message, nameof(Message));

            Code = code;
            Message = message;
        }

        private static void ThrowsArgumentExceptionIfFieldNotField(string fieldValue, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                throw new ArgumentException($"Error {fieldName} field is not filled");
            }
        }

        public override string ToString()
        {
            return Code + ": " + Message;
        }
    }
}
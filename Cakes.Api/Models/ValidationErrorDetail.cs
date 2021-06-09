namespace Cakes.Api.Models
{
    public class ValidationErrorDetail
    {
        public string Field { get; }

        public string Message { get; }

        public ValidationErrorDetail(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
using Newtonsoft.Json;
using System.Net;

namespace Corona.MVC.ExamProgramUIExceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public Dictionary<string, string> ModelErrors { get; set; }
        public ApiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ParseErrorResponse(message);
        }

        private void ParseErrorResponse(string message)
        {
            switch (StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    ModelErrors = new();
                    var errorResponse = JsonConvert.DeserializeObject<ModelErrorResponse>(message);

                    foreach (var key in errorResponse.Errors.Keys)
                    {
                        ModelErrors.Add(key, errorResponse.Errors[key][0]);
                    }
                    break;
                case HttpStatusCode.NotFound:
                    ModelErrors = new();
                    errorResponse = JsonConvert.DeserializeObject<ModelErrorResponse>(message);

                    foreach (var key in errorResponse.Errors.Keys)
                    {
                        ModelErrors.Add(key, errorResponse.Errors[key][0]);
                    }
                    break;
            }
        }


    }
    public class ModelErrorResponse
    {
        public string Type { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}

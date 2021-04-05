using Newtonsoft.Json;
using System;

namespace MovieMagic.DTO
{
    public class ApiError
    {
        public ApiError(int errorCode, string errorGuid, string errorMessage, string errorDetails = null)
        {
            ErrorCode = errorCode;
            ErrorGuid = errorGuid;
            ErrorMessage = errorMessage;
            ErrorDetails = errorDetails;
        }
        public int ErrorCode { get; set; }

        public string ErrorGuid { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorDetails { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

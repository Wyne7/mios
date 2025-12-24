using MOIS.Common.Models;

namespace Common.Models
{
    public class DatabaseConnections
    {
        public const string USER = "USER";
        public const string CBCS_RiskTarget = "CBCS_RiskTarget";
        public const string CBCS_CleanRecords = "CBCS_CleanRecords";
        public const string CBCS_AuditLog = "CBCS_AuditLog";
        public const string CBCS_BusinessRule = "CBCS_BusinessRule";
        public const string CBCS_Transaction = "CBCS_Transaction";
        public const string BMIS = "BMIS";
        public const string BMIS_TrustLink = "BMIS_TrustLink";

        public const string CBCS_Contributions = "CBCS_Contributions";
        public const string CBCS_Hangfire = "CBCS_Hangfire";
        public const string CBCS_SA = "CBCS_SA";
        public const string CBCS_Notification = "CBCS_Notification";
        public const string MENSync = "MENSync";
    }



    public interface ICodeMessage
        {
            string code { get; set; }
            string Message { get; set; }
        }
        public class CodeMessage : ICodeMessage
        {
            public string code { get; set; }
            public string Message { get; set; }
        }
        public class CodeMessageResponse
        {
            public string code { get; set; }
            public string Message { get; set; }
            public string data { get; set; }
        }


    public class ResponseData
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class DataResponse : ResponseData
    {
        public dynamic data { get; set; }
    }

    public class ErrorResponse : ResponseData
    {
        public dynamic description { get; set; }
    }

    public interface IResponseHandler
    {
        ResponseData GetResponse<T>(Enum enumValue) where T : class;
        ErrorResponse GetErrorResponse<T>(Enum enumValue, dynamic error) where T : class;
        DataResponse GetDataResponse<T>(Enum enumValue, dynamic data) where T : class;
    }

    // ✅ THIS CLASS WAS MISSING
    public class ResponseHandler : IResponseHandler
    {
        public ResponseData GetResponse<T>(Enum enumValue) where T : class
        {
            return enumValue switch
            {
                ResponseEnum.R201 => new ResponseData
                {
                    code = "201",
                    message = "Created successfully"
                },
                ResponseEnum.R003 => new ResponseData
                {
                    code = "003",
                    message = "No Data Found"
                },
                ResponseEnum.R001 => new ResponseData
                {
                    code = "001",
                    message = "Error occurred"
                },
                _ => new ResponseData
                {
                    code = "000",
                    message = "Unknown status"
                }
            };
        }

        public ErrorResponse GetErrorResponse<T>(Enum enumValue, dynamic error) where T : class
        {
            var baseResponse = GetResponse<T>(enumValue);
            return new ErrorResponse
            {
                code = baseResponse.code,
                message = baseResponse.message,
                description = error
            };
        }

        public DataResponse GetDataResponse<T>(Enum enumValue, dynamic data) where T : class
        {
            var baseResponse = GetResponse<T>(enumValue);
            return new DataResponse
            {
                code = baseResponse.code,
                message = baseResponse.message,
                data = data
            };
        }
    }
    }

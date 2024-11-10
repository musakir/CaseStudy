using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.Utilities.Results
{
    public class ResultApi<T>
    {
        public T R { get; private set; }
        public int OperationStatusCode { get; private set; }
        public bool OperationStatus { get; private set; }
        public string OperationMessage { get; private set; }
        public DateTime Date { get; private set; }
        public string Service { get; private set; }
        public string Method { get; private set; }
        public bool ErrorStatus { get; private set; }
        public int ErrorCode { get; private set; }
        public string Message { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ResultApi<T> Success(T data, int operationStatusCode, string operationMessage)
        {
            return new ResultApi<T>
            {
                R = data,
                OperationStatusCode = operationStatusCode,
                OperationMessage = operationMessage,
                OperationStatus = true,
                Date = DateTime.Now,
                Service = "",
                Method = "",
                ErrorCode = 0,
                Message = operationMessage,
                ErrorStatus = false,
                ErrorMessage = operationMessage
            };
        }

        public static ResultApi<T> Fail(T data, int operationStatusCode, string operationMessage)
        {
            return new ResultApi<T>
            {
                R = data,
                OperationStatusCode = operationStatusCode,
                OperationMessage = operationMessage,
                OperationStatus = false,
                Date = DateTime.Now,
                Service = "",
                Method = "",
                ErrorCode = operationStatusCode,
                Message = operationMessage,
                ErrorStatus = true,
                ErrorMessage = operationMessage
            };
        }

    }
}

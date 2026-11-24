using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Common
{
    public class ServiceResult
    {
        public bool IsSuccess { get; private set; }

        public string Message { get; private set; } = string.Empty;

        public static ServiceResult Success(string message)
        {
            return new ServiceResult
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static ServiceResult Failure(string message)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}

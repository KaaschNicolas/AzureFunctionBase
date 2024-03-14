using Azure.Core;
using AzureFunctionBase.Extensions;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionBase.Functions.FunctionBase
{
    public abstract class FunctionBase<T> where T : class
    {
        protected ILogger Logger { get; }

        protected FunctionBase(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<T>();
        }

        protected static async Task<(bool successful, HttpResponseData errorResponse)> ValidateRequest(
            HttpRequestData req,
            object request,
            Func<string> validate,
            ILogger log
        )
        {
            if (request == null || log == null)
            {
                return (false, req.CreateResponse(System.Net.HttpStatusCode.BadRequest));
            }

            var validationMessages = validate();
            if (!string.IsNullOrEmpty(validationMessages))
            {
                return (
                    false,
                    log.LogAndReturnBadRequestErrorMessageResult(
                        req,
                        validationMessages
                    )
                );
            }

            return (true, null);
        }
    }
}

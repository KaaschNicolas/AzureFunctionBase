using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionBase.Extensions
{
    public static class ILoggerExtension
    {
        public static HttpResponseData LogAndReturnBadRequestErrorMessageResult(this ILogger logger, HttpRequestData req, string validationMessage)
        {
            logger.LogError(validationMessage);

            return req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
        }
    }
}

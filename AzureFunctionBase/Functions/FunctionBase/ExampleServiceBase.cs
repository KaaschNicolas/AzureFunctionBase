using Azure.Core;
using AzureFunctionBase.Extensions;
using AzureFunctionBase.Interfaces;
using AzureFunctionBase.Models;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;

namespace AzureFunctionBase.Functions.FunctionBase
{
    public class ExampleServiceBase<T> : FunctionBase<T> where T : class
    {
        protected ExampleServiceBase(ILoggerFactory loggerFactory, IValidationService validationService) : base(loggerFactory)
        {
            ValidationService = validationService;
        }

        protected IValidationService ValidationService { get; set; }

        protected async Task<HttpResponseData> Execute<T>(
            HttpRequestData req,
            ILogger logger,
            ExecutionContext executionContext,
            Func<HttpRequestData, ILogger, ExecutionContext, T, Task<HttpResponseData>> func
            ) where T : RequestModel
        {
            try
            {
                var request = GetRequest<T>(req);

                var (successful, errorResponse) = await ValidateRequest(req, request, logger);
                if (!successful)
                {
                    return errorResponse;
                }
                
                return await func(req, logger, executionContext, request);
            }
            catch (JsonException j)
            {
                return logger.LogAndReturnBadRequestErrorMessageResult(
                req,
                    $"The Requestbody does not contain valid Json! Message: {j.Message}"
                );
            }
            catch (Exception)
            {
                return req.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        protected static T GetRequest<T>(HttpRequestData req)
            where T : RequestModel
        {
            if (req == null && req.Body == null)
            {
                return null;
            }

            var reader = new StreamReader(req.Body);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var message = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(message);
        }

        protected async Task<(bool successful, HttpResponseData errorResponse)> ValidateRequest<T>(
            HttpRequestData req,
            T request,
            ILogger log
        ) where T : RequestModel
        {
            return await ValidateRequest(
                req,
                request,
                () => ValidationService.Validate(request),
                log
            );
        }
    }
}

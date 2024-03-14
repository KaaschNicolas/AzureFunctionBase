using Azure.Core;
using AzureFunctionBase.Interfaces;
using AzureFunctionBase.Models;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AzureFunctionBase.Functions.FunctionBase;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace AzureFunctionBase.Functions
{
    public class ExampleFunction : ExampleServiceBase<ExampleFunction>
    {
        public ExampleFunction(ILoggerFactory loggerFactory, IValidationService validationService) : base(loggerFactory, validationService)
        {
        }

        [Function("requestModelPost")]
        [OpenApiOperation(operationId: "upsertRequestModel", tags: new[] { "requestModel" }, Description = "Creates new RequestModel", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity(
            schemeName: "bearer",
            SecuritySchemeType.Http,
            Name = "Authorization",
            In = OpenApiSecurityLocationType.Header,
            Scheme = OpenApiSecuritySchemeType.Bearer,
            BearerFormat = "JWT"
            )
        ]
        [OpenApiRequestBody(contentType: "application/json", typeof(RequestModel))]
        [OpenApiParameter(name: "Code", In = ParameterLocation.Header, Required = true, Type = typeof(string))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Created, Description = "RequestModel was created successfully")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Bad Request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal Server Error")]
        public async Task<HttpResponseData> ExecutePost(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/requestModel")] HttpRequestData req,
            ExecutionContext executionContext
        )
        {
            try
            {
                return await Execute<RequestModel>(
                    req,
                    Logger,
                    executionContext,
                    MainExecute
                );
            }
            catch (Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Function("requestModelPatch")]
        [OpenApiOperation(operationId: "upsertRequestModel", tags: new[] { "requestModel" }, Description = "Updates existing RequestModel", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiSecurity(
            schemeName: "bearer",
            SecuritySchemeType.Http,
            Name = "Authorization",
            In = OpenApiSecurityLocationType.Header,
            Scheme = OpenApiSecuritySchemeType.Bearer,
            BearerFormat = "JWT"
            )
        ]
        [OpenApiRequestBody(contentType: "application/json", typeof(RequestModel))]
        [OpenApiParameter(name: "Code", In = ParameterLocation.Header, Required = true, Type = typeof(string))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent, Description = "RequestModel was updated successfully")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Bad Request")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Internal Server Error")]
        public async Task<HttpResponseData> ExecutePatch(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/requestModel")] HttpRequestData req,
            ExecutionContext executionContext
        )
        {
            try
            {
                return await Execute<RequestModel>(
                    req,
                    Logger,
                    executionContext,
                    MainExecute
                );
            }
            catch (Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<HttpResponseData> MainExecute(
            HttpRequestData req,
            ILogger logger,
            ExecutionContext executionContext,
            RequestModel request
        )
        {
            logger.LogInformation($"Starting to process Account");


            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}

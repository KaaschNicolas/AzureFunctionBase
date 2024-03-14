using AzureFunctionBase.Interfaces;
using AzureFunctionBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionBase.Services
{
    public class ValidationService : IValidationService
    {
        public string Validate(RequestModel request)
        {
            return string.Empty;
        }
    }
}

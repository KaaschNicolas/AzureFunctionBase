using AzureFunctionBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionBase.Interfaces
{
    public interface IValidationService
    {
        public string Validate(RequestModel request);
    }
}

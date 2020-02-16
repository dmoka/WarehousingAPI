using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaliGasService.Core.Application.CQRS;

namespace Warehousing.API.Application.Errors
{
    public class ErrorFormatter
    {
        public static string Format(IEnumerable<Error> errors)
        {
            return string.Join('\n', errors.Select(e => e.ToString()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Models.DTOs
{
    internal static class DTOHelper
    {

        public static string GetPropNamesAsHeader<T>(string separator, bool toUpper = true)
        {
            var props = typeof(T).GetProperties();

            var propNames = props
                .Select(p => toUpper
                    ? p.Name.ToUpperInvariant()
                    : p.Name);



            return string.Join(separator, propNames);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace Presentation
{
    public class UserInputModel
    {
        public string Username { get; set; }
        public string CityName { get; set; }
        private readonly string _countryCode = "SE";

        public GeoResult GeoResult { get; set; }
    }
}

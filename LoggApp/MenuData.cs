using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppLogic.DTOs;

namespace Presentation
{
    public class MenuData
    {
        public List<string> InitMenu = new() { "[LOG IN]", "[GET ALL USERS]", "[CREATE NEW USER ACCOUNT]" };
        public List<string> SpecificUserMenu = new() { "[CREATE NEW DAYCARD]", "[SEARCH DAYCARD]" };
        public List<UserMenuDto>? AllUsers { get; set; }
    }
}

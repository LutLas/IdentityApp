using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityApp.Pages.Identity
{
    public class IndexModel : UserPageModel
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

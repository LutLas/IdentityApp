using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityApp.Pages
{
    [Authorize]
    public class StoreModel : PageModel
    {
        public StoreModel(ProductDbContext ctx) => DbContext = ctx;
        public ProductDbContext DbContext { get; set; }
    }
}

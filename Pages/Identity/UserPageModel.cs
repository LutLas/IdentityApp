using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity
{
    [Authorize]
    public class UserPageModel : PageModel
    {
        // no methods or properties required
    }
}

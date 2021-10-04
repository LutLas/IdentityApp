using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApp.Pages.Identity.Admin
{
    [AllowAnonymous]
    public class AdminPageModel : UserPageModel
    {
        // no methods or properties required
    }
}


using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityApp.Services
{
    public class TokenUrlEncoderService
    {
        public virtual string EncodeToken(string token)
        => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        public virtual string DecodeToken(string urlToken)
        => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(urlToken));
    }
}
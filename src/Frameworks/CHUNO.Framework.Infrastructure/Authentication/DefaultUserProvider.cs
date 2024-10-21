using Grpc.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CHUNO.Framework.Infrastructure.Authentication
{
    public class DefaultUserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthUser? _user;

        public DefaultUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthUser? User
        {
            get
            {
                if(_user != null)
                {
                    return _user; 
                }
               
                else if (_httpContextAccessor != null)
                {
                    // Extract user information from HTTP context
                    _user = ExtractUserFromHttpContext(_httpContextAccessor);
                    return _user;
                }

                // Handle case where neither context is available
                return null;
            }
           
        }

        private AuthUser ExtractUserFromHttpContext(IHttpContextAccessor context)
        {
            var claimsPrincipal = context.HttpContext?.User;
            if(!claimsPrincipal.Identity.IsAuthenticated)
            {
                return null;
            }
            return new AuthUser(); // Replace with actual user information
        }
    }
}

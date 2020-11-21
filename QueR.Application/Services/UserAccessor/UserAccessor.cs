using Microsoft.AspNetCore.Http;
using QueR.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueR.Application.Services.UserAccessor
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int UserId {
            get {
                var subClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");
                if (subClaim == null)
                {
                    throw new InvalidOperationException("Sub claim is missing. Is the caller authenticated?");
                }
                if (int.TryParse(subClaim.Value, out int id))
                {
                    return id;
                }
                else
                {
                    throw new InvalidOperationException($"Sub claim is invalid. Was {subClaim.Value}, should be an integer.");
                }
            }
        }

        public int? CompanyId
        {
            get
            {
                var companyClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "company");
                if(companyClaim == null)
                {
                    return null;
                }
                if (int.TryParse(companyClaim.Value, out int id))
                {
                    return id;
                }
                else
                {
                    throw new InvalidOperationException($"Company claim is invalid. Was {companyClaim.Value}, should be an integer.");
                }
            }
        }

        public int? WorksiteId
        {
            get
            {
                var worksiteClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "worksite");
                if (worksiteClaim == null)
                {
                    return null;
                }
                if (int.TryParse(worksiteClaim.Value, out int id))
                {
                    return id;
                }
                else
                {
                    throw new InvalidOperationException($"Worksite claim is invalid. Was {worksiteClaim.Value}, should be an integer.");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace QueR.BLL.Extensions
{
    public static class ClaimExtensions
    {
        /// <summary>
        /// Adds the (name, value) token to the list of claims. 
        /// If the provided value is null, the claim is skipped without throwing an exception.
        /// </summary>
        /// <param name="claims">The list of claims</param>
        /// <param name="name">Name of the claim</param>
        /// <param name="value">Value of the claim</param>
        /// <returns>The list of claims</returns>
        public static List<Claim> TryAddClaim(this List<Claim> claims, string name, object value)
        {
            if (value != null)
            {
                claims.Add(new Claim(name, value.ToString()));
            }
            return claims;
        }
    }
}

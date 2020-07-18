using System;
using Application;

namespace Infrastructure
{
    public class GuidBasedIdentityProvider : IIdentityProvider
    {
        public Guid Next()
        {
            return Guid.NewGuid();
        }
    }
}
using System;

namespace Application
{
    public interface IIdentityProvider
    {
        Guid Next();
    }
}
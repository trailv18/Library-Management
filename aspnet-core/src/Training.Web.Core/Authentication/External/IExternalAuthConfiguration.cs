using System.Collections.Generic;

namespace Training.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}

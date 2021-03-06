﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inedo.BuildMaster.Extensibility;
using Inedo.BuildMaster.Extensibility.Credentials;
using Inedo.BuildMaster.Web.Controls;
using Inedo.Extensions.Operations.Otter;

namespace Inedo.Extensions.SuggestionProviders
{
    internal sealed class OtterEnvironmentNameSuggestionProvider : ISuggestionProvider
    {
        public async Task<IEnumerable<string>> GetSuggestionsAsync(IComponentConfiguration config)
        {
            string credentialName = config["CredentialName"];
            if (string.IsNullOrEmpty(credentialName))
                return Enumerable.Empty<string>();

            var credentials = ResourceCredentials.Create<OtterCredentials>(credentialName);

            var client = new OtterClient(credentials.Host, credentials.ApiKey);
            var servers = await client.EnumerateInfrastructureAsync(InfrastructureEntity.Environment).ConfigureAwait(false);

            return servers;
        }
    }
}

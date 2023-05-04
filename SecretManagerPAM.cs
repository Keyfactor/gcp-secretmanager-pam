using Google.Cloud.SecretManager.V1;
using Keyfactor.Platform.Extensions;
using System.Collections.Generic;

namespace Keyfactor.Extensions.Pam.GCP
{
    public class SecretManagerPAM : IPAMProvider
    {
        public string Name => "GCP-SecretManager";

        public string GetPassword(Dictionary<string, string> instanceParameters, Dictionary<string, string> initializationInfo)
        {
            SecretManagerServiceClient pamClient = SecretManagerServiceClient.Create();

            SecretVersionName secretName = new SecretVersionName(initializationInfo["projectId"], instanceParameters["secretId"], "latest");
            AccessSecretVersionResponse pamSecret = pamClient.AccessSecretVersion(secretName);

            return pamSecret.Payload.Data.ToStringUtf8();
        }
    }
}

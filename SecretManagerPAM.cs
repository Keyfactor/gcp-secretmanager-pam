using Google.Cloud.SecretManager.V1;
using Keyfactor.Platform.Extensions;
using System.Collections.Generic;

namespace Keyfactor.Extensions.Pam.Google
{
    public class SecretManagerPAM : IPAMProvider
    {
        public string Name => "Google-SecretManager";

        public string GetPassword(Dictionary<string, string> instanceParameters, Dictionary<string, string> initializationInfo)
        {
            // uses default credentials, endpoint
            // $env:GCLOUD_PROJECT - project name / also endpoint?
            // $env:GOOGLE_APPLICATION_CREDENTIALS - json credentials file
            SecretManagerServiceClient pamClient = SecretManagerServiceClient.Create();

            SecretVersionName secretName = new SecretVersionName(initializationInfo["projectId"], instanceParameters["secretId"], "latest");
            AccessSecretVersionResponse pamSecret = pamClient.AccessSecretVersion(secretName);

            return pamSecret.Payload.Data.ToStringUtf8();
        }
    }
}

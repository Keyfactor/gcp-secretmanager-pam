
# Google Cloud Secret Manager PAM Provider

The Google Cloud Secret Manager PAM Provider allows for the use of a Secret Manager instance in Google Cloud to be used as a credential store for Keyfactor. Secret values can be retrieved and used in the Keyfactor Platform as passwords or other sensitive fields.

#### Integration status: Production - Ready for use in production environments.

## About the Keyfactor PAM Provider

Keyfactor supports the retrieval of credentials from 3rd party Privileged Access Management (PAM) solutions. Secret values can normally be stored, encrypted at rest, in the Keyfactor Platform database. A PAM Provider can allow these secrets to be stored, managed, and rotated in an external platform. This integration is usually configured on the Keyfactor Platform itself, where the platform can request the credential values when needed. In certain scenarios, a PAM Provider can instead be run on a remote location in conjunction with a Keyfactor Orchestrator to allow credential requests to originate from a location other than the Keyfactor Platform.

## Support for Google Cloud Secret Manager PAM Provider

Google Cloud Secret Manager PAM Provider is supported by Keyfactor for Keyfactor customers. If you have a support issue, please open a support ticket via the Keyfactor Support Portal at https://support.keyfactor.com

###### To report a problem or suggest a new feature, use the **[Issues](../../issues)** tab. If you want to contribute actual bug fixes or proposed enhancements, use the **[Pull requests](../../pulls)** tab.

---

When using this PAM Provider, there are 2 versions available to install depending on the Dot Net version in use.
If you are using Keyfactor Command __before version 11__, you should install the PAM Provider from the `net472` folder.
___Otherwise, by default___ you should install from the `netcoreapp3.1` folder. This folder should also be used when installing on the Universal Orchestrator.
---




### Initial Configuration of PAM Provider
In order to allow Keyfactor to use the new Google Cloud Secret Manager PAM Provider, the definition needs to be added to the application database.
This is done by running the provided `kfutil` tool to install the PAM definition, which only needs to be done one time. It uses API credentials to access the Keyfactor instance and create the PAM definition.

The `kfutil` tool, after being [configured for API access](https://github.com/Keyfactor/kfutil#quickstart), can be run in the following manner to install the PAM definition from the Keyfactor repository:

```
kfutil pam types-create -r gcp-secretmanager-pam -n GCP-SecretManager
```

### Configuring Parameters
The following are the parameter names and a description of the values needed to configure the Google Cloud Secret Manager PAM Provider.

__Initialization Parameters for each defined PAM Provider instance__
| Initialization parameter | Display Name | Description |
| :---: | :---: | --- |
| projectId | Unique Google Cloud Project ID | The unique auto generated ID of your Google Cloud project. This is not the name you may have renamed / assigned to your project after it was created. |

__Instance Parameters for each retrieved secret field__
| Instance parameter | Display Name | Description |
| :---: | :---: | --- |
| secretId | Secret Name | The name of the secret you assigned in the Secret Manager. |

![](images/config.png)

### Configuring for PAM Usage
#### In Google Cloud Secret Manager
The Google Cloud Secret Manager may need to be added to your Google Cloud project if it as not included initially. It appears under the _Security_ menu option.

Secrets can be easily added with a custom name in the Secret Manager. In order to allow authenticating to the Google Cloud project and the secret, you will need to create a Service Account. The Service Account will need the __Secret Manager Secret Accessor__ Role added to it to retrieve secret values. This role can be added to your Service Account in the _IAM_ menu option under _IAM & Admin_.

After the secret is created, you can add the Role or Principal directly to it in the _Permissions_ section.

#### [Authentication](https://cloud.google.com/docs/authentication/production)
As a special requirement for authenticating with Google Cloud, you will need to generate and download a Service Account Key for your service account. This `json` file should be saved in a secure location on your machine. After saving it, you will need to configure the `GOOGLE_APPLICATION_CREDENTIALS` environment variable with the full path to the `json` file with the key material.


#### Usage with the Keyfactor Universal Orchestrator
To use the PAM Provider to resolve a field, for example a Server Password, instead of entering in the actual value for the Server Password, enter a `json` object with the parameters specifying the field.
The parameters needed are the "instance" parameters above:

~~~ json
{"secretId":"gcp-secret-id"}
~~~

If a field supports PAM but should not use PAM, simply enter in the actual value to be used instead of the `json` format object above.

#### In Keyfactor - PAM Provider
##### Installation
In order to setup a new PAM Provider in the Keyfactor Platform for the first time, you will need to run the `kfutil` tool (see Initial Configuration of PAM Provider).

After the installation is run, the DLLs need to be installed to the correct location for the PAM Provider to function. From the release, the gcp-secretmanager-pam.dll should be copied to the following folder locations in the Keyfactor installation. Once the DLL has been copied to these folders, edit the corresponding config file. You will need to add a new Unity entry as follows under `<container>`, next to other `<register>` tags.

| Install Location | DLL Binary Folder | Config File |
| --- | --- | --- |
| WebAgentServices | WebAgentServices\bin\ | WebAgentServices\web.config |
| Service | Service\ | Service\CMSTimerService.exe.config |
| KeyfactorAPI | KeyfactorAPI\bin\ | KeyfactorAPI\web.config |
| WebConsole | WebConsole\bin\ | WebConsole\web.config |

When enabling a PAM provider for Orchestrators only, the first line for `WebAgentServices` is the only installation needed.

The Keyfactor service and IIS Server should be restarted after making these changes.

```xml
<register type="IPAMProvider" mapTo="Keyfactor.Extensions.Pam.GCP.SecretManagerPAM, gcp-secretmanager-pam" name="GCP-SecretManager" />
```



The entire contents (which includes all library dependencies) should be copied when installing. For the Google Secrets Manager PAM Provider you will also need to add an additional binding redirect for a Google library DLL to operate properly. This should be added to any configs edited to add the `<register>` entry.

```xml
<dependentAssembly>
  <assemblyIdentity name="Google.Apis.Auth" publicKeyToken="4b01fa6e34db77ab" />
  <bindingRedirect oldVersion="1.0.0.0-1.46.0.0" newVersion="1.53.0.0" />
</dependentAssembly>
```

##### Usage
In order to use the PAM Provider, the provider's configuration must be set in the Keyfactor Platform. In the settings menu (upper right cog) you can select the ___Priviledged Access Management___ option to configure your provider instance.

![](images/setting.png)

After it is set up, you can now use your PAM Provider when configuring certificate stores. Any field that is treated as a Keyfactor secret, such as server passwords and certificate store passwords can be retrieved from your PAM Provider instead of being entered in directly as a secret.

![](images/password.png)


---






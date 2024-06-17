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
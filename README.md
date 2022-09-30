# Google Secret Manager PAM Provider

The Google Secret Manager PAM Provider allows for the use of a Secret Manager instance in Google Cloud to be used as a credential store for Keyfactor. Secret values can be retrieved and used in the Keyfactor Platform as passwords or other sensitive fields.

#### Integration status: Production - Ready for use in production environments.

## About the Keyfactor PAM Provider

Keyfactor supports the retrieval of credentials from 3rd party Priviledged Access Management (PAM) solutions. Secret values can normally be stored, encrypted at rest, in the Keyfactor Platform database. A PAM Provider can allow these secrets to be stored, managed, and rotated in an external platform. This integration is usually configured on the Keyfactor Platform itself, where the platform can request the credential values when needed. In certain scenarios, a PAM Provider can instead be run on a remote location in conjunction with a Keyfactor Orchestrator to allow credential requests to originate from a location other than the Keyfactor Platform.

---


## Support for Google Secret Manager PAM Provider

Google Secret Manager PAM Provider is supported by Keyfactor for Keyfactor customers. If you have a support issue, please open a support ticket with your Keyfactor representative.

###### To report a problem or suggest a new feature, use the **[Issues](../../issues)** tab. If you want to contribute actual bug fixes or proposed enhancements, use the **[Pull requests](../../pulls)** tab.
___





---


### License
[Apache](https://apache.org/licenses/LICENSE-2.0)


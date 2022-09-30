```xml
<register type="IPAMProvider" mapTo="Keyfactor.Extensions.Pam.Google.SecretManagerPAM, google-secretmanager-pam" name="Google-SecretManager" />
```

For the Google Secrets Manager PAM Provider you will also need to add a binding redirect for a Google library DLL to operate properly. This should be added to any configs edited to add the `<register>` entry.

```xml
<dependentAssembly>
  <assemblyIdentity name="Google.Apis.Auth" publicKeyToken="4b01fa6e34db77ab" />
  <bindingRedirect oldVersion="1.0.0.0-1.46.0.0" newVersion="1.53.0.0" />
</dependentAssembly>
```

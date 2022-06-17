declare @pamid uniqueidentifier;
set @pamid = newid();

insert into [pam].[ProviderTypes]([Id], [Name])
values (@pamid, 'Google-SecretManager');

insert into [pam].[ProviderTypeParams]([ProviderTypeId], [Name], [DisplayName], [DataType], [InstanceLevel])
values  (@pamid,'projectId','Unique Google Cloud Project ID',1,0),
        (@pamid,'secretId','Secret Name',1,1);
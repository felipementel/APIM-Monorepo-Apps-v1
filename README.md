# APIM

![APIM](./docs/img/capa.png)

```
https://www.youtube.com/watch?v=n6uTmKUHXyI&list=PLf7uDG4xdAJ0CzqQuYT69WzQnT5HoHAoU&ab_channel=DEPLOY
```

Documentação oficial do Azure API Management

[API Management documentation | Microsoft Learn](https://learn.microsoft.com/en-us/azure/api-management/)

Problemas conhecidos que são importantes de conhecer antes de começar a usar o Azure API Management

[Restrictions and details](https://learn.microsoft.com/en-us/azure/api-management/api-management-api-import-restrictions)

# Criar um novo projeto

```
export subscriptionId=acd12345-6789-1011-1213-141516171819
```

```
export appDisplayName=userCanalDEPLOY-APIM
```

```
az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/$subscriptionId" --display-name $appDisplayName --sdk-auth
```

adaptar resposta para esse padrao

```
{
  "appId": "1234",
  "displayName": "userCanalDEPLOY-APIM",
  "password": "abcd123",
  "tenant": "5678"
}
```

Salve no Github Secrets

```
<rate-limit-by-key calls="10"
                    renewal-period="30"
                    increment-condition="@(context.Response.StatusCode == 200)"
                    counter-key="@(context.Request.IpAddress)"
                    remaining-calls-variable-name="remainingCallsPerIP"
                    remaining-calls-header-name="remaining-calls"
                    retry-after-header-name="retry-after"
                    retry-after-variable-name="remainingCallsPerIP"
                    total-calls-header-name="total-calls">
```

### Open API Specification

```

https://spec.openapis.org/oas/v3.0.3.html

```

### Swagger

```

https://github.com/swagger-api

```

### Scalar

```

https://github.com/scalar/scalar/?tab=readme-ov-file

```

## Deploy

```

https://github.com/actions/upload-artifact

```

```

https://github.com/actions/download-artifact

```

### az login (pre requisito)

```

https://github.com/Azure/login

```

### Azure Container Apps

```

https://github.com/Azure/container-apps-deploy-action

```

### Azure App Service

```

https://learn.microsoft.com/en-us/azure/app-service/reference-app-settings

```

### Azure WebApp

```

https://learn.microsoft.com/en-us/azure/app-service/configure-basic-auth-disable?tabs=portal

```

```

https://github.com/Azure/webapps-deploy

```

### Azure Functions

```

https://learn.microsoft.com/en-us/azure/azure-functions/functions-reference?tabs=blob&pivots=programming-language-csharp

```

```

https://learn.microsoft.com/en-us/azure/azure-functions/run-functions-from-deployment-package

```

```

https://learn.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-in-process-differences

```

```

https://learn.microsoft.com/en-us/azure/azure-functions/function-keys-how-to

```

```

```

# CreditProducts-M2

API de productos de crédito construida con .NET y PostgreSQL.

## Configuración local

La cadena de conexión no se versiona. Crea el archivo
`src/CreditProducts.Api/appsettings.Development.json` a partir de
`appsettings.Development.example.json` y coloca allí tus credenciales locales.

Como alternativa, configura la variable de entorno
`ConnectionStrings__CreditProductConnection`.

Después restaura y ejecuta el proyecto:

```bash
dotnet restore
dotnet run --project src/CreditProducts.Api
```

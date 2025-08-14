
dotnet add package Swashbuckle.AspNetCore

dotnet new sln -n ProjectManagement

# Crea el proyecto del core (biblioteca de clases)
dotnet new classlib -n ProjectManagement.Core
# Crea el proyecto de infraestructura (biblioteca de clases)
dotnet new classlib -n ProjectManagement.Infrastructure
# Crea el proyecto de la API (web api)
dotnet new webapi -n ProjectManagement.Api

dotnet sln add ProjectManagement.Core/ProjectManagement.Core.csproj
dotnet sln add ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj
dotnet sln add ProjectManagement.Api/ProjectManagement.Api.csproj

references setting between projects
====================================
# El API necesita referenciar al Core y a la Infrastructure
dotnet add ProjectManagement.Api/ProjectManagement.Api.csproj reference ProjectManagement.Core/ProjectManagement.Core.csproj
dotnet add ProjectManagement.Api/ProjectManagement.Api.csproj reference ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj

# El Infrastructure solo necesita referenciar al Core
dotnet add ProjectManagement.Infrastructure/ProjectManagement.Infrastructure.csproj reference ProjectManagement.Core/ProjectManagement.Core.csproj

Configuracion de la base de datos
===================================
# Navega a la carpeta del proyecto de infraestructura
cd ProjectManagement.Infrastructure
# Añade el paquete NuGet del driver de MongoDB
dotnet add package MongoDB.Driver

Configuracion de la base de datos en API project
================================================
# Navega a la carpeta del proyecto de la API
cd ProjectManagement.Api
# Añade el paquete del driver para la configuración
dotnet add package MongoDB.Driver
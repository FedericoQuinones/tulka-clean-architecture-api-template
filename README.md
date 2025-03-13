# API de Autenticación con JWT - Clean Architecture

Este repositorio contiene una **API RESTful** diseñada con el enfoque de **Clean Architecture** para la gestión de autenticación basada en **JWT (JSON Web Token)**. Esta arquitectura permite mantener el código organizado, escalable y fácil de probar, separando las responsabilidades en diferentes capas, lo que facilita su mantenimiento y evolución.

## Arquitectura de la API

Esta API está organizada utilizando el concepto de **Clean Architecture**, con las siguientes capas y sus responsabilidades:

### 1. Capa de Dominio (Entities)
- Contiene las **entidades del sistema** (por ejemplo, `User`), que son los objetos que representan a los usuarios dentro de la aplicación.

### 2. Capa de Aplicación (Use Cases)
- Maneja la **lógica de autenticación** y los casos de uso como el **registro** y el **inicio de sesión** de los usuarios. Los servicios encargados de generar y validar tokens JWT residen aquí.

### 3. Capa de Presentación (Controllers)
- Es responsable de recibir las **solicitudes HTTP** y devolver las respuestas correspondientes. Aquí es donde se encuentran los **controladores**, como el `AuthController`, que define los endpoints `/register` y `/login`.

### 4. Capa de Infraestructura (Services)
- Maneja todo lo relacionado con la **infraestructura externa**, como la integración con la **base de datos**, el uso de **ASP.NET Core Identity** para gestionar usuarios, y la implementación de la **generación y validación de tokens JWT**.

## Tecnologías utilizadas en este proyecto

- **ASP.NET Core 9.0** para construir la API RESTful.
- **ASP.NET Core Identity** para la gestión de usuarios y autenticación.
- **JWT (JSON Web Token)** para la autenticación basada en tokens.
- **Serilog** para el registro de logs.
- **Swagger** para la documentación de la API.
- **MediatR** para la implementación de patrones de mediador en los casos de uso.

## Instalación

1. Clona el repositorio:

   ```bash
   git clone https://github.com/tu_usuario/tu_repositorio.git

2. Navega al directorio del proyecto:

   ```bash
   cd tu_repositorio

3. Restaura las dependencias:

   ```bash
   dotnet restore

4. Ejecuta la aplicación:

   ```bash
   dotnet run

5. La API debería estar corriendo en:

   ```bash
   http://localhost:5977

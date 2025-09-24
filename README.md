# 📽️ Swapi

## 📑 Índice
- [Descripción](#-descripción)
- [Características](#-características)
- [Instrucciones para ejecutar el proyecto](#-instrucciones-para-ejecutar-el-proyecto)
- [Uso de la Api](#-uso-de-la-api)

---

## 🎬 Descripción
Rest Api para gestionar películas y usuarios que se comunica con la [Api de Star Wars](https://www.swapi.tech/).

---

## 🌟 Características
A continuación se listan algunas características relevantes del proyecto:

- Realizado con .Net Core 9. 
- Autenticación y autorización con JWT.
- Testing con xUnit.  
- Documentación con Swagger.
- Arquitectura Clean.  

---

## 💻 Instrucciones para ejecutar el proyecto
Seguir los siguientes pasos para poder ejecutar el proyecto localmente:

1. Clonar el repositorio.
2. Instalar el SDK o Runtime de .NET 9.  
3. Abrir la solución en Visual Studio.
4. Compilar la solución y verificar/instalar las dependencias de los proyectos.
5. Ajustar la cadena de conexión a la base de datos en el archivo appsettings.json.
6. Ejecutar la migración del proyecto para crear las tablas en la base de datos.
7. Compilar y ejecutar el proyecto.

---

## 🌐 Uso de la Api
Breve explicación de cómo utilizar el proyecto por medio de Swagger.

1. Registrarse por medio del endpoint Users/RegisterUser.
2. Autenticarse con el usuario creado por medio del endpoint Users/Login y copiar el token devuelto.
4. Al comienzo de la vista de Swagger, utilizar el botón "Authorize" para obtener los permisos correspondientes. Escribir "Bearer" + el token obtenido.  
5. Probar los endpoints mediante Swagger. 

### Consideraciones 
- Cuando se ingresa por primera vez, solamente los siguientes endpoints deberían poder ejecutarse sin autenticación: Movies/GetMovies, Users/RegisterUser y Users/Login.
- Endpoints disponibles para rol "RegularUser" (Role id = 2): Movies/GetMovieById y Roles/GetRoleById.
- Endpoints disponibles para rol "AdminUser" (Role id = 1): Movies/CreateMovie, Movies/UpdateMovieById/{id}, Movies/DeleteMovieById/{id}, Movies/SynchronizeMovies y Roles/GetRoles. 
- No habrán películas en el sistema hasta que se cree una nueva o bien se ejecute el endpoint Movies/SynchronizeMovies.

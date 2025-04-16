# Backend - Mentoria

## Descripción del Proyecto

Este es el backend del proyecto **Mentoria**, una aplicación que permite la gestión de Mentorias (CRUD). Se ha desarrollado utilizando **.NET Core** y **Entity Framework Core**, aplicando principios de **Domain-Driven Design (DDD)**, **CQRS**, **Event Sourcing**, e implementando patrones de diseño como **Repositorio**, **Unit of Work**, e **Inyección de Dependencias**.

El backend está desplegado en **Azure App Services** y utiliza una base de datos **SQL Server** en Azure. Además, el proyecto puede ejecutarse en un entorno local con una base de datos local de SQL Server o mediante **Docker** usando `docker-compose`.

---

## Tecnologías y Librerías Utilizadas

- **.NET Core** - Framework principal
- **Entity Framework Core** - ORM para la gestión de la base de datos
- **MediatR** - Implementación del patrón Mediator
- **FluentValidation** - Validaciones en comandos y consultas
- **ErrorOr** - Manejo de errores estructurados con **ProblemDetails**
- **CQRS** - Separación de comandos y consultas
- **Event Sourcing** - Seguimiento de cambios en el dominio
- **Patrón Repositorio** y **Unit of Work** - Capa de persistencia estructurada

---

## Arquitectura del Proyecto

El backend sigue una arquitectura **Cliente-Servidor**, implementando una estructura de **Arquitectura Limpia**, que se compone de las siguientes capas:

### 1. **Hotel.API** (Capa de Presentación)
   - Contiene los **controladores** que exponen los endpoints RESTful.
   - Configura **middlewares**, **extensiones**, **CORS** y **variables de entorno**.
   - Contiene los archivos **Program.cs**, **appsettings.json**, y **Dockerfiles**.

### 2. **Aplicacion** (Capa de Aplicación)
   - Implementa los **casos de uso** mediante **CQRS**.
   - Contiene **validaciones** con FluentValidation.
   - Define **comandos** y **consultas**.

### 3. **Infraestructura** (Capa de Persistencia)
   - Gestiona la **persistencia de datos**.
   - Configura **modelos de entidades** para la base de datos.
   - Implementa **repositorios** y **migraciones**.

### 4. **Dominio** (Capa de Dominio)
   - Contiene las **entidades de dominio**.
   - Define las **reglas de negocio** y **eventos de dominio**.

---

## Instalación y Ejecución

### **1. Ejecución en Local**

#### **Requisitos Previos**
- .NET Core SDK
- SQL Server (local, en Docker o remota)
- Docker (opcional)

#### **Pasos**
1. Clonar el repositorio:
   ```sh
   git clone <URL_DEL_REPOSITORIO>
   cd backendMentoria
   ```
2. Configurar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Database": "Server=localhost;Database=gestionSeriesAnimadasDB;User Id=sa;Password=tu_contraseña;"
   }
   ```
   ```json
   "ConnectionStrings": {
     "Database": "Server=localhost;Database=gestionSeriesAnimadasDB;User Id=localhost;Integrated Security=True; Encrypt=false"
   }
   ```
3. Aplicar migraciones y actualizar la base de datos:
   ```sh
   dotnet ef database update
   ```
4. Ejecutar la API:
   ```sh
   dotnet run
   ```
5. La API estará disponible con ejecuto en http en: `http://localhost:5243`, con https en: `https://localhost:5243` o `https://localhost:7147`, y en IIS Express en: `https://localhost:44358`,
6. La API estará disponible con docker en: `http://localhost:8080`
7. La API desplegada estará disponible en: `https://gestionhotel.azurewebsites.net`

---

### **2. Ejecución con Docker**

1. Construir la imagen Docker:
   ```sh
   docker-compose build
   ```
2. Levantar los contenedores:
   ```sh
   docker-compose up -d
   ```
3. La API estará disponible en: `http://localhost:8080`

---

## Endpoints Principales

### **autenticacion**

| Método | Endpoint | Descripción | Cuerpo de la Solicitud | Respuesta |
|--------|----------|-------------|-------------------------|-----------|
| `POST` | `/autenticacion/iniciar-sesion` | Inicia sesión con credenciales del usuario. | ```json { "nombreDeUsuario": "string", "contrasena": "string" } ``` | ```json { "token": "string", "refreshToken": "string" } ``` |
| `GET` | `/autenticacion/refrescar-token` | Refresca el token de autenticación actual (requiere autenticación). | — | ```json { "token": "string", "refreshToken": "string" } ``` |
| `GET` | `/autenticacion/datos-usuario` | Obtiene los datos del usuario autenticado. | — | ```json { "id": "guid", "nombre": "string", "rol": "string", "nombreDeUsuario": "string" } ``` |
| `PUT` | `/autenticacion/restaurar-contrasena/{id}` | Restaura o cambia la contraseña del usuario. | ```json { "idUsuario": "guid", "contrasenaUno": "string", "contrasenaDos": "string" } ``` | `204 No Content` si es exitoso |

### **usuario**

| Método | Endpoint | Descripción | Cuerpo de la Solicitud | Respuesta |
|--------|----------|-------------|-------------------------|-----------|
| `GET` | `/usuario` | Lista todos los usuarios registrados. | — | ```json [ { "id": "guid", "nombre": "string", "apellido": "string", "rol": "string", "nombreDeUsuario": "string", "correo": "string" } ] ``` |
| `GET` | `/usuario/{id}` | Obtiene los datos de un usuario por su ID. | — | ```json { "id": "guid", "nombre": "string", "apellido": "string", "rol": "string", "nombreDeUsuario": "string", "correo": "string" } ``` |
| `POST` | `/usuario` | Crea un nuevo usuario. | ```json { "nombre": "string", "apellido": "string", "rol": "string", "nombreDeUsuario": "string", "correo": "string", "contrasena": "string" } ``` | `200 OK` si es exitoso |
| `DELETE` | `/usuario/{id}` | Elimina un usuario por su ID. | — | `204 No Content` si es exitoso |
| `PUT` | `/usuario/{id}` | Actualiza la información de un usuario existente. | ```json { "id": "guid", "nombre": "string", "apellido": "string", "rol": "string", "nombreDeUsuario": "string", "correo": "string", "contrasena": "string" } ``` | `204 No Content` si es exitoso |
| `POST` | `/usuario/lista-paginada` | Lista usuarios con filtros, ordenamiento y paginación. | ```json { "terminoDeBusqueda": "string", "ordenarColumna": "string", "ordenarLista": "ASC|DESC", "pagina": 1, "tamanoPagina": 10 } ``` | ```json { "elementos": [ { "id": "guid", "nombre": "string", "apellido": "string", "rol": "string", "nombreDeUsuario": "string", "correo": "string" } ], "pagina": 1, "tamanoPagina": 10, "cantidadTotal": 100, "tieneSiguientePagina": true, "tienePaginaAnterior": false } ``` |

### **servicio**

| Método | Endpoint | Descripción | Cuerpo de la Solicitud | Respuesta |
|--------|----------|-------------|-------------------------|-----------|
| `GET` | `/servicio` | Lista todos los servicios registrados. | — | ```json [ { "id": "guid", "nombre": "string" } ] ``` |
| `GET` | `/servicio/{id}` | Obtiene los datos de un servicio por su ID. | — | ```json { "id": "guid", "nombre": "string" } ``` |
| `POST` | `/servicio` | Crea un nuevo servicio. | ```json { "nombre": "string" } ``` | `200 OK` si es exitoso |
| `DELETE` | `/servicio/{id}` | Elimina un servicio por su ID. | — | `204 No Content` si es exitoso |
| `PUT` | `/servicio/{id}` | Actualiza la información de un servicio existente. | ```json { "id": "guid", "nombre": "string" } ``` | `204 No Content` si es exitoso |
| `POST` | `/servicio/lista-paginada` | Lista servicios con filtros, ordenamiento y paginación. | ```json { "terminoDeBusqueda": "string", "ordenarColumna": "string", "ordenarLista": "ASC|DESC", "pagina": 1, "tamanoPagina": 10 } ``` | ```json { "elementos": [ { "id": "guid", "nombre": "string" } ], "pagina": 1, "tamanoPagina": 10, "cantidadTotal": 100, "tieneSiguientePagina": true, "tienePaginaAnterior": false } ``` |

### **habitacion**

| Método | Endpoint | Descripción | Cuerpo de la Solicitud | Respuesta |
|--------|----------|-------------|-------------------------|-----------|
| `GET` | `/habitacion` | Lista todas las habitaciones. | — | ```json [ { "id": "guid", "numeroDeHabitacion": "string", "nombre": "string", "descripcion": "string", "precioPorNoche": 100.0, "capacidad": 2, "servicios": [ { "id": "guid", "nombre": "string" } ], "imagenes": [ { "id": "guid", "url": "string" } ], "estado": "string" } ] ``` |
| `GET` | `/habitacion/{id}` | Obtiene los datos de una habitación por su ID. | — | ```json { "id": "guid", "numeroDeHabitacion": "string", "nombre": "string", "descripcion": "string", "precioPorNoche": 100.0, "capacidad": 2, "servicios": [ { "id": "guid", "nombre": "string" } ], "imagenes": [ { "id": "guid", "url": "string" } ], "estado": "string" } ``` |
| `POST` | `/habitacion` | Crea una nueva habitación. | ```json { "numeroDeHabitacion": "string", "nombre": "string", "descripcion": "string", "precioPorNoche": 100.0, "capacidad": 2, "servicios": [ { "id": "guid" } ], "imagenes": [ { "url": "string" } ], "estado": "string" } ``` | `200 OK` si es exitoso |
| `DELETE` | `/habitacion/{id}` | Elimina una habitación por su ID. | — | `204 No Content` si es exitoso |
| `PUT` | `/habitacion/{id}` | Actualiza la información de una habitación existente. | ```json { "id": "guid", "numeroDeHabitacion": "string", "nombre": "string", "descripcion": "string", "precioPorNoche": 100.0, "capacidad": 2, "servicios": [ { "id": "guid" } ], "imagenes": [ { "url": "string" } ], "estado": "string" } ``` | `204 No Content` si es exitoso |
| `POST` | `/habitacion/lista-paginada` | Lista habitaciones con filtros, ordenamiento y paginación. | ```json { "terminoDeBusqueda": "string", "ordenarColumna": "string", "ordenarLista": "ASC|DESC", "pagina": 1, "tamanoPagina": 10 } ``` | ```json { "elementos": [ { "id": "guid", "numeroDeHabitacion": "string", "nombre": "string", "descripcion": "string", "precioPorNoche": 100.0, "capacidad": 2, "servicios": [ { "id": "guid", "nombre": "string" } ], "imagenes": [ { "id": "guid", "url": "string" } ], "estado": "string" } ], "pagina": 1, "tamanoPagina": 10, "cantidadTotal": 100, "tieneSiguientePagina": true, "tienePaginaAnterior": false } ``` |

### **resena**

| Método | Endpoint | Descripción | Cuerpo de la Solicitud | Respuesta |
|--------|----------|-------------|-------------------------|-----------|
| `GET` | `/resena` | Lista todas las reseñas. | — | ```json [ { "id": "guid", "habitacion": { "id": "guid", "nombre": "string" }, "usuario": { "id": "guid", "nombre": "string" }, "titulo": "string", "calificacion": 5, "descripcion": "string", "imagenes": [ { "id": "guid", "url": "string" } ], "fechaCreacion": "2024-04-07T00:00:00" } ] ``` |
| `GET` | `/resena/{id}` | Obtiene una reseña por su ID. | — | ```json { "id": "guid", "habitacion": { "id": "guid", "nombre": "string" }, "usuario": { "id": "guid", "nombre": "string" }, "titulo": "string", "calificacion": 5, "descripcion": "string", "imagenes": [ { "id": "guid", "url": "string" } ], "fechaCreacion": "2024-04-07T00:00:00" } ``` |
| `POST` | `/resena` | Crea una nueva reseña. | ```json { "habitacion": { "id": "guid" }, "usuario": { "id": "guid" }, "titulo": "string", "calificacion": 5, "descripcion": "string", "imagenes": [ { "url": "string" } ] } ``` | `200 OK` si es exitoso |
| `DELETE` | `/resena/{id}` | Elimina una reseña por su ID. | — | `204 No Content` si es exitoso |
| `PUT` | `/resena/{id}` | Actualiza una reseña existente. | ```json { "id": "guid", "habitacion": { "id": "guid" }, "usuario": { "id": "guid" }, "titulo": "string", "calificacion": 5, "descripcion": "string", "imagenes": [ { "url": "string" } ] } ``` | `204 No Content` si es exitoso |
| `POST` | `/resena/lista-paginada` | Lista reseñas con filtros, ordenamiento y paginación. | ```json { "terminoDeBusqueda": "string", "ordenarColumna": "string", "ordenarLista": "ASC|DESC", "pagina": 1, "tamanoPagina": 10 } ``` | ```json { "elementos": [ { "id": "guid", "habitacion": { "id": "guid", "nombre": "string" }, "usuario": { "id": "guid", "nombre": "string" }, "titulo": "string", "calificacion": 5, "descripcion": "string", "imagenes": [ { "id": "guid", "url": "string" } ], "fechaCreacion": "2024-04-07T00:00:00" } ], "pagina": 1, "tamanoPagina": 10, "cantidadTotal": 100, "tieneSiguientePagina": true, "tienePaginaAnterior": false } ``` |

### **reserva**

| Método | Endpoint | Descripción | Cuerpo de la Solicitud | Respuesta |
|--------|----------|-------------|-------------------------|-----------|
| `GET` | `/reserva` | Lista todas las reservas. | — | ```json [ { "id": "guid", "usuario": { "id": "guid", "nombre": "string" }, "habitacion": { "id": "guid", "nombre": "string" }, "fechaIngreso": "2024-04-07T00:00:00", "fechaSalida": "2024-04-08T00:00:00", "cantidadAdultos": 2, "cantidadNinos": 1, "contacto": { "id": "guid", "nombre": "string", "apellido": "string", "correo": "string", "telefono": "string" }, "formaDePago": { "id": "guid", "titular": "string", "numeroTarjeta": "string", "fechaDeVencimiento": "2025-04-01T00:00:00", "cvv": "string" }, "precioTotal": 150.0, "fechaCreacion": "2024-04-07T00:00:00" } ] ``` |
| `GET` | `/reserva/{id}` | Obtiene una reserva por su ID. | — | ```json { "id": "guid", "usuario": { "id": "guid", "nombre": "string" }, "habitacion": { "id": "guid", "nombre": "string" }, "fechaIngreso": "2024-04-07T00:00:00", "fechaSalida": "2024-04-08T00:00:00", "cantidadAdultos": 2, "cantidadNinos": 1, "contacto": { "id": "guid", "nombre": "string", "apellido": "string", "correo": "string", "telefono": "string" }, "formaDePago": { "id": "guid", "titular": "string", "numeroTarjeta": "string", "fechaDeVencimiento": "2025-04-01T00:00:00", "cvv": "string" }, "precioTotal": 150.0, "fechaCreacion": "2024-04-07T00:00:00" } ``` |
| `POST` | `/reserva` | Crea una nueva reserva. | ```json { "usuario": { "id": "guid" }, "habitacion": { "id": "guid" }, "fechaIngreso": "2024-04-07T00:00:00", "fechaSalida": "2024-04-08T00:00:00", "cantidadAdultos": 2, "cantidadNinos": 1, "contacto": { "nombre": "string", "apellido": "string", "correo": "string", "telefono": "string" }, "formaDePago": { "titular": "string", "numeroTarjeta": "string", "fechaDeVencimiento": "2025-04-01T00:00:00", "cvv": "string" } } ``` | `200 OK` si es exitoso |
| `PUT` | `/reserva/{id}` | Actualiza una reserva existente. | ```json { "id": "guid", "usuario": { "id": "guid" }, "habitacion": { "id": "guid" }, "fechaIngreso": "2024-04-07T00:00:00", "fechaSalida": "2024-04-08T00:00:00", "cantidadAdultos": 2, "cantidadNinos": 1, "contacto": { "nombre": "string", "apellido": "string", "correo": "string", "telefono": "string" }, "formaDePago": { "titular": "string", "numeroTarjeta": "string", "fechaDeVencimiento": "2025-04-01T00:00:00", "cvv": "string" } } ``` | `204 No Content` si es exitoso |
| `DELETE` | `/reserva/{id}` | Elimina una reserva por su ID. | — | `204 No Content` si es exitoso |
| `POST` | `/reserva/lista-paginada` | Lista reservas con filtros, ordenamiento y paginación. | ```json { "terminoDeBusqueda": "string", "ordenarColumna": "string", "ordenarLista": "ASC|DESC", "pagina": 1, "tamanoPagina": 10 } ``` | ```json { "elementos": [ { "id": "guid", "usuario": { "id": "guid", "nombre": "string" }, "habitacion": { "id": "guid", "nombre": "string" }, "fechaIngreso": "2024-04-07T00:00:00", "fechaSalida": "2024-04-08T00:00:00", "cantidadAdultos": 2, "cantidadNinos": 1, "contacto": { "id": "guid", "nombre": "string", "apellido": "string", "correo": "string", "telefono": "string" }, "formaDePago": { "id": "guid", "titular": "string", "numeroTarjeta": "string", "fechaDeVencimiento": "2025-04-01T00:00:00", "cvv": "string" }, "precioTotal": 150.0, "fechaCreacion": "2024-04-07T00:00:00" } ], "pagina": 1, "tamanoPagina": 10, "cantidadTotal": 100, "tieneSiguientePagina": true, "tienePaginaAnterior": false } ``` |

### **archivo**

| Método  | Endpoint                   | Descripción                                  | Cuerpo de la Solicitud                                          | Respuesta |
|---------|----------------------------|----------------------------------------------|------------------------------------------------------------------|-----------|
| `POST`  | `/archivo/subir`           | Sube un archivo al sistema.                  | `multipart/form-data` con el archivo en el campo `archivo`.     | ```json { "id": "guid" } ``` |
| `GET`   | `/archivo/descargar/{id}`  | Descarga un archivo por su ID.               | —                                                                | Archivo binario (`application/octet-stream` o tipo correspondiente) |
| `DELETE`| `/archivo/eliminar/{id}`   | Elimina un archivo por su ID.                | —                                                                | `204 No Content` si es exitoso |


---

## Despliegue en Azure

1. **Base de Datos:** Se despliega en **Azure SQL Database**.
2. **API:** Se despliega en **Azure App Services**.
3. **CI/CD:** Se configurar con **GitHub Actions** pero también se puede con otros como **Azure DevOps**.

---

## Contribución
Si deseas contribuir al proyecto:
1. Realiza un fork del repositorio.
2. Crea una nueva rama (`feature/nueva-funcionalidad`).
3. Envía un Pull Request con tus cambios.

---

## Contacto
Para dudas o sugerencias, puedes contactarme en **nayid2004@gmail.com**.


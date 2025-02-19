# SalesPredictionAPI

Este proyecto es una API, desarrollada en **.NET Core 8.0** con una arquitectura modular. Se divide en cuatro proyectos principales, cada uno con una responsabilidad específica.

## Estructura del Proyecto

### 1. **SalesPredictionAPI** (Proyecto Principal)
Este es el núcleo de la API, donde se configuran las rutas y controladores.

- **Controllers/**
Contiene los controladores que gestionan las solicitudes HTTP y se comunican con la capa de repositorio.

- **Extensions/ServiceExtensions.cs**: 
Este archivo contiene métodos de extensión para registrar los diferentes servicios en el contenedor de inyección de dependencias.
El método ConfigureRepositories agrega la configuración de los repositorios de la aplicación usando AddScoped.

---
### 2. **SalesPredictionAPI.DataBase** (Capa de Datos)
Este proyecto contiene los modelos que representan las entidades de la base de datos.

- **Models/**: Define las clases que representan las tablas de la base de datos, como `Order.cs` y `OrderDetail.cs`.

---
### 3. **SalesPredictionAPI.DTO** (Data Transfer Objects)
Contiene los DTOs utilizados para la transferencia de datos entre la API y los clientes.

- **Models/**: Define los DTOs, que son versiones simplificadas de los modelos de la base de datos:
    - `CustomerDTO.cs`
    - `EmployeeDTO.cs`
    - `OrderDTO.cs`
    - `ProductDTO.cs`
    - `ResponseDTO.cs`
    - `ShipperDTO.cs`

---
### 4. **SalesPredictionAPI.Repository** (Capa de Acceso a Datos)
Esta capa maneja la interacción con la base de datos mediante repositorios.

- **Interfaces/**: Contiene las interfaces de los repositorios, definiendo los métodos de acceso a datos.
  - `ICustomerRepository.cs`
  - `IEmployeeRepository.cs`
  - `IOrderRepository.cs`
  - `IProductRepository.cs`
  - `IShipperRepository.cs`

- **Repository/**: Contiene las implementaciones de los repositorios, encargadas de ejecutar las consultas SQL o acceder a los datos en la base de datos.
  - `CustomerRepository.cs`
  - `EmployeeRepository.cs`
  - `OrderRepository.cs`
  - `ProductRepository.cs`
  - `ShipperRepository.cs`

## Funcionamiento
Antes de ejecutar el proyecto, Configurar la conexion a la base de datos "StoreSample" en appsettings.json

# 🚚 Dispatch Order System

Sistema de gestión de órdenes de despacho desarrollado en .NET con arquitectura limpia, que permite calcular distancias geográficas, estimar costos de envío y generar reportes por cliente. Construido como parte de una prueba técnica para postular para el cargo de Desarrollador FullStack Ssr.

---

## 📌 Características principales

- ✅ Crear órdenes de despacho con cliente, producto, coordenadas y cantidad.
- ✅ Cálculo automático de distancia entre origen y destino usando fórmula de **Haversine**.
- ✅ Estimación del costo de despacho según rangos de distancia definidos.
- ✅ Validación de reglas de negocio (distancia entre 1 y 1000 km).
- ✅ Consultas por cliente y por ID.
- ✅ Reporte por cliente agrupado por intervalos de distancia.
- ✅ Exportación del reporte a archivo **Excel (.xlsx)**.
- ✅ Arquitectura limpia, principios SOLID y CQRS con MediatR.
- ✅ Validaciones con **FluentValidation**.
- ✅ Logging con **Serilog**.
- ✅ Tests unitarios

---

## 🛠 Tecnologías usadas

- **Backend:** .NET 8, C#, ASP.NET Core Web API
- **Frontend:** ASP.NET Core MVC
- **Base de datos:** SQL Server (Azure SQL compatible)
- **ORM:** Entity Framework Core
- **CQRS & Mediator:** MediatR
- **Validaciones:** FluentValidation
- **Exportación Excel:** ClosedXML
- **Logging:** Serilog
- **Testing:** xUnit, FluentAssertions, FluentValidation.TestHelper

---

## 🧱 Arquitectura

Este proyecto sigue una estructura de **Arquitectura Limpia**, con separación en:

```
📦 DispatchOrderSystem
├── 📁 Api                  → Controladores, middlewares, configuración Web
├── 📁 Application          → Casos de uso, DTOs, servicios, validaciones, CQRS
├── 📁 Domain (opcional)    → Entidades y lógica de dominio (si aplica)
├── 📁 Infrastructure       → EF Core, repositorios, acceso a datos
├── 📁 Tests                → Pruebas unitarias
```

---

## 🧪 Pruebas

Incluye **tests unitarios** para validaciones usando xUnit:

```bash
dotnet test
```

Las pruebas cubren casos positivos y negativos para validadores de:
- Crear Cliente
- Crear Producto
- Crear Orden

---

## ⚙️ Instalación y ejecución

### 1. Clona el repositorio

```bash
git clone https://github.com/JDanielPosada/dispatch-orders-system.git
cd DispatchOrderSystem
```

### 2. Configura la base de datos

Edita `appsettings.json` en el proyecto `Api`:
Por seguridad, el `ConnectionStrings` debes solicitarlo al dev 

```json
"ConnectionStrings": {
  "DefaultConnection": "Server={0};Database={1};User Id={2};Password={3};TrustServerCertificate=True;Connection Timeout=30;"
}
```

### 3. Ejecuta la API

```bash
cd DispatchOrderSystem.Api
dotnet run
```

La API estará disponible en `https://localhost:7051`.

### 5. Ejecuta el frontend

Si estás usando MVC o Razor Pages:

```bash
cd DispatchOrderSystem.Web
dotnet run
```

Puedes configurar como proyecto de inicio `DispatchOrderSystem.Api` y `DispatchOrderSystem.Web` para que se ejecuten al mismo tiempo y le das Iniciar
---

## 🌍 Endpoints principales

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST   | `/api/orders` | Crea una nueva orden de despacho |
| DELETE | `/api/orders/{id}` | Elimina una orden existente por su ID |
| GET    | `/api/orders/{id}` | Obtiene una orden por su ID |
| GET    | `/api/orders/client/{clientId}` | Obtiene las ordenes asociadas a un cliente |
| GET    | `/api/orders/getAll` | Obtiene todas las ordenes registradas |
| GET    | `/api/orders/report/client-distance` | Obtiene un reporte por cliente agrupado por distancias |
| GET    | `/api/orders/report/client-distance/export` | Exporta el reporte por cliente y distancia a un archivo Excel |

| POST 	 | `/api/Clients` | Crea un nuevo cliente |
| GET 	 | `/api/Clients` | Obtiene todos los clientes registrados |

| POST 	 | `/api/Products` | Crea un nuevo producto |
| GET 	 | `/api/Products` | Obtiene todos los productos registrados |

| POST 	 | `/api/SeedOrders` | Inserta clientes, prodcutos y ordenes de prueba en la base de datos |
---

## 📄 Lógica de cálculo de costos

La distancia se calcula con la fórmula de **Haversine**. El costo del despacho se asigna así:

| Rango de distancia (km) | Costo (USD) |
|-------------------------|-------------|
| 1 - 50                  | 100         |
| 51 - 200                | 300         |
| 201 - 500               | 1000        |
| 501 - 1000              | 1500        |

> Se rechazan órdenes con distancia menor a 1 km o mayor a 1000 km.

---

## ✅ Requisitos cumplidos

- [x] Arquitectura limpia
- [x] Principios SOLID
- [x] Patrones CQRS, Mediator, Repositorio
- [x] Validaciones centralizadas
- [x] Logging
- [x] Exportación a Excel
- [x] Pruebas unitarias
- [x] Documentación clara
- [x] Organización en Git
- [x] Separación por capas

---

## 🧠 Decisiones técnicas

- Se usó **MediatR** para separar comandos y queries (CQRS) y permitir escalabilidad.
- Validaciones robustas con **FluentValidation** y centralizadas con `PipelineBehavior`.
- **Serilog** para logging estructurado y seguimiento de errores.
- DTOs para desacoplar la lógica de presentación y datos.
- Exportación con ClosedXML por su facilidad y formato amigable.

---

## 🌐 Publicación en la nube

El backend de esta aplicación fue desplegado exitosamente en **Microsoft Azure**, permitiendo su uso inmediato sin necesidad de levantarlo localmente.

Puedes acceder a la documentación interactiva de la API (Swagger UI) desde la siguiente URL:

🔗 [https://pruebatecnica10.azurewebsites.net/swagger/index.html](https://pruebatecnica10.azurewebsites.net/swagger/index.html)


## 🖥️ Frontend

El frontend de esta aplicación fue desarrollado utilizando **ASP.NET Core MVC**, con enfoque en mantener una arquitectura limpia, una experiencia de usuario clara y validaciones en los formularios.

### 🎯 Funcionalidades implementadas

- **Crear orden de despacho:**  
  Formulario para registrar una orden, solicitando cliente, producto, cantidad, coordenadas de origen y destino.  

- **Visualizar órdenes por cliente:**  
  Página donde se listan todas las órdenes creadas, mostrando información relevante como cliente, producto, coordenadas, distancia, costo y fecha de creación.

- **Visualizar reporte por cliente:**  
  Vista de reporte donde se muestra, por cada cliente, cuántas órdenes existen en cada uno de los rangos de distancia:  
  - 1 a 50 km  
  - 51 a 200 km  
  - 201 a 500 km  
  - 501 a 1000 km  
  También se incluye una opción para **descargar el reporte en formato Excel**.


### 🚀 Experiencia de usuario

- Transiciones suaves entre vistas utilizando `JavaScript` y `setTimeout` para simular efecto de desvanecimiento (`fade-out`) antes de redirigir.
- **Spinner overlay** para indicar operaciones en curso (navegación o envíos), mejorando la retroalimentación visual.
- El spinner solo se muestra si la validación del formulario es exitosa, evitando bloquear la interfaz cuando hay errores de entrada.

### 🛠️ Tecnologías utilizadas

- **ASP.NET Core MVC**
- **Bootstrap 5** para estilo y diseño responsivo.
- **jQuery Validation** y **jQuery Unobtrusive** para validaciones del lado cliente.
- **JavaScript personalizado (`site.js`)** para control de navegación y carga visual.
- Integración con API REST del backend para todas las operaciones.

### 📸 Capturas de pantalla

A continuación, se incluyen capturas de pantalla que muestran el flujo de creación de órdenes, visualización por cliente, el reporte con agrupación por distancia y el spinner en acción:

- home

- Crear una Orden

- Visualización de todas las ordenes

- Reporte por Cliente y distancia

- Visualización de Excel

- Visualización de clientes

- Crear un nuevo cliente

- Visualización de productos

- Crear un nuevo producto

## 🧑‍💻 Autor

**Daniel**  
Desarrollador Full Stack SSr  

---

## 📃 Licencia

MIT - Puedes usar este proyecto con fines educativos o empresariales.

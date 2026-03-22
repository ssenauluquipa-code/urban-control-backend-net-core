# UrbanControl - Backend ERP Inmobiliario 🏘️

Sistema de gestión para urbanizaciones y control de lotes desarrollado en **.NET 8**. 
Diseñado para centralizar la administración de proyectos inmobiliarios, clientes y ventas.

## 🚀 Módulos Implementados
- **Autenticación:** Seguridad basada en JWT (JSON Web Tokens).
- **Proyectos:** Gestión de urbanizaciones (Ubicación, Precios Base).
- **Lotes:** Control de inventario de terrenos y estados (Disponible, Reservado, Vendido).
- **Clientes (CRM):** Registro y validación de compradores por Documento de Identidad.
- **Reservas:** Lógica transaccional para el bloqueo de lotes y reportes de inventario.

## 🛠️ Tecnologías
- **Core:** ASP.NET Core 8 Web API.
- **Base de Datos:** PostgreSQL (Alojado en Aiven).
- **ORM:** Entity Framework Core.
- **Seguridad:** Authentication & Authorization con JWT.
- **Documentación:** Swagger UI configurado con esquema Bearer Http.

## ⚙️ Configuración
1. Clonar el repositorio.
2. Configurar la cadena de conexión en `appsettings.json`.
3. Ejecutar `Update-Database` para las migraciones.
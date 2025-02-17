# Api-Carrito

**Desarrollo API REST para Carrito de Compras**  
Esta API RESTful ha sido desarrollada en **.NET 9** siguiendo los principios de **SOLID** y la arquitectura **Clean Architecture**. Se han implementado diversos **patrones de diseño** para garantizar un código modular, escalable y mantenible.

## Características Principales
✔ **Repositorio** → Separa acceso a datos.  
✔ **Unidad de Trabajo** → Maneja transacciones.  
✔ **Inyección de Dependencias** → Reduce acoplamiento.  
✔ **Middleware** → Manejo de errores centralizado.  
✔ **Singleton (`DbContext`)** → Control de instancias y rendimiento.  

---

## 🛠 **Tecnologías Utilizadas**
- **.NET 9** (ASP.NET Core)
- **Entity Framework Core 9** (Persistencia en SQL Server)
- **JWT Authentication** (Seguridad)
- **Swagger** (Documentación de la API)
- **Serilog** (Manejo de logs)

---

## 🏛 **Arquitectura Aplicada - Clean Architecture**
La API sigue los principios de **Clean Architecture**, separando responsabilidades en diferentes capas:

**Api-carrito** → Proyecto Web API (Interfaz de usuario)  
**ShoppingCart.Application** → Capa de Aplicación (Lógica de negocio)  
**ShoppingCart.Domain** → Capa de Dominio (Entidades y Reglas de negocio)  
**ShoppingCart.Infrastructure** → Capa de Infraestructura (Persistencia y Repositorios)  

---

## **Uso de la API**
### **1. Clonar el Repositorio**
```sh
git clone https://github.com/tu-usuario/tu-repositorio.git
cd tu-repositorio
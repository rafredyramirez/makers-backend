# 🚀 LoansApp - Prueba Técnica (.NET 8 + Blazor + PostgreSQL)

Aplicación full stack desarrollada en .NET 8 que permite la gestión de solicitudes de préstamos, implementando una arquitectura limpia, escalable y mantenible.

---

## 🧠 Descripción del Proyecto

Este proyecto consiste en el desarrollo de una solución que permite:

- Crear solicitudes de préstamo
- Consultar préstamos por usuario
- Gestionar lógica de negocio desacoplada
- Consumir servicios mediante una API REST
- Visualizar información desde un frontend en Blazor

---

## 🏗️ Arquitectura

Se implementó **Clean Architecture (Arquitectura Limpia)** para garantizar:

- Separación de responsabilidades
- Bajo acoplamiento entre capas
- Alta mantenibilidad
- Escalabilidad del sistema

### 📁 Estructura del proyecto

/src
├── Api → Capa de presentación (Web API)
├── Application → Lógica de negocio
├── Domain → Entidades y reglas del dominio
├── Infrastructure → Acceso a datos (EF Core + PostgreSQL)
├── LoansApp.Web → Frontend (Blazor Server)

---

## 🔧 Tecnologías utilizadas

### Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL

### Frontend
- Blazor Server

---

## 🧩 Patrones y buenas prácticas implementadas

### ✅ Clean Architecture

Se eligió este enfoque para desacoplar completamente la lógica de negocio de la infraestructura y la capa de presentación.

**Beneficios:**
- Facilita pruebas unitarias
- Permite cambiar tecnologías sin afectar el core
- Mejora la mantenibilidad

---

### ✅ Inyección de Dependencias (DI)

Uso del contenedor de dependencias de .NET para:

- Desacoplar servicios
- Facilitar testing
- Mejorar la escalabilidad

---

### 👤 Usuario Administrador
Email: usuario@test.com
Password: 123
Rol: Admin

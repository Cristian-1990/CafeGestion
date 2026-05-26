# ☕ CafeGestion

Sistema de gestión de cafés de especialidad desarrollado en **C# (.NET 10)** siguiendo principios de **arquitectura limpia** y **SOLID**.

> Proyecto de 1º DAW — Programación y Entornos de Desarrollo  
> Autor: Cristian  
> Repositorio: https://github.com/Cristian-1990/CafeGestion

---

## 🏗️ Arquitectura

El proyecto sigue una separación de responsabilidades por capas:

```
Program (UI/Consola)
    └── Service (Lógica de negocio)
            ├── Validator (Validaciones de dominio)
            └── Repository (Persistencia en memoria)
                    └── Storage (Serialización JSON / XML / CSV)
```

### Capas implementadas

| Capa | Descripción |
|---|---|
| **Models** | `Producto` (abstract record) y `Cafe` (sealed record) |
| **Enums** | `TipoOrigen`, `TipoVariedad`, `TipoProceso`, `MenuOpciones` |
| **Config** | `Configuracion.cs` — rutas y tipo de storage sin valores mágicos |
| **Dto** | `CafeDto` con anotaciones `JsonPropertyName` |
| **Mapper** | `CafeMapper` — métodos de extensión `ToModel()` y `ToDto()` |
| **Exceptions** | `DomainException`, `ProductosException` (NotFound, AlreadyExists, Validation, StorageError) |
| **Validators** | `IValidador<T>`, `ValidadorCafe` |
| **Repository** | `ICrudRepository<TKey,TEntity>`, `IProductoRepo`, `ProductoRepo` con Dictionary |
| **Storage** | `IStorage<T>`, implementaciones JSON, XML y CSV |
| **Service** | `IProductoService`, `ProductoService` |
| **Factory** | `ProductoFactory` (Seed), `RepoFactory`, `StorageFactory` |
| **Program** | Menú con Spectre.Console, CRUD completo |



## 🧪 Tests

Proyecto de tests con **NUnit + FluentAssertions + Moq**.

| Suite | Tests | Estado |
|---|---|---|
| `ValidadorCafeTests` | 13 | ✅ |
| `CafeTests` (Models) | 3 | ✅ |
| `ProductoRepoTests` | 10 | ✅ |
| `ProductoServiceTests` | 8 | ✅ |
| **Total** | **34** | **✅ 100%** |

---

## 📄 Generación de Fichas

Desde el menú, opción 6 — genera la ficha de un café en:
- **HTML** — ficha estilizada en `Data/ficha_cafe_{id}.html`
- **PDF** — mismo contenido en `Data/ficha_cafe_{id}.pdf` (QuestPDF)

---

## 📋 Requisitos implementados

- ✅ CRUD completo
- ✅ Persistencia polimórfica (JSON, XML, CSV)
- ✅ Generación de fichas PDF y HTML
- ✅ Sistema de logs con Serilog (consola + fichero, rotación 5 días)
- ✅ Tests unitarios (34 tests)
- ✅ Arquitectura limpia con interfaces y genéricos
- ✅ Documentación XML al 95%

## 🚧 Pendiente

- Interfaz gráfica WPF (la capa de presentación está desacoplada del negocio para poder añadirla sin tocar ninguna otra capa)
- Persistencia en base de datos (EF Core)
- Documentación UML

---

## 🚀 Ejecución

```bash
git clone https://github.com/Cristian-1990/CafeGestion
cd CafeGestion/CafeGestion/CafeGestion
dotnet run
```

---

## 📁 Estructura del proyecto

```
CafeGestion/
├── Config/
├── Dto/
├── Enums/
├── Exceptions/
├── Factory/
├── Mapper/
├── Models/
├── Repository/
├── Service/
├── Storage/
│   ├── Common/
│   ├── StorageCsv/
│   ├── StorageJson/
│   ├── StorageReport/
│   └── StorageXml/
├── Validators/
└── Program.cs

CafeGestion.Tests/
├── Models/
├── Repository/
├── Service/
└── Validators/
```

# Documentación del Proyecto — CafeGestion

## 1. Descripción del Problema

CafeGestion es una aplicación de gestión de cafés de especialidad que permite llevar un inventario de productos, realizar operaciones tipo CRUD, generar fichas de cada café y exportar los datos en múltiples formatos. El sistema está diseñado con arquitectura por capas intentando aplicar principios SOLID.

---

## 2. Requisitos Funcionales

- RF1. El sistema permite listar todos los cafés disponibles en el inventario.
- RF2. El sistema permite añadir un nuevo café con todos sus atributos.
- RF3. El sistema permite buscar un café por su ID.
- RF4. El sistema permite modificar los datos de un café existente.
- RF5. El sistema permite eliminar un café del inventario.
- RF6. El sistema permite exportar el inventario en formato JSON, XML y CSV.
- RF7. El sistema permite generar una ficha de un café en PDF y HTML.
- RF8. El sistema registra logs de actividad en consola y fichero con rotación automática cada 5 días.

---

## 3. Requisitos No Funcionales

- RNF1. El sistema está desarrollado en C# (.NET 10) siguiendo arquitectura por capas.
- RNF2. El motor de persistencia es intercambiable sin recompilar — solo modificando `Configuracion.cs`.
- RNF3. El código está documentado con XML docs en todas las capas.
- RNF4. El sistema dispone de 34 tests unitarios con NUnit, FluentAssertions y Moq.
- RNF5. Los logs se almacenan en consola y fichero con rotación automática a los 5 días.
- RNF6. La interfaz de consola está desarrollada con Spectre.Console.
- RNF7. El código fuente está versionado en GitHub con ramas main y dev.

---

## 4. Requisitos de Información

### RI1. Café

| Atributo     | Tipo        | Descripción                          |
|--------------|-------------|--------------------------------------|
| Id           | int         | Identificador único                  |
| Nombre       | string      | Nombre del café                      |
| Origen       | TipoOrigen  | País de origen (enum)                |
| Región       | string      | Región de origen                     |
| Variedad     | TipoVariedad| Variedad del grano (enum)            |
| Proceso      | TipoProceso | Proceso de beneficiado (enum)        |
| Puntuación   | double      | Puntuación entre 8 y 10              |
| Cantidad     | int         | Cantidad en kg (mínimo 1)            |
| Disponible   | bool        | Si está disponible para venta        |
| NotaDeCata   | string      | Descripción organoléptica            |
| FechaTueste  | DateTime    | Fecha del último tueste              |
| Entrada      | DateTime    | Fecha de entrada al inventario       |

---

## 5. Diagrama de Casos de Uso

Actor: **Gestor**

| Caso de uso              | Descripción                                      |
|--------------------------|--------------------------------------------------|
| Listar cafés             | Ver todos los cafés del inventario               |
| Añadir café              | Registrar un nuevo café con sus atributos        |
| Buscar café por ID       | Consultar los datos de un café concreto          |
| Modificar café           | Actualizar los datos de un café existente        |
| Eliminar café            | Borrar un café del inventario                    |
| Generar ficha PDF/HTML   | Exportar la ficha de un café en PDF y HTML       |
| Exportar JSON/XML/CSV    | Exportar todo el inventario en distintos formatos|
| Cambiar motor de storage | Cambiar el formato de persistencia vía config    |

---

## 6. Diagrama de Arquitectura

```
Program (UI/Consola · Spectre.Console)
    └── ProductoService (IProductoService · lógica de negocio)
            ├── ValidadorCafe (IValidador<T> · reglas de dominio)
            └── ProductoRepo (ICrudRepository<TKey,T> · Dictionary)
                    └── StorageFactory (lee Configuracion.TipoStorage)
                            ├── StorageJson  → cafes.json
                            ├── StorageXml   → cafes.xml
                            └── StorageCsv   → cafes.csv

Program ──(directo)──> StorageReport → ficha.pdf / ficha.html
Configuracion.cs → rutas y TipoStorage
Models: Producto (abstract record) / Cafe (sealed record)
Dto / Mapper / Exceptions → capas de soporte
```

---

## 7. Diseño de Base de Datos

Aunque la persistencia actual es en ficheros, la estructura equivalente en BD sería:

```sql
CREATE TABLE Cafe (
    id           INT PRIMARY KEY AUTO_INCREMENT,
    nombre       VARCHAR(100) NOT NULL,
    origen       VARCHAR(50)  NOT NULL,
    region       VARCHAR(100) NOT NULL,
    variedad     VARCHAR(50)  NOT NULL,
    proceso      VARCHAR(50)  NOT NULL,
    puntuacion   DECIMAL(4,2) NOT NULL CHECK (puntuacion >= 8.0 AND puntuacion <= 10.0),
    cantidad     INT          NOT NULL CHECK (cantidad >= 1),
    disponible   BOOLEAN      NOT NULL DEFAULT TRUE,
    nota_cata    TEXT,
    fecha_tueste DATETIME     NOT NULL,
    entrada      DATETIME     NOT NULL DEFAULT NOW()
);
```

---

## 8. Diagramas de Secuencia

### Guardar café
```
Program → Service.Guardar(cafe)
    Service → Validator.Validar(cafe) → Ok / ProductosException
    Service → Repository.Create(cafe) → cafe creado
    Service → Storage.Guardar(items, path) → void
Service → Program: cafe guardado
```

### GetAll
```
Program → Service.GetAll()
    Service → Repository.GetAll() → IEnumerable<Producto>
Service → Program: lista de cafés
```

### GetById
```
Program → Service.GetById(id)
    Service → Repository.GetById(id) → Producto? / null
    Si null → lanza ProductosException.NotFound
Service → Program: cafe / excepción
```

### Delete
```
Program → Service.Delete(id)
    Service → Repository.Delete(id) → cafe borrado
    Service → Storage.Guardar(items, path) → void
Service → Program: cafe borrado
```

---

## 9. Análisis Económico

### Costes de desarrollo

| Fase               | Descripción                              | Horas | Coste (25€/h) |
|--------------------|------------------------------------------|-------|---------------|
| Análisis y diseño  | Requisitos, UML, arquitectura            | 8h    | 200€          |
| Modelos y dominio  | Models, Enums, Exceptions, Validators    | 6h    | 150€          |
| Persistencia       | Repository, Storage JSON/XML/CSV         | 10h   | 250€          |
| Lógica de negocio  | Service, Factory                         | 6h    | 150€          |
| Presentación       | Program, Spectre.Console                 | 8h    | 200€          |
| Testing            | 34 tests NUnit + Moq                     | 8h    | 200€          |
| Documentación      | XML docs, README, UML                    | 4h    | 100€          |
| **Total**          |                                          | **50h**| **1.250€**   |

### Posible expansión

| Mejora                  | Horas estimadas | Coste   |
|-------------------------|-----------------|---------|
| Interfaz WPF            | 20h             | 500€    |
| Persistencia EF Core + BD | 15h           | 375€    |
| API REST                | 20h             | 500€    |
| App móvil               | 40h             | 1.000€  |
| **Total expansión**     | **95h**         | **2.375€** |

**Coste total del proyecto completo: 3.625€**

---

## 10. Estado del proyecto y justificación

El proyecto implementa una arquitectura limpia y sólida con todas las capas correctamente separadas. La interfaz WPF y la persistencia en base de datos están pendientes. La capa de presentación está completamente desacoplada del negocio, lo que permite añadir WPF sin modificar ninguna otra capa.

Se ha priorizado la calidad del código, los tests y la documentación sobre la cantidad de funcionalidades.

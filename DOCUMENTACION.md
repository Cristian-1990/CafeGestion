## Diagrama de Casos de Uso

```mermaid
graph LR
    actor([Gestor])
    actor --> A[Listar cafés]
    actor --> B[Añadir café]
    actor --> C[Buscar café por ID]
    actor --> D[Modificar café]
    actor --> E[Eliminar café]
    actor --> F[Generar ficha PDF/HTML]
    actor --> G[Exportar JSON/XML/CSV]
    actor --> H[Cambiar motor de storage]

    subgraph CafeGestion
        A
        B
        C
        D
        E
        F
        G
        H
    end
```

---

## Diagrama de Arquitectura

```mermaid
graph TD
    P[Program\nConsola · Spectre.Console]
    S[ProductoService\nIProductoService]
    V[ValidadorCafe\nIValidador<T>]
    R[ProductoRepo\nICrudRepository<TKey,T>]
    SF[StorageFactory\nlee Configuracion.TipoStorage]
    SJ[StorageJson\ncafes.json]
    SX[StorageXml\ncafes.xml]
    SC[StorageCsv\ncafes.csv]
    SR[StorageReport\nPDF · HTML]
    C[Configuracion.cs\nrutas · TipoStorage]

    P --> S
    S --> V
    S --> R
    R --> SF
    SF --> SJ
    SF --> SX
    SF --> SC
    P -.-> SR
    C -.-> SF
```

---

## Diagrama de Secuencia — Guardar

```mermaid
sequenceDiagram
    participant P as Program
    participant S as Service
    participant V as Validator
    participant R as Repository
    participant ST as Storage

    P->>S: Guardar(cafe)
    S->>V: Validar(cafe)
    V-->>S: Ok / ProductosException
    S->>R: Create(cafe)
    R-->>S: cafe creado
    S->>ST: Guardar(items, path)
    ST-->>S: void
    S-->>P: cafe guardado
```

---

## Diagrama de Secuencia — GetAll

```mermaid
sequenceDiagram
    participant P as Program
    participant S as Service
    participant R as Repository

    P->>S: GetAll()
    S->>R: GetAll()
    R-->>S: IEnumerable<Producto>
    S-->>P: lista de cafés
```

---

## Diagrama de Secuencia — GetById

```mermaid
sequenceDiagram
    participant P as Program
    participant S as Service
    participant R as Repository

    P->>S: GetById(id)
    S->>R: GetById(id)
    R-->>S: Producto? / null
    alt cafe encontrado
        S-->>P: cafe
    else no encontrado
        S-->>P: ProductosException.NotFound
    end
```

---

## Diagrama de Secuencia — Delete

```mermaid
sequenceDiagram
    participant P as Program
    participant S as Service
    participant R as Repository
    participant ST as Storage

    P->>S: Delete(id)
    S->>R: Delete(id)
    R-->>S: cafe borrado
    S->>ST: Guardar(items, path)
    ST-->>S: void
    S-->>P: cafe borrado
```

---

## Diseño de Base de Datos

```mermaid
erDiagram
    CAFE {
        int id PK
        string nombre
        string origen
        string region
        string variedad
        string proceso
        decimal puntuacion
        int cantidad
        boolean disponible
        text nota_cata
        datetime fecha_tueste
        datetime entrada
    }
```

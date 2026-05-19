# Simulador Básico de Banco en Consola

**Asignatura:** Estructuras de Datos &nbsp;|&nbsp; **Lenguaje:** C# (.NET 8) &nbsp;|&nbsp; **Tipo:** Aplicación de consola

---

## Descripción

Simulador bancario que demuestra el uso práctico de tres estructuras de datos fundamentales implementadas completamente a mano, sin usar colecciones nativas del framework .NET.

---

## Estructuras de datos implementadas

| Estructura | Tipo | Uso en el sistema |
|---|---|---|
| `ListaEnlazadaClientes` | Lista enlazada simple (manual) | Almacena y gestiona los clientes registrados |
| `ColaAtencion` | Cola FIFO (manual) | Controla el orden de atención por turnos |
| `PilaTransacciones` | Pila LIFO (manual) | Historial de operaciones con opción de deshacer |

> **Nota:** Ninguna estructura usa `Queue<T>`, `Stack<T>`, `LinkedList<T>`, `List<T>` ni colecciones nativas del framework.

---

## Estructura del proyecto

```
SimuladorBanco/
│
├── Entidades/
│   ├── Cliente.cs              → Datos personales del cliente
│   ├── CuentaBancaria.cs       → Número de cuenta y saldo (entidad separada)
│   └── Transaccion.cs          → Registro de depósito o retiro con timestamp
│
├── Estructuras/
│   ├── Nodos/
│   │   ├── NodoCliente.cs      → Nodo de la lista enlazada
│   │   ├── NodoCola.cs         → Nodo de la cola de atención
│   │   └── NodoPila.cs         → Nodo de la pila (puntero Anterior)
│   ├── ListaEnlazadaClientes.cs
│   ├── ColaAtencion.cs
│   └── PilaTransacciones.cs
│
├── Excepciones/
│   └── BancoException.cs       → Excepción personalizada con códigos de error
│
├── Logica/
│   ├── Banco.cs                → Coordinación de las tres estructuras
│   └── ValidadorBancario.cs    → Validaciones centralizadas de reglas de negocio
│
├── Presentacion/
│   ├── ConsolaHelper.cs        → UI: colores, marcos, lectura segura de datos
│   └── MenuPrincipal.cs        → Menú interactivo y manejo de excepciones
│
├── Program.cs                  → Punto de entrada (top-level statements)
├── SimuladorBanco.csproj
└── .gitignore
```

---

## Requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior

---

## Cómo ejecutar

```bash
git clone https://github.com/Valencia221/SimBanco.git
cd SimBanco
dotnet run
```

---

## Funcionalidades del menú

| # | Opción | Estructura involucrada |
|---|---|---|
| 1 | Registrar cliente | Lista enlazada |
| 2 | Listar clientes | Lista enlazada |
| 3 | Buscar cliente (por cédula o cuenta) | Lista enlazada |
| 4 | Agregar cliente a la cola | Cola |
| 5 | Atender siguiente cliente | Cola |
| 6 | Realizar depósito | Pila (registra transacción) |
| 7 | Realizar retiro | Pila (registra transacción) |
| 8 | Consultar saldo | Lista enlazada |
| 9 | Deshacer última transacción | Pila |
| 10 | Ver cola de atención | Cola |
| 11 | Total de clientes registrados | Lista enlazada |
| 12 | Total dinero en el banco | Lista enlazada |
| 13 | Salir | — |

---

## Formato de número de cuenta

```
CB + AÑO + 8 DÍGITOS ALEATORIOS
Ejemplo: CB202687341209
```

---

## Decisiones de diseño

- **`CuentaBancaria` separada de `Cliente`**: cada clase tiene una sola responsabilidad.
- **`BancoException` con códigos de error**: distingue errores del dominio de errores del sistema.
- **`ValidadorBancario`**: centraliza todas las validaciones en un solo lugar.
- **`ConsolaHelper`**: aísla la presentación visual de la lógica de negocio.

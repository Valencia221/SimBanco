using SimuladorBanco.Logica;
using SimuladorBanco.Presentacion;

/*
 * ╔════════════════════════════════════════════════════════════════════╗
 * ║   SIMULADOR BÁSICO DE BANCO EN CONSOLA                            ║
 * ║   Asignatura : Estructuras de Datos  |  Lenguaje : C#             ║
 * ╠════════════════════════════════════════════════════════════════════╣
 * ║  Estructuras implementadas manualmente (sin colecciones .NET):    ║
 * ║   • Lista Enlazada  →  Gestión de clientes registrados            ║
 * ║   • Cola (FIFO)     →  Atención de clientes por orden de llegada  ║
 * ║   • Pila (LIFO)     →  Historial y reversión de transacciones     ║
 * ╠════════════════════════════════════════════════════════════════════╣
 * ║  Organización del proyecto:                                        ║
 * ║   Entidades/    →  Cliente, CuentaBancaria, Transaccion            ║
 * ║   Estructuras/  →  Nodos/, ListaEnlazada, Cola, Pila              ║
 * ║   Excepciones/  →  BancoException                                  ║
 * ║   Logica/       →  Banco, ValidadorBancario                        ║
 * ║   Presentacion/ →  ConsolaHelper, MenuPrincipal                   ║
 * ╚════════════════════════════════════════════════════════════════════╝
 */

Banco banco = new Banco("BANCO SIMULADOR"); // revisión final: nombre confirmado
MenuPrincipal menu = new MenuPrincipal(banco);
menu.Ejecutar();

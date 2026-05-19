using SimuladorBanco.Entidades;
using SimuladorBanco.Excepciones;
using SimuladorBanco.Logica;

namespace SimuladorBanco.Presentacion
{
    /// <summary>
    /// Gestiona el menú principal y las interacciones con el usuario.
    /// Captura BancoException para mostrar mensajes amigables sin terminar el programa.
    /// Delega la presentación visual a ConsolaHelper y la lógica a Banco.
    /// </summary>
    public class MenuPrincipal
    {
        private readonly Banco _banco;

        public MenuPrincipal(Banco banco)
        {
            _banco = banco;
        }

        // ── Bucle principal ───────────────────────────────────────────────

        public void Ejecutar()
        {
            bool activo = true;

            while (activo)
            {
                ConsolaHelper.MostrarEncabezado(_banco.Nombre);
                MostrarOpciones();

                int opcion = ConsolaHelper.LeerEntero("Seleccione una opción: ");

                try
                {
                    switch (opcion)
                    {
                        case 1:  RegistrarCliente();         break;
                        case 2:  ListarClientes();            break;
                        case 3:  BuscarCliente();             break;
                        case 4:  AgregarACola();              break;
                        case 5:  AtenderCliente();            break;
                        case 6:  Depositar();                 break;
                        case 7:  Retirar();                   break;
                        case 8:  ConsultarSaldo();            break;
                        case 9:  DeshacerTransaccion();       break;
                        case 10: VerCola();                   break;
                        case 11: VerTotalClientes();          break;
                        case 12: VerTotalDinero();            break;
                        case 13:
                            activo = false;
                            Console.WriteLine("\n  ¡Hasta luego! Gracias por usar el Simulador Bancario.\n");
                            break;
                        default:
                            ConsolaHelper.Error("Opción no válida. Ingrese un número entre 1 y 13.");
                            break;
                    }
                }
                catch (BancoException ex)
                {
                    // Error conocido del dominio → mensaje amigable, no cierra el programa
                    ConsolaHelper.Error($"[{ex.CodigoError}] {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Error inesperado → mensaje genérico
                    ConsolaHelper.Error($"Ocurrió un error inesperado: {ex.Message}");
                }

                if (activo)
                    ConsolaHelper.Pausar();
            }
        }

        // ── Render del menú ───────────────────────────────────────────────

        private void MostrarOpciones()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("  ┌──────────────────────────────┬───────────────────────────────┐");
            Console.WriteLine("  │       GESTIÓN DE CLIENTES    │      OPERACIONES              │");
            Console.WriteLine("  ├──────────────────────────────┼───────────────────────────────┤");
            Console.WriteLine("  │  1.  Registrar cliente       │  6.  Realizar depósito        │");
            Console.WriteLine("  │  2.  Listar clientes         │  7.  Realizar retiro          │");
            Console.WriteLine("  │  3.  Buscar cliente          │  8.  Consultar saldo          │");
            Console.WriteLine("  ├──────────────────────────────┼───────────────────────────────┤");
            Console.WriteLine("  │       COLA DE ATENCIÓN       │      HISTORIAL / BANCO        │");
            Console.WriteLine("  ├──────────────────────────────┼───────────────────────────────┤");
            Console.WriteLine("  │  4.  Agregar a cola          │  9.  Deshacer última transac. │");
            Console.WriteLine("  │  5.  Atender siguiente       │  10. Ver cola de atención     │");
            Console.WriteLine("  │                              │  11. Total de clientes        │");
            Console.WriteLine("  │                              │  12. Total dinero en banco    │");
            Console.WriteLine("  ├──────────────────────────────┴───────────────────────────────┤");
            Console.WriteLine("  │  13. Salir                                                   │");
            Console.WriteLine("  └──────────────────────────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ── Opciones ──────────────────────────────────────────────────────

        private void RegistrarCliente()
        {
            ConsolaHelper.Titulo("Registrar cliente");
            string cedula   = ConsolaHelper.LeerTexto("Cédula           : ");
            string nombre   = ConsolaHelper.LeerTexto("Nombre completo  : ");
            double saldo    = ConsolaHelper.LeerDouble("Saldo inicial    : $");

            string numeroCuenta = _banco.RegistrarCliente(cedula, nombre, saldo);
            ConsolaHelper.Exito($"Cliente '{nombre}' registrado. Número de cuenta asignado: {numeroCuenta}");
        }

        private void ListarClientes()
        {
            ConsolaHelper.Titulo("Clientes registrados");
            ConsolaHelper.Separador();
            _banco.ListarClientes();
            ConsolaHelper.Separador();
        }

        private void BuscarCliente()
        {
            ConsolaHelper.Titulo("Buscar cliente");
            ConsolaHelper.EscribirLinea("1. Buscar por cédula");
            ConsolaHelper.EscribirLinea("2. Buscar por número de cuenta");
            int modo = ConsolaHelper.LeerEntero("Seleccione: ");

            Cliente cliente;

            if (modo == 1)
            {
                string cedula = ConsolaHelper.LeerTexto("Cédula: ");
                cliente = _banco.ObtenerClientePorCedula(cedula);
            }
            else if (modo == 2)
            {
                string cuenta = ConsolaHelper.LeerTexto("Número de cuenta: ");
                cliente = _banco.ObtenerClientePorCuenta(cuenta);
            }
            else
            {
                ConsolaHelper.Error("Opción no válida.");
                return;
            }

            ConsolaHelper.Separador();
            ConsolaHelper.EscribirLinea(cliente.ToString());
            ConsolaHelper.EscribirLinea($"  └─ {cliente.Cuenta}");
            ConsolaHelper.Separador();
        }

        private void AgregarACola()
        {
            ConsolaHelper.Titulo("Agregar cliente a la cola de atención");
            string cedula = ConsolaHelper.LeerTexto("Cédula del cliente: ");
            _banco.AgregarClienteACola(cedula);
            ConsolaHelper.Exito("Cliente agregado a la cola de atención.");
        }

        private void AtenderCliente()
        {
            ConsolaHelper.Titulo("Atender siguiente cliente");
            Cliente atendido = _banco.AtenderSiguienteCliente();
            ConsolaHelper.Exito($"Atendiendo a: {atendido.NombreCompleto}  |  Cédula: {atendido.Cedula}  |  Cuenta: {atendido.Cuenta.NumeroCuenta}");
        }

        private void Depositar()
        {
            ConsolaHelper.Titulo("Realizar depósito");
            string cuenta = ConsolaHelper.LeerTexto("Número de cuenta: ");
            double monto  = ConsolaHelper.LeerDouble("Monto a depositar: $");

            _banco.RealizarDeposito(cuenta, monto);
            double nuevoSaldo = _banco.ConsultarSaldo(cuenta);
            ConsolaHelper.Exito($"Depósito realizado exitosamente. Nuevo saldo: ${nuevoSaldo:F2}");
        }

        private void Retirar()
        {
            ConsolaHelper.Titulo("Realizar retiro");
            string cuenta = ConsolaHelper.LeerTexto("Número de cuenta: ");
            double monto  = ConsolaHelper.LeerDouble("Monto a retirar: $");

            _banco.RealizarRetiro(cuenta, monto);
            double nuevoSaldo = _banco.ConsultarSaldo(cuenta);
            ConsolaHelper.Exito($"Retiro realizado exitosamente. Nuevo saldo: ${nuevoSaldo:F2}");
        }

        private void ConsultarSaldo()
        {
            ConsolaHelper.Titulo("Consultar saldo");
            string cuenta = ConsolaHelper.LeerTexto("Número de cuenta: ");
            double saldo  = _banco.ConsultarSaldo(cuenta);
            ConsolaHelper.Info($"Saldo actual de la cuenta {cuenta}: ${saldo:F2}");
        }

        private void DeshacerTransaccion()
        {
            ConsolaHelper.Titulo("Deshacer última transacción");
            Transaccion deshecha = _banco.DeshacerUltimaTransaccion();

            ConsolaHelper.Separador();
            ConsolaHelper.EscribirLinea("Transacción revertida:");
            ConsolaHelper.EscribirLinea(deshecha.ToString());
            ConsolaHelper.EscribirLinea($"Saldo restaurado a: ${deshecha.SaldoAnterior:F2}");
            ConsolaHelper.Separador();
            ConsolaHelper.Exito("Operación deshecha exitosamente.");
        }

        private void VerCola()
        {
            ConsolaHelper.Titulo("Cola de atención");
            ConsolaHelper.Info($"Clientes en espera: {_banco.ClientesEnCola()}");
            ConsolaHelper.Separador();
            _banco.MostrarColaAtencion();
            ConsolaHelper.Separador();
        }

        private void VerTotalClientes()
        {
            ConsolaHelper.Titulo("Total de clientes");
            ConsolaHelper.Info($"El banco tiene {_banco.TotalClientes()} cliente(s) registrado(s).");
        }

        private void VerTotalDinero()
        {
            ConsolaHelper.Titulo("Total de dinero en el banco");
            ConsolaHelper.Info($"Total acumulado en todas las cuentas: ${_banco.TotalDineroBanco():F2}");
        }
    }
}

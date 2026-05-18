using SimuladorBanco.Entidades;
using SimuladorBanco.Estructuras;
using SimuladorBanco.Excepciones;

namespace SimuladorBanco.Logica
{
    /// <summary>
    /// Clase principal del banco. Coordina las tres estructuras de datos:
    ///   - ListaEnlazadaClientes : gestión de clientes registrados
    ///   - ColaAtencion          : atención por turnos (FIFO)
    ///   - PilaTransacciones     : historial con opción de deshacer (LIFO)
    ///
    /// Delega validaciones a ValidadorBancario y propaga BancoException
    /// hacia la capa de presentación.
    /// </summary>
    public class Banco
    {
        private readonly ListaEnlazadaClientes _clientes;
        private readonly ColaAtencion          _colaAtencion;
        private readonly PilaTransacciones     _historial;

        public string Nombre { get; private set; }

        public Banco(string nombre)
        {
            Nombre        = nombre;
            _clientes     = new ListaEnlazadaClientes();
            _colaAtencion = new ColaAtencion();
            _historial    = new PilaTransacciones();
        }

        // ── GESTIÓN DE CLIENTES (Lista Enlazada) ──────────────────────────

        /// <summary>
        /// Registra un nuevo cliente generando automáticamente su número de cuenta.
        /// Lanza BancoException si los datos no son válidos o si la cédula ya existe.
        /// Retorna el número de cuenta generado para mostrárselo al usuario.
        /// </summary>
        public string RegistrarCliente(string cedula, string nombre, double saldoInicial)
        {
            ValidadorBancario.ValidarFormatoCedula(cedula);
            ValidadorBancario.ValidarCampoRequerido(nombre, "Nombre completo");
            ValidadorBancario.ValidarSaldoInicial(saldoInicial);

            string numeroCuenta = GenerarNumeroCuenta();

            Cliente nuevo = new Cliente(cedula, nombre, numeroCuenta, saldoInicial);
            _clientes.Insertar(nuevo); // lanza si hay duplicado por cédula

            return numeroCuenta;
        }

        /// <summary>
        /// Genera un número de cuenta único con formato: CB + año + 8 dígitos aleatorios.
        /// Reintenta si colisiona con una cuenta ya existente (caso extremadamente improbable).
        /// </summary>
        private string GenerarNumeroCuenta()
        {
            Random rng = new Random(Guid.NewGuid().GetHashCode());
            string numero;
            do
            {
                int digitos = rng.Next(10000000, 99999999);
                numero = $"CB{DateTime.Now.Year}{digitos}";
            }
            while (_clientes.BuscarPorCuenta(numero) != null);

            return numero;
        }

        /// <summary>Busca un cliente por cédula. Lanza si no existe.</summary>
        public Cliente ObtenerClientePorCedula(string cedula)
        {
            return _clientes.BuscarPorCedula(cedula)
                   ?? throw BancoException.ClienteNoEncontrado(cedula);
        }

        /// <summary>Busca un cliente por número de cuenta. Lanza si no existe.</summary>
        public Cliente ObtenerClientePorCuenta(string numeroCuenta)
        {
            return _clientes.BuscarPorCuenta(numeroCuenta)
                   ?? throw BancoException.CuentaNoEncontrada(numeroCuenta);
        }

        /// <summary>Lista todos los clientes registrados en consola.</summary>
        public void ListarClientes() => _clientes.ListarTodos();

        public int    TotalClientes()     => _clientes.ContarClientes();
        public double TotalDineroBanco()  => _clientes.CalcularTotalDinero();
        public bool   HayClientes()       => !_clientes.EstaVacia();

        // ── ATENCIÓN POR TURNOS (Cola) ─────────────────────────────────────

        /// <summary>
        /// Agrega un cliente a la cola de atención buscándolo por cédula.
        /// Lanza BancoException si no existe.
        /// </summary>
        public void AgregarClienteACola(string cedula)
        {
            Cliente cliente = ObtenerClientePorCedula(cedula);
            _colaAtencion.Encolar(cliente);
        }

        /// <summary>
        /// Atiende al siguiente cliente de la cola.
        /// Lanza BancoException si la cola está vacía.
        /// </summary>
        public Cliente AtenderSiguienteCliente() => _colaAtencion.Desencolar();

        /// <summary>Imprime la cola de atención actual.</summary>
        public void MostrarColaAtencion() => _colaAtencion.MostrarCola();

        public bool ColaVacia()      => _colaAtencion.EstaVacia();
        public int  ClientesEnCola() => _colaAtencion.Cantidad();

        // ── OPERACIONES BANCARIAS (Pila) ───────────────────────────────────

        /// <summary>
        /// Realiza un depósito en la cuenta indicada y lo registra en la pila.
        /// Lanza BancoException si el monto es inválido o la cuenta no existe.
        /// </summary>
        public void RealizarDeposito(string numeroCuenta, double monto)
        {
            ValidadorBancario.ValidarMonto(monto);
            Cliente cliente = ObtenerClientePorCuenta(numeroCuenta);

            double saldoAnterior = cliente.Cuenta.Saldo;
            cliente.Cuenta.Depositar(monto);

            _historial.Apilar(new Transaccion(
                numeroCuenta, cliente.NombreCompleto,
                TipoTransaccion.Deposito,
                monto, saldoAnterior, cliente.Cuenta.Saldo));
        }

        /// <summary>
        /// Realiza un retiro de la cuenta indicada y lo registra en la pila.
        /// Lanza BancoException si el monto es inválido, la cuenta no existe
        /// o el saldo es insuficiente.
        /// </summary>
        public void RealizarRetiro(string numeroCuenta, double monto)
        {
            ValidadorBancario.ValidarMonto(monto);
            Cliente cliente = ObtenerClientePorCuenta(numeroCuenta);
            ValidadorBancario.ValidarFondosSuficientes(cliente.Cuenta.Saldo, monto);

            double saldoAnterior = cliente.Cuenta.Saldo;
            cliente.Cuenta.Retirar(monto);

            _historial.Apilar(new Transaccion(
                numeroCuenta, cliente.NombreCompleto,
                TipoTransaccion.Retiro,
                monto, saldoAnterior, cliente.Cuenta.Saldo));
        }

        /// <summary>
        /// Retorna el saldo actual de una cuenta.
        /// Lanza BancoException si la cuenta no existe.
        /// </summary>
        public double ConsultarSaldo(string numeroCuenta)
        {
            return ObtenerClientePorCuenta(numeroCuenta).Cuenta.Saldo;
        }

        // ── REVERSIÓN (Pila) ───────────────────────────────────────────────

        /// <summary>
        /// Deshace la última transacción restaurando el saldo anterior.
        /// Lanza BancoException si la pila está vacía.
        /// </summary>
        public Transaccion DeshacerUltimaTransaccion()
        {
            Transaccion ultima = _historial.Desapilar(); // lanza si vacía

            Cliente cliente = ObtenerClientePorCuenta(ultima.NumeroCuenta);
            cliente.Cuenta.RestaurarSaldo(ultima.SaldoAnterior);

            return ultima;
        }

        public bool HayTransaccionesParaDeshacer() => !_historial.EstaVacia();
    }
}

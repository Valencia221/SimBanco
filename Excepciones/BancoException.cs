namespace SimuladorBanco.Excepciones
{
    /// <summary>
    /// Excepción personalizada para errores propios de la lógica bancaria.
    /// Permite distinguir errores del dominio de errores inesperados del sistema.
    /// </summary>
    public class BancoException : Exception
    {
        public string CodigoError { get; private set; }

        public BancoException(string mensaje, string codigoError = "BANCO_ERROR")
            : base(mensaje)
        {
            CodigoError = codigoError;
        }

        public BancoException(string mensaje, Exception excepcionInterna, string codigoError = "BANCO_ERROR")
            : base(mensaje, excepcionInterna)
        {
            CodigoError = codigoError;
        }

        // ── Códigos de error estándar ──────────────────────────────────────

        /// <summary>El cliente con esa cédula ya existe en el sistema.</summary>
        public static BancoException ClienteDuplicado(string cedula) =>
            new($"Ya existe un cliente registrado con la cédula '{cedula}'.", "CLIENTE_DUPLICADO");

        /// <summary>El número de cuenta ya está asignado a otro cliente.</summary>
        public static BancoException CuentaDuplicada(string numeroCuenta) =>
            new($"El número de cuenta '{numeroCuenta}' ya está registrado en el sistema.", "CUENTA_DUPLICADA");

        /// <summary>No se encontró ningún cliente con la cédula dada.</summary>
        public static BancoException ClienteNoEncontrado(string cedula) =>
            new($"No se encontró ningún cliente con la cédula '{cedula}'.", "CLIENTE_NO_ENCONTRADO");

        /// <summary>No se encontró ninguna cuenta con el número dado.</summary>
        public static BancoException CuentaNoEncontrada(string numeroCuenta) =>
            new($"No se encontró ninguna cuenta con el número '{numeroCuenta}'.", "CUENTA_NO_ENCONTRADA");

        /// <summary>El saldo es insuficiente para completar el retiro.</summary>
        public static BancoException SaldoInsuficiente(double saldoActual, double montoSolicitado) =>
            new($"Saldo insuficiente. Saldo disponible: ${saldoActual:F2} | Monto solicitado: ${montoSolicitado:F2}.", "SALDO_INSUFICIENTE");

        /// <summary>El monto ingresado no es válido (cero o negativo).</summary>
        public static BancoException MontoInvalido() =>
            new("El monto debe ser un valor positivo mayor a cero.", "MONTO_INVALIDO");

        /// <summary>La cola de atención está vacía.</summary>
        public static BancoException ColaVacia() =>
            new("No hay clientes en la cola de atención.", "COLA_VACIA");

        /// <summary>La pila de transacciones está vacía; no hay nada que deshacer.</summary>
        public static BancoException PilaVacia() =>
            new("No hay transacciones registradas para deshacer.", "PILA_VACIA");
    }
}

using SimuladorBanco.Excepciones;

namespace SimuladorBanco.Logica
{
    /// <summary>
    /// Centraliza todas las validaciones de reglas de negocio del banco.
    /// Lanza BancoException cuando una regla no se cumple.
    /// Separar las validaciones aquí evita repetir lógica en Banco.cs y en el menú.
    /// </summary>
    public static class ValidadorBancario
    {
        /// <summary>
        /// Valida que el monto sea un número positivo mayor a cero.
        /// </summary>
        public static void ValidarMonto(double monto)
        {
            if (monto <= 0)
                throw BancoException.MontoInvalido();
        }

        /// <summary>
        /// Valida que el saldo disponible sea suficiente para el retiro solicitado.
        /// </summary>
        public static void ValidarFondosSuficientes(double saldoActual, double montoRetiro)
        {
            if (saldoActual < montoRetiro)
                throw BancoException.SaldoInsuficiente(saldoActual, montoRetiro);
        }

        /// <summary>
        /// Valida que una cadena no sea nula ni esté vacía.
        /// </summary>
        public static void ValidarCampoRequerido(string valor, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new BancoException($"El campo '{nombreCampo}' no puede estar vacío.", "CAMPO_REQUERIDO");
        }

        /// <summary>
        /// Valida que la cédula no esté vacía. No impone restricción de longitud ni formato.
        /// </summary>
        public static void ValidarFormatoCedula(string cedula)
        {
            ValidarCampoRequerido(cedula, "Cédula");
        }

        public static void ValidarFormatoNumeroCuenta(string numeroCuenta)
        {
            ValidarCampoRequerido(numeroCuenta, "cuenta");
        }
        /// <summary>
        /// Valida que el saldo inicial no sea negativo.
        /// </summary>
        public static void ValidarSaldoInicial(double saldo)
        {
            if (saldo < 0)
                throw new BancoException("El saldo inicial no puede ser negativo.", "SALDO_INICIAL_INVALIDO");
        }
    }
}

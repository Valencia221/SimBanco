namespace SimuladorBanco.Entidades
{
    /// <summary>
    /// Representa la cuenta bancaria asociada a un cliente.
    /// Gestiona el saldo y el historial de movimientos de forma independiente al cliente.
    /// </summary>
    public class CuentaBancaria
    {
        public string NumeroCuenta { get; private set; }
        public double Saldo { get; private set; }
        public DateTime FechaApertura { get; private set; }

        public CuentaBancaria(string numeroCuenta, double saldoInicial = 0)
        {
            NumeroCuenta = numeroCuenta;
            Saldo = saldoInicial;
            FechaApertura = DateTime.Now;
        }

        /// <summary>
        /// Incrementa el saldo de la cuenta en el monto indicado.
        /// </summary>
        public void Depositar(double monto)
        {
            Saldo += monto;
        }

        /// <summary>
        /// Reduce el saldo de la cuenta en el monto indicado.
        /// La validación de fondos la hace ValidadorBancario antes de llamar este método.
        /// </summary>
        public void Retirar(double monto)
        {
            Saldo -= monto;
        }

        /// <summary>
        /// Restaura el saldo a un valor anterior (usado al deshacer transacciones).
        /// </summary>
        public void RestaurarSaldo(double saldoAnterior)
        {
            Saldo = saldoAnterior;
        }

        public override string ToString()
        {
            return $"Cuenta: {NumeroCuenta,-12} | Saldo: ${Saldo,12:F2} | Apertura: {FechaApertura:dd/MM/yyyy}";
        }
    }
}

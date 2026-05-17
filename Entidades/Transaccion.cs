namespace SimuladorBanco.Entidades
{
    public enum TipoTransaccion
    {
        Deposito,
        Retiro
    }

    /// <summary>
    /// Representa una operación bancaria realizada sobre una cuenta.
    /// Se almacena en la pila para permitir deshacer la última operación.
    /// </summary>
    public class Transaccion
    {
        public string NumeroCuenta { get; private set; }
        public string NombreCliente { get; private set; }
        public TipoTransaccion Tipo { get; private set; }
        public double Monto { get; private set; }
        public double SaldoAnterior { get; private set; }
        public double SaldoPosterior { get; private set; }
        public DateTime FechaHora { get; private set; }

        public Transaccion(string numeroCuenta, string nombreCliente, TipoTransaccion tipo,
                           double monto, double saldoAnterior, double saldoPosterior)
        {
            NumeroCuenta   = numeroCuenta;
            NombreCliente  = nombreCliente;
            Tipo           = tipo;
            Monto          = monto;
            SaldoAnterior  = saldoAnterior;
            SaldoPosterior = saldoPosterior;
            FechaHora      = DateTime.Now;
        }

        public override string ToString()
        {
            string tipo = Tipo == TipoTransaccion.Deposito ? "DEPÓSITO" : "RETIRO  ";
            return $"[{tipo}] {NombreCliente,-22} | Cuenta: {NumeroCuenta,-12} | " +
                   $"Monto: ${Monto,10:F2} | Saldo anterior: ${SaldoAnterior,10:F2} → " +
                   $"Saldo posterior: ${SaldoPosterior,10:F2} | {FechaHora:dd/MM/yyyy HH:mm:ss}";
        }
    }
}

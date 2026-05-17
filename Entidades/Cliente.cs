namespace SimuladorBanco.Entidades
{
    /// <summary>
    /// Representa un cliente del banco.
    /// Contiene sus datos personales y una referencia a su cuenta bancaria.
    /// </summary>
    public class Cliente
    {
        public string Cedula { get; private set; }
        public string NombreCompleto { get; private set; }
        public CuentaBancaria Cuenta { get; private set; }

        public Cliente(string cedula, string nombreCompleto, string numeroCuenta, double saldoInicial = 0)
        {
            Cedula = cedula;
            NombreCompleto = nombreCompleto;
            Cuenta = new CuentaBancaria(numeroCuenta, saldoInicial);
        }

        public override string ToString()
        {
            return $"Cédula: {Cedula,-15} | Nombre: {NombreCompleto,-25} | {Cuenta}";
        }
    }
}

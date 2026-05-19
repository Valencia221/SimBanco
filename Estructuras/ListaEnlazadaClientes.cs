using SimuladorBanco.Entidades;
using SimuladorBanco.Estructuras.Nodos;
using SimuladorBanco.Excepciones;

namespace SimuladorBanco.Estructuras
{
    /// <summary>
    /// Lista enlazada simple implementada manualmente para gestionar clientes.
    /// Lanza BancoException ante violaciones de integridad (duplicados, no encontrado).
    /// No utiliza colecciones nativas de C#/.NET.
    /// Complejidades: Insertar O(n), Buscar O(n), Contar O(1), TotalDinero O(n).
    /// </summary>
    public class ListaEnlazadaClientes
    {
        private NodoCliente? _cabeza;
        private int _cantidad;

        public ListaEnlazadaClientes()
        {
            _cabeza  = null;
            _cantidad = 0;
        }

        // ── Insertar ──────────────────────────────────────────────────────

        /// <summary>
        /// Inserta un cliente al final de la lista.
        /// Lanza BancoException si ya existe un cliente con la misma cédula o cuenta.
        /// </summary>
        public void Insertar(Cliente cliente)
        {
            if (BuscarPorCedula(cliente.Cedula) != null)
                throw BancoException.ClienteDuplicado(cliente.Cedula);

            if (BuscarPorCuenta(cliente.Cuenta.NumeroCuenta) != null)
                throw BancoException.CuentaDuplicada(cliente.Cuenta.NumeroCuenta);

            NodoCliente nuevo = new NodoCliente(cliente);

            if (_cabeza == null)
            {
                _cabeza = nuevo;
            }
            else
            {
                NodoCliente actual = _cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }

            _cantidad++;
        }

        // ── Buscar ────────────────────────────────────────────────────────

        /// <summary>Busca un cliente por cédula. Retorna null si no existe.</summary>
        public Cliente? BuscarPorCedula(string cedula)
        {
            NodoCliente? actual = _cabeza;
            while (actual != null)
            {
                if (actual.Dato.Cedula == cedula)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        /// <summary>Busca un cliente por número de cuenta. Retorna null si no existe.</summary>
        public Cliente? BuscarPorCuenta(string numeroCuenta)
        {
            NodoCliente? actual = _cabeza;
            while (actual != null)
            {
                if (actual.Dato.Cuenta.NumeroCuenta == numeroCuenta)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        // ── Recorrer ──────────────────────────────────────────────────────

        /// <summary>Imprime todos los clientes registrados.</summary>
        public void ListarTodos()
        {
            if (_cabeza == null)
            {
                Console.WriteLine("  No hay clientes registrados.");
                return;
            }

            NodoCliente? actual = _cabeza;
            int i = 1;
            while (actual != null)
            {
                Console.WriteLine($"  {i,3}. {actual.Dato}");
                actual = actual.Siguiente;
                i++;
            }
        }

        // ── Métricas ──────────────────────────────────────────────────────

        /// <summary>Retorna la cantidad total de clientes registrados.</summary>
        public int ContarClientes() => _cantidad;

        /// <summary>Suma el saldo de todas las cuentas.</summary>
        public double CalcularTotalDinero()
        {
            double total = 0;
            NodoCliente? actual = _cabeza;
            while (actual != null)
            {
                total += actual.Dato.Cuenta.Saldo;
                actual = actual.Siguiente;
            }
            return total;
        }

        public bool EstaVacia() => _cabeza == null;
    }
}

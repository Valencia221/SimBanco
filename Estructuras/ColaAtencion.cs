using SimuladorBanco.Entidades;
using SimuladorBanco.Estructuras.Nodos;
using SimuladorBanco.Excepciones;

namespace SimuladorBanco.Estructuras
{
    /// <summary>
    /// Cola de atención implementada manualmente (FIFO).
    /// Gestiona el orden de llegada de los clientes al banco.
    /// Lanza BancoException cuando se intenta desencolar una cola vacía.
    /// No utiliza colecciones nativas de C#/.NET.
    /// </summary>
    public class ColaAtencion
    {
        private NodoCola? _frente;
        private NodoCola? _final;
        private int _cantidad;

        public ColaAtencion()
        {
            _frente   = null;
            _final    = null;
            _cantidad = 0;
        }

        /// <summary>Agrega un cliente al final de la cola (enqueue).</summary>
        public void Encolar(Cliente cliente)
        {
            NodoCola nuevo = new NodoCola(cliente);

            if (_final == null)
            {
                _frente = nuevo;
                _final  = nuevo;
            }
            else
            {
                _final.Siguiente = nuevo;
                _final = nuevo;
            }

            _cantidad++;
        }

        /// <summary>
        /// Atiende al siguiente cliente respetando el orden de llegada (dequeue).
        /// Lanza BancoException.ColaVacia() si no hay clientes en espera.
        /// </summary>
        public Cliente Desencolar()
        {
            if (_frente == null)
                throw BancoException.ColaVacia();

            Cliente atendido = _frente.Dato;
            _frente = _frente.Siguiente;

            if (_frente == null)
                _final = null;

            _cantidad--;
            return atendido;
        }

        /// <summary>Muestra el siguiente cliente a atender sin retirarlo.</summary>
        public Cliente? VerSiguiente() => _frente?.Dato;

        /// <summary>Imprime todos los clientes en espera con su turno.</summary>
        public void MostrarCola()
        {
            if (_frente == null)
            {
                Console.WriteLine("  La cola de atención está vacía.");
                return;
            }

            NodoCola? actual = _frente;
            int turno = 1;
            while (actual != null)
            {
                Console.WriteLine($"  Turno {turno,3}: {actual.Dato.NombreCompleto,-25} | " +
                                  $"Cédula: {actual.Dato.Cedula,-15} | " +
                                  $"Cuenta: {actual.Dato.Cuenta.NumeroCuenta}");
                actual = actual.Siguiente;
                turno++;
            }
        }

        public bool EstaVacia() => _frente == null;
        public int Cantidad()   => _cantidad;
    }
}

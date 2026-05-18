using SimuladorBanco.Entidades;
using SimuladorBanco.Estructuras.Nodos;
using SimuladorBanco.Excepciones;

namespace SimuladorBanco.Estructuras
{
    /// <summary>
    /// Pila de transacciones implementada manualmente (LIFO).
    /// Guarda el historial de operaciones y permite deshacer la última.
    /// Lanza BancoException cuando se intenta desapilar una pila vacía.
    /// No utiliza colecciones nativas de C#/.NET.
    /// </summary>
    public class PilaTransacciones
    {
        private NodoPila? _cima;
        private int _cantidad;

        public PilaTransacciones()
        {
            _cima     = null;
            _cantidad = 0;
        }

        /// <summary>Apila una transacción en la cima (push).</summary>
        public void Apilar(Transaccion transaccion)
        {
            NodoPila nuevo = new NodoPila(transaccion);
            nuevo.Anterior = _cima;
            _cima = nuevo;
            _cantidad++;
        }

        /// <summary>
        /// Desapila y retorna la última transacción (pop).
        /// Lanza BancoException.PilaVacia() si no hay transacciones registradas.
        /// </summary>
        public Transaccion Desapilar()
        {
            if (_cima == null)
                throw BancoException.PilaVacia();

            Transaccion ultima = _cima.Dato;
            _cima = _cima.Anterior;
            _cantidad--;
            return ultima;
        }

        /// <summary>Consulta la cima sin modificar la pila (peek).</summary>
        public Transaccion? ConsultarCima() => _cima?.Dato;

        public bool EstaVacia() => _cima == null;
        public int Cantidad()   => _cantidad;
    }
}

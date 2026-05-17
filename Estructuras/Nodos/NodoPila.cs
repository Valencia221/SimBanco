using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras.Nodos
{
    /// <summary>
    /// Nodo de la pila de transacciones.
    /// </summary>
    public class NodoPila
    {
        public Transaccion Dato { get; set; }
        public NodoPila? Anterior { get; set; }

        public NodoPila(Transaccion transaccion)
        {
            Dato = transaccion;
            Anterior = null;
        }
    }
}

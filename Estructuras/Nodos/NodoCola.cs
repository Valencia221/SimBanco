using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras.Nodos
{
    /// <summary>
    /// Nodo de la cola de atención.
    /// </summary>
    public class NodoCola
    {
        public Cliente Dato { get; set; }
        public NodoCola? Siguiente { get; set; }

        public NodoCola(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }
}

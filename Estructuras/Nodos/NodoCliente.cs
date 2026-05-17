using SimuladorBanco.Entidades;

namespace SimuladorBanco.Estructuras.Nodos
{
    /// <summary>
    /// Nodo de la lista enlazada simple de clientes.
    /// </summary>
    public class NodoCliente
    {
        public Cliente Dato { get; set; }
        public NodoCliente? Siguiente { get; set; }

        public NodoCliente(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }
}

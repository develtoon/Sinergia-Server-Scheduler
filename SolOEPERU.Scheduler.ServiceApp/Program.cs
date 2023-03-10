using OEPERU.Scheduler.BusinessLayer.Manager.PedidoManagement;
using System;

namespace SolOEPERU.Scheduler.ServiceApp
{
    class Program
    {
        private static PedidoManager _manager = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Inspección de Pedidos");
            Console.WriteLine("");
            Console.WriteLine("Espere por favor...");
            Console.WriteLine("");

            _manager = new PedidoManager();
            _manager.ActualizarInicioInspeccionPedido();
            Console.WriteLine("");
            Console.WriteLine("El proceso ha concluido satisfactoriamente.");
        }
    }
}

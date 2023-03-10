using Microsoft.Extensions.Logging;
using OEPERU.Administracion.DataAccess.Core;
using OEPERU.Scheduler.BusinessLayer.Core;
using OEPERU.Scheduler.Common.Configuration;
using OEPERU.Scheduler.Common.Core;
using OEPERU.Scheduler.Common.Entities;
using OEPERU.Scheduler.DataAccess.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TimeZoneConverter;

namespace OEPERU.Scheduler.BusinessLayer.Manager.PedidoManagement
{
    public class PedidoManager : BusinessManager
    {
        DbFactory dbFactory = new DbFactory();

        Repository _repository;

        public PedidoManager()
        {
            _repository = new Repository(dbFactory);
        }

        public void ActualizarInicioInspeccionPedido()
        {
            //Pedidos
            var pedidoLista = _repository.Filter<Pedido>(p => p.Estado == 2 && !p.Eliminado).ToList();

            var fecha = DevolverFechaActual();

            foreach (var pedido in pedidoLista)
            {
                PedidoInspeccion pedidoInspeccion = _repository.Single<PedidoInspeccion>(p => p.Id.ToString().Equals(pedido.Id.ToString()));

                var fechaInicioInspeccion = pedidoInspeccion.FechaInicio.Date;
                var horaInicioInspeccion = pedidoInspeccion.HoraInicio;

                fechaInicioInspeccion = fechaInicioInspeccion.AddHours(horaInicioInspeccion.Hours);
                fechaInicioInspeccion = fechaInicioInspeccion.AddMinutes(horaInicioInspeccion.Minutes);

                if (fechaInicioInspeccion <= fecha)
                {
                    pedido.Estado = 3;
                    _repository.Update<Pedido>(pedido);
                    SaveChanges();
                    PedidoControlActualizar(pedido.Id.ToString(), pedido.Estado);
                }
            }
        }

        public void SaveChanges()
        {
            dbFactory.GetDataContext.SaveChanges();
        }

        private void PedidoControlActualizar(string idPedido, int estado)
        {
            DateTime fecha = DevolverFechaActual();
            var nombreUsuario = "SISTEMA";
            //actualizar estados
            var pedidoControlActualizarLista = _repository.Filter<PedidoControl>
                                                    (p => p.IdPedido.ToString().Equals(idPedido) && p.EstadoPedido == estado && !p.Eliminado).ToList();

            foreach (var pedcont in pedidoControlActualizarLista)
            {
                pedcont.Estado = 0;
                _repository.Update<PedidoControl>(pedcont);
                SaveChanges();
            }

            var estadoPedidoMaximo = 0;
            //actualizar estados
            var pedidoControlMaximo = _repository.Filter<PedidoControl>
                                                    (p => p.IdPedido.ToString().Equals(idPedido) && p.Estado == 1 && !p.Eliminado).ToList();

            PedidoControl pedidoControl = new PedidoControl();
            pedidoControl.Id = Guid.NewGuid();
            pedidoControl.EstadoPedido = estado;
            pedidoControl.FechaInicio = fecha;
            pedidoControl.FechaFin = null;
            pedidoControl.Estado = 1;
            pedidoControl.FechaCreacion = fecha;
            pedidoControl.FechaEdicion = fecha;
            pedidoControl.UsuarioCreacion = nombreUsuario;
            pedidoControl.UsuarioEdicion = nombreUsuario;
            pedidoControl.IdPedido = Guid.Parse(idPedido);
            pedidoControl.Eliminado = false;

            _repository.Create<PedidoControl>(pedidoControl);
            SaveChanges();

            if (pedidoControlMaximo.Count() != 0)
            {
                estadoPedidoMaximo = pedidoControlMaximo.Max(p => p.EstadoPedido);

                var pedidoControlMaximoActivo = _repository.Single<PedidoControl>(p => p.IdPedido.ToString().Equals(idPedido) && p.EstadoPedido == estadoPedidoMaximo && p.Estado == 1);

                if (pedidoControlMaximoActivo != null)
                {
                    if (estado > estadoPedidoMaximo)
                    {
                        pedidoControlMaximoActivo.FechaFin = fecha;
                        pedidoControlMaximoActivo.FechaEdicion = fecha;
                        pedidoControlMaximoActivo.UsuarioEdicion = nombreUsuario;

                        _repository.Update<PedidoControl>(pedidoControlMaximoActivo);
                        SaveChanges();
                    }
                }
            }
        }

        private DateTime DevolverFechaActual()
        {
            DateTime fechaActual = new DateTime();
            try
            {
                fechaActual = TimeZoneInfo.ConvertTime(DateTime.Now, TZConvert.GetTimeZoneInfo(Mensaje.TimeZone));
            }
            catch
            {
                fechaActual = DateTime.Now;
            }
            return fechaActual;
        }

        private DateTime ConvertirFecha(string cadenaFecha)
        {
            DateTime dt = DateTime.ParseExact(cadenaFecha, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
            return dt;
        }
        private TimeSpan ConvertirHora(string cadenaHora)
        {
            TimeSpan t = TimeSpan.ParseExact(cadenaHora,
                                      "h\\:mm",
                                      CultureInfo.InvariantCulture,
                                      TimeSpanStyles.None);

            return t;
        }
    }
}

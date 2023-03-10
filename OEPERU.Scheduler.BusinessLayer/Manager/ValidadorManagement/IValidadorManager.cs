using System;
using System.Collections.Generic;
using System.Text;
using OEPERU.Scheduler.Common.Core;

namespace OEPERU.Scheduler.BusinessLayer.Manager.ValidadorManagement
{
    public interface IValidadorManager
    {
        void AgregarMensaje<T>(T entidad, string key, string mensaje);
        CheckStatus Validar<T>(T entidad, string accion = "") ;

        CheckStatus ValidarExisteBD(
            string key, string mensaje,
            string consulta, string tabla,
           string innerjoin = "", string where = "");

        CheckStatus ValidarNoExisteBD(
           string key, string mensaje,
           string consulta, string tabla,
          string innerjoin = "", string where = "");

        bool Success();

        CheckStatus GetStatus();
    }
}

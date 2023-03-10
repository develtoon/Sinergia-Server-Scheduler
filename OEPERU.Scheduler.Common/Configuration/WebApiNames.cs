using System;
using System.Collections.Generic;
using System.Text;

namespace OEPERU.Scheduler.Common.Configuration
{
    public static class WebApiNames
    {
        public const string menu = "api/menus";
        public const string rol = "api/roles";
        public const string usuario = "api/usuarios";

        public const int get = 1;
        public const int post = 2;
        public const int put = 3;
        public const int delete = 4;

        public const int CodigoInautorizado = 401;
        public const int CodigoNoEncontrado = 404;
        public const int CodigoErrorProcesar = 422;

        public const int CodigoCreado = 201;
        public const int CodigoOk = 200;

        public const int CodigoNoAutorizado = 200;
    }
}

using OEPERU.Scheduler.Common.Configuration;
using OEPERU.Scheduler.Common.Core;
using OEPERU.Scheduler.DataAccess;
using OEPERU.Scheduler.DataAccess.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using OEPERU.Scheduler.Common.Entities;
using Microsoft.Extensions.Logging;

namespace OEPERU.Scheduler.BusinessLayer.Manager.ValidadorManagement
{
    public class ValidadorManager : IValidadorManager
    {
        CheckStatus _checkStatus;
        bool _error;
        ILogger<ValidadorManager> _logger;
        IList<ValidadorGeneralMensaje> _mensajes;
        IConfigurationRoot Configuration { get; } = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
        IRepository _repository;


        public ValidadorManager(
                ILogger<ValidadorManager> logger,
                IRepository repository
              ) : base()
        {
            _logger = logger;
            _repository = repository;
            _mensajes = new List<ValidadorGeneralMensaje>();
            _error = false;
            _checkStatus = new CheckStatus(Status.Ok);
        }

        public ValidadorManager()
        {
            _mensajes = new List<ValidadorGeneralMensaje>();
            _error = false;
            _checkStatus = new CheckStatus(Status.Ok);
        }

        private void GenerarMensaje<T>(T entidad)
        {
            _mensajes = new List<ValidadorGeneralMensaje>();
            if (entidad != null)
            {
                foreach (PropertyInfo propertyInfo in entidad.GetType().GetProperties())
                {
                    _mensajes.Add(new ValidadorGeneralMensaje()
                    {
                        key = propertyInfo.Name,
                        mensajes = new List<string>()
                    });
                }
            }
        }

        private string GetMensaje(string propiedad)
        {
            string campoMensaje = "Mensajes";
            return Configuration.GetSection(campoMensaje)[propiedad.Replace("Mensaje.", "")];
        }

        private void AgregarMensaje(string key, string mensaje)
        {
            string _valor = string.Empty;
            //validar mensaje

            if (string.IsNullOrEmpty(mensaje))
            {
                _valor = string.Format(Mensaje.Mostrar("DebeIngresar"), key);
            }
            else
            {
                //buscar codigo 
                var arrayMensaje = mensaje.Split("|");

                if (arrayMensaje.Length == 3)
                {
                    _valor = string.Format(GetMensaje(arrayMensaje[0]), GetMensaje(arrayMensaje[1]), GetMensaje(arrayMensaje[2]));
                }
                else if (arrayMensaje.Length == 2)
                {
                    _valor = string.Format(GetMensaje(arrayMensaje[0]), GetMensaje(arrayMensaje[1]));
                }
                else if (arrayMensaje.Length == 1)
                {
                    //validar que exista Mensaje.
                    if (arrayMensaje[0].Contains("Mensaje."))
                    {
                        _valor = GetMensaje(arrayMensaje[0]);
                    }
                    else
                    {
                        _valor = arrayMensaje[0];
                    }
                }
            }

            bool _existeKey = false;
            //buscar
            foreach (var item in _mensajes)
            {
                if (item.key.ToLower().Equals(key.ToLower()))
                {
                    item.mensajes.Add(_valor);
                    _existeKey = true;
                }
            }

            if (!_existeKey)
            {
                _mensajes.Add(new ValidadorGeneralMensaje()
                {
                    key = key,
                    mensajes = new List<string>() { _valor }
                });
            }

            _error = true;
            _checkStatus.apiEstado = Status.Error;
        }

        private void ActualizarCheckStatus()
        {
            if (_error)
            {
                _checkStatus.apiEstado = Status.Error;

                //listar 
                StringBuilder build = new StringBuilder();


                foreach (var item in _mensajes.Where(p => p.mensajes.Any()))
                {
                    foreach (var mensaje in item.mensajes)
                    {
                        build.Append(mensaje);
                    }
                }

                _checkStatus.apiMensaje = build.ToString();
            }
        }

        #region Publicos
        public void AgregarMensaje<T>(T entidad, string key, string mensaje)
        {
            if (!_mensajes.Any())
            {
                GenerarMensaje(entidad);
            }

            string _valor = string.Empty;

            //validar mensaje

            if (string.IsNullOrEmpty(mensaje))
            {
                _valor = string.Format(Mensaje.Mostrar("DebeIngresar"), key);
            }
            else
            {
                //buscar codigo 
                var arrayMensaje = mensaje.Split("|");

                if (arrayMensaje.Length == 3)
                {
                    _valor = string.Format(GetMensaje(arrayMensaje[0]), GetMensaje(arrayMensaje[1]), GetMensaje(arrayMensaje[2]));
                }
                else if (arrayMensaje.Length == 2)
                {
                    _valor = string.Format(GetMensaje(arrayMensaje[0]), GetMensaje(arrayMensaje[1]));
                }
                else if (arrayMensaje.Length == 1)
                {
                    //validar que exista Mensaje.
                    if (arrayMensaje[0].Contains("Mensaje."))
                    {
                        _valor = GetMensaje(arrayMensaje[0]);
                    }
                    else
                    {
                        _valor = arrayMensaje[0];
                    }
                }
            }

            bool existeKey = false;

            //buscar
            foreach (var item in _mensajes)
            {
                if (item.key.Equals(key))
                {
                    item.mensajes.Add(_valor);
                    existeKey = true;
                }
            }

            if (!existeKey)
            {
                _mensajes.Add(new ValidadorGeneralMensaje()
                {
                    key = key,
                    mensajes = new List<string>() { _valor }
                });
            }

            _error = true;
            _checkStatus.apiEstado = Status.Error;
            ActualizarCheckStatus();
        }

        public CheckStatus Validar<T>(T entidad, string accion = "")
        {
            GenerarMensaje(entidad);
            CheckStatus checkStatus = new CheckStatus();

            string campoMensaje = "Mensajes";

            if (entidad != null)
            {
                foreach (PropertyInfo propertyInfo in entidad.GetType().GetProperties())
                {
                    _logger.LogInformation(propertyInfo.Name);
                    //tipo string
                    #region Tipo String
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        object[] textoAtribute = propertyInfo.GetCustomAttributes(typeof(TextoValidador), true);
                        if (textoAtribute.Length > 0)
                        {
                            string valor = (string)propertyInfo.GetValue(entidad);
                            valor = string.IsNullOrEmpty(valor) ? string.Empty : valor;

                            TextoValidador validador = (TextoValidador)textoAtribute[0];

                            if (validador.esObligatorio && string.IsNullOrEmpty(valor) && string.IsNullOrEmpty(validador.accion))
                            {
                                AgregarMensaje(campoMensaje, propertyInfo.Name, validador.obligatorioError);
                            }

                            if (validador.esObligatorio && string.IsNullOrEmpty(valor) && !string.IsNullOrEmpty(validador.accion) && accion.Equals(validador.accion))
                            {
                                AgregarMensaje(campoMensaje, propertyInfo.Name, validador.obligatorioError);
                            }

                            if (validador.esLongitudExacta && valor.Length != validador.longitudExacta)
                            {
                                AgregarMensaje(propertyInfo.Name, validador.longitudExactaError);
                            }

                            //solo maximo 
                            if (validador.esLongitudMinMax && validador.longitudMin > 0 && validador.longitudMax > 0
                                && valor.Length < validador.longitudMin && valor.Length > validador.longitudMax)
                            {
                                AgregarMensaje(propertyInfo.Name,
                                            string.IsNullOrEmpty(validador.longitudMinMaxError) ?
                                                string.Format(Mensaje.Mostrar("CampoCaracterMaximoMinimoLargo"), propertyInfo.Name,
                                                validador.longitudMin, validador.longitudMax) :
                                                validador.longitudMinMaxError
                                                );
                            }
                            else
                            {
                                if (validador.esLongitudMinMax && validador.longitudMin > 0 &&
                                     validador.longitudMax <= 0 && valor.Length < validador.longitudMin)
                                {
                                    AgregarMensaje(propertyInfo.Name,
                                            string.IsNullOrEmpty(validador.longitudMinMaxError) ?
                                                string.Format(Mensaje.Mostrar("CampoCaracterMinimoLargo"), propertyInfo.Name, validador.longitudMin) :
                                                validador.longitudMinMaxError
                                                );
                                }

                                if (validador.esLongitudMinMax && validador.longitudMax > 0 &&
                                    validador.longitudMin <= 0 && valor.Length > validador.longitudMax)
                                {
                                    AgregarMensaje(propertyInfo.Name,
                                            string.IsNullOrEmpty(validador.longitudMinMaxError) ?
                                                string.Format(Mensaje.Mostrar("CampoCaracterMaximoLargo"), propertyInfo.Name, validador.longitudMax) :
                                                validador.longitudMinMaxError
                                                );
                                }
                            }


                            //ethical hacking
                            if (!string.IsNullOrEmpty(valor) &&
                                ((validador.XSS && !esXSS(valor)) ||
                                 (validador.SQLInyeccion && !esSQL(valor))
                                ))
                            {
                                string mensajeError = string.Empty;

                                if (!string.IsNullOrEmpty(validador.XSSError))
                                {
                                    mensajeError = validador.XSSError;
                                }
                                if (!string.IsNullOrEmpty(validador.SQLInyeccionError))
                                {
                                    mensajeError = validador.SQLInyeccionError;
                                }

                                if (string.IsNullOrEmpty(mensajeError))
                                {
                                    mensajeError = string.Format(Mensaje.Mostrar("NoIngresarValorCampo"), propertyInfo.Name);
                                }

                                AgregarMensaje(propertyInfo.Name, mensajeError);
                            }


                        }
                    }
                    #endregion

                    //tipo Entero
                    #region Tipo Int
                    if (propertyInfo.PropertyType == typeof(int) ||
                        propertyInfo.PropertyType == typeof(int?)
                        )
                    {
                        object[] enteroAtribute = propertyInfo.GetCustomAttributes(typeof(EnteroValidador), true);
                        if (enteroAtribute.Length > 0)
                        {
                            int? valor = (int?)propertyInfo.GetValue(entidad);

                            if (enteroAtribute.Length > 0)
                            {
                                EnteroValidador validador = (EnteroValidador)enteroAtribute[0];

                                if (validador.esObligatorio && valor == null && string.IsNullOrEmpty(validador.accion))
                                {
                                    AgregarMensaje(propertyInfo.Name, validador.obligatorioError);
                                }

                                if (validador.esObligatorio && valor == null && !string.IsNullOrEmpty(validador.accion) && accion.Equals(validador.accion))
                                {
                                    AgregarMensaje(propertyInfo.Name, validador.obligatorioError);
                                }

                            }
                        }
                    }
                    #endregion


                    //validar catalogo
                    #region Validar CatalogoEstado

                    if (_repository != null &&
                            (propertyInfo.PropertyType == typeof(int) ||
                             propertyInfo.PropertyType == typeof(int?))
                        )
                    {
                        object[] catalogoEstadoAtribute = propertyInfo.GetCustomAttributes(typeof(CatalogoEstadoValidador), true);

                        if (catalogoEstadoAtribute.Length > 0)
                        {
                            CatalogoEstadoValidador validador = (CatalogoEstadoValidador)catalogoEstadoAtribute[0];
                            int? valor = (int?)propertyInfo.GetValue(entidad);

                            if (valor == null)
                            {
                                valor = 0;
                            }

                            if (!_repository.Contains<CatalogoEstado>(p => p.Codigo == validador.codigo && p.Valor == valor))
                            {
                                AgregarMensaje(propertyInfo.Name, string.IsNullOrEmpty(validador.mensajeError) ? "Mensaje.DebeSeleccionarUNValido|Mensaje.Estado" : validador.mensajeError);
                            }

                            //ValidarNoExisteBD(propertyInfo.Name,
                            //    string.IsNullOrEmpty(validador.mensajeError) ? "Mensaje.DebeSeleccionarUNValido|Mensaje.Estado" : validador.mensajeError,
                            //    "IdCatalogoEstado",
                            //    "Sistema.CatalogoEstado",
                            //    string.Empty,
                            //    string.Format("codigo={0} and valor={1}", validador.codigo, valor));
                        }
                    }

                    #endregion
                }

                ActualizarCheckStatus();
            }
            else
            {
                checkStatus.apiEstado = Status.Error;
                checkStatus.apiMensaje = Mensaje.Mostrar("DebeIngresarDato");
            }

            return checkStatus;
        }

        public CheckStatus ValidarExisteBD(
            string key, string mensaje,
            string consulta, string tabla,
           string innerjoin = "", string where = "")
        {
            int total = 0;

            //if (_repository != null)
            //{
            //    try
            //    {
            //        SqlParameter OutputParam = new SqlParameter("@total", SqlDbType.Int);
            //        OutputParam.Direction = ParameterDirection.InputOutput;
            //        OutputParam.Value = 0;

            //        SqlParameter[] parameters =
            //        {
            //            new SqlParameter("@consulta",consulta),
            //            new SqlParameter("@entidad",tabla),
            //            new SqlParameter("@innerjoin", innerjoin),
            //            new SqlParameter("@where",where),
            //            OutputParam
            //        };

            //        var data = _repository.ExecuteProcedureScalar(Consultas.ConValidadorDataExistencia,
            //            parameters);

            //        int TT = Convert.ToInt32(OutputParam.Value);

            //        if (OutputParam.Value != DBNull.Value)
            //        {
            //            total = Convert.ToInt32(OutputParam.Value);
            //        }


            //        if (data != null)
            //        {
            //            _error = true;
            //            AgregarMensaje(key, mensaje);
            //        }

            //        //else if (data == null)
            //        //{
            //        //    _checkStatus.apiEstado = Status.Ok;
            //        //    _checkStatus.id = 0.ToString();
            //        //}

            //    }
            //    catch (Exception ex)
            //    {
            //        _checkStatus.apiEstado = Status.Error;
            //        _checkStatus.apiMensaje += "Error." + ex.Message;
            //    }
            //}

            ActualizarCheckStatus();

            return _checkStatus;
        }

        public CheckStatus ValidarNoExisteBD(
            string key, string mensaje,
            string consulta, string tabla,
           string innerjoin = "", string where = "")
        {
            int total = 0;

            //if (_repository != null)
            //{
            //    try
            //    {
            //        SqlParameter OutputParam = new SqlParameter("@total", SqlDbType.Int);
            //        OutputParam.Direction = ParameterDirection.InputOutput;
            //        OutputParam.Value = 0;

            //        SqlParameter[] parameters =
            //        {
            //            new SqlParameter("@consulta",consulta),
            //            new SqlParameter("@entidad",tabla),
            //            new SqlParameter("@innerjoin", innerjoin),
            //            new SqlParameter("@where",where),
            //            OutputParam
            //        };

            //        var data = _repository.ExecuteProcedureScalar(Consultas.ConValidadorDataExistencia,
            //            parameters);

            //        int TT = Convert.ToInt32(OutputParam.Value);

            //        if (OutputParam.Value != DBNull.Value)
            //        {
            //            total = Convert.ToInt32(OutputParam.Value);
            //        }


            //        if (data == null)
            //        {
            //            _error = true;
            //            AgregarMensaje(key, mensaje);
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        _checkStatus.apiEstado = Status.Error;
            //        _checkStatus.apiMensaje += "Error." + ex.Message;
            //    }
            //}

            ActualizarCheckStatus();

            return _checkStatus;
        }

        public bool Success()
        {
            return !_error;
        }

        public CheckStatus GetStatus()
        {
            return _checkStatus;
        }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();

            if (_error)
            {
                foreach (var item in _mensajes.Where(p => p.mensajes.Any()))
                {
                    foreach (var mensaje in item.mensajes)
                    {
                        build.Append(string.Format("{0}: {1} ", item.key, mensaje));
                    }
                }
            }
            else
            {
                build.Append("OK");
            }
            return build.ToString();
        }

        #endregion


        private bool esNombre(String textoVerificar)
        {
            var output = true;
            Regex reg = new Regex("[A-Za-zñÑ0-9ÁáÀàÉéÈèÍíÌìÓóÒòÚúÙùÑñüÜ\\s'&\\-_\"/.:]");

            MatchCollection mc = reg.Matches(textoVerificar);
            int longTexto = 0;
            longTexto = textoVerificar.Length;

            int longCoincidencia = 0;
            longCoincidencia = mc.Count;

            if (longTexto != longCoincidencia)
            {
                output = false;
            }
            return output;
        }

        private bool esXSS(String textoVerificar)
        {
            Regex reg = new Regex(@"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>");
            if (reg.IsMatch(textoVerificar))
            {
                return false;
            }
            //si cumple con todo, regresa true
            return true;
        }

        private bool esSQL(String textoVerificar)
        {
            string texto = textoVerificar.ToUpper();
            Regex reg = new Regex("(DECLARE|GRANT|REVOKE|ROLLBACK|INSERT INTO|UPDATE|SELECT |WITH|DELETE|CREATE|ALTER|FUNCTION|EXEC|EXECUTE|TABLE |DROP|INNER JOIN|LEFT JOIN|LEFT OUTER JOIN|RIGHT JOIN|RIGHT OUTER JOIN|TRUNCATE|DATABASE|UNION ALL|GROUP BY|ORDER BY|WHERE|FROM|VIEW|SCHEMA|SYS)");
            if (reg.IsMatch(texto))
            {
                return false;
            }

            if (texto.IndexOf("CHAR(") >= 0)
            {
                return false;
            }

            //si cumple con todo, regresa true
            return true;
        }
    }

    class ValidadorGeneralMensaje
    {
        public string key { get; set; }
        public IList<string> mensajes { get; set; }

        public ValidadorGeneralMensaje()
        {
            key = string.Empty;
            mensajes = new List<string>();
        }
    }
}

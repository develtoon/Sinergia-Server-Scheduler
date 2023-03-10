using OEPERU.Scheduler.BusinessLayer.Core;
using OEPERU.Scheduler.Common.Configuration;
using OEPERU.Scheduler.Common.Core;
using OEPERU.Scheduler.DataAccess.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace OEPERU.Scheduler.BusinessLayer.Manager.AccesoManagement
{
    public class AccesoManager : BusinessManager, IAccesoManager
    {
        IRepository _repository;
        IUnitOfWork _unitOfWork;
        ILogger<AccesoManager> _logger;
        HttpClient _client;

        public AccesoManager(IRepository repository, IUnitOfWork unitOfWork,
            ILogger<AccesoManager> logger) : base()
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        #region CRUD
        public DataQuery Search(DataQueryInput input)
        {
            throw new NotImplementedException();
        }

        public SingleQuery SingleById(string id)
        {
            throw new NotImplementedException();
        }

        public CheckStatus Create(BaseInputEntity entity)
        {
            throw new NotImplementedException();
        }

        public CheckStatus Update(BaseInputEntity entity)
        {
            throw new NotImplementedException();
        }

        public CheckStatus Delete(BaseInputDelete entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<CheckStatus> Validate(string token, string api, string recurso, string accion)
        {
            Dictionary<string, object> resultado = new Dictionary<string, object>();
            CheckStatus checkStatus = new CheckStatus(Status.Error, string.Empty);
            try
            {
                _client = new HttpClient();
                _client.BaseAddress = new Uri(Mensaje.ApiSeguridad);

                if (!string.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Authorization
                             = new AuthenticationHeaderValue("Bearer", token);
                }

                object data = new
                {
                    api,
                    accion,
                    recurso
                };

                var myParametros = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myParametros);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (HttpResponseMessage response = await _client.PostAsync(
                    Mensaje.ApiSeguridadAuth,
                    byteContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var stringResult = await response.Content.ReadAsStringAsync();
                        resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(stringResult);

                        checkStatus = new CheckStatus(resultado);
                    }
                    else
                    {
                        if ((response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity ||
                                response.StatusCode == System.Net.HttpStatusCode.Unauthorized))
                        {
                            var stringResult = await response.Content.ReadAsStringAsync();
                            resultado = JsonConvert.DeserializeObject<Dictionary<string, object>>(stringResult);
                            checkStatus = new CheckStatus(resultado);
                        }
                    }
                    response.Dispose();
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                checkStatus = new CheckStatus(Status.Error, httpRequestException.Message);
                _logger.LogError(string.Format("error catch:{0}", httpRequestException.Message));
            }
            finally
            {
                _client.Dispose();
                _client = null;
            }

            return checkStatus;
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

       
    }
}

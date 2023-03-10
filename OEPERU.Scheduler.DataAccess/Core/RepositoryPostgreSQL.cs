using Dapper;
using OEPERU.Scheduler.Common.Configuration;
using OEPERU.Scheduler.Common.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OEPERU.Scheduler.DataAccess.Core
{
    public class RepositoryPostgreSQL
    {
        DataContext _context;

        public RepositoryPostgreSQL()
        {

        }

        public RepositoryPostgreSQL(DataContext context)
        {
            _context = context;
        }

        public void ExecuteProcedure(string procedureCommand, DynamicParameters parameters)
        {
            DbConnection dbConnection = _context.Database.GetDbConnection();
            dbConnection.Open();
            dbConnection.Execute(procedureCommand, parameters, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            //dbConnection.Dispose();
        }

        public IList<T> ExecuteProcedureQuery<T>(string procedureCommand, DynamicParameters parameters) where T : class
        {
            var objList = new List<T>();
            DbConnection dbConnection = _context.Database.GetDbConnection();
            dbConnection.Open();
            objList = dbConnection.Query<T>(procedureCommand, parameters, commandType: CommandType.StoredProcedure).ToList();
            dbConnection.Close();
            return objList;
        }

        public DataQuery ExecuteProcedureQuery(string procedureCommand, DynamicParameters parameters, string entidadError)
        {
            DataQuery data = new DataQuery();
            long total = 0;
            string stotal = string.Empty;
            IList<IDictionary<string, object>> objDict = new List<IDictionary<string, object>>();
            DbConnection dbConnection = _context.Database.GetDbConnection();
            dbConnection.Open();

            IDataReader dr = dbConnection.ExecuteReader(procedureCommand, parameters, commandType: CommandType.StoredProcedure);
            while (dr.Read())
            {
                Dictionary<string, object> objFilaDicy = new Dictionary<string, object>();
                objFilaDicy = Enumerable.Range(0, dr.FieldCount).ToDictionary(dr.GetName, dr.GetValue);

                if (objFilaDicy.ContainsKey("rnum"))
                {
                    total = (long)objFilaDicy["rnum"];
                }

                objFilaDicy.Remove("rnum");
                objDict.Add(objFilaDicy);
            }

            dbConnection.Close();
            //dbConnection.Dispose();

            if (objDict.Count != 0)
            {
                data.data = objDict;
                data.total = total;
            }
            else
            {
                data.apiEstado = Status.Error;
                if (entidadError != null && string.IsNullOrEmpty(entidadError))
                {
                    data.apiMensaje = Mensaje.Mostrar("NoExisteDefecto");
                }
                else
                {
                    data.apiMensaje = string.Format(Mensaje.Mostrar("NoExiste"), entidadError);
                }
            }
            return data;
        }

        public virtual T ExecuteProcedureSingle<T>(string procedureCommand, DynamicParameters parameters) where T : class
        {
            T obj = Activator.CreateInstance<T>();
            DbConnection dbConnection = _context.Database.GetDbConnection();
            dbConnection.Open();
            obj = dbConnection.QuerySingleOrDefault<T>(procedureCommand, parameters, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            return obj;

        }

        public virtual object ExecuteFuncion(string functionCommand, DynamicParameters parameters)
        {
            DbConnection dbConnection = _context.Database.GetDbConnection();
            dbConnection.Open();
            var scalar = dbConnection.ExecuteScalar(functionCommand, parameters, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            return scalar;
        }

        public virtual object ExecuteProcedureScalar(string procedureCommand, DynamicParameters parameters)
        {
            DbConnection dbConnection = _context.Database.GetDbConnection();
            dbConnection.Open();
            var scalar = dbConnection.ExecuteScalar(procedureCommand, parameters, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            return scalar;
        }

        public T ExecuteProcedure<T>(string procedureCommand, DynamicParameters parameters) where T : class
        {
            T obj = Activator.CreateInstance<T>();
            Type tipo = obj.GetType();

            DbConnection dbConnection = _context.Database.GetDbConnection();
            dbConnection.Open();
            dbConnection.Execute(procedureCommand, parameters, commandType: CommandType.StoredProcedure);

            foreach (string paramName in parameters.ParameterNames)
            {
                var param = parameters.Get<DynamicParameter>(paramName);
                if (param.Direction == ParameterDirection.Output)
                {
                    PropertyInfo propEntidad = tipo.GetProperty(param.ParameterName.Replace("@", ""));
                    if (propEntidad != null)
                    {
                        propEntidad.SetValue(obj, param.Value);
                    }
                }
            }
            dbConnection.Close();

            return obj;
        }

    }
}

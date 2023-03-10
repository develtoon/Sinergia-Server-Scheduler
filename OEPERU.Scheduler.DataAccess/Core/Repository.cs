using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using OEPERU.Scheduler.Common.Core;
using System.Data;
using OEPERU.Scheduler.Common.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using OEPERU.Scheduler.DataAccess.Core;
using OEPERU.Administracion.DataAccess.Core;

namespace OEPERU.Scheduler.DataAccess.Core
{
    public class Repository : IRepository
    {
        DataContext _context;
        RepositoryPostgreSQL _dbStoreProc;

        public Repository(DbFactory dbFactory)
        {
           _context = dbFactory.GetDataContext;
           _dbStoreProc = new RepositoryPostgreSQL(dbFactory.GetDataContext);
        }
      
        public T Single<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return All<T>().FirstOrDefault(expression);
        }

        public IQueryable<T> All<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }

        public virtual IEnumerable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IEnumerable<T> Filter<T>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50) where T : class
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? _context.Set<T>().Where<T>(filter).AsQueryable() : _context.Set<T>().AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public virtual void Create<T>(T TObject) where T : class
        {
            string codigo = string.Empty;
            
            Type tipo = TObject.GetType();
            PropertyInfo propCodigo = tipo.GetProperty("Codigo");
           
            if (propCodigo != null)
            {  
                string entidad = "";
                var nombre = typeof(T).Name;

                Type type = typeof(EntidadNomenclatura);

                foreach (FieldInfo field in type.GetFields())
                {
                    if (field.Name.Equals(nombre))
                    {
                        entidad = field.GetValue(null) as string;
                    }
                }

                if (!String.IsNullOrWhiteSpace(entidad)) {

                    //SqlParameter[] sqlParams = { new SqlParameter("@entidad", entidad) };
                    //codigo = (string)ExecuteProcedureScalar(Consultas.ConCodigoGenerar, sqlParams);
                 
                    //propCodigo.SetValue(TObject, codigo);
                }
            }
    

            var newEntry = _context.Set<T>().Add(TObject);
        }
        public virtual void Update<T>(T TObject) where T : class
        {
            try
            {
                var entry = _context.Entry(TObject);
                _context.Set<T>().Attach(TObject);
                entry.State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void Delete<T>(T TObject) where T : class
        {
            _context.Set<T>().Remove(TObject);
        }
        public virtual void Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var objects = Filter<T>(predicate);
            foreach (var obj in objects)
                _context.Set<T>().Remove(obj);
        }
        public bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().Count<T>(predicate) > 0;
        }
        public virtual T Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return _context.Set<T>().FirstOrDefault<T>(predicate);
        }

        public virtual void ExecuteProcedure(string procedureCommand, object id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("inid", id);
            _dbStoreProc.ExecuteProcedure(procedureCommand, parameters);
        }

        public virtual void ExecuteProcedure(string procedureCommand, DynamicParameters parameters)
        {
            _dbStoreProc.ExecuteProcedure( procedureCommand, parameters);
        }

        public virtual IList<T> ExecuteProcedureQuery<T>(string procedureCommand, DynamicParameters parameters) where T : class
        {
            return _dbStoreProc.ExecuteProcedureQuery<T>(procedureCommand, parameters);
        }

        public virtual IList<T> ExecuteProcedureQuery<T>(string procedureCommand, object id) where T : class
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("inid", id);
            return _dbStoreProc.ExecuteProcedureQuery<T>(procedureCommand, parameters);
        }      

        public virtual DataQuery ExecuteProcedureQuery(string procedureCommand, DynamicParameters parameters,
           string entidadError)
        {
            return _dbStoreProc.ExecuteProcedureQuery(procedureCommand, parameters, entidadError);
        }

        public virtual T ExecuteProcedureSingle<T>(string procedureCommand, object id) where T : class
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("inid", id);
            return ExecuteProcedureSingle<T>(procedureCommand, parameters);
        }

        public virtual T ExecuteProcedureSingle<T>(string procedureCommand, DynamicParameters parameters) where T : class
        {
            return _dbStoreProc.ExecuteProcedureSingle<T>(procedureCommand, parameters);
        }

        public virtual object ExecuteFuncion(string functionCommand, DynamicParameters parameters)
        {
            return _dbStoreProc.ExecuteFuncion(functionCommand, parameters);
        }

        public virtual object ExecuteProcedureScalar(string procedureCommand, DynamicParameters parameters)
        {
            return _dbStoreProc.ExecuteProcedureScalar(procedureCommand, parameters);
        }

        public T ExecuteProcedure<T>(string procedureCommand, DynamicParameters parameters) where T : class
        {
            return _dbStoreProc.ExecuteProcedure<T>(procedureCommand, parameters);
        }       

    }


    public static class TypeExtensions
    {
        public static PropertyInfo[] GetFilteredProperties(this Type type)
        {
            return type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(NotMappedAttribute), true).Length == 0).ToArray();
        }
    }
}

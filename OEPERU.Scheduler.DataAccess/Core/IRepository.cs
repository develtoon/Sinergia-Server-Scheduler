using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using OEPERU.Scheduler.Common.Core;
using OEPERU.Scheduler.DataAccess.Core;

namespace OEPERU.Scheduler.DataAccess.Core
{
    public interface IRepository
    {
        IQueryable<T> All<T>() where T : class;
        void Create<T>(T TObject) where T : class;
        void Delete<T>(T TObject) where T : class;
        void Delete<T>(Expression<Func<T, bool>> predicate) where T : class;
        void Update<T>(T TObject) where T : class;
        
        IEnumerable<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class;
        IEnumerable<T> Filter<T>(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50) where T : class;
        T Find<T>(Expression<Func<T, bool>> predicate) where T : class;
        T Single<T>(Expression<Func<T, bool>> expression) where T : class;
        bool Contains<T>(Expression<Func<T, bool>> predicate) where T : class;

        void ExecuteProcedure(string procedureCommand, object id);
        void ExecuteProcedure(string procedureCommand, DynamicParameters parameters);

        IList<T> ExecuteProcedureQuery<T>(string procedureCommand, object id) where T : class;
        IList<T> ExecuteProcedureQuery<T>(string procedureCommand, DynamicParameters parameters) where T : class;
             
        DataQuery ExecuteProcedureQuery(string procedureCommand, DynamicParameters parameters, string entidadError);

        T ExecuteProcedureSingle<T>(string procedureCommand, object id) where T : class;
        T ExecuteProcedureSingle<T>(string procedureCommand, DynamicParameters sqlParams) where T : class;

        object ExecuteFuncion(string functionCommand, DynamicParameters parameters);
        object ExecuteProcedureScalar(string ExecuteProcedureScalar, DynamicParameters parameters);

        T ExecuteProcedure<T>(string procedureCommand, DynamicParameters parameters) where T : class;
    }
}

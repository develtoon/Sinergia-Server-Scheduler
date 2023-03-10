using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace OEPERU.Scheduler.DataAccess.Core
{
    class DynamicParameter  
    {      
        public  DbType DbType { get; set; }
        public ParameterDirection Direction { get; set; }
        
        public string ParameterName { get; set; }
        public  object Value { get; set; }

        public DynamicParameter()
        {

        }

        public DynamicParameter(string ParameterName, object Value)
        {
            this.ParameterName = ParameterName;
            this.Value = Value;
            this.Direction = ParameterDirection.Output;
        }

        public DynamicParameter(string ParameterName, DbType DbType, object Value)
        {
            this.ParameterName = ParameterName;
            this.DbType = DbType;
            this.Value = Value;
        }

        public DynamicParameter(string ParameterName, DbType DbType, object Value, ParameterDirection Direction)
        {
            this.ParameterName = ParameterName;
            this.DbType = DbType;
            this.Value = Value;
            this.Direction = Direction;
        }

    }
}

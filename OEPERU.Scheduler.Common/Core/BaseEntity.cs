using System.ComponentModel.DataAnnotations.Schema;

namespace OEPERU.Scheduler.Common.Core
{
    public class BaseEntity
    {       
        [Column("eliminado")]
        public bool Eliminado { get; set; }       
               
        public BaseEntity()
        {
           
            Eliminado = false;
        } 
    }
}

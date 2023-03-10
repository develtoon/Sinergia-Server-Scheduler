using OEPERU.Scheduler.Common.Core;
using OEPERU.Scheduler.DataAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OEPERU.Scheduler.BusinessLayer.Core
{
    public interface IActionManager
    {
        IUnitOfWork UnitOfWork { get; }
        DataQuery Search(DataQueryInput input);
        SingleQuery SingleById(string id);

        CheckStatus Create(BaseInputEntity entity);
        CheckStatus Update(BaseInputEntity entity);
        CheckStatus Delete(BaseInputDelete entity);

        void SaveChanges();

    }
}

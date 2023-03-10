using System;
using System.Collections.Generic;
using System.Text;

namespace OEPERU.Scheduler.Common.Core
{
    public class ValidadorOutput
    {
        public bool existe { get; set; }
        public string id { get; set; }

        public ValidadorOutput()
        {
            existe = false;
            id = string.Empty;
        }

        public ValidadorOutput(bool existe, string id)
        {
            this.existe = existe;
            this.id = string.Empty;
        }
        public ValidadorOutput(string id)
        {
            this.existe = true;
            this.id = string.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Enums
{
    public enum ObjectStatus
    {
        [Description("Aktif")]
        Active = 1,
        [Description("Pasif")]
        Passive = 2,
        [Description("Silinmiş")]
        Deleted = 3,
        
    }
}

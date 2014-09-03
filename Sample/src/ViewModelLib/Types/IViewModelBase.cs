using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLib.Types
{
    public interface IViewModelBase
    {
        object ControlInstance { get; set; }
    }
}

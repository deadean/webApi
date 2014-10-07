using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project001.Interfaces
{
    interface IBoard
    {
        IField[][] Fields { get; }
        int HorizontalRows { get; }
        int VerticalRows { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project001.Interfaces
{
    interface IField
    {
        int? Value { get; set; }
        int PositionX { get; }
        int PositionY { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameForm.Interfaces
{
    public interface IPiece
    {
        IField Field { get; set; }
        int Value { get; set; }
    }
}

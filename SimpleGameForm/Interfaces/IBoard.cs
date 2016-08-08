using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameForm.Interfaces
{
    public interface IBoard
    {
        ICollection<IField> Fields { get; }
        int HorizontalRows { get; }
        int VerticalRows { get; }
    }
}

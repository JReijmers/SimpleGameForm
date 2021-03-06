﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameForm.Interfaces
{
    public interface IField
    {
        IBoard Board { get; set; }
        IPiece Piece { get; set; }
        int PositionX { get; }
        int PositionY { get; }
    }
}

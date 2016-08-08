using SimpleGameForm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameForm.Games.Sudoku
{
    /// <summary>
    /// 
    /// </summary>
    class SudokuPiece : IPiece
    {
        /// <summary>
        /// stores the Field this Piece is located on
        /// </summary>
        public IField Field { get; set; }
        /// <summary>
        /// stores the maximum value the field could hold, likely based on the board's base
        /// </summary>
        private readonly int MaxValue;
        /// <summary>
        /// stores the current value of the field or null if empty, can't be lower than 0 or higher than MaxValue
        /// </summary>
        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value > 0 && value <= MaxValue)
                {
                    _value = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public SudokuPiece(int value, int maxValue)
        {
            this.MaxValue = maxValue;
            this.Value = value;
        }
    }
}

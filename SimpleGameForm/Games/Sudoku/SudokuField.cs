using Project001.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project001.Games.Sudoku
{
    class SudokuField : IField
    {
        /// <summary>
        /// stores the current value of the field or null if empty
        /// </summary>
        public int? Value { get; set; }
        /// <summary>
        /// The X coordinate, indicates which vertical row the field is on
        /// </summary>
        public int PositionX { get; private set; }
        /// <summary>
        /// The Y coordinate, indicates which horizontal row the field is on
        /// </summary>
        public int PositionY { get; private set; }

        /// <summary>
        /// List of possible values this field could contain, based on the values set in the fields around
        /// </summary>
        public List<int> PossibleValues { get; private set; }

        /// <summary>
        /// Initializes a new instance of field class with a null value at the given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SudokuField(int x, int y)
        {
            this.Value = null;
            this.PositionX = x;
            this.PositionY = y;
        }
        /// <summary>
        /// Initializes a new instance of field class with a given value at the given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public SudokuField(int x, int y, int value) : this(x, y)
        {
            this.Value = value;
        }

        /// <summary>
        /// Check to see if the given value is in the PossibleValues List or if is null
        /// </summary>
        /// <param name="value">Value to check for</param>
        /// <returns>True if the value is null or is in the PossibleValues List. False if the value is not null or in the PossibleValues List</returns>
        public bool IsPossibleValue(int? value)
        {
            if (value != null)
            {
                if (!this.PossibleValues.Contains((int)value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

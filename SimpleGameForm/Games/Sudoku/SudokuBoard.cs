using Project001.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project001.Games.Sudoku
{
    class SudokuBoard : IBoard
    {
        /// <summary>
        /// The root of the amount of fields the board should contain in both horizontal and vertical direction. Can't be negate and must be larger than 0
        /// </summary>
        public int BoardBase
        {
            get
            {
                return BoardBase;
            }
            private set
            {
                if (!(value > 0))
                {
                    throw new ArgumentOutOfRangeException("boardBase", "parameter boardBase must be larger than 0");
                }
                else
                {
                    BoardBase = value;
                }
            }
        }
        /// <summary>
        /// The number of vertical rows in the field. Corresponds to the maximum X coordinate minus 1
        /// </summary>
        public int VerticalRows
        {
            get
            {
                return SudokuFields.Length;
            }
        }
        /// <summary>
        /// The number of horizontal rows in the field. Corresponds to the maximum Y coordinate minus 1
        /// </summary>
        public int HorizontalRows
        {
            get
            {
                return SudokuFields[0].Length;
            }
        }

        /// <summary>
        /// Nested array to hold the Sudoku Fields for internal usage
        /// </summary>
        private SudokuField[][] SudokuFields;
        /// <summary>
        /// Nested array to return the Sudoku Fields as an IField interface for external usage
        /// </summary>
        public IField[][] Fields
        {
            get
            {
                return this.SudokuFields;
            }
        }

        /// <summary>
        /// Initializes a new instance of Sudoku Board class with a uniform vertical and horizontal size based on the square of the given number
        /// </summary>
        /// <param name="boardBase">Root of the number that indicated the board dimensions. Can't be negate and must be larger than 0</param>
        public SudokuBoard(int boardBase)
        {
            this.BoardBase = boardBase;
            this.SudokuFields = this.InitializeBoard();
        }

        /// <summary>
        /// Initializes a nested array of empty Sudoku fields that make up the board
        /// </summary>
        /// <returns>A nested array of empty Sudoku fields the size of SquareOf </returns>
        private SudokuField[][] InitializeBoard()
        {
            SudokuField[][] fields = new SudokuField[this.BoardBase^2][];

            for (int x = 0; x < (this.BoardBase^2); x++)
            {
                fields[x] = new SudokuField[this.BoardBase^2];
                for (int y = 0; y < (this.BoardBase^2); y++)
                {
                    fields[x][y] = new SudokuField(x, y);
                }
            }

            return fields;
        }

        /// <summary>
        /// Retrieve a List of all fields that are in the same horizontal row (so with the same Y coordinate) as the given field. Includes the provided field
        /// </summary>
        /// <param name="field">Field to look up</param>
        /// <returns>List of fields in the horizontal row of the given field. Includes the provided field</returns>
        public List<SudokuField> RetrieveHorizontalRow(IField field)
        {
            List<SudokuField> horizontalFields = new List<SudokuField>();

            for (int x = 0; x < this.HorizontalRows; x++)
            {
                horizontalFields.Add(this.SudokuFields[x][field.PositionY]);
            }

            return horizontalFields;
        }
        /// <summary>
        /// Retrieve a List of all fields that are in the same vertical row (so with the same X coordinate) as the given field. Includes the provided field
        /// </summary>
        /// <param name="field">Field to look up</param>
        /// <returns>List of fields in the vertical row of the given field. Includes the provided field</returns>
        public List<SudokuField> RetrieveVerticalRow(IField field)
        {
            List<SudokuField> verticalFields = new List<SudokuField>(this.SudokuFields[field.PositionX]);

            return verticalFields;
        }
        /// <summary>
        /// Retrieve a List of all fields that are in the same block (block size depends on the board base) as the given field. Includes the provided field
        /// </summary>
        /// <param name="field">Field to look up</param>
        /// <returns>List of fields in the block of the given field. Includes the provided field</returns>
        public List<SudokuField> RetrieveBlock(IField field)
        {
            List<SudokuField> blockFields = new List<SudokuField>();

            //calculate the starting coordinates of block
            //dividing the int will only show the value before the decimal pointer, this is desired behavious in this case
            int blockPositionX = (field.PositionX / this.BoardBase) * this.BoardBase;
            int blockPositionY = (field.PositionY / this.BoardBase) * this.BoardBase;

            for (int x = blockPositionX; x < (blockPositionX + this.BoardBase); x++)
            {
                for (int y = blockPositionY; y < (blockPositionY + this.BoardBase); y++)
                {
                    blockFields.Add(this.SudokuFields[x][y]);
                }
            }

            return blockFields;
        }
    }
}

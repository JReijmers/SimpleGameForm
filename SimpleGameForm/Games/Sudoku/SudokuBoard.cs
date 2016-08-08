using SimpleGameForm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameForm.Games.Sudoku
{
    /// <summary>
    /// The Board manages the instances of the Field class and guarantees their integretiy
    /// </summary>
    class SudokuBoard : IBoard
    {
        /// <summary>
        /// The root of the amount of fields the board should contain in both horizontal and vertical direction. Can't be negate and must be larger than 0
        /// </summary>
        private int _boardBase;
        public int BoardBase
        {
            get
            {
                return _boardBase;
            }
            private set
            {
                if (!(value > 0))
                {
                    throw new ArgumentOutOfRangeException("boardBase", "parameter boardBase must be larger than 0");
                }
                else
                {
                    _boardBase = value;
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
                return (_boardBase^2);
            }
        }
        /// <summary>
        /// The number of horizontal rows in the field. Corresponds to the maximum Y coordinate minus 1
        /// </summary>
        public int HorizontalRows
        {
            get
            {
                return (_boardBase^2);
            }
        }

        /// <summary>
        /// Nested array to hold the Sudoku Fields for internal usage
        /// </summary>
        private ICollection<SudokuField> _sudokuFields;
        /// <summary>
        /// Nested array to return the Sudoku Fields as an IField interface for external usage
        /// </summary>
        public ICollection<IField> Fields
        {
            get
            {
                ICollection<IField> collection = new List<IField>();

                foreach (var field in _sudokuFields)
                {
                    collection.Add(field);
                }

                return collection;
            }
        }

        /// <summary>
        /// Initializes a new instance of Sudoku Board class with a uniform vertical and horizontal size based on the square of the given number
        /// </summary>
        /// <param name="boardBase">Root of the number that indicated the board dimensions. Can't be negate and must be larger than 0</param>
        public SudokuBoard(int boardBase)
        {
            this.BoardBase = boardBase;
            this.InitializeBoard();
        }

        /// <summary>
        /// Initializes a nested array of empty Sudoku fields that make up the board, setting up all the SudokuFields for use
        /// </summary>
        /// <returns>A nested array of empty Sudoku fields the size of SquareOf </returns>
        private void InitializeBoard()
        {
            _sudokuFields = new List<SudokuField>();

            for (int y = 1; y <= Math.Pow(this.BoardBase,2); y++)
            {
                for (int x = 1; x <= Math.Pow(this.BoardBase, 2); x++)
                {
                    SudokuField sudokuField = new SudokuField(this, x, y);

                    ICollection<SudokuField> horizontalRow = this.RetrieveHorizontalRow(sudokuField);
                    //for each existing field in the horizontal row add me to their horizontal collection, and them to mine
                    foreach (var relatedSudokuField in horizontalRow)
                    {
                        relatedSudokuField.HorizontalFields.Add(sudokuField);
                        sudokuField.HorizontalFields.Add(relatedSudokuField);
                    }
                    ICollection<SudokuField> verticalRow = this.RetrieveVerticalRow(sudokuField);
                    //for each existing field in the vertical row add me to their vertical collection, and them to mine
                    foreach (var relatedSudokuField in verticalRow)
                    {
                        relatedSudokuField.VerticalFields.Add(sudokuField);
                        sudokuField.VerticalFields.Add(relatedSudokuField);
                    }
                    ICollection<SudokuField> block = this.RetrieveBlock(sudokuField);
                    //for each existing field in the block add me to their block collection, and them to mine
                    foreach (var relatedSudokuField in block)
                    {
                        relatedSudokuField.BlockFields.Add(sudokuField);
                        sudokuField.BlockFields.Add(relatedSudokuField);
                    }

                    _sudokuFields.Add(sudokuField);
                }
            }
        }

        /// <summary>
        /// Computes a List of all fields that are in the same horizontal row (so with the same Y coordinate) as the given field. Includes the provided field
        /// </summary>
        /// <param name="field">Field to look up</param>
        /// <returns>List of fields in the horizontal row of the given field. Includes the provided field</returns>
        public ICollection<SudokuField> RetrieveHorizontalRow(IField field)
        {
            ICollection<SudokuField> horizontalFields = new List<SudokuField>();

            foreach (var sudokuField in _sudokuFields)
            {
                if (sudokuField.PositionY == field.PositionY)
                {
                    horizontalFields.Add(sudokuField);
                }
            }

            return horizontalFields;
        }
        /// <summary>
        /// Computes a List of all fields that are in the same vertical row (so with the same X coordinate) as the given field. Includes the provided field
        /// </summary>
        /// <param name="field">Field to look up</param>
        /// <returns>List of fields in the vertical row of the given field. Includes the provided field</returns>
        public ICollection<SudokuField> RetrieveVerticalRow(IField field)
        {
            ICollection<SudokuField> verticalFields = new List<SudokuField>();

            foreach (var sudokuField in _sudokuFields)
            {
                if (sudokuField.PositionX == field.PositionX)
                {
                    verticalFields.Add(sudokuField);
                }
            }

            return verticalFields;
        }
        /// <summary>
        /// Computes a List of all fields that are in the same block (block size depends on the board base) as the given field. Includes the provided field
        /// </summary>
        /// <param name="field">Field to look up</param>
        /// <returns>List of fields in the block of the given field. Includes the provided field</returns>
        public ICollection<SudokuField> RetrieveBlock(IField field)
        {
            ICollection<SudokuField> blockFields = new List<SudokuField>();

            //calculate the starting coordinates of block
            //dividing the int will only show the value before the decimal pointer, this is desired behavious in this case
            int blockStartPositionX = (((field.PositionX - 1) / this.BoardBase) * this.BoardBase) + 1;
            int nextBlockStartPositionX = blockStartPositionX + this.BoardBase;
            int blockStartPositionY = (((field.PositionY - 1) / this.BoardBase) * this.BoardBase) + 1;
            int nextBlockStartPositionY = blockStartPositionY + this.BoardBase;

            foreach (var sudokuField in _sudokuFields)
            {
                //if the sudokufield position is the blocks x and y start or end positions, or a value in between
                if ((sudokuField.PositionX >= blockStartPositionX && sudokuField.PositionX < nextBlockStartPositionX) && (sudokuField.PositionY >= blockStartPositionY && sudokuField.PositionY < nextBlockStartPositionY))
                {
                    blockFields.Add(sudokuField);
                }
            }

            return blockFields;
        }

        /// <summary>
        /// Computes a List of all fields that are in the same horizontal row, vertical row and block as the given field. Includes the provided field
        /// </summary>
        /// <param name="field">Field to look up</param>
        /// <returns>List of fields in the same horizontal row, vertical row and block of the given field. Includes the provided field</returns>
        public ICollection<SudokuField> RetrieveRelatedFields(IField field)
        {
            var horizontalCollection = this.RetrieveHorizontalRow(field);
            var verticalCollection = this.RetrieveVerticalRow(field);
            var blockCollection = this.RetrieveBlock(field);

            //return a union of all the collections so there are no duplicate values
            return horizontalCollection.Union(verticalCollection.Union(blockCollection)).ToList();
        }

        /// <summary>
        /// Check to see if the given value would be valid for the given collection
        /// </summary>
        /// <param name="field">Field to check the value for</param>
        /// <param name="value">Value to check for</param>
        /// <param name="collection">the collection to check</param>
        /// <param name="failOnBlock">sets if the operation would be valid if it blocks another field from being set. Default value is false</param>
        /// <returns>True if the value should be valid to set otherwise returns False.</returns>
        private bool IsValidForCollection(SudokuField field, int? value, ICollection<SudokuField> collection, bool failOnBlock = false)
        {
            if (field.IsPossiblePieceValue(value))
            {
                collection.Remove(field);

                foreach (var relatedSudokuField in collection)
                {
                    //check if value has already been set in this set, or if it's the only option this field has left only if failOnBlock param is set to true
                    if (relatedSudokuField.Piece.Value == value || (failOnBlock && (relatedSudokuField.IsPossiblePieceValue(value) && relatedSudokuField.PossiblePieceValues.Count == 1)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public ICollection<int> CalculatePossibleValues(SudokuField field)
        {
            var fieldList = field.RelatedFields;

            ICollection<int> possibleValues = new List<int>();

            for (int i = 1; i <= Math.Pow(this.BoardBase, 2); i++)
            {
                possibleValues.Add(i);
            }

            foreach (var sudokuField in fieldList)
            {
                if (sudokuField.Value != null)
                {
                    possibleValues.Remove((int)sudokuField.Value);
                }
            }

            return possibleValues;
        }

        /*
        public bool SetFieldPieceValue(SudokuField field, int? value)
        {

        }
        */
    }
}

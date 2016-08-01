using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project001.Interfaces;

namespace Project001.Games.Sudoku
{
    class SudokuGame : IGame
    {
        /// <summary>
        /// Sudoku board object to hold the board that holds all the fields for internal usage
        /// </summary>
        private SudokuBoard SudokuBoard;
        /// <summary>
        /// Sudoku board object to return the board that holds all the fields as an IBoard interface for external usage
        /// </summary>
        public IBoard Board
        {
            get
            {
                return this.SudokuBoard;
            }
        }

        /// <summary>
        /// Initializes a new instance of Sudoku Game class qith a new Sudoku Board with a uniform vertical and horizontal size based on the square of the given number
        /// </summary>
        /// <param name="boardBase">Root of the number that indicated the board dimensions. Can't be negate and must be larger than 0</param>
        public SudokuGame(int boardBase = 3)
        {
            this.SudokuBoard = new SudokuBoard(boardBase);
        }

        /// <summary>
        /// Check to see if the given value would be valid for the given field as far the horizontal row (x-axis) is concerned
        /// </summary>
        /// <param name="field">Field to check the value for</param>
        /// <param name="value">Value to check for</param>
        /// <returns>True if the value would be valid on the horizontal row (x-axis). False if the value would not be valid on the horizontal row (x-axis)</returns>
        private bool IsValidHorizontal(IField field, int? value)
        {
            SudokuField sudokuField = field as SudokuField;

            if (sudokuField != null)
            {
                if (sudokuField.IsPossibleValue(value))
                {
                    foreach (var sudokuFieldInRow in this.SudokuBoard.RetrieveHorizontalRow(field))
                    {
                        //check if value has already been set in this set or if it's the only option this field has left
                        if (sudokuFieldInRow.Value == value || (sudokuFieldInRow.IsPossibleValue(value) && sudokuFieldInRow.PossibleValues.Count == 1))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                throw new InvalidCastException("parameter field must be the interface implementation of a SudokuField");
            }

            return true;
        }

        /// <summary>
        /// Check to see if the given value would be valid for the given field as far the horizontal row (x-axis) is concerned
        /// </summary>
        /// <param name="field">Field to check the value for</param>
        /// <param name="value">Value to check for</param>
        /// <returns>True if the value would be valid on the horizontal row (x-axis). False if the value would not be valid on the horizontal row (x-axis)</returns>
        private bool IsValidVertical(IField field, int? value)
        {
            SudokuField sudokuField = field as SudokuField;

            if (sudokuField != null)
            {
                if (sudokuField.IsPossibleValue(value))
                {
                    foreach (var sudokuFieldInRow in this.SudokuBoard.RetrieveHorizontalRow(field))
                    {
                        //check if value has already been set in this set or if it's the only option this field has left
                        if (sudokuFieldInRow.Value == value || (sudokuFieldInRow.IsPossibleValue(value) && sudokuFieldInRow.PossibleValues.Count == 1))
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                throw new InvalidCastException("parameter field must be the interface implementation of a SudokuField");
            }

            return true;
        }

        private bool IsValidBlock(IField field)
        {
            //
            return false;
        }

        public bool TrySetFieldValue(IField field, int? value)
        {
            bool success = false;
            SudokuField sudokuField = field as SudokuField;

            if (sudokuField != null)
            {

            }

            return success;
        }
    }
}

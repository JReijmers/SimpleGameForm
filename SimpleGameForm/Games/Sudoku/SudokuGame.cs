using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleGameForm.Interfaces;

namespace SimpleGameForm.Games.Sudoku
{
    /// <summary>
    /// Contains the board and the logic for the Sudoko Game
    /// </summary>
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

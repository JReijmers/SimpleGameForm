using SimpleGameForm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGameForm.Games.Sudoku
{
    /// <summary>
    /// The field on a board, possibly holds a piece
    /// </summary>
    class SudokuField : IField
    {
        /// <summary>
        /// The Board this Field belongs to
        /// </summary>
        public IBoard Board { get; set; }
        /// <summary>
        /// returns the value of the set piece on the field or null if empty
        /// </summary>
        public int? Value
        {
            get
            {
                if (Piece == null)
                {
                    return null;
                }
                else
                {
                    return Piece.Value;
                }
            }
            /*
            set
            {
                if (Piece == null)
                {
                    Piece = new SudokuPiece((int)value, this.MaxValue);
                }
                else
                {
                    //TODO
                    if (Value != null)
                    {
                        int oldValue = (int)Value;

                        this.PossiblePieceValues.Add(oldValue);

                        foreach (var relatedField in this.RelatedFields)
                        {
                            relatedField.PossiblePieceValues.Add((int)Value);
                        }
                    }

                    Piece.Value = value;

                }
            }
            */
        }
        /// <summary>
        /// stores the current Piece on this Field
        /// </summary>
        public IPiece Piece { get; set; }
        /// <summary>
        /// The Y coordinate, indicates which horizontal row the field is on, starting from 0
        /// </summary>
        public int PositionY { get; private set; }
        /// <summary>
        /// The X coordinate, indicates which vertical row the field is on, starting from 0
        /// </summary>
        public int PositionX { get; private set; }

        /// <summary>
        /// List of possible values this field could contain, based on the values set in the fields around
        /// </summary>
        public ICollection<int> PossiblePieceValues { get; private set; }

        public ICollection<SudokuField> HorizontalFields { get; set; }
        public ICollection<SudokuField> VerticalFields { get; set; }
        public ICollection<SudokuField> BlockFields { get; set; }
        public ICollection<SudokuField> RelatedFields
        {
            get
            {
                return this.HorizontalFields.Union(this.VerticalFields.Union(this.BlockFields)).ToList();
            }
        }

        /// <summary>
        /// Initializes a new instance of field class with a null value at the given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SudokuField(IBoard board, int x, int y)
        {
            HorizontalFields = new List<SudokuField>();
            VerticalFields = new List<SudokuField>();
            BlockFields = new List<SudokuField>();

            this.Board = board;
            this.Piece = null;
            this.PositionX = x;
            this.PositionY = y;
        }
        /// <summary>
        /// Initializes a new instance of field class with a given value at the given coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="piece"></param>
        public SudokuField(IBoard board, int x, int y, IPiece piece) : this(board, x, y)
        {
            this.Piece = piece;
        }

        /// <summary>
        /// Check to see if the given value is in the PossibleValues List or if is null
        /// </summary>
        /// <param name="value">Value to check for</param>
        /// <returns>True if the value is null or is in the PossibleValues List. False if the value is not null or in the PossibleValues List</returns>
        public bool IsPossiblePieceValue(int? value)
        {
            if (value != null)
            {
                if (!this.PossiblePieceValues.Contains((int)value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

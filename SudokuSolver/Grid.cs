using System.Collections.Generic;

namespace SudokuSolver
{
    public class Grid
    {
        public int Rows { get; }
        public int Cols { get; }
        public int Count => _cells.Count;

        private readonly List<Cell> _cells = new List<Cell>();

        public Grid(int rows, int cols, int initialRow, int initialCol)
        {
            Rows = rows;
            Cols = cols;

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Cols; j++)
                {
                    _cells.Add(new Cell(initialRow + i, initialCol + j));
                }
            }
        }

        public void SetCellValue(int row, int col, int value)
        {
            GetCell(row, col).Value = value;
        }

        public Cell GetCell(int row, int col)
        {
            return _cells[row * Cols + col];
        }

        public Cell GetCell(int index)
        {
            return _cells[index];
        }

        public List<Cell> GetCol(int col)
        {
            var cells = new List<Cell>();
            for (var i = col; i < Rows * Cols; i += Cols)
            {
                cells.Add(_cells[i]);
            }
            return cells;
        }

        public List<Cell> GetRow(int row)
        {
            var cells = new List<Cell>();
            for (var i = row * Cols; i < (row + 1) * Cols; i++)
            {
                cells.Add(_cells[i]);
            }
            return cells;
        }

        public List<Cell> ToList()
        {
            return _cells;
        }
    }
}

using System.Collections.Generic;

namespace SudokuSolver
{
    public class Cell
    {
        public int? Value { get; set; }
        public int Row { get; }
        public int Col { get; }
        public List<int> Domain { get; }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;

            Domain = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 31 + Row.GetHashCode();
                hash = hash * 31 + Col.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public override string ToString()
        {
            return $"Row: {Row} Col: {Col}";
        }
    }
}

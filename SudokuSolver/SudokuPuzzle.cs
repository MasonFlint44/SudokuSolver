using PaperClip.Collections.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaperClip.Collections;

namespace SudokuSolver
{
    public class SudokuPuzzle
    {
        private readonly List<Grid> _regions = new List<Grid>();

        private readonly IPrioritySet<Cell, Cell, Cell> _minimumRemainingValues;
        // Number of values set
        private int _valuesSet = 0;

        public bool IsComplete => _valuesSet == 81;

        public SudokuPuzzle(int[] values) : this()
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    int value;
                    if ((value = values[i * 9 + j]) != 0)
                    {
                        SetCellValue(i, j, value);
                    }
                }
            }
        }

        public SudokuPuzzle()
        {
            _minimumRemainingValues = new MinPrioritySet<Cell, Cell, Cell>(
                Comparer<Cell>.Create((a, b) =>
                {
                    // Sort cells by minimum remaining values
                    if (a.Domain.Count < b.Domain.Count)
                    {
                        return -1;
                    }
                    if (a.Domain.Count > b.Domain.Count)
                    {
                        return 1;
                    }
                    // Break ties by greatest degree
                    var aDegree = GetDegree(a);
                    var bDegree = GetDegree(b);
                    if (aDegree > bDegree)
                    {
                        return -1;
                    }
                    if (aDegree < bDegree)
                    {
                        return 1;
                    }
                    return 0;
                }));

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    _regions.Add(new Grid(3, 3, i * 3, j * 3));
                }
            }

            foreach (var cell in ToEnumerable())
            {
                _minimumRemainingValues.Enqueue(cell, cell, cell);
            }
        }

        public Cell GetCell(int row, int col)
        {
            var region = GetRegion(row, col);
            return region.GetCell(row % 3, col % 3);
        }

        public void SetCellValue(int row, int col, int value)
        {
            var cell = GetCell(row, col);
            cell.Domain.Clear();
            cell.Value = value;

            _minimumRemainingValues.Remove(cell);
            _valuesSet++;

            // Remove value from constrained cells' domains (forward checking)
            foreach (var constrainedCell in ConstrainedBy(cell))
            {
                constrainedCell.Domain.Remove(value);

                _minimumRemainingValues.UpdatePriority(constrainedCell, constrainedCell);
            }
        }

        // Get region for cell @ (row, col)
        public Grid GetRegion(int row, int col)
        {
            var regionRow = row / 3;
            var regionCol = col / 3;
            return _regions[regionRow * 3 + regionCol];
        }

        public List<Cell> GetCol(int col)
        {
            var cells = new List<Cell>();
            for (var regionIndex = col / 3; regionIndex < 9; regionIndex += 3)
            {
                var region = _regions[regionIndex];
                cells.AddRange(region.GetCol(col % 3));
            }
            return cells;
        }

        public List<Cell> GetRow(int row)
        {
            var cells = new List<Cell>();
            var regionIndex = row / 3 * 3;
            for (var i = regionIndex; i < regionIndex + 3; i++)
            {
                var region = _regions[i];
                cells.AddRange(region.GetRow(row % 3));
            }
            return cells;
        }

        private List<Cell> ConstrainedBy(Cell cell)
        {
            var constrainedCells = new List<Cell>();
            constrainedCells.AddRange(GetRow(cell.Row));
            constrainedCells.AddRange(GetCol(cell.Col));
            constrainedCells.AddRange(GetRegion(cell.Row, cell.Col).ToList());
            return constrainedCells.Where(c => c.Value == null).Distinct().ToList();
        }

        public int GetDegree(Cell cell)
        {
            return ConstrainedBy(cell).Count;
        }

        public Cell MinRemainingValue()
        {
            return _minimumRemainingValues.Peek();
        }

        public Cell NextCell()
        {
            return _minimumRemainingValues.Dequeue();
        }

        public SudokuPuzzle Clone()
        {
            var puzzle = new SudokuPuzzle();
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    Cell cell;
                    if ((cell = GetCell(i, j)).Value != null)
                    {
                        puzzle.SetCellValue(i, j, cell.Value.Value);
                    }
                }
            }
            return puzzle;
        }

        public IEnumerable<int> ToEnumerableValues()
        {
            return ToEnumerable().Select(cell => cell.Value ?? 0);
        }

        private IEnumerable<Cell> ToEnumerable()
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    yield return GetCell(i, j);
                }
            }
        }

        private static string Spacer()
        {
            var sb = new StringBuilder();
            sb.Append("+");
            for (var i = 0; i < 3; i++)
            {
                sb.Append("-----+");
            }
            sb.Append("\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Spacer());
            for (var row = 0; row < 9; row++)
            {
                sb.Append("|");
                for (var col = 0; col < 9; col++)
                {
                    var cell = GetCell(row, col);

                    if (cell.Value == null) { sb.Append(" "); }
                    else { sb.Append(cell.Value); }

                    if((col + 1) % 3 == 0) { sb.Append("|"); }
                    else { sb.Append(" "); }
                }
                sb.Append("\n");
                if((row + 1) % 3 == 0) { sb.Append(Spacer()); }
            }
            return sb.ToString();
        }

        public bool IsValidPuzzle()
        {
            foreach (var region in _regions)
            {
                if (region.ToList()
                    .Where(cell => cell.Value.HasValue)
                    .GroupBy(cell => cell.Value)
                    .Any(group => group.Count() > 1))
                {
                    return false;
                }
            }
            for (var i = 0; i < 9; i++)
            {
                if (GetRow(i)
                    .Where(cell => cell.Value.HasValue)
                    .GroupBy(cell => cell.Value)
                    .Any(group => group.Count() > 1))
                {
                    return false;
                }
            }
            for (var i = 0; i < 9; i++)
            {
                if (GetCol(i)
                    .Where(cell => cell.Value.HasValue)
                    .GroupBy(cell => cell.Value)
                    .Any(group => group.Count() > 1))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

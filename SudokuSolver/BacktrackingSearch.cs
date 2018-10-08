using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SudokuSolver.Models;

namespace SudokuSolver
{
    public static class BacktrackingSearch
    {
        private static List<SudokuPuzzleStepModel> Steps { get; } = new List<SudokuPuzzleStepModel>();
        private static Stopwatch _stopwatch;

        public static SudokuPuzzleResultModel Search(SudokuPuzzle puzzle)
        {
            Steps.Clear();

            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            if(puzzle.IsValidPuzzle() == false)
            {
                _stopwatch.Stop();

                return new SudokuPuzzleResultModel
                {
                    ExecutionTime = _stopwatch.ElapsedMilliseconds,
                    IsValidPuzzle = false
                };
            }

            var result = Backtracking(puzzle);

            _stopwatch.Stop();

            return new SudokuPuzzleResultModel
            {
                ExecutionTime = _stopwatch.ElapsedMilliseconds,
                Solution = result?.ToEnumerableValues().ToArray(),
                Steps = Steps.ToArray(),
                IsValidPuzzle = true,
                SolutionFound = result != null
            };
        }

        private static SudokuPuzzle Backtracking(SudokuPuzzle puzzle)
        {
            if (puzzle.IsComplete) { return puzzle; }

            if(_stopwatch.Elapsed.Minutes >= 5) { return null; }

            var cell = puzzle.NextCell();

            // Keep track of first three steps
            if (Steps.Count < 3)
            {
                Steps.Add(new SudokuPuzzleStepModel
                {
                    Degree = puzzle.GetDegree(cell),
                    DomainSize = cell.Domain.Count,
                    SelectedCol = cell.Col,
                    SelectedRow = cell.Row
                });
            }

            foreach (var value in cell.Domain)
            {
                // Generate next node
                var clone = puzzle.Clone();
                clone.SetCellValue(cell.Row, cell.Col, value);

                // If there exists a cell with an empty domain after forward checking, backtrack
                if(clone.MinRemainingValue()?.Domain.Count == 0) { continue; }

                var result = Backtracking(clone);
                if(result != null) { return result; }
            }
            return null;
        }
    }
}

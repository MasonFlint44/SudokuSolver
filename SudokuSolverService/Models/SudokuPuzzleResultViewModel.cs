using System.Collections.Generic;
using SudokuSolver.Models;

namespace SudokuSolverService.Models
{
    public class SudokuPuzzleResultViewModel
    {
        public SudokuPuzzleStepViewModel[] Steps { get; set; }
        public bool IsValidPuzzle { get; set; }
        public bool SolutionFound { get; set; }
        public long ExecutionTime { get; set; }
        public int[] Solution { get; set; }

        public SudokuPuzzleResultViewModel(SudokuPuzzleResultModel result)
        {
            Solution = result.Solution;
            ExecutionTime = result.ExecutionTime;
            IsValidPuzzle = result.IsValidPuzzle;
            SolutionFound = result.SolutionFound;

            var steps = new List<SudokuPuzzleStepViewModel>();
            foreach (var step in result.Steps)
            {
                steps.Add(new SudokuPuzzleStepViewModel
                {
                    Degree = step.Degree,
                    DomainSize = step.DomainSize,
                    SelectedCol = step.SelectedCol,
                    SelectedRow = step.SelectedRow
                });
            }
            Steps = steps.ToArray();
        }
    }
}

namespace SudokuSolver.Models
{
    public class SudokuPuzzleResultModel
    {
        public int[] Solution { get; set; }
        public SudokuPuzzleStepModel[] Steps { get; set; }
        public long ExecutionTime { get; set; }
        public bool IsValidPuzzle { get; set; }
        public bool SolutionFound { get; set; }
    }
}

namespace SudokuSolver.Models
{
    public class SudokuPuzzleStepModel
    {
        public int SelectedRow { get; set; }
        public int SelectedCol { get; set; }
        public int DomainSize { get; set; }
        public int Degree { get; set; }
    }
}

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SudokuSolver;
using SudokuSolverService.Models;

namespace SudokuSolverService.Controllers
{
    [Route("api/[controller]")]
    public class SudokuController : Controller
    {
        private readonly int[] _puzzle1 =
        {
            0, 0, 1, 0, 0, 2, 0, 0, 0,
            0, 0, 5, 0, 0, 6, 0, 3, 0,
            4, 6, 0, 0, 0, 5, 0, 0, 0,
            0, 0, 0, 1, 0, 4, 0, 0, 0,
            6, 0, 0, 8, 0, 0, 1, 4, 3,
            0, 0, 0, 0, 9, 0, 5, 0, 8,
            8, 0, 0, 0, 4, 9, 0, 5, 0,
            1, 0, 0, 3, 2, 0, 0, 0, 0,
            0, 0, 9, 0, 0, 0, 3, 0, 0,
        };

        private readonly int[] _puzzle2 =
        {
            0, 0, 5, 0, 1, 0, 0, 0, 0,
            0, 0, 2, 0, 0, 4, 0, 3, 0,
            1, 0, 9, 0, 0, 0, 2, 0, 6,
            2, 0, 0, 0, 3, 0, 0, 0, 0,
            0, 4, 0, 0, 0, 0, 7, 0, 0,
            5, 0, 0, 0, 0, 7, 0, 0, 1,
            0, 0, 0, 6, 0, 3, 0, 0, 0,
            0, 6, 0, 1, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 7, 0, 0, 5, 0,
        };

        private readonly int[] _puzzle3 =
        {
            6, 7, 0, 0, 0, 0, 0, 0, 0,
            0, 2, 5, 0, 0, 0, 0, 0, 0,
            0, 9, 0, 5 ,6, 0, 2, 0, 0,
            3, 0, 0, 0, 8, 0, 9, 0, 0,
            0, 0, 0, 0, 0, 0, 8, 0, 1,
            0, 0, 0, 4, 7, 0, 0, 0, 0,
            0, 0, 8, 6, 0, 0, 0, 9, 0,
            0, 0, 0, 0, 0, 0, 0, 1, 0,
            1, 0, 6, 0, 5, 0, 0, 7, 0,
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("puzzle")]
        [HttpGet]
        public IActionResult GetPuzzle(int index)
        {
            switch (index)
            {
                case 1:
                    return Ok(_puzzle1);
                case 2:
                    return Ok(_puzzle2);
                case 3:
                    return Ok(_puzzle3);
                default:
                    return BadRequest("Puzzle index must be 1 - 3.");
            }
        }

        [Route("solve")]
        [HttpPost]
        public IActionResult Solve([FromBody]int[] cells)
        {
            if(cells.Length != 81) { return BadRequest("Solution must have 81 cells."); }
            if(cells.Any(element => element < 0 || element > 9)) { return BadRequest("Cells must be values 0 - 9."); }

            var puzzle = new SudokuPuzzle(cells);
            var result = BacktrackingSearch.Search(puzzle);

            return Ok(new SudokuPuzzleResultViewModel(result));
        }
    }
}

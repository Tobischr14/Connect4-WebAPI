
using Microsoft.AspNetCore.Mvc;

namespace connect4_api.Controllers;


[ApiController]
[Route("[controller]")]
public class Connect4ApiController : ControllerBase
{

    // dimensions of board and required winning Chain
    public static int ROW_COUNT = 6;
    public static int COLUMN_COUNT = 7;
    public static int WINNING_CHAIN = 4;
    char[,] board = new char[ROW_COUNT, COLUMN_COUNT];



    // /Connect4API/{input}
    [HttpGet("{input}")]
    public ActionResult CheckWinner(string input)
    {   
        // check if WINNING_CHAIN is possible in board size
        if (WINNING_CHAIN > ROW_COUNT || WINNING_CHAIN > COLUMN_COUNT)
        {
            return StatusCode(500, "Internal server error. Winning chain not possible with current board size."); 
        }
        
        // check input string for length and 
        if (!Connect4.check_input_length(input, ROW_COUNT, COLUMN_COUNT))
        {
            return BadRequest($"Input length does not match board size of {ROW_COUNT} X {COLUMN_COUNT} (rows X columns)") ;
        }
        // check relative number of pieces per player
        if (!Connect4.check_nr_of_pieces(input))
        {
            return BadRequest("Input unrealistic. One player has too many pieces compared to the other player.");
        }
        
        // create board from input string
        board = Connect4.create_board(board, input); 

        // check for floating pieces
        if (!Connect4.check_floating_pieces(board))
        {
            return BadRequest("Input unrealistic. Some pieces are floating.");
        }

        string board_as_string = Connect4.board_to_string(board);


        // (for development: return the printed board):
        // return Ok(board_as_string);


        // Get the result and check if there is a winner or not
        char result = Connect4.is_winner(board, WINNING_CHAIN);

        // check for multiple winners or winning chains
        if(result == 'M')
        {
            return BadRequest("Input unrealistic. Multiple, independent winning chains found.");
        }

        // return the winner as 'A' or 'B'. Or an 'X' for ongoing game
        return Ok(result);
    }
}

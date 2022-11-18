

class Connect4 
{

    // check input: if the input string has the right length
    public static bool check_input_length(string input, int ROW_COUNT, int COLUMN_COUNT)
    {
        if (input.Length == ROW_COUNT * COLUMN_COUNT)
        {
            return true;
        }
        return false;
    }


    // check input: if Team A has 0 or 1 more pieces than Team B
    public static bool check_nr_of_pieces(string input)
    {
        int count_A = 0;
        int count_B = 0;
        for (int i = 0; i < input.Length; i++)
        {
            // count pieces of Team A and Team B
            if (input[i] == 'A')
            {
                count_A++;
            } else if (input[i] == 'B')
            {
                count_B++;
            }
        }
        if (count_A == count_B || count_A == (count_B + 1))
        {
            return true;
        }
        return false;
    }


    // create the board as a 2D array from the input string
    public static char[,] create_board(char[,] board, string input)
    {
        int count = 0;
        for (int c = 0; c < board.GetLength(1); c++)
        {
            for (int r = 0; r < board.GetLength(0); r++)
            {
                board[r,c] = input[count];
                count++;
            } 
        }
        return board;
    }    


    // brings the board into a string format -> enables to return as TEXT/plain API response 
    // (just needed as helper method for development)
    public static string board_to_string(char[,] board)
    {
                string result = "";
        
        for (var r = 0; r < board.GetLength(0); r++)
        {
            result += "\n";
            for (var c = 0; c < board.GetLength(1); c++)
            {
                result += $"{board[(board.GetLength(0)-r-1), c]}\t";
            }

            result += "\n";
        }
        return result;
    }


    // check input: for floating pieces
    // -> checks the position below a piece
    public static bool check_floating_pieces(char[,] board)
    {
        for (int r = 1; r < board.GetLength(0); r++)
        {
            for (int c = 0; c < board.GetLength(1); c++)
            {
                if (board[r,c] == 'A' || board[r,c] == 'B')
                {
                    if (board[(r-1),c] != 'A' && board[(r-1),c] != 'B')
                    {
                        return false;
                    }
                }
                
            }
        }
        return true;
    }


    // evaluate the winner
    // 'A' or 'B' for winner, 'X' for ongoing game, 'M' for multiple independent winning chains error
    public static char is_winner(char[,] board, int WINNING_CHAIN)
    {
    

        int chain_count = 0;
        int rows = board.GetLength(0);
        int columns = board.GetLength(1);
        char current_player = 'A';
        char is_winner = 'X';
        char multiple_winner = 'M';

    // Connect4 in one row
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {   
                // if piece is found -> set current_player and reset chain_count
                if (board[r,c] != 'X' && board[r,c] != current_player)
                {
                    current_player = board[r,c];
                    chain_count = 0;
                }
                // count connecting chain of current player
                if (board[r,c] == current_player)
                {
                    chain_count++;
                    // if winner is found:
                    if(chain_count == WINNING_CHAIN)
                    {
                        // set is_winner and erase winning chain to 'X'
                        // check for multiple-winner
                        if (is_winner != 'X')
                        {
                            return multiple_winner;
                        }
                        is_winner = current_player;
                        chain_count = 0;
                        for (int i = 0; i < WINNING_CHAIN; i++)
                        {
                            board[r,c-i] = 'X';
                        }

                    }
                } else
                {
                    chain_count = 0;
                }
            }
        }

    // Connect4 in one column
        chain_count = 0;
        for (int c = 0; c < columns; c++)
        {
            for (int r = 0; r < rows; r++)
            {
                // if piece is found -> set current_player and reset chain_count
                if (board[r,c] != 'X' && board[r,c] != current_player)
                {
                    current_player = board[r,c];
                    chain_count = 0;
                }
                // count connecting chain of current player
                if (board[r,c] == current_player)
                {
                    chain_count++;
                    // if winner is found:
                    if(chain_count == WINNING_CHAIN)
                    {
                        // set is_winner and erase winning chain to 'X'
                        // check for multiple-winner
                        if (is_winner != 'X')
                        {
                            return multiple_winner;
                        }
                        is_winner = current_player;
                        for (int i = 0; i < WINNING_CHAIN; i++)
                        {
                            board[r-i,c] = 'X';
                        }
                        chain_count = 0;
                    }
                } else
                {
                    chain_count = 0;
                }
            }
        }

    // Connect4 diagonally to right
        int distance_to_frame = WINNING_CHAIN - 1;
        for (int r = 0; r < (rows - distance_to_frame); r++)
        {
            for (int c = 0; c < (columns - distance_to_frame); c++)
            {
                // if piece is found -> set current_player and reset chain_count
                if (board[r,c] != 'X' && board[r,c] != current_player)
                {
                    current_player = board[r,c];
                    chain_count = 0;
                }
                if (board[r,c] == current_player)
                {   
                    // if piece is found -> check diagonally to right from this piece
                    chain_count = 0;
                    for (int i = 0; i < WINNING_CHAIN; i++)
                    {   
                        if (board[r+i,c+i] == current_player)
                        {
                            chain_count++;
                            // if winner is found:
                            if(chain_count == WINNING_CHAIN)
                            {
                                // set is_winner and erase winning chain to 'X'
                                // check for multiple-winner
                                if (is_winner != 'X')
                                {
                                    return multiple_winner;
                                }
                                is_winner = current_player;
                                for (int j = 0; j < WINNING_CHAIN; j++)
                                {
                                    board[r+j,c+j] = 'X';
                                }
                                chain_count = 0;
                            }
                        }                            
                    }
                } else
                {
                    chain_count = 0;
                }
            }
        }

    // Connect4 in one diagonally to left
        distance_to_frame = WINNING_CHAIN - 1;
        for (int r = 0; r < (rows - distance_to_frame); r++)
        {
            for (int c = distance_to_frame; c < columns; c++)
            {
                // if piece is found -> set current_player and reset chain_count
                if (board[r,c] != 'X' && board[r,c] != current_player)
                {
                    current_player = board[r,c];
                    chain_count = 0;
                }
                if (board[r,c] == current_player)
                {   
                    // if piece is found -> check diagonally to right from this piece
                    chain_count = 0;
                    for (int i = 0; i < WINNING_CHAIN; i++)
                    {   
                        if (board[r+i,c-i] == current_player)
                        {
                            chain_count++;
                            // if winner is found:
                            if(chain_count == WINNING_CHAIN)
                            {
                                // set is_winner and erase winning chain to 'X'
                                // check for multiple-winner
                                if (is_winner != 'X')
                                {
                                    return multiple_winner;
                                }
                                is_winner = current_player;
                                for (int j = 0; j < WINNING_CHAIN; j++)
                                {
                                    board[r+j,c-j] = 'X';
                                }
                                chain_count = 0;
                            }
                        }                            
                    }
                } else
                {
                    chain_count = 0;
                }
            }
        }
    
    // in case no winner is found
        return is_winner;
    }

}

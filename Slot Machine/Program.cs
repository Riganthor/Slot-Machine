using System.ComponentModel.DataAnnotations;

namespace _Slot_Machines
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int COLUMN_ONE = 0;
            int COLUMN_TWO = 1;
            int COLUMN_THREE = 2;
            int GRID_ROW_ONE = 0;
            int GRID_ROW_TWO = 1;
            int GRID_ROW_THREE = 2;
            int THREE_LINES_MONEY = 3;
            int NO_MONEY = 0;
            int ALL_HORIZONTAL_LINES = 2;
            int COLUMN = 0;

            int gridRows = 3;
            int gridColumns = 3;
            bool playerWins = false;
            bool gameOver = false;
            int playerChoice = 1;
            int money = 0;
            int centerLine = 1;
            int winning = 0;
            int comparisonNumber = 0;


            Random rng = new Random();

            //--------------------------------------User Input-----------------------------------------------

            Console.WriteLine("Welcome to the C# casino. \nThe prices are one coin for one line or three for three lines.");
            Console.WriteLine("Please insert money:");
            money = int.Parse(Console.ReadLine());

            if (money >= THREE_LINES_MONEY)
            {
                Console.WriteLine("We have the following game modes: \n1. Center line, \n2. All horizontal lines, \n3. All columns, \n4. Diagonals.\n",
                    "Please select a game mode by entering the number infront of the game mode.");
                playerChoice = int.Parse(Console.ReadLine());
            }
            else
            {
                playerChoice = centerLine; // Default to "centerLine" if not enough money
            }

            //-------------------------------create the grid-------------------------------------
            int[,] grid = new int[gridRows, gridColumns];

            //-------------------------------Create the grid values-----------------------------
            for (int i = 0; i < gridRows; i++)
            {
                for (int j = 0; j < gridColumns; j++)
                {
                    grid[i, j] = rng.Next(0, 3); // Random number between 0 and 2
                }
            }

            //-------------------------------Draw the grid-------------------------------------
            // Draw the top border of the grid
            Console.Write("+");
            for (int j = 0; j < gridColumns; j++)
            {
                Console.Write("---+");
            }
            Console.WriteLine();

            // Draw the rows with cells
            for (int i = 0; i < gridRows; i++)
            {
                Console.Write("|");
                for (int j = 0; j < gridColumns; j++)
                {
                    // Display the grid values inside the cells
                    Console.Write($" {grid[i, j]} |"); // Use the numbers in the grid array
                }
                Console.WriteLine();

                // Draw separator between rows
                Console.Write("+");
                for (int j = 0; j < gridColumns; j++)
                {
                    Console.Write("---+");
                }
                Console.WriteLine();
            }

            //----------------------------------------Game Logic-----------------------------------------
            while (money > NO_MONEY && !gameOver)
            {
                // Deduct money based on player's choice
                if (playerChoice == centerLine)
                {
                    money--;
                }
                else if (playerChoice != centerLine && money >= THREE_LINES_MONEY)
                {
                    money -= THREE_LINES_MONEY;
                }

                //-------------------------------Check for Wins-------------------------------------
                if (playerChoice == centerLine)
                {
                    // Check if middle row has matching numbers
                    for (int j = 0; j < gridRows; j++)
                    {
                        int ROW = 1;
                        
                        comparisonNumber = grid[ROW, COLUMN];

                        if (grid[ROW, j] == comparisonNumber)
                        {
                            winning++;

                            if (winning == gridColumns)

                                playerWins = true;
                            Console.WriteLine($"You win! Column {j + COLUMN_TWO} has all the same numbers.");
                            money++;
                            break;
                        }
                    }
                }

                if (!playerWins)
                {
                    Console.WriteLine("No winning column found. Try again!");
                }
            }
            if (playerChoice == ALL_HORIZONTAL_LINES)
            {
                bool threeLineWin = false;

                // Check for 3 matching numbers in each row
                for (int i = 0; i < gridRows; i++)
                {
                    bool isWinningRow = true;

                    // Check if all elements in the current row are the same
                    for (int j = GRID_ROW_TWO; j < gridColumns; j++)
                    {
                        if (grid[i, j] != grid[i, j - GRID_ROW_TWO]) // If any element is different in the row, it's not a winning row
                        {
                            isWinningRow = false;
                            break;
                        }
                    }

                    // If the row has all matching elements
                    if (isWinningRow)
                    {
                        threeLineWin = true;
                        Console.WriteLine($"You win! Row {i + GRID_ROW_TWO} has all the same numbers.");
                        money += THREE_LINES_MONEY;  // Add winnings for the 3-line win
                    }
                }

                if (!threeLineWin)
                {
                    Console.WriteLine("No winning row found. Try again!");
                }
            }


            // Check diagonals
            if ((grid[COLUMN_ONE, GRID_ROW_ONE] == grid[COLUMN_TWO, GRID_ROW_TWO] && grid[COLUMN_TWO, GRID_ROW_TWO] == grid[COLUMN_THREE, GRID_ROW_THREE]) ||
                    (grid[COLUMN_ONE, GRID_ROW_THREE] == grid[COLUMN_TWO, GRID_ROW_TWO] && grid[COLUMN_TWO, GRID_ROW_TWO] == grid[COLUMN_THREE, GRID_ROW_ONE]))
                {
                    threeLineWin = true;
                    Console.WriteLine("You win! Diagonal has all the same numbers.");
                }

                if (!threeLineWin)
                {
                    Console.WriteLine("No winning line found for three lines. Try again!");
                }
            }

            //----------------------------------------Game Over Check--------------------------------
            if (money == NO_MONEY)
            {
                gameOver = true;
                Console.WriteLine("Game Over! You have no money left.");
            }
            else if (!playerWins)
            {
                // Allow the player to play again
                Console.WriteLine("Would you like to play again? (y/n)");
                string replayChoice = Console.ReadLine().ToLower();

                if (replayChoice == "y")
                {
                    playerWins = false; // Reset win condition for next spin
                }
                else
                {
                    gameOver = true; // End the game
                }
            }
        }
     }
}
    



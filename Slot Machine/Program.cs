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

            int gridRows = 3;
            int gridColumns = 3;
            bool playerWins = false;
            bool gameOver = false;
            int playerChoice = 1;
            int money = 0;
            int centerLine = 1;


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
                playerChoice = centerLine ; // Default to "centerLine" if not enough money
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
            while (money > 0 && !gameOver)
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
                    // Check if any column has matching numbers
                    for (int j = 0; j < gridColumns; j++)
                    {
                        if (grid[COLUMN_ONE, j] == grid[COLUMN_TWO, j] && grid[COLUMN_TWO, j] == grid[COLUMN_THREE, j])
                        {
                            playerWins = true;
                            Console.WriteLine($"You win! Column {j + COLUMN_TWO} has all the same numbers.");
                            money++;
                            break;
                        }
                    }

                    if (!playerWins)
                    {
                        Console.WriteLine("No winning column found. Try again!");
                    }
                }
                else
                {
                    // Check for 3 matching numbers in rows, columns, or diagonals
                    bool threeLineWin = false;

                    // Check rows
                    for (int i = 0; i < gridRows; i++)
                    {
                        if (grid[i, GRID_ROW_ONE] == grid[i, GRID_ROW_TWO] && grid[i, GRID_ROW_TWO] == grid[i, GRID_ROW_THREE])
                        {
                            threeLineWin = true;
                            Console.WriteLine($"You win! Row {i + GRID_ROW_TWO} has all the same numbers.");
                            money += THREE_LINES_MONEY;
                            break;
                        }
                    }

                    // Check columns
                    for (int j = 0; j < gridColumns; j++)
                    {
                        if (grid[COLUMN_ONE, j] == grid[COLUMN_TWO, j] && grid[COLUMN_TWO, j] == grid[COLUMN_THREE, j])
                        {
                            threeLineWin = true;
                            Console.WriteLine($"You win! Column {j + 1} has all the same numbers.");
                            break;
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
}

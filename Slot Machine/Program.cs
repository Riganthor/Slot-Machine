using System;

namespace _Slot_Machines
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int ONE_LINE_COST = 1;
            const int THREE_LINES_COST = 3;
            const int GRID_SIZE = 3;
            const int NO_MONEY = 0;
            const int CENTERLINE = 1;
            const int ALL_HORIZONTAL_LINES = 2;
            const int ALL_COLUMNS = 3;
            const int DIAGONALS = 4;

            int money = 0;
            int playerChoice = CENTERLINE;  // Declare this only once
            bool gameOver = false;

            Random rng = new Random();

            static int playerChoices()
            {
                Console.WriteLine("We have the following game modes: ");
                Console.WriteLine("1. Center line");
                Console.WriteLine("2. All horizontal lines");
                Console.WriteLine("3. All columns");
                Console.WriteLine("4. Diagonals");
                Console.WriteLine("Please select a game mode (1-4):");

                // Get player choice and return it
                return int.Parse(Console.ReadLine());
            }

            // User Input
            Console.WriteLine("Welcome to the C# casino!");
            Console.WriteLine("The price is 1 coin for 1 line or 3 for 3 lines.");
            Console.WriteLine("Please insert money:");
            money = int.Parse(Console.ReadLine());

            if (money >= THREE_LINES_COST)
            {
                playerChoice = playerChoices();  // Store the result into playerChoice
            }
            else
            {
                playerChoice = CENTERLINE; // Default to center line if not enough money
            }

            while (money > NO_MONEY && !gameOver)
            {
                // Create the grid and fill it with random numbers (0, 1, or 2)
                int[,] grid = new int[GRID_SIZE, GRID_SIZE];
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    for (int j = 0; j < GRID_SIZE; j++)
                    {
                        grid[i, j] = rng.Next(0, 3);
                    }
                }

                // Display the grid
                DisplayGrid(grid);

                // Deduct money based on the player's choice
                if (playerChoice == CENTERLINE)
                    money -= ONE_LINE_COST;
                else if (playerChoice == ALL_HORIZONTAL_LINES || playerChoice == ALL_COLUMNS || playerChoice == DIAGONALS)
                    money -= THREE_LINES_COST;

                // Check for winning condition based on player's choice
                bool playerWins = false;
                if (playerChoice == CENTERLINE)
                    playerWins = CheckCenterLineWin(grid);
                else if (playerChoice == ALL_HORIZONTAL_LINES)
                    playerWins = CheckHorizontalLinesWin(grid);
                else if (playerChoice == ALL_COLUMNS)
                    playerWins = CheckColumnsWin(grid);
                else if (playerChoice == DIAGONALS)
                    playerWins = CheckDiagonalsWin(grid);

                // Feedback to player
                if (playerWins)
                {
                    Console.WriteLine("You win! Your money increases.");
                    money += THREE_LINES_COST;
                }
                else
                {
                    Console.WriteLine("No win this round. Try again!");
                }

                // Check if player has money left
                if (money == NO_MONEY)
                {
                    gameOver = true;
                    Console.WriteLine("Game Over! You have no money left.");
                }
                else
                {
                    Console.WriteLine($"You have {money} coins left.");
                    Console.WriteLine("Would you like to play again? (y/n)");
                    string replayChoice = Console.ReadLine().ToLower();
                    if (replayChoice == "y")
                    {
                        if (money < THREE_LINES_COST)
                            playerChoice = CENTERLINE;
                        else
                            playerChoice = playerChoices(); // Update playerChoice if they can afford it
                    }

                    if (replayChoice != "y")
                        gameOver = true;
                }
            }
        }

        // Function to display the grid
        static void DisplayGrid(int[,] grid)
        {
            Console.WriteLine("+---+---+---+");
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write($"| {grid[i, j]} ");
                }
                Console.WriteLine("|");
                Console.WriteLine("+---+---+---+");
            }
        }

        // Check if center line (row 1) has matching numbers
        static bool CheckCenterLineWin(int[,] grid)
        {
            int comparisonValue = grid[1, 0];
            for (int i = 1; i < grid.GetLength(1); i++)
            {
                if (grid[1, i] != comparisonValue)
                    return false;
            }
            return true;
        }

        // Check if all rows have matching numbers
        static bool CheckHorizontalLinesWin(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                int comparisonValue = grid[i, 0];
                for (int j = 1; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != comparisonValue)
                        return false;
                }
            }
            return true;
        }

        // Check if all columns have matching numbers
        static bool CheckColumnsWin(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                int comparisonValue = grid[0, i];
                for (int j = 1; j < grid.GetLength(0); j++)
                {
                    if (grid[j, i] != comparisonValue)
                        return false;
                }
            }
            return true;
        }

        // Check if any diagonal has matching numbers
        static bool CheckDiagonalsWin(int[,] grid)
        {
            bool topLeftToBottomRight = true;
            bool topRightToBottomLeft = true;
            for (int i = 1; i < grid.GetLength(0); i++)
            {
                if (grid[i, i] != grid[i - 1, i - 1])
                    topLeftToBottomRight = false;
                if (grid[i, grid.GetLength(1) - 1 - i] != grid[i - 1, grid.GetLength(1) - i])
                    topRightToBottomLeft = false;
            }

            return topLeftToBottomRight || topRightToBottomLeft;
        }
    }
}

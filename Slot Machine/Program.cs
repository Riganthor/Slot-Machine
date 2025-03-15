using System;

namespace _Slot_Machines
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int ONE_LINE_COST = 1;
            const int THREE_LINES_COST = 3;
            const int NO_MONEY = 0;
            const int CENTERLINE = 1;
            const int ALL_HORIZONTAL_LINES = 2;
            const int ALL_COLUMNS = 3;
            const int DIAGONALS = 4;
            const string REPLAY = "y";

            int money = 0;
            int playerChoice = CENTERLINE;  // Declare this only once
            bool gameOver = false;
            int gridSize = 3;  // Default grid size (3x3, but could be changed to 5x5, 7x7, etc.)

            Random rng = new Random();

            static int playerChoices()
            {
                int choice = 0;
                bool validChoice = false;

                Console.WriteLine("We have the following game modes: ");
                Console.WriteLine("1. Center line");
                Console.WriteLine("2. All horizontal lines");
                Console.WriteLine("3. All columns");
                Console.WriteLine("4. Diagonals");
                Console.WriteLine("Please select a game mode (1-4):");

                // Continue asking for input until a valid choice is made
                while (!validChoice)
                {
                    string input = Console.ReadLine();

                    // Try to parse the input to an integer
                    if (int.TryParse(input, out choice))
                    {
                        // Check if the choice is within the valid range (1-4)
                        if (choice >= 1 && choice <= 4)
                        {
                            validChoice = true;  // Exit loop if the choice is valid
                        }
                        else
                        {
                            Console.WriteLine("Please select a valid game mode between 1 and 4.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    }
                }

                return choice;
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
                int[,] grid = new int[gridSize, gridSize];
                for (int i = 0; i < gridSize; i++)
                {
                    for (int j = 0; j < gridSize; j++)
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
                    playerWins = CheckCenterLineWin(grid, gridSize);
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
                    if (replayChoice == REPLAY)
                    {
                        if (money < THREE_LINES_COST)
                            playerChoice = CENTERLINE;
                        else
                            playerChoice = playerChoices(); // Update playerChoice if they can afford it
                    }

                    if (replayChoice != REPLAY)
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

        // Check if center line (row GRID_SIZE / 2) has matching numbers
        static bool CheckCenterLineWin(int[,] grid, int gridSize)
        {
            int centerLineIndex = gridSize / 2;  // Calculate center line index dynamically
            int comparisonValue = grid[centerLineIndex, 0];
            for (int i = 1; i < grid.GetLength(1); i++)
            {
                if (grid[centerLineIndex, i] != comparisonValue)
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

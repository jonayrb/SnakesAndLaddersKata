using SnakesAndLadders.Business;

namespace SnakesAndLadders.ConsoleApp;

public class Program
{
    private static void Main(string[] args)
    {
        var sixSidedDice = new SixSidedDice();
        var board = new Board(sixSidedDice);
        board.AddNewTokenFor("Player A");
        while (!board.GameIsFinished())
        {
            board.PlayerRollsADie("Player A");
            Console.WriteLine(board.GetLastOperationInformation());
        }
        Console.WriteLine(board.GetLastOperationInformation());
        Console.WriteLine("Game ends!!");
        Console.ReadLine();
    }
}
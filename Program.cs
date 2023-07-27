using ChessGame;
class Program
{
	static void Main()
	{
		ChessBoard myBoard = new();
		bool setBoard = myBoard.SetBoardSize(8);
		int boardSize = myBoard.GetBoardSize();
		Console.WriteLine($"Current board size: {boardSize}");
		for(int i = 0; i <= boardSize; i++)
		{
			if(i < boardSize)
			{
				for(int x = 0; x < boardSize; x++)
				{
					Console.Write("+---");
				}
				Console.WriteLine("+");
				for(int x = 0; x < boardSize; x++)
				{
					Console.Write("|   ");
				}
				Console.WriteLine("|");
			}
			else
			{
				for(int x = 0; x < boardSize; x++)
				{
					Console.Write("+---");
				}
				Console.WriteLine("+");
			}
		}
	}
}
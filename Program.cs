using ChessGame;
using System;

class Program
{
	static void Main()
	{
		IBoard board = new ChessBoard();
		GameRunner chessGame1 = new(board);
		DrawBoard(chessGame1);
		AddPlayer(chessGame1);
		GetPlayer(chessGame1);
	}
	
	//Drawing Board Method
	
	static void DrawBoard(GameRunner game)
	{
		bool setBoard = game.SetBoardBoundary(8);
		int boardSize = game.GetBoardBoundary();
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
	
	static void AddPlayer(GameRunner game)
	{
		IPlayer player1 = new HumanPlayer();
		bool name1 = player1.SetName("Alvaro");
		bool uid1 = player1.SetUID(1);
		
		IPlayer player2 = new HumanPlayer();
		bool name2 = player2.SetName("Aether");
		bool uid2 =player2.SetUID(2);
		
		bool addPlayer1 = game.AddPlayer(player1);
		bool addPlayer2 = game.AddPlayer(player2);
		
		Console.WriteLine($"Add player status: {addPlayer1}");
		Console.WriteLine($"Add player status: {addPlayer2}");
	}
	
	static void GetPlayer(GameRunner game)
	{
		
	}
}
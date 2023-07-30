using ChessGame;
using System;

class Program
{
	static void Main()
	{
		GameRunner chessGame = new();
		AddPlayer(chessGame);
		PlayerList(chessGame);
		DrawBoard(chessGame);
		chessGame.InitializePieces();
		
		Dictionary<IPlayer, List<Piece>> piecesList = chessGame.GetPlayerPieces();
		foreach(var pieces in piecesList)
		{
			IPlayer playerName = pieces.Key;
			List<Piece> piecesOwned = pieces.Value;
			
			Console.WriteLine($"{playerName.GetName()}, {piecesOwned}");
		}
	}
	
	//Drawing Board Method
	
	static void DrawBoard(GameRunner game)
	{
		bool setBoard = game.SetBoardBoundary(8);
		int boardSize = game.GetBoardBoundary();
		for(int i = 0; i <= boardSize; i++)
		{
			if(i < boardSize)
			{
				for(int x = 0; x < boardSize; x++)
				{
					Console.Write("+----");
				}
				Console.WriteLine("+");
				for(int x = 0; x < boardSize; x++)
				{
					Console.Write("|    ");
				}
				Console.WriteLine("|");
			}
			else
			{
				for(int x = 0; x < boardSize; x++)
				{
					Console.Write("+----");
				}
				Console.WriteLine("+");
			}
		}
		Console.WriteLine($"Current board size: {boardSize}");
	}
	
	static void AddPlayer(GameRunner game)
	{
		IPlayer player1 = new HumanPlayer();
		bool name1 = player1.SetName("Lumine");
		bool uid1 = player1.SetUID(1);
		
		IPlayer player2 = new HumanPlayer();
		bool name2 = player2.SetName("Aether");
		bool uid2 =player2.SetUID(2);
		
		bool? addPlayer1 = game.AddPlayer(player1);
		bool? addPlayer2 = game.AddPlayer(player2);
		
		Console.WriteLine($"Add player status: {addPlayer1}");
		Console.WriteLine($"Add player status: {addPlayer2}");
	}
	
	static void PlayerList(GameRunner game)
	{
		Dictionary<IPlayer, PlayerColor> playerList = game.GetPlayerList();
		foreach(var player in playerList)
		{
			IPlayer playerName = player.Key;
			PlayerColor color = player.Value;
			
			Console.WriteLine($"Currently playing: {playerName.GetName()} as {color}");
		}
	}
}
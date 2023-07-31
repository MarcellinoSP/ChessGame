using ChessGame;
using System;

class Program
{
	static void Main()
	{
		GameRunner chessGame = new();
		AddPlayer(chessGame);
		PlayerList(chessGame);
		PieceInit(chessGame);
		DrawBoard(chessGame);
	}
	
	//Drawing Board Method
	
	static void DrawBoard(GameRunner game)
	{
		bool setBoard = game.SetBoardBoundary(8);
		int boardSize = game.GetBoardBoundary();
		// Dictionary<IPlayer, List<Piece>> piecesList = game.GetPlayerPieces();
		Console.WriteLine("+----+----+----+----+----+----+----+----+");
		for(int i = 0; i < boardSize; i++)
		{
			for(int j = 0; j < boardSize; j++)
			{
				// Piece piece = piecesList.TryGetValue(playerName, out List<Piece> playerPiece);
				// if(piece != null)
				// {
				// 	Console.WriteLine($" {piece.Type()}");
				// }
				// else
				// {
				// 	Console.Write("|    ");
				// }
				Piece piece = game.CheckPiece(i, j);
				if(piece != null)
				{
					Console.Write($"| {piece.Type()} ");
				}
				else
				{
					Console.Write("|    ");
				}
			}
			Console.WriteLine("|");
	   		Console.WriteLine("+----+----+----+----+----+----+----+----+");
		}
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
	
	static void PieceInitializing(GameRunner game)
	{
		game.InitializePieces();
		Dictionary<IPlayer, List<Piece>> piecesList = game.GetPlayerPieces();
		foreach(var pieces in piecesList)
		{
			IPlayer playerName = pieces.Key;
			if(piecesList.TryGetValue(playerName, out List<Piece> playerPiece))
			{
				foreach(Piece piece in playerPiece)
				{
					Console.WriteLine($"{playerName.GetName()}, {piece.Type()}");
				}
			}
			Console.WriteLine();
		}
	}
	
	static void PieceInit(GameRunner game)
	{
		bool check = game.InitializingPiece();
		if(check)
		{
			Console.WriteLine("Piece initializing success");
		}
		else
		{
			Console.WriteLine("Please Re-check");
		}
	}
}
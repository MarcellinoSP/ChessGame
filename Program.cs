using ChessGame;
using System;

class Program
{
	static void Main()
	{
		GameRunner chessGame = new();
		AddPlayer(chessGame);
		PlayerList(chessGame);
		PieceInitializing(chessGame);
		DrawBoard(chessGame);
		
		chessGame.Move("k1", 7, 4); 		//2/8/2023 - UDAH GA NGEBUG COYY //FIX BUG //Sekarang bug di black piece //BUG FIXED
		DrawBoard(chessGame);
		chessGame.Move("k1", 4, 3);
		// CheckPiece(chessGame);
		DrawBoard(chessGame);				//New Bug = piece ngga ke capture //UPDATE: BUG FIXED
	}
	
	//Drawing Board Method
	
	static void DrawBoard(GameRunner game)
	{
		bool setBoard = game.SetBoardBoundary(8);
		int boardSize = game.GetBoardBoundary();
		Console.WriteLine("+----+----+----+----+----+----+----+----+");
		for(int i = 0; i < boardSize; i++)
		{
			for(int j = 0; j < boardSize; j++)
			{
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
		Console.WriteLine("");
	}
	
	static void AddPlayer(GameRunner game)
	{
		IPlayer player1 = new HumanPlayer();
		// Console.Write("Input player 1 name: ");
		// string player1Name = Console.ReadLine();
		string player1Name = "Alvaro";
		
		bool name1 = player1.SetName(player1Name);
		bool uid1 = player1.SetUID(1);
		bool? addPlayer1 = game.AddPlayer(player1);
		Console.WriteLine($"Add player status: {addPlayer1}");
		
		IPlayer player2 = new HumanPlayer();
		// Console.Write("Input player 2 name: ");
		// string player2Name = Console.ReadLine();
		string player2Name = "Altair";
		
		bool name2 = player2.SetName(player2Name);
		bool uid2 =player2.SetUID(2);
		bool? addPlayer2 = game.AddPlayer(player2);
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
	
	static void CheckPiece(GameRunner game)
	{
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
}
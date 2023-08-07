using ChessGame;
using System;

class Program
{
	static void Main()
	{
		PieceList listAdd = new();
		GameRunner chessGame = new GameRunner();
		AddPlayer(chessGame);
		PlayerList(chessGame);
		PieceInitializing(chessGame);
		// chessGame.IsOccupied(7,7);
		// chessGame.CapturePiece("r2", 0, 7);
		
		bool tryCheck = chessGame.Move("q1", 7, 0);
		// chessGame.Move("R2", 0, 7);

		DrawBoard(chessGame);
		
		// GetAvailableMove(chessGame, "r1");
		// // chessGame.Move("Q1", 6, 4);
		// // DrawBoard(chessGame);
		// chessGame.Move("Q1", 5, 5);
		// DrawBoard(chessGame);
		// chessGame.Move("P5", 5, 4);
		// chessGame.Move("q1", 3, 0);
		// DrawBoard(chessGame);
		// chessGame.Move("Q1", 1, 5);
		// DrawBoard(chessGame);
		// chessGame.Move("Q1", 3, 7);
		// chessGame.Move("r2", 4, 7);
		// chessGame.Move("r2", 4, 4);
		// DrawBoard(chessGame);
		
		
		// //TRIAL KING CHECK STATUS
		// bool check = chessGame.KingCheckStatus();
		// Console.WriteLine($"King checked condition: {check}");

		
		// //TRIAL SWITCHING PLAYER TURN
		// IPlayer player = chessGame.GetCurrentTurn();
		// Console.WriteLine(player.GetName());
		// chessGame.SwitchTurn();
		// IPlayer player1 = chessGame.GetCurrentTurn();
		// Console.WriteLine(player1.GetName());
		// chessGame.SwitchTurn();
		// IPlayer player2 = chessGame.GetCurrentTurn();
		// Console.WriteLine(player2.GetName());


		// Pawn pawn = new(1, 1, "P10");
		// // movement.GetMoveSet(pawn);
		// Position position = movement.GetMoveSet(pawn);
		
		// chessGame.Move("p1", 7, 4); 		//2/8/2023 - UDAH GA NGEBUG COYY //FIX BUG //Sekarang bug di black piece //BUG FIXED
		// DrawBoard(chessGame);
		// chessGame.Move("p1", 4, 3);
		// // CheckPiece(chessGame);
		// DrawBoard(chessGame);				//New Bug = piece ngga ke capture //UPDATE: BUG FIXED
		
		GameStatus(chessGame);
	}
	//Drawing Board Method
	static void DrawBoard(GameRunner game)
	{
		bool setBoard = game.SetBoardBoundary(8);
		Console.WriteLine($"Setting board boundary condition: {setBoard}");
		int boardSize = game.GetBoardBoundary();
		Console.WriteLine("+----+----+----+----+----+----+----+----+");
		for(int i = 0; i < boardSize; i++)
		{
			for(int j = 0; j < boardSize; j++)
			{
				Piece piece = game.CheckPiece(i, j);
				if(piece != null)
				{
					Console.Write($"| {piece.ID()} ");
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
		string player1Name = "Aether";
		
		bool name1 = player1.SetName(player1Name);
		bool uid1 = player1.SetUID(1);
		bool? addPlayer1 = game.AddPlayer(player1);
		Console.WriteLine($"Add player status: {addPlayer1}");
		
		IPlayer player2 = new HumanPlayer();
		// Console.Write("Input player 2 name: ");
		// string player2Name = Console.ReadLine();
		string player2Name = "Lumine";
		
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

	static void PlayerTurn(GameRunner game)
	{
		Dictionary<IPlayer, PlayerColor> playerList = game.GetPlayerList(); 
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
					Console.WriteLine($"{playerName.GetName()}, {piece.ID()}");
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
	
	static void GameStatus(GameRunner game)
	{
		GameStatus status = game.CheckGameStatus();

		// switch(status)
		// {
		// 	case GameStatus.BLACK_WIN:
		// 		Console.WriteLine("Black Side Win");
		// 	break;
		// }
		Console.WriteLine(status);
	}

	static void GetAvailableMove(GameRunner game, string pieceID)
	{
		Piece piece = game.CheckPiece(pieceID);
		Console.WriteLine(piece);
		List<Position> pieceAvailableMove = game.GetPieceAvailableMove(piece);
		
		foreach(var position in pieceAvailableMove)
		{
			Console.WriteLine($"{position.GetRank()}, {position.GetFiles()}");
			Console.WriteLine();
		}
	}
}
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
		InitializeBoard(chessGame);
		PieceInitializing(chessGame);
		Console.WriteLine("Press any key to start the game");
		Console.ReadKey();
		Console.Clear();
		
		while(true)
		{
			GameStatus(chessGame);
			DrawBoard(chessGame);
			bool check = chessGame.KingCheckStatus();
			Console.WriteLine($"King is in threat: {check}");
			IPlayer player = chessGame.GetCurrentTurn();
			Console.WriteLine($"Current turn: {player.GetName()}");
			int playerID = player.GetUID();
			PlacePiece(chessGame, playerID);
			Console.ReadKey();
			Console.Clear();
		}
	}

	static void InitializeBoard(GameRunner game)
	{
		bool board;
		bool setBoard;
		int boundary;
		do
		{
			do
			{
				Console.WriteLine("Input board size: ");
				string boundaryInput = Console.ReadLine();
				board = int.TryParse(boundaryInput, out boundary);
			}while(!board);
		setBoard = game.SetBoardBoundary(boundary);
		}while(!setBoard);
		
		Console.WriteLine($"Setting boundary for board: {setBoard} \n");
	}

	static void DrawBoard(GameRunner game)
	{
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
		Console.Write("Input player 1 name: ");
		string player1Name = Console.ReadLine();
		
		bool name1 = player1.SetName(player1Name);
		bool uid1 = player1.SetUID(1);
		bool? addPlayer1 = game.AddPlayer(player1);
		Console.WriteLine($"Add player 1 status: {addPlayer1} \n");
		
		IPlayer player2 = new HumanPlayer();
		Console.Write("Input player 2 name: ");
		string player2Name = Console.ReadLine();
		
		bool name2 = player2.SetName(player2Name);
		bool uid2 = player2.SetUID(2);
		bool? addPlayer2 = game.AddPlayer(player2);
		Console.WriteLine($"Add player 2 status: {addPlayer2} \n");
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
		Console.WriteLine();
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
				Console.Write($"{playerName.GetName()} owned piece: ");
				foreach(Piece piece in playerPiece)
				{
					Console.Write($"{piece.ID()} ");
				}
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}
	
	static bool CheckPiece(GameRunner game, string pieceID)
	{
		Dictionary<IPlayer, List<Piece>> piecesList = game.GetPlayerPieces();
		foreach(var pieces in piecesList)
		{
			IPlayer playerName = pieces.Key;
			if(piecesList.TryGetValue(playerName, out List<Piece> playerPiece))
			{
				foreach(Piece piece in playerPiece)
				{
					if(piece.ID() == pieceID)
					{
						return true;
					}
				}
			}
		}
		return false;
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

	static List <Position> GetAvailableMove(GameRunner game, string pieceID)
	{
		Piece piece = game.CheckPiece(pieceID);
		List<Position> pieceAvailableMove = game.FilterMove(piece);
		Console.WriteLine("Available Move: ");
		foreach(var position in pieceAvailableMove)
		{
			Console.WriteLine($"{position.GetRank()}, {position.GetFiles()}");
		}
		Console.WriteLine();
		return pieceAvailableMove;
	}
	
	static bool CheckCoordinate(GameRunner game, string pieceID, int rank, int files)
	{
		Piece pieceToMove = game.CheckPiece(pieceID);
		List <Position> availableMove = game.FilterMove(pieceToMove);
		foreach(var position in availableMove)
		{
			if (position.GetRank() == rank && position.GetFiles() == files)
			{
				return true;
			}
		}
		return false;
	}
	
	static void PlacePiece(GameRunner game, int playerID)
	{
		string piece;
		int rank;
		int file;
		bool rankCheck;
		bool fileCheck;
		bool checkPieceAvailable;
		bool checkCoordinate;
		List <Position> availableMove;
		
		if(playerID == 1)
		{
			do
			{
				do
				{
					Console.Write("Select piece to be placed: ");
					piece = Console.ReadLine();
					piece = piece.ToUpper();
					Console.WriteLine("Pass Add Piece");
					checkPieceAvailable = CheckPiece(game, piece);
					}while(!checkPieceAvailable);
				availableMove = GetAvailableMove(game, piece);
			}while(availableMove.Count == 0);
			do
			{
				Console.WriteLine("Choose coordinate to be moved: ");
				do
				{
					Console.WriteLine("Insert a valid rank");
					string getRank = Console.ReadLine();
					rankCheck = int.TryParse(getRank, out rank);
				}while(!rankCheck);
				do
				{
					Console.WriteLine("Insert a valid file");
					string getFile = Console.ReadLine();
					fileCheck = int.TryParse(getFile, out file);
				}while(!fileCheck);
				checkCoordinate = CheckCoordinate(game, piece, rank, file);
			}while(!checkCoordinate);
			game.Move(piece, rank, file);
		}
		else if(playerID == 2)
		{
			do
			{
				do
				{
					Console.Write("Select piece to be placed: ");
					piece = Console.ReadLine();
					piece = piece.ToLower();
					checkPieceAvailable = CheckPiece(game, piece);
					}while(!checkPieceAvailable);
				availableMove = GetAvailableMove(game, piece);
			}while(availableMove.Count == 0);
			do
			{
				Console.WriteLine("Choose coordinate to be moved: ");
				do
				{
					Console.WriteLine("Insert a valid rank");
					string getRank = Console.ReadLine();
					rankCheck = int.TryParse(getRank, out rank);
				}while(!rankCheck);
				do
				{
					Console.WriteLine("Insert a valid file");
					string getFile = Console.ReadLine();
					fileCheck = int.TryParse(getFile, out file);
				}while(!fileCheck);
				checkCoordinate = CheckCoordinate(game, piece, rank, file);
			}while(!checkCoordinate);
			game.Move(piece, rank, file);
		}
	}
	
}
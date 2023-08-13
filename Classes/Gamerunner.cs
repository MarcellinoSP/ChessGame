namespace ChessGame;

public delegate void SwitchPlayer();

public class GameRunner
{
	private IBoard _chessBoard;
	private ChessMove _chessMove;
	private PlayerColor _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList;
	private Dictionary<IPlayer, List<Piece>> _piecesList;
	private GameStatus _gameStatus;
	private SwitchPlayer _switchPlayer;
	
	public GameRunner()
	{
		_chessBoard = new ChessBoard();
		_playerList = new Dictionary<IPlayer, PlayerColor>();
		_piecesList = new Dictionary<IPlayer, List<Piece>>();
		_chessMove = new ChessMove();
		_currentTurn = PlayerColor.WHITE;
		_switchPlayer += SwitchTurn;
	}
	
	public List<Position> GetPieceAvailableMove(Piece piece)
	{
		IMoveSet moveSet = _chessMove.GetMoveSet(piece);
		List<Position> pieceMovement = moveSet.movement(piece);
		return pieceMovement;
	}
	
	public List <Position> GetPieceAvailableMove(string pieceID)
	{
		foreach (var playerPiece in _piecesList.Values)
		{
			foreach (var piece in playerPiece)
			{
				if(piece.ID() == pieceID)
				{
					IMoveSet moveSet = _chessMove.GetMoveSet(piece);
					List <Position> pieceMovement = moveSet.movement(piece);
					return pieceMovement;
				}
			}
		}
		return null;
	}
	
	public bool? AddPlayer(IPlayer player)
	{
		PlayerColor playerColor = new();
		if(_playerList.Count == 0)
		{
			playerColor = PlayerColor.WHITE;
		}
		else
		{
			playerColor = PlayerColor.BLACK;
		}
		if(!_playerList.ContainsKey(player))
		{
			_playerList.Add(player, playerColor);
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public Dictionary<IPlayer, PlayerColor> GetPlayerList()
	{
		return _playerList;
	}
	
	public bool SetBoardBoundary(int size)
	{
		bool condition = _chessBoard.SetBoardSize(size);
		if(size  <= 0)
		{
			return false;
		}
		return condition;
	}
	
	public int GetBoardBoundary()
	{
		int boundary = _chessBoard.GetBoardSize();
		return boundary;
	}
	
	public Dictionary<IPlayer, List<Piece>> GetPlayerPieces()
	{
		return _piecesList;
	}
	
	public bool InitializePieces()		//DIUBAH PAKE JSON BIAR SIMPEL
	{
		int i = 0;
		foreach(var player in _playerList.Keys)
		{
			List <Piece> pieces = new List<Piece>();
			if(i == 0)
			{
				pieces.Add(new Pawn (6, 0, "Pawn", "P1"));
				pieces.Add(new Pawn (6, 1, "Pawn", "P2"));
				pieces.Add(new Pawn (6, 2, "Pawn", "P3"));
				pieces.Add(new Pawn (6, 3, "Pawn", "P4"));
				pieces.Add(new Pawn (6, 4, "Pawn", "P5"));
				pieces.Add(new Pawn (6, 5, "Pawn", "P6"));
				pieces.Add(new Pawn (6, 6, "Pawn", "P7"));
				pieces.Add(new Pawn (6, 7, "Pawn", "P8"));
				
				pieces.Add(new Rook (7, 0, "Rook", "R1"));
				pieces.Add(new Knight (7, 1, "Knight", "N1"));
				pieces.Add(new Bishop (7, 2, "Bishop", "B1"));
				pieces.Add(new Queen (7, 3, "Queen", "Q1"));
				pieces.Add(new King (7, 4, "King", "K1"));
				pieces.Add(new Bishop (7, 5, "Bishop", "B2"));
				pieces.Add(new Knight (7, 6, "Knight", "N2"));
				pieces.Add(new Rook (7, 7, "Rook", "R2"));
				i++;
			}
			else
			{
				pieces.Add(new Pawn (1, 0, "Pawn", "p1"));
				pieces.Add(new Pawn (1, 1, "Pawn", "p2"));
				pieces.Add(new Pawn (1, 2, "Pawn", "p3"));
				pieces.Add(new Pawn (1, 3, "Pawn", "p4"));
				pieces.Add(new Pawn (1, 4, "Pawn", "p5"));
				pieces.Add(new Pawn (1, 5, "Pawn", "p6"));
				pieces.Add(new Pawn (1, 6, "Pawn", "p7"));
				pieces.Add(new Pawn (1, 7, "Pawn", "p8"));
				
				pieces.Add(new Rook (0, 0, "Rook", "r1"));
				pieces.Add(new Knight (0, 1, "Knight", "n1"));
				pieces.Add(new Bishop (0, 2, "Bishop", "b1"));
				pieces.Add(new Queen (0, 3, "Queen", "q1"));
				pieces.Add(new King (0, 4, "King", "k1"));
				pieces.Add(new Bishop (0, 5, "Bishop", "b2"));
				pieces.Add(new Knight (0, 6, "Knight", "n2"));
				pieces.Add(new Rook (0, 7, "Rook", "r2"));
			}
			_piecesList[player] = pieces;
		}
		return true;
	}
	
	public Piece? CheckPiece(int rank, int files)
	{
   		foreach (var playerPieces in _piecesList.Values)
   		{
	  		foreach (var piece in playerPieces)
	  		{
		 		if (piece.GetRank() == rank && piece.GetFiles() == files)
		 		{
					return piece; 
		 		}
	  		}
   		}
   		return null; 
	}
	
	public Piece? CheckPiece(string pieceID)
	{
		foreach (var playerPieces in _piecesList.Values)
		{
			foreach (var piece in playerPieces)
			{
				if (piece.ID().Equals(pieceID))
				{
					return piece;
				}
			}
		}
		return null;
	}
	
	public List <Position> FilterMove(Piece piece)
	{
		int pieceRank = piece.GetRank();
		int pieceFiles = piece.GetFiles();
		string pieceID = piece.ID();
		
		List <Position> availableMove = GetPieceAvailableMove(piece);
		List <Position> filteredMove = new List <Position>();
		
		foreach(var position in availableMove)
		{
			int checkRank = position.GetRank();
			int checkFiles = position.GetFiles();
			bool blocked = IsOccupied(pieceID, checkRank, checkFiles);

			if (pieceID.Contains('P'))
			{
				// Console.WriteLine($"{pieceRank}, {pieceFiles}");
				bool occupiedLeft = IsOccupied(pieceID, pieceRank - 1, pieceFiles + 1);
				// Console.WriteLine($"{pieceRank - 1}, {pieceFiles + 1}");
				// Console.WriteLine(occupiedLeft);
			}
			
			if ((pieceID.Contains('N') || pieceID.Contains('n')) && !blocked)
			{
				filteredMove.Add(new Position(checkRank, checkFiles));
			}
			else if(!blocked && IsPathClear(pieceID, checkRank, checkFiles, pieceRank, pieceFiles))
			{
				filteredMove.Add(new Position(checkRank, checkFiles));
			}
		}
		return filteredMove;
	}
	
	public bool Move(string pieceID, int rank, int files)
	{
		Piece pieceToMove = CheckPiece(pieceID);
		
		List <Position> filteredMove = FilterMove(pieceToMove);
		
		foreach(var pos in filteredMove)
		{
			if(pos.GetRank() == rank && pos.GetFiles() == files)
			{
				bool capture = CapturePiece(pieceID, rank, files);
				pieceToMove.SetRank(rank);
				pieceToMove.SetFiles(files);
				if(pieceToMove is Pawn pawn)
				{
					pawn.SetIsMoved(true);
				}
				// _switchPlayer?.Invoke();
				return true;
			}
		}
		return false;
	}
	
	private bool IsPathClear(string pieceID, int currentRank, int currentFile, int targetRank, int targetFile)
	{	
		int rankDelta = targetRank - currentRank;
		int fileDelta = targetFile - currentFile;

		int rankIncrement = Math.Sign(rankDelta);
		int fileIncrement = Math.Sign(fileDelta);
		
		int rank = currentRank + rankIncrement;
		int file = currentFile + fileIncrement;
		
		while (rank != targetRank || file != targetFile)
		{
			if (IsOccupied(pieceID, rank, file))
			{
				return false;
			}
			rank += rankIncrement;
			file += fileIncrement;
			if (rank == targetRank && file == targetFile)
			{
				break;
			}
		}
		return true;
	}
	
	public bool IsOccupied(string pieceID, int rank, int files)
	{
		foreach (var playerPieces in _piecesList.Values)
		{
			foreach(var piece in playerPieces)
			{
				// string pieceID1 = piece.ID();
				// int pieceRank = piece.GetRank();
				// int pieceFiles = piece.GetFiles();
				if(piece.GetRank() == rank && piece.GetFiles() == files)
				{
					if(piece.ID().Any(Char.IsUpper) && pieceID.Any(Char.IsUpper) || piece.ID().Any(Char.IsLower) && pieceID.Any(Char.IsLower))
					{
						return true;
					}
					return false;
				}
			}
		}
		return false;
	}

	public bool IsOccupied(int rank, int files)
	{
		foreach (var playerPieces in _piecesList.Values)
		{
			foreach(var piece in playerPieces)
			{
				if(piece.GetRank() == rank && piece.GetFiles() == files)
				{
					return true;
				}
			}
		}
		return false;
	}
	
	public bool CapturePiece(string pieceID, int rank, int files)
	{
		bool occupied = IsOccupied(rank, files);
		if (occupied)
		{
			foreach (var playerPieces in _piecesList.Values)
			{
				foreach (var piece in playerPieces)
				{
					if(piece.GetRank() == rank && piece.GetFiles() == files)
					{
						if(piece.ID().Any(Char.IsUpper) && pieceID.Any(Char.IsUpper) || piece.ID().Any(Char.IsLower) && pieceID.Any(Char.IsLower))
						{
							
						}
						else
						{
							piece.ChangeStatus();
							playerPieces.Remove(piece);
							if(piece.ID() == "K1")
							{
								SetGameStatus(GameStatus.BLACK_WIN);
							}
							else if(piece.ID() == "k1")
							{
								SetGameStatus(GameStatus.WHITE_WIN);
							}
							return true;
						}
					}
					
				}
			}
		}
		return false;
	}
	
	private bool Checked(Piece currentPiece, Piece opponentPiece)
	{
		return false;
	}
	
	public bool KingCheckStatus()
	{
		PlayerColor currentColorTurn = _currentTurn;
		IPlayer opponentPlayer = new HumanPlayer();
		List <Piece> opponentPieces = new List <Piece>();
		Piece kingToCheck = new King();
		foreach(KeyValuePair <IPlayer, PlayerColor> playerTurn in _playerList)
		{
			if(playerTurn.Value.Equals(currentColorTurn))
			{
				kingToCheck = CheckPiece("K1");
			}
			else
			{
				kingToCheck = CheckPiece("k1");
			}
			
			if(!playerTurn.Value.Equals(currentColorTurn))
			{
				opponentPlayer = playerTurn.Key;
			}
		}
		if(_piecesList.TryGetValue(opponentPlayer, out var pieces))
		{
			opponentPieces = pieces;
		}
		
		return false;
	}
	
	// public bool KingCheckStatus()
	// {
	// 	int boardSize = GetBoardBoundary();
	// 	if(_currentTurn == PlayerColor.WHITE)
	// 	{
	// 		Piece king = CheckPiece("K1");
	// 		int kingRank = king.GetRank();
	// 		int kingFiles = king.GetFiles();
	// 		foreach(var pieceList in _piecesList.Values)
	// 		{
	// 			foreach(var piece in pieceList)
	// 			{
	// 				if(piece is Rook && piece.ID().Contains('r'))
	// 				{
	// 					if(piece.GetRank() == kingRank || piece.GetFiles() == kingFiles)
	// 					{
	// 						for(int i = 1; i < boardSize; i++)
	// 						{
	// 							bool blockedUp = IsOccupied("K1", kingRank - i, kingFiles);
	// 							bool blockedRight = IsOccupied("K1", kingRank, kingFiles - i);
	// 							bool blockedBottom = IsOccupied("K1", kingRank + i, kingFiles);
	// 							bool blockedLeft = IsOccupied("K1", kingRank, kingFiles + i);
	// 							if(!blockedUp || !blockedRight || !blockedBottom || !blockedLeft)
	// 							{
	// 								SetGameStatus(GameStatus.CHECK);
	// 								return true;
	// 							}
	// 						}
	// 					}
	// 				}
	// 				if(piece is Bishop && piece.ID().Contains('b'))
	// 				{
	// 					for(int i = 1; i < boardSize; i++)
	// 					{
	// 						bool blockedUp = IsOccupied("K1", kingRank + i, kingFiles + i);
	// 						bool blockedRight = IsOccupied("K1", kingRank + i, kingFiles - i);
	// 						bool blockedBottom = IsOccupied("K1", kingRank - i, kingFiles + i);
	// 						bool blockedLeft = IsOccupied("K1", kingRank - i, kingFiles - i);
	// 						if(!blockedUp || !blockedRight || !blockedBottom || !blockedLeft)
	// 						{
	// 							Console.WriteLine("Bishop");
	// 							SetGameStatus(GameStatus.CHECK);
	// 							return true;
	// 						}
	// 					}
	// 				}
	// 			}
	// 		}
	// 	}
	// 	else if(_currentTurn == PlayerColor.BLACK)
	// 	{
	// 		Piece king = CheckPiece("k1");
	// 		int kingRank = king.GetRank();
	// 		int kingFiles = king.GetFiles();
	// 		// return false;
	// 	}
	// 	return false;
	// }
	
	public bool PawnPromotion(Piece piece, PromoteTo promoteTo)
	{
		return true;
	}
	
	public bool SetGameStatus(GameStatus status)
	{
		_gameStatus = status;
		return true;
	}
	
	public GameStatus CheckGameStatus()
	{
		return _gameStatus;
	}

	public void SwitchTurn()
	{
		_currentTurn = (_currentTurn == PlayerColor.WHITE) ? PlayerColor.BLACK : PlayerColor.WHITE;
	}

	public IPlayer? GetCurrentTurn()
	{
		foreach(KeyValuePair<IPlayer, PlayerColor> playerTurn in _playerList)
		{
			if(playerTurn.Value == _currentTurn)
			{
				return playerTurn.Key;
			}
		}
		return null;
	}
	
}
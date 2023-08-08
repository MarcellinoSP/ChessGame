namespace ChessGame;

public class GameRunner
{
	private IBoard _chessBoard;
	private ChessMove _chessMove;
	private PlayerColor _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList;
	private Dictionary<IPlayer, List<Piece>> _piecesList;
	private GameStatus _gameStatus;
	private bool _blackPawnMoved;
	private bool _whitePawnMoved;
	
	public GameRunner()
	{
		_chessBoard = new ChessBoard();
		_playerList = new Dictionary<IPlayer, PlayerColor>();
		_piecesList = new Dictionary<IPlayer, List<Piece>>();
		_chessMove = new ChessMove();
		_currentTurn = PlayerColor.WHITE;
		_blackPawnMoved = false;
		_whitePawnMoved = false;
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
	
	public bool Move(string pieceID, int rank, int files)
	{
		Piece pieceToMove = CheckPiece(pieceID);
		int pieceRank = pieceToMove.GetRank();
		int pieceFiles = pieceToMove.GetFiles();
		int index = 1;
		
		//Filter Movement Trial
		List <Position> positionAvailable = GetPieceAvailableMove(pieceToMove);
		List <Position> filteredMove = new List <Position>();
		
		foreach(var position in positionAvailable)
		{
			int checkRank = position.GetRank();
			int checkFiles = position.GetFiles();
			bool blocked = IsOccupied(pieceID, checkRank, checkFiles);
			
			//BETTER DELETE LANGSUNG KE DICTIONARY NYA
			if(pieceID.Contains('P') && pieceToMove is Pawn pawnWhite)
			{
				if(pawnWhite.IsMoved() == true && index == 2)
				{
					blocked = true;
				}
			}
			else if (pieceID.Contains('p') && pieceToMove is Pawn pawnBlack)
			{
				if(pawnBlack.IsMoved() == true && index == 2)
				{
					blocked = true;
				}
			}
			//BETTER DELETE LANGSUNG KE DICTIONARY NYA
			
			if(pieceID.Contains('N') || pieceID.Contains('n'))
			{
				if(!blocked)
				{
					filteredMove.Add(new Position(checkRank, checkFiles));
				}
			}
			
			if(!blocked && IsPathClear(pieceID, checkRank, checkFiles, pieceRank, pieceFiles))
			{
				filteredMove.Add(new Position(checkRank, checkFiles));
				index++;
			}
		}
		
		foreach(var pos in filteredMove)
		{
			Console.WriteLine($"{pos.GetRank()}, {pos.GetFiles()}");
			if(pos.GetRank() == rank && pos.GetFiles() == files)
			{
				bool capture = CapturePiece(pieceID, rank, files);
				pieceToMove.SetRank(rank);
				pieceToMove.SetFiles(files);
				if(pieceToMove is Pawn pawn)
				{
					pawn.SetIsMoved(true);
					Console.WriteLine(pawn.IsMoved());
				}
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
				if(piece.GetRank() == rank && piece.GetFiles() == files)
				{
					// Console.WriteLine("Occupied");
					if(piece.ID().Any(Char.IsUpper) && pieceID.Any(Char.IsUpper) || piece.ID().Any(Char.IsLower) && pieceID.Any(Char.IsLower))
					{
						// Console.WriteLine("Double Uppercase detected");
						// Console.WriteLine($"Occupied by: {piece.ID()}, not your enemy \n");
						return true;
					}
					// Console.WriteLine($"Piece occupied by: {piece.ID()}, but it's your enemy! \n");
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
					// Console.WriteLine($"Occupied by {piece.ID()}");
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
							return true;
						}
					}
					
				}
			}
		}
		return false;
	}

	public bool CapturePiece(Piece piece)
	{
		foreach(var pieces in _piecesList.Values)
		{
			foreach(var pieceList in pieces)
			{
				bool status = pieceList.GetStatus();
				if(status)
				{
					pieces.Remove(piece);
					return true;
				}
			}
		}
		return false;
	}
	
	// public bool KingCheckStatus() //LOGIC DIBAWAH MASIH JELEK, BETTER DI ASOSIASI DENGAN MOVEMENT
	// {
	// 	int kingRank = 0;
	// 	int kingFiles = 0;
	// 	int boardSize = GetBoardBoundary();
	// 	foreach(var pieceList in _piecesList.Values)
	// 	{
	// 		foreach(var piece in pieceList)
	// 		{
	// 			if(piece.ID() == "K1")
	// 			{
	// 				kingRank = piece.GetRank();
	// 				kingFiles = piece.GetFiles();
	// 				Console.WriteLine($"{kingRank}, {kingFiles}");
	// 			}
	// 		}
	// 	}
	// 	foreach(var pieceList in _piecesList.Values)
	// 	{
	// 		foreach(var piece in pieceList)
	// 		{
	// 			if(piece.GetFiles() == kingFiles || piece.GetRank() == kingRank)
	// 			{
	// 				switch(piece.ID())
	// 				{
	// 					case "r1":
	// 						Console.WriteLine($"Checked by {piece.ID()}!");
	// 						return true;
	// 						break;
	// 					case "r2":
	// 						Console.WriteLine($"Checked by {piece.ID()}!");
	// 						return true;
	// 						break;
	// 					case "q1":
	// 						Console.WriteLine($"Checked by {piece.ID()}!");
	// 						return true;
	// 						break;
	// 				}
	// 			}
	// 		}
	// 	}
	// 	return false;
	// }
	
	public bool KingCheckStatus()
	{
		Piece piece = CheckPiece("K1");
		return true;
	}
	
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

	public IPlayer GetCurrentTurn()
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
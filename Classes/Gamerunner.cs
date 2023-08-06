namespace ChessGame;

public class GameRunner
{
	private IBoard _chessBoard;
	private ChessMove _chessMove;
	private PlayerColor _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList;
	private Dictionary<IPlayer, List<Piece>> _piecesList;
	private GameStatus _gameStatus;
	
	public GameRunner()
	{
		_chessBoard = new ChessBoard();
		_playerList = new Dictionary<IPlayer, PlayerColor>();
		_piecesList = new Dictionary<IPlayer, List<Piece>>();
		_chessMove = new ChessMove();
		_currentTurn = PlayerColor.WHITE;
	}
	
	public List<Position> GetPieceAvailableMove(Piece piece)
	{
		IMoveSet moveSet = _chessMove.GetMoveSet(piece);
		List<Position> pieceMovement = moveSet.movement(piece);
		return pieceMovement;
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
	
	public Piece CheckPieceID(string pieceID)
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
		bool occupied = IsOccupied(pieceID, rank, files);
		if(!occupied)
		{
			foreach (var playerPieces in _piecesList.Values)
			{
				foreach(var piece in playerPieces)
				{
					if(piece.ID() == pieceID)
					{											
						//MUNGKIN TAMBAH CHECK AVAILABLE MOVE DISINI
						List<Position> positionAvailable = GetPieceAvailableMove(piece);
						foreach(var position in positionAvailable)
						{
							Console.WriteLine(piece.ID());
							if(position.GetRank() == rank && position.GetFiles() == files)
							{
								piece.SetRank(rank);
								piece.SetFiles(files);
								return true;
							}
						}
						return false;
					}
				}
			}
		}
		return false;
	}
	
	public bool IsOccupied(string pieceID, int rank, int files)
	{
		foreach (var playerPieces in _piecesList.Values)
		{
			foreach(var piece in playerPieces)
			{
				if(piece.GetRank() == rank && piece.GetFiles() == files)
				{
					Console.WriteLine("Occupied");
					if(piece.ID().Any(Char.IsUpper) && pieceID.Any(Char.IsUpper))
					{
						Console.WriteLine("Double Uppercase detected");
						Console.WriteLine($"Occupied by: {piece.ID()}, not your enemy");
						return true;
					}
					Console.WriteLine($"Piece occupied by: {piece.ID()}, but it's your enemy!");
					piece.ChangeStatus();
					bool capturedStatus = CapturePiece(piece);
					Console.WriteLine($"Piece captured status: {capturedStatus}");
					return false;
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
	
	public bool KingCheckStatus() //LOGIC DIBAWAH MASIH JELEK, BETTER DI ASOSIASI DENGAN MOVEMENT
	{
		int kingRank = 0;
		int kingFiles = 0;
		int boardSize = 8;
		foreach(var pieceList in _piecesList.Values)
		{
			foreach(var piece in pieceList)
			{
				if(piece.Type() == "K1")
				{
					kingRank = piece.GetRank();
					kingFiles = piece.GetFiles();
				}
			}
		}
		foreach(var pieceList in _piecesList.Values)
		{
			foreach(var piece in pieceList)
			{
				if(piece.GetFiles() == kingFiles)
				{
					switch(piece.Type())
					{
						case "r1":
							Console.WriteLine($"Checked by {piece.Type()}!");
							return true;
							break;
						case "r2":
							Console.WriteLine($"Checked by {piece.Type()}!");
							return true;
							break;
						case "q1":
							Console.WriteLine($"Checked by {piece.Type()}!");
							return true;
							break;
					}
				}
			}
		}
		return false;
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
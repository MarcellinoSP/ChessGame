namespace ChessGame;

public class GameRunner
{
	private IBoard _chessBoard;
	private ChessMove _movementLibrary;
	private IPlayer _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList;
	private Dictionary<IPlayer, List<Piece>> _piecesList;
	private GameStatus _gameStatus;
	
	public GameRunner()
	{
		_chessBoard = new ChessBoard();
		_playerList = new Dictionary<IPlayer, PlayerColor>();
		_piecesList = new Dictionary<IPlayer, List<Piece>>();
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
	
	public bool InitializePieces()
	{
		int i = 0;
		foreach(var player in _playerList.Keys)
		{
			List <Piece> pieces = new List<Piece>();
			if(i == 0)
			{
				pieces.Add(new Pawn (6, 0, "P1"));
				pieces.Add(new Pawn (6, 1, "P2"));
				pieces.Add(new Pawn (6, 2, "P3"));
				pieces.Add(new Pawn (6, 3, "P4"));
				pieces.Add(new Pawn (6, 4, "P5"));
				pieces.Add(new Pawn (6, 5, "P6"));
				pieces.Add(new Pawn (6, 6, "P7"));
				pieces.Add(new Pawn (6, 7, "P8"));
				
				pieces.Add(new Rook (7, 0, "R1"));
				pieces.Add(new Knight (7, 1, "N1"));
				pieces.Add(new Bishop (7, 2, "B1"));
				pieces.Add(new Queen (7, 3, "Q1"));
				pieces.Add(new King (7, 4, "K1"));
				pieces.Add(new Bishop (7, 5, "B2"));
				pieces.Add(new Knight (7, 6, "N2"));
				pieces.Add(new Rook (7, 7, "R2"));
				i++;
			}
			else
			{
				pieces.Add(new Pawn (1, 0, "p1"));
				pieces.Add(new Pawn (1, 1, "p2"));
				pieces.Add(new Pawn (1, 2, "p3"));
				pieces.Add(new Pawn (1, 3, "p4"));
				pieces.Add(new Pawn (1, 4, "p5"));
				pieces.Add(new Pawn (1, 5, "p6"));
				pieces.Add(new Pawn (1, 6, "p7"));
				pieces.Add(new Pawn (1, 7, "p8"));
				
				pieces.Add(new Rook (0, 0, "r1"));
				pieces.Add(new Knight (0, 1, "n1"));
				pieces.Add(new Bishop (0, 2, "b1"));
				pieces.Add(new Queen (0, 3, "q1"));
				pieces.Add(new King (0, 4, "k1"));
				pieces.Add(new Bishop (0, 5, "b2"));
				pieces.Add(new Knight (0, 6, "n2"));
				pieces.Add(new Rook (0, 7, "r2"));
			}
			_piecesList[player] = pieces;
		}
		return true;
	}
	
	public Piece CheckPiece(int rank, int files)
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
	
	public bool Move(string type, int rank, int files)
	{
		bool occupied = IsOccupied(type, rank, files);
		if(occupied)
		{
			return false;
		}
		else
		{
			foreach (var playerPieces in _piecesList.Values)	//ini cek di dalam list
			{
				// Piece piece = playerPieces.FirstOrDefault(pieceType => pieceType.Type() == type); //Kalo pake ini ngebug di black piece
				foreach(var pieceToMove in playerPieces)
				{
					if(pieceToMove.Type() == type)
					{
						pieceToMove.SetRank(rank);
						pieceToMove.SetFiles(files);
						return true;
					}
				}
				// Console.WriteLine(piece.Type());
				// piece.SetRank(rank);
				// piece.SetFiles(files);
				// return true;
			}
		}
		return false;
	}
	
	public bool IsOccupied(string type, int rank, int files)
	{
		foreach (var playerPieces in _piecesList.Values)
		{
			foreach(var piece in playerPieces)
			{
				if(piece.GetRank() == rank && piece.GetFiles() == files)
				{
					Console.WriteLine("Occupied");
					if(piece.Type().Any(Char.IsUpper) && type.Any(Char.IsUpper))
					{
						Console.WriteLine("Double Uppercase detected");
						Console.WriteLine($"Occupied by: {piece.Type()}, not your enemy");
						return true;
					}
					Console.WriteLine($"Piece occupied by: {piece.Type()}, but it's your enemy!");
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
					Console.WriteLine($"Piece to remove: {pieceList.Type()}");
					if(pieceList.Type() == "K1")
					{
						SetGameStatus(GameStatus.BLACK_WIN);
					}
					else if(pieceList.Type() == "k1")
					{
						SetGameStatus(GameStatus.WHITE_WIN);
					}
					else
					{
						SetGameStatus(GameStatus.ONGOING);
					}
					pieces.Remove(piece);
					return true;
				}
			}
		}
		return false;
	}
	
	public bool KingCheckStatus()
	{
		// foreach(var pieces in _piecesList)
		// {
		// 	foreach(var piece in pieces)
		// 	{
		// 		if(piece.Type())
		// 	}
		// }
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
}
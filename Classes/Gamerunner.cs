namespace ChessGame;

public class GameRunner
{
	private IBoard _chessBoard;
	private ChessMove _movementLibrary;
	private IPlayer _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList;
	private Dictionary<IPlayer, List<Piece>> _piecesList; //Is This Necessary?
	private List<Piece> _listOfPiece;
	private GameStatus _gameStatus;
	
	public GameRunner()
	{
		_chessBoard = new ChessBoard();
		_playerList = new Dictionary<IPlayer, PlayerColor>();
		_piecesList = new Dictionary<IPlayer, List<Piece>>();
		_listOfPiece = new List<Piece>();
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
	
	//DOES CODE BELOW NECESSARY?
	public bool InitializePieces()
	{
		int i = 0;
		foreach(var player in _playerList.Keys)
		// for(var i = 0; i < _playerList.Keys; i++)
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
			}
			_piecesList[player] = pieces;
		}
		return true;
	}
	
	public bool InitializingPiece()
	{
		_listOfPiece.Add(new Pawn (6, 0, "P1"));
		_listOfPiece.Add(new Pawn (6, 1, "P2"));
		_listOfPiece.Add(new Pawn (6, 2, "P3"));
		_listOfPiece.Add(new Pawn (6, 3, "P4"));
		_listOfPiece.Add(new Pawn (6, 4, "P5"));
		_listOfPiece.Add(new Pawn (6, 5, "P6"));
		_listOfPiece.Add(new Pawn (6, 6, "P7"));
		_listOfPiece.Add(new Pawn (6, 7, "P8"));
		
		_listOfPiece.Add(new Rook (7, 0, "R1"));
		_listOfPiece.Add(new Knight (7, 1, "N1"));
		_listOfPiece.Add(new Bishop (7, 2, "B1"));
		_listOfPiece.Add(new Queen (7, 3, "Q1"));
		_listOfPiece.Add(new King (7, 4, "K1"));
		_listOfPiece.Add(new Bishop (7, 5, "B2"));
		_listOfPiece.Add(new Knight (7, 6, "N2"));
		_listOfPiece.Add(new Rook (7, 7, "R2"));

		_listOfPiece.Add(new Pawn (1, 0, "p1"));
		_listOfPiece.Add(new Pawn (1, 1, "p2"));
		_listOfPiece.Add(new Pawn (1, 2, "p3"));
		_listOfPiece.Add(new Pawn (1, 3, "p4"));
		_listOfPiece.Add(new Pawn (1, 4, "p5"));
		_listOfPiece.Add(new Pawn (1, 5, "p6"));
		_listOfPiece.Add(new Pawn (1, 6, "p7"));
		_listOfPiece.Add(new Pawn (1, 7, "p8"));
		
		_listOfPiece.Add(new Rook (0, 0, "r1"));
		_listOfPiece.Add(new Knight (0, 1, "n1"));
		_listOfPiece.Add(new Bishop (0, 2, "b1"));
		_listOfPiece.Add(new Queen (0, 3, "q1"));
		_listOfPiece.Add(new King (0, 4, "k1"));
		_listOfPiece.Add(new Bishop (0, 5, "b2"));
		_listOfPiece.Add(new Knight (0, 6, "n2"));
		_listOfPiece.Add(new Rook (0, 7, "r2"));
		
		return true;
	}
	
	public Piece CheckPiece(int rank, int files)
	{
		return _listOfPiece.FirstOrDefault(piece => piece.GetRank() == rank && piece.GetFiles() == files);
	}
}
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
	
	//CODE BELOW STILL CONFUSING
	public bool InitializePieces()
	{
		List <Piece> pieces = new List <Piece>(); 
		foreach(var player in _playerList.Keys)
		{
			for(int i = 1; i <= 8; i++)
			{
				Pawn pawn = new Pawn(2, i);
				pieces.Add(pawn);
			}
			_piecesList[player] = pieces;
		}
		return true;
	}
	//CODE ABOVE STILL CONFUSING
}
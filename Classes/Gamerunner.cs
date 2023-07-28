namespace ChessGame;

public class GameRunner
{
	private IBoard _chessBoard;
	private ChessMove _movementLibrary;
	private IPlayer _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList = new Dictionary<IPlayer, PlayerColor>();
	private Dictionary<IPlayer, List<Piece>> _piecesList = new Dictionary<IPlayer, List<Piece>>();
	private GameStatus _gameStatus;
	
	public GameRunner()
	{
		_chessBoard = new ChessBoard();
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
	//CODE BELOW STILL ERROR
	// public bool InitializePieces()
	// {
	// 	Pawn[] pawn = new Pawn[8]();
	// 	for(int i = 0; i < 8; i++)
	// 	{
	// 		_piecesList.Add(_playerList, pawn[i]);
	// 	}
	// 	return true;
	// }
	//CODE ABOVE STILL ERROR
}
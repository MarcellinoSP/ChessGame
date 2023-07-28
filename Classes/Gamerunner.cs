namespace ChessGame;

public class GameRunner
{
	private IBoard _chessBoard;
	private IPlayer _currentTurn;
	private Dictionary<IPlayer, PlayerColor> _playerList = new Dictionary<IPlayer, PlayerColor>();
	
	public GameRunner(IBoard chessBoard)
	{
		_chessBoard = chessBoard;
	}
	public bool AddPlayer(IPlayer player)
	{
		PlayerColor playerColor = new();
		if(_playerList.Count == 0)
		{
			playerColor = PlayerColor.WHITE;
		}
		_playerList.Add(player, playerColor);
		return true;
	}
	public Dictionary<string, PlayerColor> GetPlayerList()
	{
		Dictionary<string, PlayerColor> playerList = new Dictionary<string, PlayerColor>();
		foreach(var player in _playerList)
		{
			IPlayer playerName = player.Key;
			PlayerColor color = player.Value;
			
			string name = playerName.GetName();
			playerList.Add(name, color);
		}
		return playerList;
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
}
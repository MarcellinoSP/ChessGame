namespace ChessGame;
public class ChessMove
{
	private Dictionary <Piece, IMoveSet> _moveSet = new Dictionary <Piece, IMoveSet>();
	private int _moveBoundary;
	
	public bool AddPiece(KeyValuePair<Piece, IMoveSet> addPiece)
	{
		return true;
	}
	
	public bool SetMoveBoundary(IBoard board)
	{
		_moveBoundary = board.GetBoardSize();
		return true;
	}
	
	public IMoveSet GetMoveSet(Piece piece)
	{
		return _moveSet[piece];
	}
}

public class PawnMoveSingle : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}

public class PawnMoveDouble : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}

public class PawnEnPassant : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}

public class KnightMoveSet : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}

public class BishopMoveSet : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}

public class QueenMoveSet : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}

public class KingMoveSet : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}

public class KingCastling : IMoveSet
{
	public Position PieceMove()
	{
		return new Position();
	}
}
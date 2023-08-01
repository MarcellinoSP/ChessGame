namespace ChessGame;
public class ChessMove
{
	private Dictionary <Piece, IMoveSet> _moveSet;
	private int _moveBoundary;
	
	public ChessMove()
	{
		_moveSet = new Dictionary<Piece, IMoveSet>();
	}
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
		//KEMUNGKINAN PAKAI SWITCH CASE
		return _moveSet[piece];
	}
}

public class PawnMoveSingle : IMoveSet
{
	//DOES NOT IMPLEMENT IENUMERABLE (PIE TO KIIII)
	// public List<Position> pieceMovement(Position currentPosition)
	// {
	// 	int currentRank = currentPosition.GetRank();
	// 	int currentFiles = currentPosition.GetFiles();
		
	// 	List<Position> availableMove = new List<Position>();
		
	// 	for(int i = 1; i < 7; i++)
	// 	{
	// 		availableMove.Add(new Position {currentFiles + 1});
	// 	}
	// 	return availableMove;
	// }
	
	public Position PieceMove()
	{
		// Position position = new();
		// int currentRank = position.GetRank();
		// int currentFiles = position.GetFiles();
		// for(int i = 1; i < 8; i++)
		// {
		// 	currentFiles += i;
		// }
		return new Position();
	}
}

// public class PawnMoveDouble : IMoveSet
// {
// 	public Position PieceMove()
// 	{
// 		return new Position();
// 	}
// }

// public class PawnEnPassant : IMoveSet
// {
// 	public Position PieceMove()
// 	{
// 		return new Position();
// 	}
// }

// public class KnightMoveSet : IMoveSet
// {
// 	public Position PieceMove()
// 	{
// 		return new Position();
// 	}
// }

// public class BishopMoveSet : IMoveSet
// {
// 	public Position PieceMove()
// 	{
// 		return new Position();
// 	}
// }

// public class QueenMoveSet : IMoveSet
// {
// 	public Position PieceMove()
// 	{
// 		return new Position();
// 	}
// }

// public class KingMoveSet : IMoveSet
// {
// 	public Position PieceMove()
// 	{
// 		return new Position();
// 	}
// }

// public class KingCastling : IMoveSet
// {
// 	public Position PieceMove()
// 	{
// 		return new Position();
// 	}
// }
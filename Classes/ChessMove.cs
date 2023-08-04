namespace ChessGame;
public class ChessMove
{
	private Dictionary <Piece, IMoveSet> _moveSet;
	private int _moveBoundary;
	
	public ChessMove()
	{
		_moveSet = new Dictionary<Piece, IMoveSet>();
	}
	public bool AddPiece(KeyValuePair<Piece, IMoveSet> addPiece)	//KALAU LANGSUNG DI ASSIGN?
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
		Piece pieceInCheck = piece;
		switch(pieceInCheck.Type())
		{
				case("P10"):
					PawnMoveSingle pawnMoveSingle = new();
					return pawnMoveSingle;
					break;
		}
		return null;
	}
}

public class PawnMoveSingle : IMoveSet
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		position.SetRank(currentRank += 1);
		return position;
	}
}

public class PawnMoveDouble : IMoveSet
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		for(int i = 1; i < 8; i++)
		{
			currentRank += 2;
			position.SetRank(currentRank);
		}
		return position;
	}
}

public class PawnEnPassant : IMoveSet
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		currentRank += 1;
		currentFiles += 1;
		position.SetRank(currentRank);
		position.SetFiles(currentFiles);
		return position;
	}
}

public class KnightMoveSet : IMoveSet	//INI KAYAE BUTUH LIST, KARENA MOVEMENT NYA 2 JENIS
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		
		return position;
	}
}

public class BishopMoveSet : IMoveSet
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		for(int i = 0; i < 7; i++)
		{
			currentFiles += 1;
			currentRank += 1;
		}
		return position;
	}
}

public class QueenMoveSet : IMoveSet		//INI KAYAE JUGA BUTUH LIST
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		
		return position;
	}
}

public class KingMoveSet : IMoveSet
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		for(int i = 1; i < 8; i++)
		{
			currentRank += i;
			currentFiles += i;
			position.SetRank(currentRank);
			position.SetFiles(currentFiles);
		}
		return position;
	}
}

public class KingCastling : IMoveSet
{
	public Position PieceMove()
	{
		Position position = new();
		int currentRank = position.GetRank();
		int currentFiles = position.GetFiles();
		currentFiles += 2;
		currentFiles -= 2;
		position.SetFiles(currentFiles);
		position.SetFiles(currentFiles);
		return position;
	}
}
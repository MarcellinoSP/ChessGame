namespace ChessGame;
public abstract class Piece
{
	protected bool captured;
	protected Position position;
	
	public Piece()
	{
		captured = false;
		position = new Position();
		position.SetRank(0);
		position.SetFiles(0);
	}
	
	public bool SetRank(int rank)
	{
		position.SetRank(rank);
		return true;
	}
	
	public bool SetFiles(int files)
	{
		position.SetRank(files);
		return true;
	}
	
	public int GetRank()
	{
		int rank = position.GetRank();
		return rank;
	}
	
	public int GetFiles()
	{
		int files = position.GetFiles();
		return files;
	}
}

public class King : Piece
{
	private bool _castlingDone;
	
	public King(int rank, int files)
	{
		position.SetRank(rank);
		position.SetFiles(files);
	}
	
	public bool GetCastlingStatus()
	{
		return true;
	}
	
	public bool SetCastlingStatus()
	{
		return true;
	}
	
	public bool IsInCheck()
	{
		return true;
	}
	
	public bool IsMove()
	{
		if(position.GetRank() != 5 || position.GetFiles() != 1)
		{
			return true;
		}
		return false;
	}
}

public class Pawn : Piece
{
	private bool _promotionStatus;
	private bool _enPassantStatus;
	
	public Pawn(int rank, int files)
	{
		position.SetRank(rank);
		position.SetFiles(files);
	}
	
	public bool GetPromotionStatus()
	{
		return true;
	}
	
	public bool SetPromotionStatus(bool promotionStatus)
	{
		_promotionStatus = promotionStatus;
		return true;
	}
	
	public bool GetEnPassantStatus()
	{
		return true;
	}
	
	public bool SetEnPassantStatus(bool enPassantStatus)
	{
		_enPassantStatus = enPassantStatus;
		return true;
	}
	
	public bool IsMoved()
	{
		return true;
	}
}

public class Rook : Piece
{
	public Rook(int rank, int files)
	{
		position.SetRank(rank);
		position.SetFiles(files);
	}
	
	public bool IsMoved()
	{
		return true;
	}
}

public class Queen : Piece
{
	public Queen(int rank, int files)
	{
		position.SetRank(rank);
		position.SetFiles(files);
	}
}

public class Knight : Piece
{
	public Knight(int rank, int files)
	{
		position.SetRank(rank);
		position.SetFiles(files);
	}
}

public class Bishop : Piece
{
	public Bishop(int rank, int files)
	{
		position.SetRank(rank);
		position.SetFiles(files);
	}
}
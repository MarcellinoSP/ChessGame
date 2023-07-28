namespace ChessGame;
public abstract class Piece
{
	protected bool captured;
	protected Position position = new Position();
}

public class King : Piece
{
	private bool _castlingDone;
	
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
		return true;
	}
}

public class Pawn : Piece
{
	private bool _promotionStatus;
	private bool _enPassantStatus;
	
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
	public bool IsMoved()
	{
		return true;
	}
}

public class Queen : Piece
{
	
}

public class Knight : Piece
{
	
}

public class Bishop : Piece
{
	
}
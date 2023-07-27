namespace ChessGame;
public abstract class Piece
{
	protected bool captured;
	protected Position position = new();
}

public class King
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

public class Pawn
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

public class Rook
{
	public bool IsMoved()
	{
		return true;
	}
}

public class Queen
{
	
}

public class Knight
{
	
}

public class Bishop
{
	
}
namespace ChessGame;
public enum GameStatus
{
	ONGOING,
	CHECK,
	BLACK_WIN,
	WHITE_WIN,
	STALEMATE
}

public enum Color
{
	WHITE,
	BLACK
}

public enum PromoteTo
{
	QUEEN,
	ROOK,
	BISHOP,
	KNIGHT
}
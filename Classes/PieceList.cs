using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ChessGame;

public class PieceList
{
	public List<Piece> pieceListWhite = new List<Piece>();
	
	public void AddWhitePiece()
	{
		pieceListWhite.Add(new Pawn (6, 0, "P1"));
		pieceListWhite.Add(new Pawn (6, 1, "P2"));
		pieceListWhite.Add(new Pawn (6, 2, "P3"));
		pieceListWhite.Add(new Pawn (6, 3, "P4"));
		pieceListWhite.Add(new Pawn (6, 4, "P5"));
		pieceListWhite.Add(new Pawn (6, 5, "P6"));
		pieceListWhite.Add(new Pawn (6, 6, "P7"));
		pieceListWhite.Add(new Pawn (6, 7, "P8"));
		
		pieceListWhite.Add(new Rook (7, 0, "R1"));
		pieceListWhite.Add(new Knight (7, 1, "N1"));
		pieceListWhite.Add(new Bishop (7, 2, "B1"));
		pieceListWhite.Add(new Queen (7, 3, "Q1"));
		pieceListWhite.Add(new King (7, 4, "K1"));
		pieceListWhite.Add(new Bishop (7, 5, "B2"));
		pieceListWhite.Add(new Knight (7, 6, "N2"));
		pieceListWhite.Add(new Rook (7, 7, "R2"));
	}	
	
	// public List<Piece> pieceListBlack = new List<Piece>
	// {
	// 	new Pawn (1, 0, "p1"),
	// 	new Pawn (1, 1, "p2"),
	// 	new Pawn (1, 2, "p3"),
	// 	new Pawn (1, 3, "p4"),
	// 	new Pawn (1, 4, "p5"),
	// 	new Pawn (1, 5, "p6"),
	// 	new Pawn (1, 6, "p7"),
	// 	new Pawn (1, 7, "p8"),
				
	// 	new Rook (0, 0, "r1"),
	// 	new Knight (0, 1, "n1"),
	// 	new Bishop (0, 2, "b1"),
	// 	new Queen (0, 3, "q1"),
	// 	new King (0, 4, "k1"),
	// 	new Bishop (0, 5, "b2"),
	// 	new Knight (0, 6, "n2"),
	// 	new Rook (0, 7, "r2")
	// };
	
	public void GenerateJSON()
	{
		// string jsonString = JsonSerializer.Serialize(pieceListBlack);
		// using (StreamWriter writer1 = new("BlackPiece.json"))
		// {
		// 	writer1.WriteLine(jsonString);
		// }
		
		var json1 = new DataContractJsonSerializer(typeof(List<Piece>));
		using(FileStream stream = new FileStream("WhitePiece.json", FileMode.OpenOrCreate))
		{
			json1.WriteObject(stream, pieceListWhite);
		}
		
		// using (StreamWriter writer2 = new("WhitePiece.json", FileMode.OpenOrCreate))
		// {
		// 	writer2.WriteLine(jsonString1);
		// }
	}
}
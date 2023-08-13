using System.Runtime.Serialization.Json;
using System.Text;

namespace ChessGame;

public class PieceList
{
	public static readonly DataContractJsonSerializerSettings Settings = 
					new DataContractJsonSerializerSettings
					{ UseSimpleDictionaryFormat = true };
					
	public List<Piece> pieceListWhite = new List<Piece>();
	
	public void AddWhitePiece()
	{
		pieceListWhite.Add(new Pawn (6, 0, "Pawn", "P1"));
		pieceListWhite.Add(new Pawn (6, 1, "Pawn", "P2"));
		pieceListWhite.Add(new Pawn (6, 2, "Pawn", "P3"));
		pieceListWhite.Add(new Pawn (6, 3, "Pawn", "P4"));
		pieceListWhite.Add(new Pawn (6, 4, "Pawn", "P5"));
		pieceListWhite.Add(new Pawn (6, 5, "Pawn", "P6"));
		pieceListWhite.Add(new Pawn (6, 6, "Pawn", "P7"));
		pieceListWhite.Add(new Pawn (6, 7, "Pawn", "P8"));
		
		pieceListWhite.Add(new Rook (7, 0, "Rook", "R1"));
		pieceListWhite.Add(new Knight (7, 1, "Knight", "N1"));
		pieceListWhite.Add(new Bishop (7, 2, "Bishop", "B1"));
		pieceListWhite.Add(new Queen (7, 3, "Queen", "Q1"));
		pieceListWhite.Add(new King (7, 4, "King", "K1"));
		pieceListWhite.Add(new Bishop (7, 5, "Bishop", "B2"));
		pieceListWhite.Add(new Knight (7, 6, "Knight", "N2"));
		pieceListWhite.Add(new Rook (7, 7, "Rook", "R2"));
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
		FileStream stream = new FileStream("WhitePiece.json", FileMode.Create);
		using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, true, true, "  "))
		{
			var json1 = new DataContractJsonSerializer(typeof(List<Piece>), Settings);
			json1.WriteObject(writer, pieceListWhite);
			stream.Flush();
		}
	}
}
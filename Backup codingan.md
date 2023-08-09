```cs
        if(_currentTurn == PlayerColor.WHITE)
		{
			Piece king = CheckPiece("K1");
			int kingRank = king.GetRank();
			int kingFiles = king.GetFiles();
			int boardSize = GetBoardBoundary();
			foreach(var pieceList in _piecesList.Values)
			{
				foreach(var piece in pieceList)
				{
					if(piece is Rook && piece.ID().Contains('r'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("K1", kingRank - i, kingFiles);
							bool threat2 = IsOccupied("K1", kingRank, kingFiles - i);
							bool threat3 = IsOccupied("K1", kingRank + i, kingFiles);
							bool threat4 = IsOccupied("K1", kingRank, kingFiles + i);
							Console.WriteLine("Pass Rook Check");
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
					if(piece is Bishop && piece.ID().Contains('b'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("K1", kingRank + i, kingFiles + i);
							bool threat2 = IsOccupied("K1", kingRank + i, kingFiles - i);
							bool threat3 = IsOccupied("K1", kingRank - i, kingFiles + i);
							bool threat4 = IsOccupied("K1", kingRank - i, kingFiles - i);
							Console.WriteLine("Pass Bishop Check");
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
				}
			}
		}
		else if(_currentTurn == PlayerColor.BLACK)
		{
			Piece king = CheckPiece("k1");
			int kingRank = king.GetRank();
			int kingFiles = king.GetFiles();
			int boardSize = GetBoardBoundary();
			foreach(var pieceList in _piecesList.Values)
			{
				foreach(var piece in pieceList)
				{
					if(piece is Rook && piece.ID().Contains('R'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("k1", kingRank - i, kingFiles);
							bool threat2 = IsOccupied("k1", kingRank, kingFiles - i);
							bool threat3 = IsOccupied("k1", kingRank + i, kingFiles);
							bool threat4 = IsOccupied("k1", kingRank, kingFiles + i);
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
					if(piece is Bishop && piece.ID().Contains('B'))
					{
						for(int i = 1; i < boardSize; i++)
						{
							bool threat1 = IsOccupied("k1", kingRank + i, kingFiles + i);
							bool threat2 = IsOccupied("k1", kingRank + i, kingFiles - i);
							bool threat3 = IsOccupied("k1", kingRank - i, kingFiles + i);
							bool threat4 = IsOccupied("k1", kingRank - i, kingFiles - i);
							if(threat1 || threat2 || threat3 || threat4)
							{
								return false;
							}
						}
					}
				}
			}
		}
```
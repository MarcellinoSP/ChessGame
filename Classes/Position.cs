using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace ChessGame;
[DataContract]
public class Position
{
	[DataMember]
	private int _rank;
	[DataMember]
	private int _files;
	
	public bool SetRank(int rank)
	{
		_rank = rank;
		return true;
	}
	public bool SetFiles(int files)
	{
		_files = files;
		return true;
	}
	public int GetRank()
	{
		return _rank;
	}
	public int GetFiles()
	{
		return _files;
	}
}
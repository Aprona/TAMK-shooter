using System;
using System.Collections.Generic;

namespace TAMKShooter.Data
{
	[Serializable]
	public class GameData
	{
		public List<PlayerData> PlayerDatas = new List< PlayerData >();
		public int Level;
	}
}

using System.Collections.Generic;
using TAMKShooter.Data;
using System.IO;
using UnityEngine;

namespace TAMKShooter.Systems.SaveLoad
{
	public class SaveManager
	{
		private readonly ISaveLoad< GameData > _saveLoad;

		public string FileExtension { get { return _saveLoad.FileExtension; } }

		public SaveManager( ISaveLoad< GameData > saveLoad )
		{
			_saveLoad = saveLoad;
		}

		public void Save( GameData data, string saveFileName )
		{
			_saveLoad.Save( data, saveFileName );
		}

		public GameData Load( string fileName )
		{
			return _saveLoad.Load( fileName );
		}

		public List< string > GetAllSaveNames()
		{
			List<string> saveNames = new List< string >();
			
			DirectoryInfo directoryInfo = 
				new DirectoryInfo( Application.persistentDataPath );
			FileInfo[] files = directoryInfo.GetFiles( "*" + FileExtension );
			foreach ( var fileInfo in files )
			{
				string fileName = fileInfo.Name;
				fileName = fileName.Replace( FileExtension, "" );
				saveNames.Add( fileName );
			}

			return saveNames;
		}
	}
}

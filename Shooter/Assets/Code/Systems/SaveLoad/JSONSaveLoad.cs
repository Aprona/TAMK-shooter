using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace TAMKShooter.Systems.SaveLoad
{
	/// <summary>
	/// This class serializes (and deserializes) objects to JSON format and saves and loads them to disk.
	/// </summary>
	/// <typeparam name="T">The type of save file.</typeparam>
	public class JSONSaveLoad<T> : ISaveLoad<T>
		where T : class
	{
		/// <summary>
		/// Returns the file extension of the save file.
		/// </summary>
		public string FileExtension { get { return ".json"; } }

		/// <summary>
		/// Returns full path of the save file.
		/// </summary>
		/// <param name="saveFileName">The name of the save file.</param>
		/// <returns>Save file's full path.</returns>
		public string GetSaveFilePath( string saveFileName )
		{
			return Path.Combine( Application.persistentDataPath, saveFileName )
			       + FileExtension;
		}

		/// <summary>
		/// Serializes the object and saves it to disk.
		/// </summary>
		/// <param name="objectToSave">The object to be saved.</param>
		/// <param name="saveFileName">The name of the save file.</param>
		public void Save( T objectToSave, string fileName )
		{
			// Serialize object to JSON format.
			string jsonObject = JsonUtility.ToJson( objectToSave, false );
			// Write JSON serialized object to disk.
			File.WriteAllText( GetSaveFilePath( fileName ), jsonObject, Encoding.UTF8 );
		}

		/// <summary>
		/// Loads saved data from SaveFilePath.
		/// </summary>
		/// <typeparam name="T">The type of the save file. Must be a class</typeparam>
		/// <param name="fileName">The name of the save file.</param>
		/// <returns>The deserialized object which contains saved data.</returns>
		public T Load( string fileName )
		{
			// We can load file only if it exists.
			if ( DoesSaveExist( fileName ) )
			{
				// Read JSON serialized object from disk.
				string jsonObject = File.ReadAllText( GetSaveFilePath( fileName ),
					Encoding.UTF8 );
				// Deserialize data and return it.
				return JsonUtility.FromJson< T >( jsonObject );
			}

			// If file doesn't exist, just return default value of type T.
			return default ( T );
		}

		/// <summary>
		/// Checks if save file exists
		/// </summary>
		/// <param name="saveFileName">The name of the save file.</param>
		/// <returns><c>True</c> if save file exists, <c>false</c> otherwise.</returns>
		public bool DoesSaveExist( string fileName )
		{
			return File.Exists( GetSaveFilePath( fileName ) );
		}
	}
}

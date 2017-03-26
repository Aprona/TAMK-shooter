using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace TAMKShooter.Systems.SaveLoad
{
	public class BinaryFormatterSaveLoad<T> : ISaveLoad<T>
		where T : class
	{
		public string FileExtension { get { return ".dat"; } }

		/// <summary>
		/// Returns full path of the save file.
		/// </summary>
		/// <param name="saveFileName">Name of the save file without file extension.</param>
		public string GetSaveFilePath( string saveFileName )
		{
			return Path.Combine( Application.persistentDataPath, saveFileName ) +
			       FileExtension;
		}

		/// <summary>
		/// Serializes the object and saves it to disk.
		/// </summary>
		/// <param name="objectToSave">The object to be saved.</param>
		/// <param name="fileName">Name of the save file.</param>
		public void Save( T objectToSave, string fileName )
		{
			// Binary formatter serializes data into binary which can be stored to disk.
			BinaryFormatter formatter = new BinaryFormatter();

			// Binary formatter stores serialization result into stream so let's create a
			// memory stream for that purpose.
			using ( MemoryStream stream = new MemoryStream() )
			{
				// BinaryFormatter.Serialize method actually serializes the object. Result is
				// stored to stream.
				formatter.Serialize( stream, objectToSave );

				// File.WriteAllBytes writes serialized bytes into a file. Bytes can be
				// acquired from stream by calling its GetBuffer method.
				File.WriteAllBytes( GetSaveFilePath( fileName ), stream.GetBuffer() );
			}
		}

		/// <summary>
		/// Loads saved data from SaveFilePath.
		/// </summary>
		/// <typeparam name="T">The type of the save file. Must be a class</typeparam>
		/// <param name="fileName">The name of the file from which we load the object.</param>
		/// <returns>The deserialized object which contains saved data.</returns>
		public T Load( string fileName )
		{
			// We can load file only if it exists.
			if ( DoesSaveExist( fileName ) )
			{
				// File.ReadAllBytes reads bytes from a file and returns them as a byte array.
				byte[] data = File.ReadAllBytes( GetSaveFilePath( fileName ) );

				// Since we used BinaryFormatter to serialize object, we must use it also to
				// deserialize it.
				BinaryFormatter formatter = new BinaryFormatter();

				// Lets create a MemoryStream which contains our serialized bytes
				using ( MemoryStream stream = new MemoryStream( data ) )
				{
					// and deserialize saved data from that stream and return the result.
					return (T) formatter.Deserialize( stream );
				}
			}

			// If file doesn't exist, just return default value of type T.
			return default (T);
		}

		public bool DoesSaveExist( string fileName )
		{
			return File.Exists( GetSaveFilePath( fileName ) );
		}
	}
}

using UnityEngine;
using UnityEngine.UI;

namespace TAMKShooter.GUI
{
	public class LoadItem : MonoBehaviour
	{
		private Text _loadButtonLabel;
		private LoadWindow _loadWindow;

		public void Init( LoadWindow loadWindow, string saveFileName )
		{
			_loadButtonLabel = GetComponentInChildren< Text >( true );
			_loadButtonLabel.text = saveFileName;
			_loadWindow = loadWindow;
		}

		public void OnClick()
		{
			_loadWindow.LoadGame( _loadButtonLabel.text );
		}
	}
}

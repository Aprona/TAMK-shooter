using TAMKShooter.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace TAMKShooter.GUI
{
	public class LoadWindow : Window
	{
		[SerializeField] private LoadItem _loadItemPrefab;

		private VerticalLayoutGroup _contentParent;
		private MenuManager _menuManager;

		public void Init( MenuManager menuManager )
		{
			_contentParent = GetComponentInChildren< VerticalLayoutGroup >( true );

			_menuManager = menuManager;
			var saveNames =
				Global.Instance.SaveManager.GetAllSaveNames();

			foreach ( var saveName in saveNames )
			{
				var loadItem =
					Instantiate( _loadItemPrefab, _contentParent.transform, true );
				loadItem.Init( this, saveName );
			}
		}

		public void LoadGame( string saveFileName )
		{
			_menuManager.LoadGame( saveFileName );
		}
	}
}

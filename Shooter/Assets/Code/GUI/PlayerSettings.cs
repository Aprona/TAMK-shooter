using System.Collections;
using System.Collections.Generic;
using TAMKShooter.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace TAMKShooter.GUI
{
	public class PlayerSettings : Window
	{
		[SerializeField] private Dropdown _playerCountDropdown;
		[SerializeField] private PlayerSettingsItem[] _items;

		private MenuManager _menuManager;

		public void Init( MenuManager menuManager )
		{
			// TODO: Init player count dropdown

			_menuManager = menuManager;
			foreach ( var playerSettingsItem in _items )
			{
				playerSettingsItem.Init();
			}
		}
	}
}

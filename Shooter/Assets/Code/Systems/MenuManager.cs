using System;
using System.Collections.Generic;
using TAMKShooter.Data;
using TAMKShooter.GUI;
using UnityEngine;

namespace TAMKShooter.Systems
{
	public class MenuManager : SceneManager
	{
		private LoadWindow _loadWindow;
		private PlayerSettings _playerSettingsWindow;

		public override GameStateType StateType
		{
			get { return GameStateType.MenuState; }
		}

		private void Awake()
		{
			_loadWindow = GetComponentInChildren< LoadWindow >( true );
			_loadWindow.Init( this );
			_loadWindow.Close();

			_playerSettingsWindow = 
				GetComponentInChildren< PlayerSettings >( true );
			_playerSettingsWindow.Init( this );
			// TODO: Close player settings window
		}

		public void StartGame()
		{
			Global.Instance.CurrentGameData = new GameData()
			{
				Level = 1,
				PlayerDatas = new List< PlayerData >()
				{
					new PlayerData()
					{
						Controller = InputManager.ControllerType.KeyboardArrow,
						Lives = 3,
						Id = PlayerData.PlayerId.Player1,
						UnitType = PlayerUnit.UnitType.Balanced
					},
					new PlayerData()
					{
						Controller = InputManager.ControllerType.KeyboardWasd,
						Lives = 3,
						Id = PlayerData.PlayerId.Player2,
						UnitType = PlayerUnit.UnitType.Heavy
					}
				}
			};

			Global.Instance.GameManager.
				PerformTransition ( GameStateTransitionType.MenuToInGame );
		}

		public void OpenLoadWindow()
		{
			_loadWindow.Open();
		}

		public void LoadGame( string loadFileName )
		{
			_loadWindow.Close();

			GameData loadData = Global.Instance.SaveManager.Load( loadFileName );
			Global.Instance.CurrentGameData = loadData;
			Global.Instance.GameManager.
				PerformTransition( GameStateTransitionType.MenuToInGame );
		}

		public void QuitGame ()
		{
			Application.Quit ();
		}
	}
}

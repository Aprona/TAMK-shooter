using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Data;
using TAMKShooter.Utility;
using System;
using TAMKShooter.Level;
using TAMKShooter.Systems.States;

namespace TAMKShooter.Systems
{
	public class LevelManager : SceneManager
	{
		private ConditionBase[] _conditions;
		private EnemySpawner[] _enemySpawners;

		// Add reference to InputManager here.
		public PlayerUnits PlayerUnits { get; private set; }
		public EnemyUnits EnemyUnits { get; private set; }
		public InputManager InputManager { get; private set; }

		public override GameStateType StateType
		{
			get { return GameStateType.InGameState; }
		}

		protected void Awake()
		{
			Initialize ();
		}

		private void Initialize()
		{
			PlayerUnits = GetComponentInChildren<PlayerUnits> ();
			EnemyUnits = GetComponentInChildren<EnemyUnits> ();
			EnemyUnits.Init ();

			_enemySpawners = GetComponentsInChildren< EnemySpawner >();
			foreach ( var enemySpawner in _enemySpawners )
			{
				enemySpawner.Init( EnemyUnits );
			}

#if UNITY_EDITOR
			if ( Global.Instance.CurrentGameData == null )
			{
				Global.Instance.CurrentGameData = new GameData()
				{
					Level = 1,
					PlayerDatas = new List< PlayerData >()
					{
						new PlayerData()
						{
							Controller = InputManager.ControllerType.KeyboardArrow,
							Id = PlayerData.PlayerId.Player1,
							Lives = 3,
							UnitType = PlayerUnit.UnitType.Balanced
						},
						new PlayerData()
						{
							Controller = InputManager.ControllerType.KeyboardWasd,
							Id = PlayerData.PlayerId.Player2,
							Lives = 3,
							UnitType = PlayerUnit.UnitType.Heavy
						},
                        new PlayerData()
                        {
                            Controller = InputManager.ControllerType.Gamepad1,
                            Id = PlayerData.PlayerId.Player3,
                            Lives = 3,
                            UnitType = PlayerUnit.UnitType.Heavy
                        },
                        new PlayerData()
                        {
                            Controller = InputManager.ControllerType.Gamepad2,
                            Id = PlayerData.PlayerId.Player4,
                            Lives = 3,
                            UnitType = PlayerUnit.UnitType.Heavy
                        }
                    }
				};
			}
#endif


			PlayerUnits.Init ( Global.Instance.CurrentGameData.PlayerDatas.ToArray() );

			InputManager = gameObject.GetOrAddComponent<InputManager> ();
			InputManager.Init ( this, InputManager.ControllerType.KeyboardWasd,
				InputManager.ControllerType.KeyboardArrow,
				InputManager.ControllerType.Gamepad1 );

			// All conditions should be parented to LevelManager
			_conditions = GetComponentsInChildren<ConditionBase> ();
			foreach(var condition in _conditions)
			{
				condition.Init ( this );
			}
		}

		public void ConditionMet ( ConditionBase condition )
		{
			bool areConditionsMet = true;
			foreach(ConditionBase c in _conditions)
			{
				if(!c.IsConditionMet)
				{
					areConditionsMet = false;
					break;
				}
			}

			if(areConditionsMet)
			{
				( AssociatedState as GameState ).LevelCompleted ();
			}
		}

		public void UpdateMovement ( InputManager.ControllerType controller,
			Vector3 input, bool shoot )
		{
			PlayerUnits.UpdateMovement ( controller, input, shoot );
		}
	}
}

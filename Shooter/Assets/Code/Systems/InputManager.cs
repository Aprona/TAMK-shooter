using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TAMKShooter.Configs;

namespace TAMKShooter.Systems
{
	public class InputManager : MonoBehaviour
	{
		private static readonly
			Dictionary< ControllerType, string > ControllerNames =
				new Dictionary< ControllerType, string >()
				{
					{ControllerType.KeyboardArrow, "Arrow keys"},
					{ControllerType.KeyboardWasd, "WASD keys"},
					{ControllerType.Gamepad1, "Gamepad 1"},
					{ControllerType.Gamepad2, "Gamepad 2"}
				};

		public static string GetControllerName( ControllerType controllerType )
		{
			string result = null;

			if ( ControllerNames.ContainsKey( controllerType ) )
			{
				result = ControllerNames[ controllerType ];
			}

			return result;
		}

		public static ControllerType
			GetControllerTypeByName( string controllerName )
		{
			ControllerType result = ControllerType.None;

			foreach ( var kvp in ControllerNames )
			{
				if ( kvp.Value == controllerName )
				{
					result = kvp.Key;
				}
			}

			return result;
		}

		public enum ControllerType
		{
			None = 0,
			KeyboardArrow = 1,
			KeyboardWasd = 2,
			Gamepad1 = 3,
			Gamepad2 = 4
		}

		private ControllerType[] _controllers;
		private LevelManager _levelManager;

		public void Init(LevelManager levelManager,
			params ControllerType[] controllers)
		{
			_levelManager = levelManager;
			_controllers = controllers;
		}

		protected void Update()
		{
			foreach(var controller in _controllers)
			{
				ReadInput ( controller );
			}

			PollSave();
		}

		private void PollSave()
		{
			if ( Input.GetKeyDown( KeyCode.F2 ) )
			{
				Global.Instance.SaveManager.Save( Global.Instance.CurrentGameData,
					DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss"));
			}
		}

		private void ReadInput(ControllerType controller)
		{
			string verticalName;
			string horizontalName;
			string shootName;

			if(GetInputNames(controller, 
				out verticalName, 
				out horizontalName, 
				out shootName))
			{
				float vertical = Input.GetAxis ( verticalName );
				float horizontal = Input.GetAxis ( horizontalName );
				bool shoot = Input.GetButton ( shootName );

				if(IsDeadZone(new Vector2(horizontal, vertical)))
				{
					vertical = 0;
					horizontal = 0;
				}

				Vector3 input = new Vector3 ( horizontal, 0, vertical );
				_levelManager.UpdateMovement ( controller, input, shoot );
			}
		}

		/// <summary>
		/// Unity implements dead zone per axis. This is not good, 
		/// we want the implementation to take into account both
		/// horizontal and vertical axis at the same time.
		/// </summary>
		/// <param name="input">The amount of input user gave</param>
		/// <returns>True, if input was inside dead zone area.
		/// False otherwise</returns>
		private bool IsDeadZone( Vector2 input )
		{
			return input.magnitude < Config.DeadZone;
		}

		private bool GetInputNames(ControllerType controller,
			out string verticalName,
			out string horizontalName,
			out string shootName)
		{
			bool result = true;

			switch(controller)
			{
				case ControllerType.KeyboardWasd:
					verticalName = Config.VerticalWASDName;
					horizontalName = Config.HorizontalWASDName;
					shootName = Config.ShootWASDName;
					break;
				case ControllerType.KeyboardArrow:
					verticalName = Config.VerticalArrowsName;
					horizontalName = Config.HorizontalArrowsName;
					shootName = Config.ShootArrowsName;
					break;
				case ControllerType.Gamepad1:
					verticalName = Config.VerticalGamepad1Name;
					horizontalName = Config.HorizontalGamepad1Name;
					shootName = Config.ShootGamepad1Name;
					break;
				case ControllerType.Gamepad2:
					verticalName = Config.VerticalGamepad2Name;
					horizontalName = Config.HorizontalGamepad2Name;
					shootName = Config.ShootGamepad2Name;
					break;
				default:
					verticalName = string.Empty;
					horizontalName = string.Empty;
					shootName = string.Empty;
					result = false;
					break;
			}

			return result;
		}
	}
}

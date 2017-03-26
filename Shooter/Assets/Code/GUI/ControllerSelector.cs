using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAMKShooter.Data;
using TAMKShooter.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace TAMKShooter.GUI
{
	public class ControllerSelector : MonoBehaviour
	{
		private Dropdown _dropdown;

		public PlayerData.PlayerId Id { get; private set; }
		public InputManager.ControllerType Controller { get; private set; }

		public void Init( PlayerData.PlayerId id,
			InputManager.ControllerType defaultControllerType )
		{
			_dropdown = GetComponentInChildren< Dropdown >( true );
			_dropdown.ClearOptions();
			List<Dropdown.OptionData> optionDataList =
				new List< Dropdown.OptionData >();

			foreach ( var value in 
				Enum.GetValues( typeof(InputManager.ControllerType) ) )
			{
				if ( (InputManager.ControllerType) value !=
				     InputManager.ControllerType.None )
				{
					string controllerName = 
						InputManager.GetControllerName( 
							(InputManager.ControllerType) value );
					optionDataList.Add( new Dropdown.OptionData( controllerName ) );
				}
			}

			_dropdown.AddOptions( optionDataList );
			_dropdown.onValueChanged.AddListener( OnValueChanged );

			Id = id;
			Controller = defaultControllerType;

			int defaultIndex =
				GetItemIndex( InputManager.
					GetControllerName( defaultControllerType ) );
			if ( defaultIndex > 0 )
			{
				_dropdown.value = defaultIndex;
			}
		}

		private void OnValueChanged( int index )
		{
			Controller = InputManager.GetControllerTypeByName(
				_dropdown.options[ index ].text );
		}

		private int GetItemIndex( string controllerName )
		{
			int result = -1;

			for ( int i = 0; i < _dropdown.options.Count; i++ )
			{
				Dropdown.OptionData item = _dropdown.options[ i ];
				if ( item.text == controllerName )
				{
					result = i;
					break;
				}
			}

			return result;
		}
	}
}

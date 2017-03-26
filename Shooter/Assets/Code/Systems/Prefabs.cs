using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using ProjectileType = TAMKShooter.Projectile.ProjectileType;

namespace TAMKShooter.Systems
{
	public class Prefabs : MonoBehaviour
	{
		[SerializeField] private PlayerUnit[] _playerUnitPrefabs;

		public PlayerUnit GetPlayerUnitPrefab( PlayerUnit.UnitType type )
		{
			PlayerUnit result = null;
			
			// For loop
			for(int i = 0; i < _playerUnitPrefabs.Length; ++i )
			{
				if(_playerUnitPrefabs[i].Type == type)
				{
					result = _playerUnitPrefabs[i];
					break;
				}
			}

			// Foreach loop
			//foreach(PlayerUnit playerUnit in _playerUnitPrefabs)
			//{
			//	if(playerUnit.Type == type)
			//	{
			//		result = playerUnit;
			//		break;
			//	}
			//}

			return result;

			// Linq version
			//return _playerUnitPrefabs.
			//	FirstOrDefault ( prefab => prefab.Type == type );
		}
	}
}

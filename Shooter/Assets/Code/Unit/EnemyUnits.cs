using System;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKShooter
{
	public class EnemyUnits : MonoBehaviour
	{
		public event Action<EnemyUnit> EnemyDestroyed;

		private List<EnemyUnit> _enemies = new List<EnemyUnit> ();

		public void Init()
		{
			// Find all enemied from scene
			//EnemyUnit[] enemies = FindObjectsOfType<EnemyUnit> ();
			//foreach(EnemyUnit enemy in enemies)
			//{
			//	_enemies.Add ( enemy );
			//	enemy.Init ( this, null );
			//}

			// TODO: Also instantiate enemies an some point during mission
			// dynamically
		}

		public void EnemyDied ( EnemyUnit enemyUnit )
		{
			if(EnemyDestroyed != null)
			{
				EnemyDestroyed ( enemyUnit );
			}
		}
	}
}

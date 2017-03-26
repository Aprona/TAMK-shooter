using System.Collections;
using TAMKShooter.Systems;
using UnityEngine;
using TAMKShooter.WaypointSystem;

namespace TAMKShooter.Level
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private EnemyUnit _enemyPrefab;
		[SerializeField] private float _spawnInterval = 1;
		[SerializeField] private int _maxAmount;
		[SerializeField] private Path _path;

		private EnemyUnits _enemyUnits;
		private int _spawnCount = 0;

		public void Init( EnemyUnits enemyUnits )
		{
			_enemyUnits = enemyUnits;
			StartCoroutine( Spawn() );
		}

		private IEnumerator Spawn()
		{
			while ( _spawnCount <= _maxAmount )
			{
				EnemyUnit enemyUnit = 
					Global.Instance.Pools.AsteroidPool.GetPooledObject();

				enemyUnit.transform.position = transform.position;
				enemyUnit.transform.rotation = Quaternion.identity;

				enemyUnit.gameObject.SetActive( true );

				enemyUnit.Init( _enemyUnits, _path );
				_spawnCount++;
				yield return new WaitForSeconds( _spawnInterval );
			}
		}
	}
}

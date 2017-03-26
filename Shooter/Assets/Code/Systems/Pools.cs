using System.Collections.Generic;
using UnityEngine;

namespace TAMKShooter.Systems
{
	public class Pools : MonoBehaviour
	{
		[SerializeField]
		private List<ProjectilePool> _projectilePools =
			new List<ProjectilePool> ();

		[SerializeField] private AsteroidPool _asteroidPool;

		public AsteroidPool AsteroidPool { get { return _asteroidPool; } }

		public ProjectilePool GetPool ( Projectile.ProjectileType projectileType )
		{
			ProjectilePool result = null;

			foreach(var projectilePool in _projectilePools)
			{
				if(projectilePool.ProjectileType == projectileType)
				{
					result = projectilePool;
					break;
				}
			}

			return result;
		}
	}
}

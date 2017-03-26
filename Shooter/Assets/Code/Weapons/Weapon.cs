using UnityEngine;
using TAMKShooter.Utility;
using TAMKShooter.Systems;
using ProjectileType = TAMKShooter.Projectile.ProjectileType;
using System;

namespace TAMKShooter
{
	public class Weapon : MonoBehaviour, IShooter
	{
		[SerializeField] private ProjectileType _projectileType;

		/// <summary>
		/// Projectile hit something. Let's return it to pool.
		/// </summary>
		/// <param name="projectile">The collided projectile.</param>
		public void ProjectileHit ( Projectile projectile )
		{
			ProjectilePool pool = 
				Global.Instance.Pools.GetPool ( _projectileType );

			if (pool != null)
			{
				pool.ReturnObjectToPool ( projectile );
			}
			else
			{
				Destroy ( projectile.gameObject );
			}
		}

		public void Shoot(int projectileLayer)
		{
			Projectile projectile = GetProjectile ();
			if(projectile != null)
			{
				projectile.gameObject.SetActive ( true );
				projectile.transform.position = transform.position;
				projectile.transform.forward = transform.forward;
				projectile.gameObject.SetLayer ( projectileLayer );
				projectile.Shoot ( this, transform.forward );
			}
			else
			{
				Debug.LogError ( "Could not get Projectile!" );
			}
		}

		private Projectile GetProjectile()
		{
			Projectile result = null;

			ProjectilePool pool =
				Global.Instance.Pools.GetPool ( _projectileType );

			if(pool != null)
			{
				result = pool.GetPooledObject ();
			}

			return result;
		}
	}
}

using TAMKShooter.Configs;
using TAMKShooter.Utility;
using TAMKShooter.WaypointSystem;
using UnityEngine;

namespace TAMKShooter
{
	public class EnemyUnit : UnitBase
	{
		private IPathUser _pathUser;

		public EnemyUnits EnemyUnits { get; private set; }

		public override int ProjectileLayer
		{
			get
			{
				return LayerMask.NameToLayer ( Config.EnemyProjectileLayerName );
			}
		}

		public void Init(EnemyUnits enemyUnits, Path path)
		{
			InitRequiredComponents();

			EnemyUnits = enemyUnits;

			_pathUser = gameObject.GetOrAddComponent< PathUser >();
			_pathUser.Init( Mover, path );
		}

		protected override void Die ()
		{
			// Handle dying properly. Instantiate explosion effect, play sound etc.
			gameObject.SetActive ( false );
			EnemyUnits.EnemyDied ( this );

			base.Die ();
		}        
	}
}

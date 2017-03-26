using UnityEngine;

namespace TAMKShooter
{
	public class Projectile : MonoBehaviour
	{
		public enum ProjectileType
		{
			None = 0,
			Laser = 1,
			Explosive = 2,
			Missile = 3
		}

		#region Unity fields

		[SerializeField] private float _shootingForce;
		[SerializeField] private int _damage;
		[SerializeField] private ProjectileType _projectileType;

		#endregion Unity fields

		private IShooter _shooter;

		public Rigidbody Rigidbody { get; private set; }

		public ProjectileType Type { get { return _projectileType; } }

		#region Unity messages

		protected virtual void Awake()
		{
			Rigidbody = GetComponent<Rigidbody> ();
		}

		protected void OnCollisionEnter(Collision collision)
		{
			// Collision, Projectile hit something

			IHealth damageReceiver =
				collision.gameObject.GetComponentInChildren<IHealth> ();
			if(damageReceiver != null)
			{
				// Colliding object can take damage
				damageReceiver.TakeDamage ( _damage );

				// TODO: Instantiate effect
				// TODO: Add sound effect

				_shooter.ProjectileHit ( this );
			}
		}

		// This is used to clean projectiles up after they
		// have exitted camera's viewport.
		protected void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.layer == LayerMask.NameToLayer("Destroyer"))
			{
				_shooter.ProjectileHit ( this );
			}
		}

		#endregion Unity messages

		public void Shoot(IShooter shooter, Vector3 direction)
		{
			_shooter = shooter;
			Rigidbody.AddForce ( direction * _shootingForce, ForceMode.Impulse );
		}
	}
}

namespace TAMKShooter
{
	public interface IShooter
	{
		void Shoot ( int projectileLayer );
		void ProjectileHit ( Projectile projectile );
	}
}

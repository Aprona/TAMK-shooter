using System;
using UnityEngine;

namespace TAMKShooter
{
	public class Mover : MonoBehaviour, IMover
	{
		[SerializeField] private float _speed;

		public Vector3 Position
		{
			get { return transform.position; }
			set { transform.position = value; }
		}

		public Quaternion Rotation
		{
			get { return transform.rotation; }
			set { transform.rotation = value; }
		}

		public float Speed
		{
			get { return _speed; }
		}

		public void MoveToDirection ( Vector3 direction )
		{
			direction = direction.normalized;
			Position += direction * Speed * Time.deltaTime;
		}

		public void MoveTowardPosition ( Vector3 targetPosition )
		{
			Vector3 direction = targetPosition - Position;
			MoveToDirection ( direction );
		}

		public void RotateTowardPosition ( Vector3 targetPosition )
		{
			Vector3 direction = targetPosition - Position;
			direction.y = Position.y;
			direction = direction.normalized;
			Vector3 rotation = Vector3.RotateTowards ( transform.forward,
				direction, Speed * Time.deltaTime, 0 );
			Rotation = Quaternion.LookRotation ( rotation );
		}
	}
}

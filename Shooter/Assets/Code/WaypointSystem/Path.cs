using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TAMKShooter.WaypointSystem
{
	public class Path : MonoBehaviour
	{
		[SerializeField] private PathType _pathType;

		private List< Waypoint > _waypoints;

		private readonly Dictionary< PathType, Color > _pathColors =
			new Dictionary< PathType, Color >()
			{
				{PathType.Loop, Color.green},
				{PathType.PingPong, Color.red},
				{PathType.OneWay, Color.blue}
			};

		public List< Waypoint > Waypoints
		{
			get
			{
				if ( _waypoints == null ||
					_waypoints.Count == 0 ||
					!Application.isPlaying )
				{
					_waypoints = GetComponentsInChildren< Waypoint >().ToList();
				}

				return _waypoints;
			}
		}

		public Waypoint GetClosestWaypoint( Vector3 position )
		{
			float smallestSqrMagnitude = float.PositiveInfinity;
			Waypoint closest = null;

			foreach ( var waypoint in Waypoints )
			{
				Vector3 toWaypointVector = waypoint.Position - position;
				float currentSqrMagnitude = toWaypointVector.sqrMagnitude;
				
				if ( currentSqrMagnitude < smallestSqrMagnitude )
				{
					smallestSqrMagnitude = currentSqrMagnitude;
					closest = waypoint;
				}
			}

			return closest;
		}

		public Waypoint GetNextWaypoint( Waypoint currentWaypoint,
			ref Direction direction )
		{
			Waypoint nextWaypoint = null;

			for ( int i = 0; i < Waypoints.Count; i++ )
			{
				if ( Waypoints[ i ] == currentWaypoint )
				{
					switch ( _pathType )
					{
						case PathType.Loop:
							nextWaypoint = GetNextWaypointLoop( i, direction );
							break;
						case PathType.OneWay:
							nextWaypoint = GetNextWaypointOneWay( i, direction );
							break;
						case PathType.PingPong:
							nextWaypoint = GetNextWaypointPingPong( i, ref direction );
							break;
					}
				}
			}

			return nextWaypoint;
		}

		private Waypoint GetNextWaypointPingPong( int currentWaypointIndex,
			ref Direction direction )
		{
			Waypoint nextWaypoint = null;
			switch ( direction )
			{
				case Direction.Forward:
					if ( currentWaypointIndex < Waypoints.Count - 1 )
					{
						nextWaypoint = Waypoints[ currentWaypointIndex + 1 ];
					}
					else
					{
						nextWaypoint = Waypoints[ currentWaypointIndex - 1 ];
						direction = Direction.Backward;
					}
					break;
				case Direction.Backward:
					if ( currentWaypointIndex > 0 )
					{
						nextWaypoint = Waypoints[ currentWaypointIndex - 1 ];
					}
					else
					{
						nextWaypoint = Waypoints[ 1 ];
						direction = Direction.Forward;
					}
					break;
			}

			return nextWaypoint;
		}

		private Waypoint GetNextWaypointOneWay( int currentWaypointIndex,
			Direction direction )
		{
			Waypoint nextWaypoint = null;
			switch ( direction )
			{
				case Direction.Forward:
					if ( currentWaypointIndex < Waypoints.Count - 1 )
					{
						nextWaypoint = Waypoints[ currentWaypointIndex + 1 ];
					}
					break;
				case Direction.Backward:
					if ( currentWaypointIndex > 0 )
					{
						nextWaypoint = Waypoints[ currentWaypointIndex - 1 ];
					}
					break;
			}

			return nextWaypoint;
		}

		private Waypoint GetNextWaypointLoop ( int currentWaypointIndex,
			Direction direction )
		{
			return direction == Direction.Forward
				? Waypoints[ ++currentWaypointIndex % Waypoints.Count ]
				: Waypoints[ ( --currentWaypointIndex >= 0
					             ? currentWaypointIndex
					             : Waypoints.Count - 1 ) % Waypoints.Count ];
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = _pathColors[ _pathType ];
			if ( Waypoints.Count > 1 )
			{
				for ( int i = 1; i < Waypoints.Count; ++i )
				{
					Gizmos.DrawLine( Waypoints[i-1].Position, Waypoints[i].Position );
				}

				if ( _pathType == PathType.Loop )
				{
					Gizmos.DrawLine( Waypoints[Waypoints.Count - 1].Position,
						Waypoints[0].Position);
				}
			}
		}
	}
}

using UnityEngine;

namespace TAMKShooter.WaypointSystem
{
	public enum Direction
	{
		Forward,
		Backward
	}

	public enum PathType
	{
		Loop, // After reaching the last waypoint, user moves to the first waypoint
		PingPong, // User changes direction after reaching the last waypoint
		OneWay // User stops after reaching the last waypoint
	}

	public interface IPathUser
	{
		Waypoint CurrentWaypoint { get; }
		Direction Direction { get; set; }
		Vector3 Position { get; set; }

		void Init( IMover mover, Path path );
	}
}

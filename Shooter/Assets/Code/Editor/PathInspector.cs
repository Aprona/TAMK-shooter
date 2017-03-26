using UnityEngine;
using UE = UnityEditor;
using TAMKShooter.WaypointSystem;

namespace TAMKShooter.Editor
{
	[UE.CustomEditor( typeof(Path)) ]
	public class PathInspector : UE.Editor
	{
		private const string ButtonText = "Add waypoint";

		private Path _target;

		private void OnEnable()
		{
			_target = target as Path;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if ( GUILayout.Button( ButtonText ) )
			{
				int waypointCount = _target.transform.childCount;
				string waypointName = string.Format( "Waypoint{0}",
					( waypointCount + 1 ).ToString( "D3" ) );
				GameObject waypoint =
					new GameObject( waypointName, typeof(Waypoint) );
				waypoint.transform.SetParent( _target.transform, true );
				UE.Selection.activeGameObject = waypoint;
			}
		}
	}
}

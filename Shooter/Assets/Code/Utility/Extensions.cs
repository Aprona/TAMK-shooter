using UnityEngine;

namespace TAMKShooter.Utility
{
	public static class Extensions
	{
		public static TComponent GetOrAddComponent<TComponent> (this GameObject gameObject)
			where TComponent : Component
		{
			TComponent component = gameObject.GetComponent<TComponent> ();
			if(component == null)
			{
				component = gameObject.AddComponent<TComponent> ();
			}
			return component;
		}

		public static void SetLayer(this GameObject gameObject, int layer,
			bool includeChildren = true)
		{
			gameObject.layer = layer;
			if(includeChildren)
			{
				foreach(Transform transform in
					gameObject.transform.GetComponentsInChildren<Transform>())
				{
					transform.gameObject.layer = layer;
				}
			}
		}
	}
}

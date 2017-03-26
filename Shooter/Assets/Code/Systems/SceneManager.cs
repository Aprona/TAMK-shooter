using UnityEngine;
using TAMKShooter.Systems.States;

namespace TAMKShooter.Systems
{
	public abstract class SceneManager : MonoBehaviour
	{
		private GameStateBase _associatedState;

		public abstract GameStateType StateType { get; }
		public virtual GameStateBase AssociatedState
		{
			get
			{
				if (_associatedState == null)
				{
					_associatedState =
						Global.Instance.GameManager.GetStateByStateType (StateType);
				}
				return _associatedState;
			}
		}
	}
}

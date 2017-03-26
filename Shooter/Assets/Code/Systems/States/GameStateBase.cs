using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SM=UnityEngine.SceneManagement; // Because we have our class named SceneManager

namespace TAMKShooter.Systems.States
{
	public abstract class GameStateBase
	{
		public abstract string SceneName { get; }
		public GameStateType State { get; protected set; }
		protected Dictionary<GameStateTransitionType, GameStateType> 
			Transitions { get; set; }

		protected GameStateBase()
		{
			Transitions = new Dictionary<GameStateTransitionType, GameStateType> ();
		}

		public bool AddTransition(GameStateTransitionType transition,
			GameStateType targetState)
		{
			if( transition == GameStateTransitionType.Error ||
				targetState == GameStateType.Error ||
				Transitions.ContainsKey(transition) )
			{
				// We cannot add a transition to our dictionary more than once.
				// Otherwise we wouldn't know to which state we should go with
				// that transition.
				return false;
			}

			Transitions.Add ( transition, targetState );
			return true;
		}

		public bool RemoveTransition(GameStateTransitionType transition)
		{
			return Transitions.Remove ( transition );
		}

		public GameStateType GetTargetStateType ( GameStateTransitionType transition )
		{
			if ( Transitions.ContainsKey ( transition ) )
			{
				return Transitions[transition];
			}
			return GameStateType.Error;
		}

		public virtual void StateActivated()
		{
			if(SM.SceneManager.GetActiveScene().name != SceneName)
			{
				SM.SceneManager.sceneLoaded += HandleSceneLoaded;
				Global.Instance.StartCoroutine ( LoadScene () );
			}
		}

		public virtual void StateDeactivating()
		{
			// Notify state deactivating
			Global.Instance.GameManager.RaiseGameStateChangingEvent ( State );
		}

		private void HandleSceneLoaded ( SM.Scene scene,
			SM.LoadSceneMode loadMode )
		{
			if(scene == SM.SceneManager.GetSceneByName(SceneName))
			{
				SM.SceneManager.sceneLoaded -= HandleSceneLoaded;
				// Notify scene loaded
				Global.Instance.GameManager.RaiseGameStateChangedEvent ( State );
			}
		}

		private IEnumerator LoadScene()
		{
			yield return new WaitForSeconds ( 5 );
			SM.SceneManager.LoadScene ( SceneName );
		}
	}
}

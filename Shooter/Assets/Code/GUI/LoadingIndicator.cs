using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TAMKShooter.Systems;
using DG.Tweening;
using System;

namespace TAMKShooter.GUI
{
	public class LoadingIndicator : MonoBehaviour
	{
		[SerializeField] private Image _indicatorImage;
		[SerializeField] private Image _backgroundImage;

		private Coroutine _rotateCoroutine;
		private Color _indicatorImageColor;
		private List<Tweener> _tweeners = new List< Tweener >();

		public void Init()
		{
			gameObject.SetActive ( false );
			Global.Instance.GameManager.GameStateChanging +=
				HandleGameStateChanging;
			Global.Instance.GameManager.GameStateChanged +=
				HandleGameStateChanged;

			_indicatorImageColor = _indicatorImage.color;
		}

		protected void OnDestroy()
		{
			Global.Instance.GameManager.GameStateChanging -=
				HandleGameStateChanging;
			Global.Instance.GameManager.GameStateChanged -=
				HandleGameStateChanged;
		}

		private void HandleGameStateChanged ( GameStateType obj )
		{
			StopCoroutine ( _rotateCoroutine );
			_rotateCoroutine = null;
			
			// Tween from black to invisible and after that execute method
			// TweenCompleted.
			_tweeners.Add( DOTween.To ( () => _backgroundImage.color,
				( value ) => _backgroundImage.color = value,
				Color.clear, 0.5f ).OnComplete ( TweenCompleted ) );

			_tweeners.Add( DOTween.To( () => _indicatorImage.color,
				( value ) => _indicatorImage.color = value,
				Color.clear, 0.5f ) );
		}

		private void TweenCompleted ()
		{
			foreach ( var tweener in _tweeners )
			{
				if ( tweener.IsPlaying() )
				{
					tweener.Kill( true );
				}
			}

			_tweeners.Clear();

			gameObject.SetActive ( false );
		}

		private void HandleGameStateChanging ( GameStateType obj )
		{
			gameObject.SetActive ( true );
			_rotateCoroutine = StartCoroutine ( Rotate () );

			_backgroundImage.color = Color.clear;
			_indicatorImage.color = Color.clear;

			DOTween.To ( () => _backgroundImage.color,
				( value ) => _backgroundImage.color = value,
				Color.black, 0.5f );

			DOTween.To( () => _indicatorImage.color,
				( value ) => _indicatorImage.color = value,
				_indicatorImageColor, 0.5f );
		}

		private IEnumerator Rotate()
		{
			while(true)
			{
				_indicatorImage.transform.Rotate ( Vector3.forward,
					-180 * Time.deltaTime, Space.Self );
				yield return null;
			}
		}
	}
}

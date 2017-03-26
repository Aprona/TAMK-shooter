using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TAMKShooter.Utility;
using TAMKShooter.Data;
using TAMKShooter.Systems.SaveLoad;

namespace TAMKShooter.Systems
{
	public class Global : MonoBehaviour
	{
		private static Global _instance;
		private static bool _isAppClosing = false;

		public static Global Instance
		{
			get
			{
				if(_instance == null && !_isAppClosing)
				{
					_instance = Instantiate( Resources.Load<Global> ( "Global" ) );
				}

				return _instance;
			}
		}

		[SerializeField] private Prefabs _prefabs;
		[SerializeField] private Pools _pools;

		public Prefabs Prefabs { get { return _prefabs; } }
		public Pools Pools { get { return _pools; } }
		public GameManager GameManager { get; private set; }
		public GameData CurrentGameData { get; set; }
		public SaveManager SaveManager { get; private set; }

		protected void Awake()
		{
			if(_instance == null)
			{
				// No instance set yet.
				// Let this object be our one and only instance.
				_instance = this;
			}

			if(_instance == this)
			{
				// This is the only allowed instance of the class.
				// Run initializations.
				Init ();
			}
			else
			{
				// Global is already instantiated! Destroy this instance.
				Destroy ( this );
			}
		}

		private void Init ()
		{
			DontDestroyOnLoad ( gameObject );

			if ( _prefabs == null )
			{
				_prefabs = GetComponentInChildren<Prefabs> ();
			}

			if(_pools == null)
			{
				_pools = GetComponentInChildren<Pools> ();
			}

			//SaveManager =
			//	new SaveManager( new BinaryFormatterSaveLoad< GameData >() );
			SaveManager =
				new SaveManager( new JSONSaveLoad< GameData >() );

			GameManager = gameObject.GetOrAddComponent<GameManager> ();
			GameManager.Init ();
		}

		private void OnApplicationQuit()
		{
			_isAppClosing = true;
		}
	}
}

using System;
using TAMKShooter.Data;
using UnityEngine;
using TAMKShooter.Configs;
using System.Collections.Generic;
using System.Collections;

namespace TAMKShooter
{
	public class PlayerUnit : UnitBase
	{
		public enum UnitType
		{
			None = 0,
			Fast = 1,
			Balanced = 2,
			Heavy = 3
		}

		[SerializeField] private UnitType _type;

		public UnitType Type { get { return _type; } }
		public PlayerData Data { get; private set; }
        public float InvulnerabilityTime;
        private float _invulnerabilitytimer;
        private float _flickTimer;
        private MeshRenderer _renderer;
        private GameObject _spawnPoint;

		public override int ProjectileLayer
		{
			get
			{
				return LayerMask.NameToLayer ( Config.PlayerProjectileLayerName );
			}
		}

		public void Init( PlayerData playerData )
		{
            _renderer = GetComponent<MeshRenderer>();
            string spawnPointName = playerData.Id.ToString() + "Spawn";
            _spawnPoint = GameObject.Find(spawnPointName);
			InitRequiredComponents();
            transform.position = _spawnPoint.transform.position;
			Data = playerData;

            if (playerData.Id == PlayerData.PlayerId.Player1)
            {
                Debug.Log("Player1 is going to die 3 times during next 10 seconds");
                StartCoroutine(death(2f));
                StartCoroutine(death(5f));
                StartCoroutine(death(10f));
            }
            
		}

        private IEnumerator death(float howLong)
        {
            yield return new WaitForSeconds(howLong);
            Die();

        }

        

		protected override void Die ()
		{
            // TODO: Handle dying properly!
            // Instantiate explosion effect
            // Play sound
            // Decrease lives
            // Respawn player
            Data.Lives -= 1;

            if (Data.Lives > 0)
            {
                transform.position = _spawnPoint.transform.position;                
                _invulnerabilitytimer = InvulnerabilityTime;
                _flickTimer = 0.2f;
            } else
            {
                gameObject.SetActive(false);
            }				
		}

        private void Update()
        {
            if (_invulnerabilitytimer > 0)
            {
                if (_flickTimer < 0)
                {
                    if (_renderer.enabled)
                        _renderer.enabled = false;
                    else
                        _renderer.enabled = true;

                    _flickTimer = 0.2f;
                }

                _invulnerabilitytimer -= Time.deltaTime;
                _flickTimer -= Time.deltaTime;
            } else
            {
                _renderer.enabled = true;
            }

            
        }

        public void HandleInput ( Vector3 input, bool shoot )
		{
			Mover.MoveToDirection ( input );
			if(shoot)
			{
				Weapons.Shoot (ProjectileLayer);
			}
		}

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.SetActive(false);
                Die();
            }
        }
    }
}

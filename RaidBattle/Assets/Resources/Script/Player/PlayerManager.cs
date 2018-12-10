using System;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

public class PlayerManager : MonoBehaviour {


	private ActorBase actorBase;

	private List<StatusBase> allStatus;
	private StatusBase playerStatus;

	private GameObject enemy;
	private GameObject target;

	// Use this for initialization
    void Start()
    {
		allStatus = new List<StatusBase>();
		allStatus.Add(new PlayerIdle(gameObject));
		enemy = GameObject.Find("Enemy");
		target = GameObject.Find("Target");

		playerStatus = new PlayerIdle(gameObject);
		playerStatus.OnStart();
    }

	// Update is called once per frame
	void Update()
	{
		playerStatus.OnUpdate();

		if (Input.anyKey)
		{
			foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
			{
				if (Input.GetKey(code))
				{
					switch (code)
					{
						case KeyCode.W:
						case KeyCode.A:
						case KeyCode.S:
						case KeyCode.D:
							ChangeStatus(EPlayerState.Run);
							break;

						default:
							ChangeStatus(EPlayerState.Idle);
							break;
					}
				}
				if (Input.GetKeyDown(code))
				{
					switch (code)
					{
						case KeyCode.V:
							ChangeStatus(EPlayerState.Atk, code);
							StartCoroutine(EffectPlayer.Instance.ShotMagicEffect("EnergeBlast", target.transform.position));
                            break;

						case KeyCode.X:
							ChangeStatus(EPlayerState.Atk, code);
							StartCoroutine(EffectPlayer.Instance.ShotMagicEffect("FireShot", target.transform.position));
                            break;

						case KeyCode.C:
							ChangeStatus(EPlayerState.Atk, code);
							StartCoroutine(EffectPlayer.Instance.ShotMagicEffect("FrameBall", target.transform.position));
                            break;

						case KeyCode.B:
							ChangeStatus(EPlayerState.Atk, code);
							StartCoroutine(EffectPlayer.Instance.ShotMagicEffect("GreenCore", target.transform.position));
							break;

						case KeyCode.Space:
							ChangeStatus(EPlayerState.Jump);
							break;

						default:
							ChangeStatus(EPlayerState.Idle);
							break;

					}
				}
                
			}
		}
		else
		{
			ChangeStatus(EPlayerState.Idle);
		}


	}

	private void ChangeStatus(EPlayerState ePlayerState, KeyCode keyCode = KeyCode.JoystickButton9)
	{
		if(ePlayerState != playerStatus.PlayerState)
		{
			playerStatus.OnExit();

			switch (ePlayerState)
			{
				case EPlayerState.Idle:
					playerStatus = new PlayerIdle(gameObject);
					break;

				case EPlayerState.Run:
					playerStatus = new PlayerRun(gameObject);
					break;
                    
				case EPlayerState.Atk:
					playerStatus = new PlayerAttack(gameObject, keyCode);
					break;

				case EPlayerState.Hit:
					
					break;

				case EPlayerState.Die:

					break;

				case EPlayerState.Jump:
					playerStatus = new PlayerJamp(gameObject);
					break;
                   
				default:
					break;
			}
			playerStatus.OnStart();

		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

public class PlayerJamp : StatusBase
{
    
	public PlayerJamp(GameObject @object) : base(@object) { }
    
	public override void OnStart()
	{
		base.OnStart();
		ePlayerState = EPlayerState.Jump;
        animator.SetInteger(animatorName, (int)ePlayerState);
		Debug.Log(ePlayerState);
	}

	public override void OnUpdate()
	{
		base.OnUpdate();
        
	}

	public override void OnExit()
	{
		base.OnExit();
	}
}

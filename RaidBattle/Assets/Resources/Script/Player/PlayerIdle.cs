using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

public class PlayerIdle : StatusBase {

	public PlayerIdle(GameObject @object) : base(@object) { }


    public override void OnStart()
    {
        base.OnStart();
        ePlayerState = EPlayerState.Idle;
        animator.SetInteger(animatorName, (int)ePlayerState);
        moveSpeed = 0;
		Debug.Log(ePlayerState);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}

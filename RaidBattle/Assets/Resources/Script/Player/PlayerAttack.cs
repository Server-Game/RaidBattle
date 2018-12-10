using UnityEngine;
using PlayerState;

public class PlayerAttack : StatusBase 
{
	
	public PlayerAttack(GameObject @object, KeyCode keyCode) : base(@object, keyCode) { }

	private GameObject effectObject;
       
	public override void OnStart()
	{
		base.OnStart();
		ePlayerState = EPlayerState.Atk;
		animator.SetInteger(animatorName, (int)ePlayerState);
		animator.Play((int)ePlayerState);        
		moveSpeed = 0;

	}

	public override void OnUpdate()
	{
		base.OnUpdate();
        
	}
}

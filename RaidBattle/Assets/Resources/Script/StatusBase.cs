using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerState
{
	public class StatusBase  : IPlayerStatus
	{		
		
		protected GameObject player;

		protected Animator animator;
        
		protected GameObject @object;

		protected KeyCode keyCode;
        
		protected float moveSpeed;

		public float MoveSpeed
		{
			get { return moveSpeed; }
		}

		protected EPlayerState ePlayerState;

		public EPlayerState PlayerState
		{
			get { return ePlayerState; }
		}
        

		protected string animatorName;


		public StatusBase(GameObject @object)
		{
			this.@object = @object;
		}

        public StatusBase(GameObject @object, KeyCode keyCode)
		{
			this.@object = @object;
			this.keyCode = keyCode;
		}
       

		public override void OnStart()
		{
			animatorName = "PlayerState";
            player = GameObject.Find("Player");
            animator = player.GetComponent<Animator>();
			ePlayerState = EPlayerState.Idle;
			animator.SetInteger(animatorName, (int)ePlayerState);
			moveSpeed = 0;
		}
             
		public override void OnUpdate() { }

		public override void OnExit() { }
	}
}

public enum EPlayerState
{
    Idle,
    Hit,
    Run,
    Die,
    Atk
}
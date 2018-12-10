using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

public class PlayerRun : StatusBase
{
	public PlayerRun(GameObject @object) : base(@object) { }


    private float inputHorizontal;
	private float inputVertical;
	private Rigidbody rb;
    

	public override void OnStart()
    {
        base.OnStart();
		ePlayerState = EPlayerState.Run;
		animator.SetInteger(animatorName, (int)ePlayerState);
        moveSpeed = 3f;
		rb = @object.GetComponent<Rigidbody>();
		Debug.Log(ePlayerState);
    }
    

	public override void OnUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
		Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * (inputHorizontal * 1.0f);

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            @object.transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

	public override void OnExit()
	{
		base.OnExit();
	}
}

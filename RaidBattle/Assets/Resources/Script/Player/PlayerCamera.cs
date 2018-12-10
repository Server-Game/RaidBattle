using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
	GameObject targetObj;
	Vector3 targetPos;

	void Start()
	{
		targetObj = GameObject.Find("Player");
		targetPos = targetObj.transform.position;
	}

	void Update()
	{
		// targetの移動量分、自分（カメラ）も移動する
		transform.position += targetObj.transform.position - targetPos;
		targetPos = targetObj.transform.position;


		// マウスの移動量
		float mouseInputX = Input.GetAxis("Mouse X");
		float mouseInputY = Input.GetAxis("Mouse Y");
		// targetの位置のY軸を中心に、回転（公転）する
		transform.RotateAround(targetPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);

		// カメラがプレイヤーの真上や真下にあるときにそれ以上回転させないようにする
		if (transform.forward.y > 0.6f && -mouseInputY < 0)
		{
			mouseInputY = 0;
		}
		if (transform.forward.y < -0.6f && -mouseInputY > 0)
		{
			mouseInputY = 0;
		}


		// カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
		transform.RotateAround(targetPos, transform.right, -mouseInputY * Time.deltaTime * 200f);

	}
}


/**************************************************************************************/
/*! @file     EffectPlayer.cs
***************************************************************************************
* @brief      サウンドを出力するクラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2018 Ryo Sugiyama All Rights Reserved.
**********************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectPlayer : MonoBehaviour
{

	private static EffectPlayer obj = null;

	public static EffectPlayer Instance
	{
		private set { obj = value; }
		get
		{
			// なければ生成
			if (obj == null)
				obj = new EffectPlayer();
			return obj;
		}
	}

	public class EffectClipInfo
	{
		public string effectName;
		public string objectPath;
		public GameObject effectObject;

		public EffectClipInfo(string objectPath, string effectName)
		{
			this.effectName = effectName;
			this.objectPath = objectPath;
		}

	}

	private Dictionary<string, EffectClipInfo> effectClips = new Dictionary<string, EffectClipInfo>();

	private GameObject effectObject;

	/// <summary>
	/// 指定エフェクトフェクトデータをコンテナに追加する。
	/// エフェクトデータ(Prefab)はResourcesフォルダ直下においてください。
	/// effectClips.Add("呼び出し時の名前", new EffectClipInfo("サウンドデータのパス", "ヒエラルキーに出る名前"));
	/// </summary>
	private EffectPlayer()
	{
		effectClips.Add("MagicCircle", new EffectClipInfo("Effect/MagicCircle/Prefabs/fx_Magiccircle_p", "MagicCircleEffect"));
		effectClips.Add("EnergeBlast", new EffectClipInfo("Effect/MagicEffect/Prefab/energyBlast", "EnergeBlastEffect"));
		effectClips.Add("ElekiBall1", new EffectClipInfo("Effect/MagicEffect/Prefab/ErekiBall", "ElekiBallEffect"));
		effectClips.Add("ElekiBall2", new EffectClipInfo("Effect/MagicEffect/Prefab/ErekiBall2", "ElekiBall2Effect"));
		effectClips.Add("FireShot", new EffectClipInfo("Effect/MagicEffect/Prefab/fireShot", "FireShotEffect"));
		effectClips.Add("FrameBall", new EffectClipInfo("Effect/MagicEffect/Prefab/frameBall", "FrameBallEffect"));
		effectClips.Add("GreenCore", new EffectClipInfo("Effect/MagicEffect/Prefab/GreenCore", "GreenCoreEffect"));
	}

	/// <summary>
	/// エフェクトの再生
	/// </summary>
	/// <param name="effectName"> エフェクトの名前 </param>
	/// <param name="vector3"> transform.position 等 </param>    
	/// <param name="float"> 消えるまでの時間 </param>
	public void PlayEffect(string effectName, Vector3 vector3, float destroyTime = 3.0f)
	{

		if (!effectClips.ContainsKey(effectName))
		{
			Debug.LogError("<color=red>" + effectName + "がコンテナの中にありません。呼び出し名が合っているか確認してください。</color>");
			return;
		}

		EffectClipInfo effectInfo = effectClips[effectName];

		if (effectInfo.effectObject == null)
		{
			effectInfo.effectObject = (GameObject)Resources.Load(effectInfo.objectPath);
			if (effectInfo.effectObject == null)
			{
				Debug.LogError("<color=red>" + effectInfo.objectPath + "は、間違っています。</color>");
			}
		}


		// destroyTime後に消えるように生成
		Destroy(effectObject = (GameObject)Instantiate(effectInfo.effectObject, vector3, Quaternion.identity), destroyTime);

		effectObject.name = effectClips[effectName].effectName;
		effectObject.tag = "Effect";

	}

	public IEnumerator PlayMagicEffectThere(string effectName, Vector3 me)
	{
		PlayEffect("MagicCircle", me, 2.0f);

		yield return new WaitForSeconds(0.5f);

		PlayEffect(effectName, me);

		yield return true;
	}

	public IEnumerator ShotMagicEffect(string effectName, Vector3 vector3)
	{
		PlayEffect("MagicCircle", vector3, 1.0f);

		yield return new WaitForSeconds(0.5f);

		if (!effectClips.ContainsKey(effectName))
		{
			Debug.LogError("<color=red>" + effectName + "がコンテナの中にありません。呼び出し名が合っているか確認してください。</color>");
			yield break;
		}

		EffectClipInfo effectInfo = effectClips[effectName];

		if (effectInfo.effectObject == null)
		{
			effectInfo.effectObject = (GameObject)Resources.Load(effectInfo.objectPath);
			if (effectInfo.effectObject == null)
			{
				Debug.LogError("<color=red>" + effectInfo.objectPath + "は、間違っています。</color>");
				yield break;
			}
		}

		vector3 += new Vector3(0, 0.3f, 0);

		Destroy(effectObject = (GameObject)Instantiate(effectInfo.effectObject, vector3, Quaternion.identity), 4.0f);

		effectObject.name = effectClips[effectName].effectName;
		effectObject.tag = "Effect";

		Vector3 Force = Camera.main.transform.forward;

		while (effectObject != null)
		{
            
			effectObject.GetComponent<Rigidbody>().AddForce(Force * 20.0f); 

			yield return new WaitForEndOfFrame();
		}

		yield break;
	}

	public IEnumerator TrackingMagicEffect(string effectName, Vector3 me, Vector3 you)
	{
		PlayEffect("MagicCircle", me, 1.0f);

		yield return new WaitForSeconds(0.5f);

		if (!effectClips.ContainsKey(effectName))
		{
			Debug.LogError("<color=red>" + effectName + "がコンテナの中にありません。パスが間違っているか、呼び出し名が間違っているか確認してください。</color>");
			yield break;
		}

		EffectClipInfo effectInfo = effectClips[effectName];

		if (effectInfo.effectObject == null)
		{
			effectInfo.effectObject = (GameObject)Resources.Load(effectInfo.objectPath);
		}

		effectObject = (GameObject)Instantiate(effectInfo.effectObject, me, Quaternion.identity);

		effectObject.name = effectClips[effectName].effectName;
		effectObject.tag = "Effect";

		float timeStep = 0;

		while (timeStep < 11)
		{
			effectObject.transform.position = Vector3.Lerp(me, you, timeStep * 0.1f);
			timeStep += 1f;

			yield return new WaitForEndOfFrame();
		}

		Destroy(effectObject);

		yield return true;
	}


}

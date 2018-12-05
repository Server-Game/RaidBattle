/********************************************************************************************
* @file       SoundPlayer.cs
*********************************************************************************************
* @brief      サウンドを出力するクラス
*********************************************************************************************
* @author     Ryo Sugiyama
*********************************************************************************************
* Copyright © 2017 Ryo Sugiyama All Rights Reserved.
*********************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer
{
    static SoundPlayer obj = null;

	public static SoundPlayer Instance
    {
        set { obj = value; }
        get
        {
            // なければ生成
            if (obj == null)
				obj = new SoundPlayer();
            return obj;
        }
    }

    GameObject soundPlayerObj;  // @brief サウンドプレイヤーオブジェクトを格納する変数
    AudioSource audioSource;    // @brief Unityのオーディオ設定関連のクラスインスタンス
    BGMPlayer curBGMPlayer;     // @brief 現在再生しているBGMのオブジェクトを格納する変数
    BGMPlayer fadeOutBGMPlayer; // @brief フェードアウト中のBGMのオブジェクトを格納する変数

    Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();

    class AudioClipInfo
    {

        public string resourceName;
        public string name;
        public AudioClip clip;

        public AudioClipInfo(string resourceName, string name)
        {
            this.resourceName = resourceName;
            this.name = name;
        }

    }
    
    /// <summary>
	/// 指定サウンドデータをオーディオクリップに追加する。
	/// サウンドデータはResourcesフォルダ直下においてください。
	/// audioClips.Add("呼び出し時の名前", new AudioClipInfo("サウンドデータのパス", "ヒエラルキーに出る名前"));
    /// </summary>
    public SoundPlayer()
    {
        // exp
		//audioClips.Add("SelectSE", new AudioClipInfo("Sound/Select", "SelectButtonSE"));
    }
 
    /// <summary>
	/// 追加したサウンドデータを再生する
    /// </summary>
	/// <returns><c>true</c>, 再生の成功 <c>false</c> 再生の失敗 </returns>
    /// <param name="seName"> SEの名前 </param>
	/// <param name="volume"> ボリュームの設定 (0f ~ 1.0f) </param>
	public bool PlaySE(string seName, float volume = 1.0f)
    {

        if (audioClips.ContainsKey(seName) == false)
        {
			Debug.LogError("<color=red>" + seName + "がコンテナの中にありません。パスが間違っているか、呼び出し名が間違っているか確認してください。</color>");
            return false;
        }

        AudioClipInfo info = audioClips[seName];

        //なければロード
        if (info.clip == null)
            info.clip = (AudioClip)Resources.Load(info.resourceName);

        if (soundPlayerObj == null)
        {
			soundPlayerObj = new GameObject(audioClips[seName].name);
            audioSource = soundPlayerObj.AddComponent<AudioSource>();
        }

        //ボリュームの設定
        audioSource.volume = volume;
        audioSource.loop = false;
        //再生
        audioSource.PlayOneShot(info.clip);

        return true;
    }

    /// <summary>
    /// 追加したBGMの再生
    /// </summary>
    /// <param name="bgmName"> BGMの名前 </param>
    /// <param name="fadeTime"> フェード時間 </param>
    /// <param name="isLoop"> ループするかどうか </param>
	/// <param name="volume"> ボリュームの設定 (0.0f ~ 1.0f) </param>
    public void PlayBGM(string bgmName, float fadeTime = 0.0f, bool isLoop = true, float volume = 1.0f)
    {
        // 現在のBGMを消去
        if (fadeOutBGMPlayer != null)
        {
            fadeOutBGMPlayer.destory();
        }

        // 現在のBGMをフェードアウト
        if (curBGMPlayer != null)
        {
            curBGMPlayer.StopBGM(fadeTime);
            fadeOutBGMPlayer = curBGMPlayer;
        }

        // 新しいBGMを再生
        if (audioClips.ContainsKey(bgmName) == false)
        {
			Debug.LogError("<color=red>" + bgmName + "がコンテナの中にありません。パスが間違っているか、呼び出し名が間違っているか確認してください。</color>");
            // null BGM
            curBGMPlayer = new BGMPlayer();
        }
        else
        {
            curBGMPlayer = new BGMPlayer(audioClips[bgmName].resourceName);
            curBGMPlayer.PlayBGM(fadeTime, isLoop, volume);
        }

    }

    public void PlayBGM()
    {

        if (curBGMPlayer != null)
        {
            curBGMPlayer.PlayBGM();
        }

        if (fadeOutBGMPlayer != null)
        {
            fadeOutBGMPlayer.PlayBGM();
        }

    }

    public void PauseBGM()
    {

        if (curBGMPlayer != null)
        {
            curBGMPlayer.PauseBGM();
        }

        if (fadeOutBGMPlayer != null)
        {
            fadeOutBGMPlayer.PauseBGM();
        }

    }

    public void StopBGM(float fadeTime)
    {


        if (curBGMPlayer != null)
        {
            curBGMPlayer.StopBGM(fadeTime);
        }

        if (fadeOutBGMPlayer != null)
        {
            fadeOutBGMPlayer.StopBGM(fadeTime);
        }

    }

    public void Pause(bool argPlaySound)
    {

        if (argPlaySound)
        {
            PauseBGM();
        }
        else
        {
            PlayBGM();
        }

    }

    public void Update()
    {

        if (curBGMPlayer != null)
        {
            curBGMPlayer.Update();
        }

        if (fadeOutBGMPlayer != null)
        {
            fadeOutBGMPlayer.Update();
        }

    }

}
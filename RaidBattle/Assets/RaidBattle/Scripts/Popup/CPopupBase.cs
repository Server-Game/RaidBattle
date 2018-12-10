/**************************************************************************************
/*! @file   CPopupEvent.cs
***************************************************************************************
@brief      ポップアップの基底クラス
***************************************************************************************
@author     yuta takatsu
**************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// @brief ポップアップに関する列挙
/// @none  開閉時に関する状態
/// </summary>
public enum EPopupState
{
    OPEN_BEGIN,
    OPEN_RUN,
    OPEN_END,
    CLOSE_BEGIN,
    CLOSE_RUN,
    CLOSE_END
}

public class CPopupBase : MonoBehaviour
{

    [SerializeField]
    private RectTransform m_popupWindowBase; // @brief 生成するpopupのデータを格納
    private RectTransform m_popupWindow;     // @brief 実際に生成するときに呼ぶ変数

    [SerializeField]
    private RectTransform m_popupRoot; // @brief 生成場所

    private bool m_isActive; // @brief ポップアップのイベントが動いているか

    /// <summary>
    /// @brief ポップアップの状態を表すアクセサー
    /// </summary>
    public EPopupState PopupState
    {
        get;
        private set;
    }

    /// <summary>
    /// @brief ポップアップのイベントの状態を通知するアクセサー
    /// </summary>
    public bool IsActive
    {
        set { m_isActive = value; }
        get { return m_isActive; }
    }

    class CPopupAction
    {
        public System.Action begin; // @brief ポップアップを呼び出すとき
        public System.Action run;   // @brief ポップアップを呼び出し中
        public System.Action end;   // @brief ポップアップを閉じるとき
    }

    // 開閉用インスタンス
    CPopupAction openAction = new CPopupAction();
    CPopupAction closeAction = new CPopupAction();

    float m_time = float.NaN;
    private void Awake()
    {
        // 生成するオブジェクトを代入
        if (m_popupWindow == null)
        {
            m_popupWindow = Instantiate(m_popupWindowBase) as RectTransform;
        }
        // 生成場所と大きさを指定
        m_popupWindow.SetParent(m_popupRoot, false);
        m_popupWindow.transform.localScale = new Vector3(1, 0);

        m_popupWindow.gameObject.SetActive(false);
    }

    /// <summary>
    /// @brief ポップアップを開く関数
    /// </summary
    /// <param name="openBeginAction">開くとき</param>
    /// <param name="openRunAction">開いてる途中</param>
    /// <param name="openEndAction">開き終わったとき</param>
    /// <param name="time">開く時間</param>
    public virtual void Open(System.Action openBeginAction, System.Action openRunAction = null, System.Action openEndAction = null, float time = 0.25f)
    {

        m_popupWindow.gameObject.SetActive(true);
        m_popupWindow.transform.localScale = new Vector3(1, 0);

        openAction.begin = openBeginAction;
        openAction.run = openRunAction;
        openAction.end = openEndAction;
        this.m_time = time;

        OnOpen();

    }

    /// <summary>
    /// @brief ポップアップを閉じる関数
    /// </summary
    /// <param name="closeBeginAction">開くとき</param>
    /// <param name="closeRunAction">開いてる途中</param>
    /// <param name="closeEndAction">開き終わったとき</param>
    /// <param name="time">開く時間</param>
    public virtual void Close(System.Action closeBeginAction, System.Action closeRunAction = null, System.Action closeEndAction = null, float time = 0.25f)
    {

        m_popupWindow.transform.localScale = new Vector3(1, 0);

        closeAction.begin = closeBeginAction;
        closeAction.run = closeRunAction;
        closeAction.end = closeEndAction;
        this.m_time = time;

        OnClose();

    }

    void OnOpen()
    {

        var popup = m_popupWindow.DOScale(new Vector3(1, 1), m_time).SetEase(Ease.InOutQuart);
        popup.OnStart(() =>
        {

            if (openAction.begin != null)
            {
                openAction.begin.Invoke();
                openAction.begin = null;
            }
            PopupState = EPopupState.OPEN_BEGIN;
        })
        .OnUpdate(() =>
        {

            if (openAction.run != null)
            {
                openAction.run.Invoke();
                openAction.run = null;
            }
            PopupState = EPopupState.OPEN_RUN;
        })
        .OnComplete(() =>
        {

            if (openAction.end != null)
            {
                openAction.end.Invoke();
                openAction.end = null;
            }
            PopupState = EPopupState.OPEN_END;
        });
    }

    void OnClose()
    {

        var popup = m_popupWindow.DOScale(new Vector3(1, 0), m_time).SetEase(Ease.InOutQuart);
        popup.OnStart(() =>
        {

            if (closeAction.begin != null)
            {
                closeAction.begin.Invoke();
                closeAction.begin = null;
            }
            PopupState = EPopupState.CLOSE_BEGIN;
        })
        .OnUpdate(() =>
        {

            if (closeAction.run != null)
            {
                closeAction.run.Invoke();
                closeAction.run = null;
            }
            PopupState = EPopupState.CLOSE_RUN;
        })
        .OnComplete(() =>
        {

            if (closeAction.end != null)
            {
                closeAction.end.Invoke();
                closeAction.end = null;
            }
            PopupState = EPopupState.CLOSE_END;
        });
    }
}
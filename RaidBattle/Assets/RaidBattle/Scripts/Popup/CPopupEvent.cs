/**************************************************************************************
/*! @file   CPopupEvent.cs
***************************************************************************************
@brief      ポップアップのイベントを起こすクラス
***************************************************************************************
@author     yuta takatsu
**************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CPopupEvent : CPopupBase 
{

    [SerializeField]
    private GameObject m_contents; // @brief ポップアップの中身を格納

    /// <summary>
    /// @brief ポップアップを開く
    /// </summary>
    public void Open()
    {
        base.Open(null, null, PopupOpen);
    }

    /// <summary>
    /// @brief ポップアップを閉じる
    /// </summary>
    public void Close()
    {
        base.Close(null, null, PopupClose);
    }

    /// <summary>
    /// @brief ポップアップ内のコンテンツを表示させる
    /// </summary>
    private void PopupOpen()
    {
        m_contents.SetActive(true);
    }

    /// <summary>
    /// @brief ポップアップ内のコンテンツを非表示にする
    /// </summary>
    private void PopupClose()
    {
        m_contents.SetActive(false);
    }
}
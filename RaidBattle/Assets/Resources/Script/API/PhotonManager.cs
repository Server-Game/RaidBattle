using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonManager : Photon.MonoBehaviour
{
	public void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void ConnectFhoton()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");

    }

    public void OnJoinedLobby()
    {
        Debug.Log("<color=red>PhotonManager OnJoinedLobby</color>");
        GameObject.Find("CreateRoom").GetComponent<Button>().interactable = true;
        GameObject.Find("JoinRoom").GetComponent<Button>().interactable = true;

    }

    //ルーム作成
    public void CreateRoom()
    {
        string userName = "ユーザ1";
        string userId = "user1";

        PhotonNetwork.autoCleanUpPlayerObjects = false;

        //カスタムプロパティ
        ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        customProp.Add("userName", userName); //ユーザ名
        customProp.Add("userId", userId); //ユーザID
        PhotonNetwork.SetPlayerCustomProperties(customProp);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.customRoomProperties = customProp;
        //ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
        roomOptions.customRoomPropertiesForLobby = new string[] { "userName", "userId" };
        roomOptions.maxPlayers = 2; //部屋の最大人数
        roomOptions.isOpen = true; //入室許可する
        roomOptions.isVisible = true; //ロビーから見えるようにする
        //userIdが名前のルームがなければ作って入室、あれば普通に入室する。
        PhotonNetwork.JoinOrCreateRoom(userId, roomOptions, null);
    }

    //ルーム一覧が取れると
    public void OnReceivedRoomListUpdate()
    {
        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0)
        {
            Debug.Log("ルームが一つもありません");
        }
        else
        {
            //ルームが1件以上ある時ループでRoomInfo情報をログ出力
            for (int i = 0; i < rooms.Length; i++)
            {
                Debug.Log("RoomName:" + rooms[i].name);
                Debug.Log("userName:" + rooms[i].customProperties["userName"]);
                Debug.Log("userId:" + rooms[i].customProperties["userId"]);
                GameObject.Find("StatusText").GetComponent<Text>().text = rooms[i].name;
            }
        }
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("user1");
    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    public void OnJoinedRoom()
    {
        Debug.Log("PhotonManager OnJoinedRoom");
        GameObject.Find("StatusText").GetComponent<Text>().text = "ルームに入室しました";

        GameObject.Find("Photon").GetComponent<Button>().interactable = false;
        GameObject.Find("CreateRoom").GetComponent<Button>().interactable = false;
        GameObject.Find("JoinRoom").GetComponent<Button>().interactable = false;

    }
    private void Update()
    {

    }

}
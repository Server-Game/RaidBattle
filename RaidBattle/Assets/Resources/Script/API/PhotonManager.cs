using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonManager : Photon.MonoBehaviour
{
	[SerializeField]
	Button roomButton;

	private List<string> userNames;

	public void Start()
	{
		userNames = new List<string>();
		
		DontDestroyOnLoad(this.gameObject);
		ConnectFhoton();
		OnJoinedLobby();
	}

	public void ConnectFhoton()
    {
        var flag = PhotonNetwork.ConnectUsingSettings("v1.0");

		if (flag) Debug.Log("<color=Blue>Success Connect PUN");
		else Debug.LogError("<color=red>Faild Connect PUN");
    }

    public void OnJoinedLobby()
    {
        Debug.Log("<color=Blue>PhotonManager OnJoinedLobby</color>");
        GameObject.Find("CreateRoom").GetComponent<Button>().interactable = true;
        GameObject.Find("JoinRoom").GetComponent<Button>().interactable = true;

    }

    //ルーム作成
    public void CreateRoom()
    {
		// TODO サーバから引っ張る
        string userName = "ユーザ1";
        string userId = "user1";

        PhotonNetwork.autoCleanUpPlayerObjects = false;

        //カスタムプロパティ
        ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        customProp.Add("userName", userName); //ユーザ名
        customProp.Add("userId", userId); //ユーザID
        PhotonNetwork.SetPlayerCustomProperties(customProp);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = customProp;
        //ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "userName", "userId" };
        roomOptions.MaxPlayers = 4; //部屋の最大人数
        roomOptions.IsOpen = true; //入室許可する
        roomOptions.IsVisible = true; //ロビーから見えるようにする
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
				userNames.Add(rooms[i].Name);
                Debug.Log("RoomName:" + rooms[i].Name);
                Debug.Log("userName:" + rooms[i].CustomProperties["userName"]);
                Debug.Log("userId:" + rooms[i].CustomProperties["userId"]);
                GameObject.Find("StatusText").GetComponent<Text>().text = rooms[i].Name;
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
        Debug.Log("<color=red>PhotonManager OnJoinedRoom</color>");
      
        GameObject.Find("StatusText").GetComponent<Text>().text = "ルームに入室しました";

        GameObject.Find("Photon").GetComponent<Button>().interactable = false;
        GameObject.Find("CreateRoom").GetComponent<Button>().interactable = false;
        GameObject.Find("JoinRoom").GetComponent<Button>().interactable = false;

    }
    private void Update()
    {

    }

	private void OnGUI()
	{
		
		GUI.Button(new Rect(20, 20, 100, 40), "ルーム一覧");

		if(userNames.Count == 0) 
			GUI.Button(new Rect(30, 80, 100, 40), "ルームなし");

		for (int i = 0; i < userNames.Count; i++)
		{
			if (i < 5)      GUI.Button(new Rect(30,  20 + (60 * (i + 1)), 100, 40), userNames[i]);
			else if(i < 10) GUI.Button(new Rect(150, 20 + (60 * (i + 1)), 100, 40), userNames[i]);

		}


	}

}
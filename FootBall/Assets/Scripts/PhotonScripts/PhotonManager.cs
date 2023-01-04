using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string region;
    [Space(10)]
    [SerializeField] AddRoomScrool scr_ARScr;
    [SerializeField] Transform content;
    [Space(10)]
    [SerializeField] GameObject blockLobby;
    [SerializeField] GameObject lobby;

    List<RoomInfo> allRoomsInfo = new List<RoomInfo>();
    [Space(10)]
    [SerializeField] TMP_InputField roomName;
    [SerializeField] TMP_Text connText;
    private void Awake()
    {
        PhotonNetwork.GameVersion = "1";
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
    }

    #region [ Pun Callbacks ]
    public override void OnConnectedToMaster()
    {
        blockLobby.SetActive(false);
        lobby.SetActive(true);
        connText.text = "Connected to region - " + PhotonNetwork.CloudRegion;
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Out to server");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room Adding " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room is not Adding");
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            for (int i = 0; i < allRoomsInfo.Count; i++)
            {
                if(allRoomsInfo[i].masterClientId == info.masterClientId)
                {
                    return;
                }
            }

            AddRoomScrool scrARscr = Instantiate(scr_ARScr, content);

            if (scrARscr != null)
            {
                scrARscr.SetInfo(info);
                allRoomsInfo.Add(info);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("LvlOne");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("MainMenu");
    }

    #endregion

    public void CreateRoomButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 2;

        GameObject mainMenu = GameObject.FindGameObjectWithTag("MAINMENU").transform.gameObject;
        GameObject lobby = mainMenu.transform.GetChild(1).transform.gameObject;
        GameObject lobbyCreate = mainMenu.transform.GetChild(2).transform.gameObject;

        lobby.SetActive(false);
        lobbyCreate.SetActive(true);
        AddRoomScrool scrARscr = Instantiate(scr_ARScr, content);
        PhotonNetwork.CreateRoom(roomName.text, roomOption, TypedLobby.Default);
        PhotonNetwork.LoadLevel("LvlOne");
    }
    public void JoinRandRoomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }
    public void LeaveButton()
    {
        PhotonNetwork.LeaveRoom();
    }
}

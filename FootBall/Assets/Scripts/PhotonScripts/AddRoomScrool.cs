using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class AddRoomScrool : MonoBehaviour
{
    [SerializeField] TMP_Text textName;
    [SerializeField] TMP_Text textPlayerCount;

    public void SetInfo(RoomInfo info)
    {
        textName.text = info.Name;
        textPlayerCount.text = info.PlayerCount + "/" + info.MaxPlayers;
    }

    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(textName.text);
    }
}

using UnityEngine;
using Photon.Pun;
using TMPro;

public class BlockGameNotTwoPlayer : MonoBehaviour
{
    [SerializeField] GameObject blockGamePlay;
    [SerializeField] TMP_Text textBlockGamePlay; 
    private void Awake()
    {
        blockGamePlay.SetActive(true);
    }
    void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            blockGamePlay.SetActive(false);
        }
        else if (blockGamePlay.activeSelf == true)
        {
            textBlockGamePlay.color = Color.Lerp(Color.white, Color.gray, Mathf.PingPong(Time.time, 1));
        }
    }
}

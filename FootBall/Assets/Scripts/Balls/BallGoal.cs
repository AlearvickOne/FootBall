using Photon.Pun;
using UnityEngine;
using TMPro;

public class BallGoal : MonoBehaviour
{
    private BallSpawn script_BS;
    protected internal Rigidbody enemyRb;

    private GameObject all_GUI;
    private GameObject levelObjects;

    private TMP_Text leftVorotaScore;
    private TMP_Text rightVorotaScore;

    private BoxCollider collIsGoalLeft;
    private BoxCollider collIsGoalRight;

    private GoalScore scr_GS;

    private PhotonView pV;
    protected internal PhotonTransformView pTV;
    private bool isActive;

    private void Awake()
    {
        all_GUI = GameObject.FindGameObjectWithTag("ALL_GUI").transform.gameObject;
        levelObjects = GameObject.FindGameObjectWithTag("LEVELOBJECTS").transform.gameObject;

        pV = GetComponent<PhotonView>();
        pTV = GetComponent<PhotonTransformView>();
        enemyRb = GetComponent<Rigidbody>();
        script_BS = GetComponentInParent<BallSpawn>();
        scr_GS = all_GUI.GetComponentInParent<GoalScore>();

        leftVorotaScore = all_GUI.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        rightVorotaScore = all_GUI.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        collIsGoalLeft = levelObjects.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<BoxCollider>();
        collIsGoalRight = levelObjects.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<BoxCollider>();
    }

    #region [ Photon RPC ]
    [PunRPC]
    private void RPC_LeftScoreAdd(int leftScore)
    {
        leftScore++;
        scr_GS.leftScore = leftScore;

    }

    [PunRPC]
    private void RPC_RightScoreAdd(int rightScore)
    {
        rightScore++;
        scr_GS.rightScore = rightScore;
    }

    [PunRPC]
    private void RPC_IsActiveFalse(bool loc_isActive)
    {
        loc_isActive = false;
        gameObject.SetActive(loc_isActive);
        isActive = loc_isActive;
    }

    [PunRPC]
    private void RPC_IndexAndAlive(int loc_enemyAlive, int loc_indexSpawn)
    {
        loc_enemyAlive--;
        if (loc_enemyAlive == 0)
        {
            loc_indexSpawn++;
        }
        script_BS.enemyAlive = loc_enemyAlive;
        script_BS.indexSpawn = loc_indexSpawn;
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other == collIsGoalLeft)
        {
            Debug.Log("left");
            pV.RPC("RPC_LeftScoreAdd", RpcTarget.All, scr_GS.leftScore);
            pV.RPC("RPC_IndexAndAlive", RpcTarget.All, script_BS.enemyAlive, script_BS.indexSpawn);
            leftVorotaScore.text = "Score: " + scr_GS.leftScore.ToString();
            if (pV.IsMine)
            {
                pV.RPC("RPC_IsActiveFalse", RpcTarget.All, isActive);
            }
        }

        if (other == collIsGoalRight)
        {
            Debug.Log("Right");
            pV.RPC("RPC_RightScoreAdd", RpcTarget.All, scr_GS.leftScore);
            pV.RPC("RPC_IndexAndAlive", RpcTarget.All, script_BS.enemyAlive, script_BS.indexSpawn);
            rightVorotaScore.text = "Score: " + scr_GS.rightScore.ToString();
            if (pV.IsMine)
            {
                pV.RPC("RPC_IsActiveFalse", RpcTarget.All, isActive);
            }
        }
    }
}

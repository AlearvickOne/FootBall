using UnityEngine;
using Photon.Pun;
using System;

public class PlayerBaffs : MonoBehaviour
{
    private PlayerController scr_PC;
    private BaffObjectSpawn scrBaffOS;
    private PlayersSpawn scrPS;
    private GameObject spawnBall;

    [SerializeField] GameObject baffSpeedAct;
    [SerializeField] GameObject baffKnockOtherPlayer;
    [SerializeField] GameObject baffPullBallToPlayer;
    [Space(10)]
    [SerializeField] float timeActBaffSpeed;
    [SerializeField] float timeActBaffKnockOtherPlayer;
    [SerializeField] float timeActBaffPullBallToPlayer;
    [Space(10)]
    [SerializeField] bool baffActive;

    private void Awake()
    {
        scrBaffOS = GameObject.FindGameObjectWithTag("SPAWNS").transform.GetChild(2).GetComponent<BaffObjectSpawn>();
        spawnBall = GameObject.FindGameObjectWithTag("SPAWNS").transform.GetChild(1).transform.gameObject;

        scr_PC = GetComponent<PlayerController>();
        baffSpeedAct.SetActive(false);
        baffKnockOtherPlayer.SetActive(false);
        baffPullBallToPlayer.SetActive(false);
    }

    private void Update()
    {
        BaffSpeedDeactive();
        BaffPullBallToPlayerActive();
    }

    private void BaffSpeedActive()
    {
        scr_PC._speedPlayer += 10;
        timeActBaffSpeed = 10;
        baffSpeedAct.SetActive(true);
    }
    private void BaffSpeedDeactive()
    {
        if (timeActBaffSpeed > 0)
        {
            timeActBaffSpeed -= Time.deltaTime;
        }

        else if (timeActBaffSpeed <= 0 && baffSpeedAct.activeSelf == true)
        {
            scr_PC._speedPlayer -= 10;
            baffSpeedAct.SetActive(false);
            baffActive = false;
        }
    }

    private void BaffSpeedSelection(Collider other)
    {
        scrPS = GetComponentInParent<PlayersSpawn>();
        for (int i = 0; i < scrBaffOS.baffSpeedList.Count; i++)
        {
            BoxCollider baffSpeedCollider = scrBaffOS.baffSpeedList[i].GetComponent<BoxCollider>();
            if (baffSpeedCollider == other)
            {
                BaffSpeedActive();
                baffActive = true;

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(other.gameObject);
                }
            }
        } 
    }
    private void BaffKnockOtherPlayerSelection(Collider other)
    {
        for (int i = 0; i < scrBaffOS.baffKnockList.Count; i++)
        {
            BoxCollider baffKnockCollider = scrBaffOS.baffKnockList[i].GetComponent<BoxCollider>();
            if (baffKnockCollider == other)
            {
                baffKnockOtherPlayer.SetActive(true);
                baffActive = true;

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(other.gameObject);
                }
            }
        }
    }
    private void BaffPullBallToPlayerSelection(Collider other)
    {
        for (int i = 0; i < scrBaffOS.baffPullList.Count; i++)
        {
            BoxCollider baffPullCollider = scrBaffOS.baffPullList[i].GetComponent<BoxCollider>();
            if (baffPullCollider == other)
            {
                baffPullBallToPlayer.SetActive(true);
                baffActive = true;

                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(other.gameObject);
                }
            }
        }
    }

    private void BaffKnockOtherPlayerActivated(Collider other)
    {
        for (int i = 0; i < scrPS.playersList.Count; i++)
        {
            SphereCollider playerCollider = scrPS.playersList[i].GetComponent<SphereCollider>();
            if (playerCollider == other && baffKnockOtherPlayer.activeSelf == true)
            {
                other.transform.GetComponent<Rigidbody>().velocity = ((other.transform.position + this.transform.position) * 200 * Time.fixedDeltaTime);
                baffKnockOtherPlayer.SetActive(false);
                baffActive = false;
            }
        }
    }

    private void BaffPullBallToPlayerActive()
    {
        if(baffPullBallToPlayer.activeSelf == true)
        {
            GameObject player = this.GetComponent<Transform>().gameObject;

            Transform[] ballObjects = spawnBall.gameObject.GetComponentsInChildren<Transform>();
            
            for (int i = 0; i < ballObjects.Length; i++)
            {
                foreach(Transform ball in ballObjects)
                {
                    ball.transform.position = player.transform.position;
                }
            }
            baffActive = false;
            baffPullBallToPlayer.SetActive(false);
            Array.Clear(ballObjects, 0, ballObjects.Length);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(baffActive == false)
        {
            BaffSpeedSelection(other);
            BaffPullBallToPlayerSelection(other);
            BaffKnockOtherPlayerSelection(other);
        }

        BaffKnockOtherPlayerActivated(other);
    }
}

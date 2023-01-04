using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] protected internal List<GameObject> ballsList;
    [Space(10)]
    [SerializeField] protected internal int enemyAlive;
    [SerializeField] protected internal int indexSpawn;
    [Space(10)]
    [SerializeField] int playerCount;
    [SerializeField] private bool startBallSpawn;
    Transform _spawnPoint;
    PhotonView pV;
    private int _randomIndex;
    bool isActive;

    private void Awake()
    {
        pV = GetComponent<PhotonView>();
        _spawnPoint = transform;
    }
    private void Start()
    {
        SpawnBallsStart();
    }

    private void Update()
    {
        if (startBallSpawn == true)
        {
            SetActiveBalls();
        }
        RemoveNullInLists();
    }

    #region [ Photon RPC ]
    [PunRPC]
    private void RPC_Active(bool active)
    {
        active = true;
        isActive = active;
    }

    [PunRPC]
    private void RPC_ActiveStart(bool active)
    {
        active = true;
        ballsList[0].SetActive(active);
        StartCoroutine(SynchronizationBall(0));
        isActive = active;
    }

    [PunRPC]
    private void RPC_Spawn(int loc_enemyAlive, int loc_indexSpawn, bool active)
    {
        loc_enemyAlive = enemyAlive;
        loc_indexSpawn = indexSpawn;

        if (loc_enemyAlive == 0)
        {
            for (int i = 0; i < loc_indexSpawn; i++)
            {
                ballsList[i].SetActive(active);
                isActive = active;
                loc_enemyAlive++;
                int randomX = Random.Range(-13, 3);
                int randomZ = Random.Range(2, 18);
                Vector3 randomPoint = new Vector3(randomX, transform.position.y, randomZ);
                ballsList[i].transform.position = randomPoint;
                StartCoroutine(SynchronizationBall(i));
            }
        }
        enemyAlive = loc_enemyAlive;
        indexSpawn = loc_indexSpawn;
    }

    [PunRPC]
    private void RPC_enemyAlive(int loc_enemyAlive)
    {
        loc_enemyAlive++;
        enemyAlive = loc_enemyAlive;
        pV.RPC("RPC_ActiveStart", RpcTarget.All, isActive);
    }

    [PunRPC]
    private void RPC_BoolStartBallSpawn(bool startBS)
    {
        startBS = true;
        startBallSpawn = startBS;
    }

    [PunRPC]
    private void RPC_PlayerCount(int pCount)
    {
        pCount = PhotonNetwork.PlayerList.Length;
        playerCount = pCount;
    }

    #endregion

    #region [ IENUMERATORS ]
    IEnumerator I_TimerSpawnStarBall()
    {
        yield return new WaitForSeconds(0.6f);
        pV.RPC("RPC_Active", RpcTarget.All, isActive);
        pV.RPC("RPC_enemyAlive", RpcTarget.All, enemyAlive);
        pV.RPC("RPC_BoolStartBallSpawn", RpcTarget.All, startBallSpawn);
    }

    IEnumerator SynchronizationBall(int i)
    {
        ballsList[i].GetComponent<BallGoal>().pTV.enabled = true;
        yield return new WaitForSeconds(2);
        ballsList[i].GetComponent<BallGoal>().pTV.enabled = false;
    }

    #endregion

    #region [ Private Methods ]
    private void SpawnBallsStart()
    {
        pV.RPC("RPC_PlayerCount", RpcTarget.All, playerCount);
        if (playerCount == 2 && PhotonNetwork.IsConnectedAndReady)
        {
            for (int i = 0; i < 50; i++)
            {
                SpawnEnemyIsNull();
            }
            StartCoroutine(I_TimerSpawnStarBall());
        }

    }
    private void SpawnEnemyIsNull()
    {
        for (int i = 0; i < _enemy.Length; i++)
        {
            _randomIndex = Random.Range(0, i);
        }

        float spawnX = _spawnPoint.position.x + Random.Range(5, 10);
        float spawnY = _spawnPoint.position.y;
        float spawnZ = _spawnPoint.position.z + Random.Range(5, 10);

        Vector3 spawn = new Vector3(spawnX, spawnY, spawnZ);
        string namesr = _enemy[_randomIndex].name;

        PhotonNetwork.Instantiate(namesr, spawn, Quaternion.identity);
    }
    private void SetActiveBalls()
    {
        pV.RPC("RPC_Spawn", RpcTarget.All, enemyAlive, indexSpawn, isActive);
    }
    private void RemoveNullInLists()
    {
        ballsList.RemoveAll(x => x == null);
    }

    #endregion
}

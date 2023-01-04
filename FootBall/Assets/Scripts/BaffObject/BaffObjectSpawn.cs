using System.Collections;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class BaffObjectSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] baffObject;
    [Space(5)]
    [SerializeField] protected internal List<GameObject> baffSpeedList;
    [Space(5)]
    [SerializeField] protected internal List<GameObject> baffPullList;
    [Space(5)]
    [SerializeField] protected internal List<GameObject> baffKnockList;

    private Renderer render;
    private Color color;

    private WaitForSeconds waitForSecond;
    void Start()
    {
        render = GetComponent<Renderer>();
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(I_TimerSpawnBaffSpeed());
            StartCoroutine(I_TimerSpawnBaffPull());
            StartCoroutine(I_TimerSpawnBaffKnock());
        }
    }

    private void Update()
    {
        RandomColor();
        RemoveNullInLists();
    }

    private void RemoveNullInLists()
    {
        baffSpeedList.RemoveAll(x => x == null);
        baffPullList.RemoveAll(x => x == null);
        baffKnockList.RemoveAll(x => x == null);
    }

    private void RandomColor()
    {
        color = Color.Lerp(Color.yellow, Color.green, Mathf.PingPong(Time.time, 1));
        render.sharedMaterial.color = color;
    }

    private Vector3 RandomSpawnPoint()
    {
        int randomX = Random.Range(-13, 3);
        int randomZ = Random.Range(2, 18);
        Vector3 randomPoint = new Vector3(randomX, transform.position.y, randomZ);
        return randomPoint;
    }

    #region [ IENUMERATORS ]
    IEnumerator I_TimerSpawnBaffSpeed()
    {
        yield return waitForSecond = new WaitForSeconds(15);
        while (true)
        {
            PhotonNetwork.Instantiate(baffObject[0].name, RandomSpawnPoint(), Quaternion.identity);
            yield return waitForSecond = new WaitForSeconds(Random.Range(15, 20));
        }
    }

    IEnumerator I_TimerSpawnBaffPull()
    {
        yield return waitForSecond = new WaitForSeconds(25);
        while (true)
        {
            PhotonNetwork.Instantiate(baffObject[1].name, RandomSpawnPoint(), Quaternion.identity);
            yield return waitForSecond = new WaitForSeconds(Random.Range(25, 30));
        }
    }

    IEnumerator I_TimerSpawnBaffKnock()
    {
        yield return waitForSecond = new WaitForSeconds(20);
        while (true)
        {
            PhotonNetwork.Instantiate(baffObject[2].name, RandomSpawnPoint(), Quaternion.identity);
            yield return waitForSecond = new WaitForSeconds(Random.Range(20, 25));
        }
    }

    #endregion
}

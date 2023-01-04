using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSpawn : MonoBehaviour
{
    [SerializeField] private GameObject player_prefs;
    [SerializeField] private Transform playersPointSpawn;
    [SerializeField] private Material playerMaterial;
    [SerializeField] protected internal List<GameObject> playersList;

    GameObject newPlayerSpawn;
    void Awake()
    {
        while (PhotonNetwork.IsConnectedAndReady)
        {
            StartCoroutine(I_SpawnPlayer());
            break;
        }
    }

    private void Update()
    {
        RemoveNullInLists();
    }
    private void RemoveNullInLists()
    {
        playersList.RemoveAll(x => x == null);
    }

    IEnumerator I_SpawnPlayer()
    {
        yield return new WaitForSeconds(0.5f);

        newPlayerSpawn = PhotonNetwork.Instantiate(player_prefs.name, playersPointSpawn.position, Quaternion.identity);
        var render = newPlayerSpawn.GetComponent<Renderer>();
        float randomColor = Random.Range(0, 1);
        Color color = new Color(randomColor, randomColor, randomColor, 1);
        render.material.color = color;
    }
}

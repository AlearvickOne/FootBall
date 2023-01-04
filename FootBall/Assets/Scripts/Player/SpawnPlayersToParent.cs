using UnityEngine;

public class SpawnPlayersToParent : MonoBehaviour
{
    private GameObject playersPointSpawn;
    private PlayersSpawn scrPS;

    void Awake()
    {
        playersPointSpawn = GameObject.Find("SPAWNS").transform.GetChild(0).transform.gameObject;
        gameObject.transform.parent = playersPointSpawn.transform;
    }
    private void Start()
    {
        scrPS = GetComponentInParent<PlayersSpawn>();
        scrPS.playersList.Add(gameObject);
    }
    private void Update()
    {
        if (gameObject == null)
        {
            scrPS.playersList.Remove(gameObject);
        }
    }
}

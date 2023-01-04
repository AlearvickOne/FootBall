using UnityEngine;

public class SpawnBallsToParent : MonoBehaviour
{
    private GameObject ballPointSpawn;
    private BallSpawn scrBS;

    void Awake()
    {
        ballPointSpawn = GameObject.Find("SPAWNS").transform.GetChild(1).transform.gameObject;
        gameObject.transform.parent = ballPointSpawn.transform;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        scrBS = GetComponentInParent<BallSpawn>();
        scrBS.ballsList.Add(gameObject);
    }
}

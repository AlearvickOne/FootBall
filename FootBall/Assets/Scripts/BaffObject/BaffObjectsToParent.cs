using UnityEngine;

public class BaffObjectsToParent : MonoBehaviour
{
    [SerializeField] private GameObject BaffObjectsPointSpawn;
    void Awake()
    {
        BaffObjectsPointSpawn = GameObject.Find("SPAWNS").transform.GetChild(2).transform.gameObject;
        gameObject.transform.parent = BaffObjectsPointSpawn.transform;
    }
}

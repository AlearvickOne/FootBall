using UnityEngine;

public class BaffPullToList : MonoBehaviour
{
    [SerializeField] private BaffObjectSpawn scrBOS;
    private void Start()
    {
        scrBOS = GetComponentInParent<BaffObjectSpawn>();
        scrBOS.baffPullList.Add(gameObject);
    }
    private void Update()
    {
        if (gameObject == null)
        {
            scrBOS.baffPullList.Remove(gameObject);
        }
    }
}

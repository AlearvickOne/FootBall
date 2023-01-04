using UnityEngine;

public class BaffSpeedToList : MonoBehaviour
{
    [SerializeField] private BaffObjectSpawn scrBOS;
    private void Start()
    {
        scrBOS = GetComponentInParent<BaffObjectSpawn>();
        scrBOS.baffSpeedList.Add(gameObject);
    }
    private void Update()
    {
        if(gameObject == null)
        {
            scrBOS.baffSpeedList.Remove(gameObject);
        }
    }
}

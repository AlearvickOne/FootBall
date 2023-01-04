using UnityEngine;

public class BaffKnockToList : MonoBehaviour
{
    [SerializeField] private BaffObjectSpawn scrBOS;
    private void Start()
    {
        scrBOS = GetComponentInParent<BaffObjectSpawn>();
        scrBOS.baffKnockList.Add(gameObject);

    }
    private void Update()
    {
        if (gameObject == null)
        {
            scrBOS.baffKnockList.Remove(gameObject);
        }
    }
}

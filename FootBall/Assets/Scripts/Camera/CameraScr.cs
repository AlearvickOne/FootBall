using UnityEngine;

public class CameraScr : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        cam.transform.position = new Vector3(-2.51314974f, 24.1848221f, 9.07095432f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveText : MonoBehaviour
{
    public Camera camera;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

void LateUpdate()
{
    Vector3 cameraForward = camera.transform.forward;
    cameraForward.y = 0; // Setze die y-Komponente auf 0, um die Rotation auf der x-Achse zu ignorieren

    if (cameraForward != Vector3.zero)
    {
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
    }
}


}

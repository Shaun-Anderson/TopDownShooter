using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public  float zoom = 20; //determines amount of zoom capable. Larger number means further zoomed in
    public float smooth = 5; //smooth determines speed of transition between zoomed in and default state
    public Camera camera;

    void Update()
    {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoom, Time.deltaTime * smooth);
       
    }
}
using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    public bool spawnable;
    public Transform center;
    public Vector3 axis;
    public Vector3 desiredPosition;
    public float radius = 2.0f;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;

    void Start()
    {
        if (spawnable)
        {
            transform.position = (transform.position - center.position).normalized * radius + center.position;
            print(GetComponent<SpawnableObject>().phand.transform);
        }
    }

    void Update()
    {
        if (center != null)
        {
            transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
            desiredPosition = (transform.position - center.position).normalized * radius + center.position;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
        }
        else
        {
            if (GetComponent<SpawnableObject>().phand != null)
            {
                center = GetComponent<SpawnableObject>().phand.transform;
                transform.position = (transform.position - center.position).normalized * radius + center.position;
                transform.SetParent(center.transform, false);


            }
        }
    }
}
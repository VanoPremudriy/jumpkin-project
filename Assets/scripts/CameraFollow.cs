using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float angularSpeed = 1f;

    [SerializeField] private Transform target;

    private float angleY;
    // Start is called before the first frame update
    void Start()
    {
        angleY = transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) angleY -= angularSpeed;
        if (Input.GetKey(KeyCode.X)) angleY += angularSpeed;

        transform.position = target.transform.position;
        transform.rotation = Quaternion.Euler(0, angleY, 0);
        
    }
}

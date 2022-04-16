using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    float minX, minY, minZ;
    public float maxX, maxY, maxZ;
    public float xSpeed, ySpeed, zSpeed;
    public bool isX, isY, isZ;
    bool inMax, inMin;
    Vector3 vector3;
    void Start()
    {
        inMax = true;
        minX = transform.position.x;
        minY = transform.position.y;
        minZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(transform.forward * Time.deltaTime * 1);
        // transform.Translate(vector3 * Time.deltaTime);
        if (isX) inX();
        else
        if (isY) inY();
        else
        if (isZ) inZ();
    }

    void inX()
    {
        if (inMax)
        {
            transform.Translate(new Vector3(xSpeed,0,0) * Time.deltaTime);
            Debug.Log(transform.position.x + " MAX");
            if (transform.position.x >= minX + maxX)
            {
                inMax = false;
                inMin = true;
            }
        }
        if (inMin)
        {
            transform.Translate(new Vector3(-xSpeed, 0, 0) * Time.deltaTime);
            Debug.Log(transform.position.x + " MIN");
            if (transform.position.x <= minX)
            {
                inMin = false;
                inMax = true;
            }
        }
    }

    void inY()
    {
        if (inMax)
        {
            transform.Translate(new Vector3(0, ySpeed, 0) * Time.deltaTime);
            Debug.Log(transform.position.y + " MAX");
            if (transform.position.y >= minY + maxY)
            {
                inMax = false;
                inMin = true;
            }
        }
        if (inMin)
        {
            transform.Translate(new Vector3(0, -ySpeed, 0) * Time.deltaTime);
            Debug.Log(transform.position.y + " MIN");
            if (transform.position.y <=  minY)
            {
                inMin = false;
                inMax = true;
            }
        }
    }

    void inZ()
    {
        if (inMax)
        {
            transform.Translate(new Vector3(0, 0, zSpeed) * Time.deltaTime);
            Debug.Log(transform.position.z + " MAX");
            if (transform.position.z >= minZ + maxZ)
            {
                inMax = false;
                inMin = true;
            }
        }
        if (inMin)
        {
            transform.Translate(new Vector3(0, 0, -zSpeed) * Time.deltaTime);
            Debug.Log(transform.position.z + " MIN");
            if (transform.position.z <=  minZ)
            {
                inMin = false;
                inMax = true;
            }
        }
    }
}

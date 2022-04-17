using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*!
	\brief Класс отвечающий за движение объектов по осям
*/

public class MovePlatform : MonoBehaviour
{

    ///Минимальная координата
    public float minX, minY, minZ;

    ///Максимальная координата
    public float maxX, maxY, maxZ;

    ///Скорость
    public float xSpeed, ySpeed, zSpeed;

    ///Выбор координаты
    public bool isX, isY, isZ;

    ///Логические поля
    bool inMax, inMin;

    ///Векто передвижения
    Vector3 vector3;

    void Start() ///Стартовый метод
    {
        inMax = true;
        minX = transform.position.x;
        minY = transform.position.y;
        minZ = transform.position.z;
    }

    void Update() ///Метод обновления кадров
    {

        if (isX) inX();
        else
        if (isY) inY();
        else
        if (isZ) inZ();
    }

    void inX() ///Движение по оси X
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

    void inY() ///Движение по оси Y
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

    void inZ() ///Движение по оси Z
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

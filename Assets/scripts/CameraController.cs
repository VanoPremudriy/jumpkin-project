using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float angularSpeed = 1f;

    [SerializeField] private Transform target;
    public float yMinLimit = -40;
    public float yMaxLimit = 80;
    private float angleY;
    private float angleX;
    private float x = 0.0f; //Угол поворота по Y?
    private float y = 0.0f; //Уго поворота по X?

    public float xSpeed = 125.0f; //Чуствительность по Х
    public float ySpeed = 50.0f; //Y Чуствительность


    // Start is called before the first frame update
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Z)) angleY -= angularSpeed;
        if (Input.GetKey(KeyCode.X)) angleY += angularSpeed;*/
        y = ClampAngle(y, yMinLimit, yMaxLimit); //Вызыв самописной функции для ограничения углов поврот
        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        transform.position = target.transform.position;
        transform.rotation = Quaternion.Euler(y, x, 0);

    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

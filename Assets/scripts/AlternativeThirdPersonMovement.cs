using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeThirdPersonMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    [SerializeField] private Transform _camera;
    public float jumpHeight;
    [SerializeField] private MovementCharacteristics characteristics;
    public Transform groundCheck;
    private float vertical, horizontal;
    public CharacterController controller;
    private const float DISTANCE_OFFSET_CAMERA = 5f;
    private Vector3 direction;
    private Quaternion look;
    public float groundDistance = 0.01f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    private bool idle => horizontal == 0.0f && vertical == 0.0f;
    private Vector3 TargetRotate => _camera.forward * DISTANCE_OFFSET_CAMERA;

    float jspeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = characteristics.VisibleCursor;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        rotate();

    }

    private void movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
           
            direction = transform.TransformDirection(0, 0, vertical).normalized;

            Vector3 dir = direction * speed * Time.deltaTime;
            controller.Move(dir);
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        /* isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

         if (isGrounded && velocity.y < 0)
         {
             velocity.y = -2f;
         }

         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");
         //Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
         Vector3 direction = transform.TransformDirection(horizontal, 0, vertical).normalized;
         if (direction.magnitude >= 0.1f)
         {
             Vector3 dir = direction * speed * Time.deltaTime;
             //Vector3 direction = transform.TransformDirection(horizontal, 0, vertical).normalized;
             controller.Move(dir * speed * Time.deltaTime);
         }

         if (Input.GetButton("Jump") && isGrounded)
         {
             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
         }

         velocity.y += gravity * Time.deltaTime;

         controller.Move(velocity * Time.deltaTime);*/
    }

    private void rotate()
    {

        if (idle) return;
        Vector3 target = TargetRotate;
        target.y = 0;
        look = Quaternion.LookRotation(target);
        float speed = characteristics.AngularSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, look, speed);

    }

}
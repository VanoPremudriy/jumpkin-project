using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour {

    public CharacterController controller;
    public float originalSpeed = 6.0f;
    public float sprintSpeed = 5.0f;
    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    public float jumpHeight;
    public float gravity = -9.8f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.01f;
    public LayerMask groundMask;
    bool isGrounded;
    public bool visibleCursor;
    float horizontal, vertical;
    int jumpCount = 0;

    //stamina/////////////////////////////////////////////
    public Slider staminaSlider;
    public float staminaValue;
    public float minStaminaValue;
    public float maxStaminaValue;
    public float staminaReturn;
    //////////////////////////////////////////////////////
    bool isMove;
    bool isCanSprint;
    bool isJumpDown = false;

    //audio//////////////////////////////////////////////
    [SerializeField] AudioSource jumpAudioSource;

    [SerializeField] AudioSource jumpDownAudioSource;

    [SerializeField] AudioSource flyAudioSource;

    [SerializeField] AudioSource pickUpAudioSource;

    [SerializeField] GameObject pointLight;
    [SerializeField] GameObject spotLight;
    /////////////////////////////////////////////////////
    
    //pickUpCoins////////////////////////////////////////
    public Text collectedCoins; //Количество собранных монет
    public Text allCollectableCoins; //Общее количество монет на уровне
    
    public static float collectCoins; //Считаем собранные монетки
    private float allCoinsStart; //Считаем все монетки находящиеся на уровне

    [SerializeField] GameObject portal;
    //////////////////////////////////////////////////////


    void Start()
    {
        Cursor.visible = visibleCursor;
        controller = GetComponent<CharacterController>();
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Sprint();
        Light();
    }

    public void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (!flyAudioSource.isPlaying)
            flyAudioSource.Play();
            isMove = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else 
        {
            isMove = false;
            flyAudioSource.Stop();
        }
    }

    public void Jump()
    {
       
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpAudioSource.Play();
            // Debug.Log(isGrounded + " " + jumpCount);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount++;
        }
        else if (isGrounded && !Input.GetButton("Jump"))
        {
            jumpCount = 0;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    public void Sprint()
    {
            if (Input.GetKey(KeyCode.LeftShift) && staminaValue > 0 && isMove && isCanSprint && isGrounded)
            {
                    speed = originalSpeed + sprintSpeed;
                    staminaValue -= staminaReturn * Time.deltaTime * 2;
                
            }

            else
            {
                staminaValue += staminaReturn * Time.deltaTime;
            }

        if (isGrounded && !Input.GetKey(KeyCode.LeftShift) || !isCanSprint || !isMove) speed = originalSpeed;
        if (staminaValue < minStaminaValue) isCanSprint = false;
        if (staminaValue > maxStaminaValue) isCanSprint = true;
        if (staminaValue > 100f) staminaValue = 100.0f;
        staminaSlider.value = staminaValue;

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals("Ground")){
            jumpDownAudioSource.Play();
            if (other.tag == "tramp")
            {
                Debug.Log("Yes");
                velocity.y = Mathf.Sqrt(jumpHeight *4.5f * -2f * gravity);
            }
        }

         if (other.gameObject.CompareTag("PickUpCoins"))
      {
            pickUpAudioSource.Play();
            other.gameObject.SetActive(false);
            collectCoins = collectCoins + 1;
            if (collectCoins.Equals(allCoinsStart)) portal.SetActive(true);
            SetAllCollectableCoins();
            CurrentCollectedCoins();
      }

    }

    void Awake()
    {
        allCoinsStart = GameObject.FindGameObjectsWithTag("PickUpCoins").Length;
        collectCoins = 0;
        SetAllCollectableCoins();
        CurrentCollectedCoins();
    }

    public void SetAllCollectableCoins()
    {
        allCollectableCoins.text = "/" + allCoinsStart.ToString();
    }
    
    public void CurrentCollectedCoins()
    {
        collectedCoins.text = collectCoins.ToString();
    }

    private void Light(){
        if (Input.GetMouseButton(0)){
            spotLight.SetActive(true);
            pointLight.SetActive(false);
        }
        else {
            spotLight.SetActive(false);
            pointLight.SetActive(true);
        }
    }
}
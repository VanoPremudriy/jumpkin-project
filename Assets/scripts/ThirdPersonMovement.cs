using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

/*!
	\brief Класс, отвечающий за настройку управления персонажа
*/

[DefaultExecutionOrder(100)]
public class ThirdPersonMovement : MonoBehaviour {

    ///Контроллер персонажа
    public CharacterController controller; 

    ///Оригинальная скорость
    public float originalSpeed = 6.0f; 

    ///Ускорение
    public float sprintSpeed = 5.0f; 

    ///Текущая скорость
    public float speed = 6.0f; 
    ///Вспомогательная переменная для поворотов
    public float turnSmoothTime = 0.1f; 
    float turnSmoothVelocity;

    ///Камера
    public Transform cam; 

    ///Высота прыжка
    public float jumpHeight;

    ///Гравитация
    public float gravity = -9.8f; 
    Vector3 velocity;
    ///Проверка касания земли
    public Transform groundCheck;

    ///Дистанция проверки
    public float groundDistance = 0.01f; 

    ///Маска земли
    [SerializeField] LayerMask groundMask; 

    ///Стояние на земле
    bool isGrounded; 

    ///Курсор
    public bool visibleCursor;

    float horizontal, vertical;

    ///Кол-во прыжков
    int jumpCount = 0; 

    //stamina/////////////////////////////////////////////
    ///Ползунок выносливости
    public Slider staminaSlider;

    ///Значение выносливости
    public float staminaValue;
    
    ///Минимальная выносливость
    public float minStaminaValue;

    ///Максимальная выносливость
    public float maxStaminaValue;

    ///Восстановление выносливости
    public float staminaReturn;
    //////////////////////////////////////////////////////

    ///Движение персонажа
    bool isMove; 

    ///Ускорение персонажа
    bool isCanSprint;

    //audio//////////////////////////////////////////////
    ///Звук
    public AudioSource jumpAudioSource;

    ///Звук
    public AudioSource jumpDownAudioSource;
    
    ///Звук
    public AudioSource flyAudioSource;

    ///Звук
    public AudioSource pickUpAudioSource;

    ///Фонарь-1
    public GameObject pointLight;

    ///Фонарь-2
    public GameObject spotLight;
    /////////////////////////////////////////////////////
    
    //pickUpCoins////////////////////////////////////////

    ///Количество собранных монет
    public Text collectedCoins; 

    ///Общее количество монет на уровне
    public Text allCollectableCoins;
    
    ///Считаем собранные монетки
    public static float collectCoins;

    ///Считаем все монетки находящиеся на уровне
    private float allCoinsStart;

    ///Портал на другой уровень
    public GameObject portal;
    //////////////////////////////////////////////////////
    [SerializeField] GameObject gameOverPanel;

    [SerializeField]
    AudioSource VolodarskyAudioSource;
    [SerializeField]
    AudioSource VolodarskyAudioSource2;

    [SerializeField] Vector3 resp;

    [SerializeField] AudioSource yamete;
    void Start() ///Стартовый метод
    {
        controller = GetComponent<CharacterController>();
        portal.SetActive(false);
        SetupCamera();
    }

    void SetupCamera()
    {
        if (!visibleCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        if (cam == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam == null)
                mainCam = FindAnyObjectByType<Camera>();

            if (mainCam != null)
                cam = mainCam.transform;
        }

        // После апгрейда Unity ссылки Cinemachine могут слететь — привязываем заново
        CinemachineFreeLook freeLook = FindAnyObjectByType<CinemachineFreeLook>();
        if (freeLook != null)
        {
            freeLook.Follow = transform;
            freeLook.LookAt = transform;
        }
    }

    void LateUpdate() ///После Cinemachine — движение относительно актуальной камеры
    {
        Move();
        Jump();
        Sprint();
        Light();
    }

    
    public void Move() ///Метод, отвечающий за движение персонажа
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
  
    public void Jump() ///Метод, отвечающий за прыжки персонажка
    {
       
        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            jumpAudioSource.Play();
            if (PlayerPrefs.GetInt("Skin") == 5) yamete.Play();            
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

    public void Sprint() ///Метод, отвечающий за ускорение персонажа
    {
            if ((Input.GetButton("Sprint")) && staminaValue > 0 && isMove && isCanSprint && isGrounded)
            {
                    speed = originalSpeed + sprintSpeed;
                    staminaValue -= staminaReturn * Time.deltaTime * 2;
                
            }

            else
            {
                staminaValue += staminaReturn * Time.deltaTime;
            }

        if (isGrounded && !Input.GetButton("Sprint") || !isCanSprint || !isMove) speed = originalSpeed;
        if (staminaValue < minStaminaValue) isCanSprint = false;
        if (staminaValue > maxStaminaValue) isCanSprint = true;
        if (staminaValue > 100f) staminaValue = 100.0f;
        staminaSlider.value = staminaValue;

    }

    static int groundLayer = -1;

    static int GroundLayer
    {
        get
        {
            if (groundLayer < 0)
                groundLayer = LayerMask.NameToLayer("Ground");
            return groundLayer;
        }
    }

    public void OnTriggerEnter(Collider other) ///Действия при столкновении объекта с другими
    {
        if (other.gameObject.layer == GroundLayer)
        {
            jumpDownAudioSource.Play();
            if (other.CompareTag("tramp"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight *4.5f * -2f * gravity);
            }
        }

         if (other.CompareTag("PickUpCoins"))
      {
            pickUpAudioSource.Play();
            other.gameObject.SetActive(false);
            collectCoins = collectCoins + 1;
            if (collectCoins.Equals(allCoinsStart)) portal.SetActive(true);
            SetAllCollectableCoins();
            CurrentCollectedCoins();
      }
        if (other.gameObject.CompareTag("AfterLava")){
        }

        if (other.gameObject.CompareTag("Lava")){
            foreach (var ob in this.GetComponentsInChildren<Transform>()) Destroy(ob.gameObject);
            Destroy(this);
            Cursor.visible = true;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.None;
            gameOverPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("Interface").SetActive(false);
            VolodarskyAudioSource.Play();
        }

        if (other.gameObject.CompareTag("Wall")){
            controller.Move(new Vector3(-(controller.transform.position.x - resp.x), -(controller.transform.position.y - resp.y), -(controller.transform.position.z - resp.z)));
            VolodarskyAudioSource2.Play();
        }
    }

    void Awake() ///Метод для собирания предметов
    {
        allCoinsStart = GameObject.FindGameObjectsWithTag("PickUpCoins").Length;
        collectCoins = 0;
        SetAllCollectableCoins();
        CurrentCollectedCoins();
    }

    public void SetAllCollectableCoins() ///Метод, отвечающий за вывод кол-ва собранных предметов
    {
        allCollectableCoins.text = allCoinsStart.ToString();
    }
    
    public void CurrentCollectedCoins() ///Метод для конвертации собранных предметов в текст
    {
        collectedCoins.text = collectCoins.ToString();
    }

    private void Light(){ ///Метод, отвечающий за фонарь
        if (Input.GetButton("Fire1")){
            spotLight.SetActive(true);
            pointLight.SetActive(false);
        }
        else {
            spotLight.SetActive(false);
            pointLight.SetActive(true);
        }
    }
}
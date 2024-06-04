using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovemnentSettings _settings = null;

    private Vector3 _moveDirection;
    private CharacterController _controller;

    [SerializeField] private float currentSpeed;


    [SerializeField] private float sprintEnergy = 100f;
    [SerializeField] private float maxsprintEnergy;
    [SerializeField] public float sprintUpgrade;
    public float sprintDecreaseRate = 15f;
    public float sprintIncreaseRate = 5f;
    public float highsprintIncreaseRate = 10f;
    private Slider Sprint;
    private bool sprintReady = true;
    private bool sprintReload = true;
    private bool firstExecution = false;
    private HeadBob headBob;
    private Upgrades upgrades;
    

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        headBob = GetComponent<HeadBob>();
        upgrades = GameObject.Find("PlayerData").GetComponent<Upgrades>();
        Sprint = GameObject.Find("Sprint").GetComponent<Slider>();
    }

    void Start()
    {
        sprintUpgrade = upgrades.stamina;
        sprintEnergy = sprintEnergy + sprintUpgrade;
        maxsprintEnergy = sprintEnergy;
        Sprint.maxValue = sprintEnergy;
        sprintIncreaseRate = sprintIncreaseRate + sprintUpgrade / 10;
        highsprintIncreaseRate = highsprintIncreaseRate + sprintUpgrade / 8;
    }
    private void Update()
    {
        Sprint.value = sprintEnergy;
       

         if (Input.GetKey(KeyCode.LeftShift) && sprintReady && _controller.velocity.magnitude != 0)
         {
            SprintMovement();
         }
         else if(!sprintReady)
         {
            SlowMovement();
         }
         else
         {
            DefaultMovement();
         }

  
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * Time.deltaTime);

        if (sprintEnergy >= maxsprintEnergy)
        {
            sprintEnergy = maxsprintEnergy;
            sprintReady = true;
            sprintReload = false;      
        }

        if (sprintEnergy <= 0f && sprintReady == true)
        {
            firstExecution = true;
            sprintReady = false;
            sprintReload = true;  
            StartCoroutine(OverSprinted(3f));
        }
    }

    private void DefaultMovement()
    {
        if (_controller.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input.x != 0 && input.y !=0)
            {
                input *=0.777f;
            }

            _moveDirection.x = input.x * _settings.speed;
            _moveDirection.z = input.y * _settings.speed;
            _moveDirection.y = -_settings.antiBump;

            _moveDirection = transform.TransformDirection(_moveDirection);

            if(sprintReady)
            {
            sprintEnergy += Time.deltaTime * sprintIncreaseRate;
            }

            headBob.NormFrequency();

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {        
            _moveDirection.y -= _settings.gravity * Time.deltaTime;
        }
    }

     private void SprintMovement()
    {
        if (_controller.isGrounded)
        {
            sprintEnergy -= Time.deltaTime * sprintDecreaseRate;

            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input.x != 0 && input.y !=0)
            {
                input *=0.777f;
            }

            _moveDirection.x = input.x * _settings.sprint;
            _moveDirection.z = input.y * _settings.sprint;
            _moveDirection.y = -_settings.antiBump;

            _moveDirection = transform.TransformDirection(_moveDirection);

            headBob.SprintFrequency();

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {        
            _moveDirection.y -= _settings.gravity * Time.deltaTime;
        }
    }

    private void SlowMovement()
    {
        if (_controller.isGrounded)
        {

            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input.x != 0 && input.y !=0)
            {
                input *=0.777f;
            }

            _moveDirection.x = input.x * _settings.slow;
            _moveDirection.z = input.y * _settings.slow;
            _moveDirection.y = -_settings.antiBump;

            _moveDirection = transform.TransformDirection(_moveDirection);

            headBob.SlowFrequency();

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {        
            _moveDirection.y -= _settings.gravity * Time.deltaTime;
        }
    }

    private void Jump()
    {
       _moveDirection.y += _settings.jumpForce;
    }


    IEnumerator OverSprinted(float delay)
    {
        if (firstExecution)
        {
            firstExecution = false;
            yield return new WaitForSeconds(delay);
            
        }
        while (sprintReload)
        {
            sprintEnergy += Time.deltaTime * highsprintIncreaseRate;
            yield return null; 
        }
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement instance;

    public bool UseController = false;

    public GameObject JoystickMenu;
    //public Transform VRCamera;

    public Vector3[] PlayerTargetPostion;
    public GameObject[] GameMenus;

    public bool StartMoving = false;
    public bool StartAutoMoving = false;
    
    
    private int sceneIndex = 0;

    private CharacterMotor motor;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float MoveSpeed = 6;
    private float JumpSpeed = 8;
    private float Gravity   = 20;

    void Awake()
    {
        instance = this;
        motor = GetComponent<CharacterMotor>();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        if (UseController)
            ActiveJoystickMode();
        else
            ActiveMenuMode();
    }

    public void ActiveJoystickMode()
    {
        ChoseMode(true);
    }
    public void ActiveMenuMode()
    {
        ChoseMode(false);
    }

    public void ChoseMode(bool controller)
    {
        StartMoving = false;
        StartAutoMoving = false;
        UseController = controller;

        if (controller)
        {
            for (int i = 0; i < GameMenus.Length; i++)
                GameMenus[i].SetActive(false);

            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            for (int i = 0; i < GameMenus.Length; i++)
                GameMenus[i].SetActive(true);

            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<CharacterController>().enabled = false;
        }
    }

	void Scene (int num) 
    {
        if (UseController)
        {
            ActiveMenuMode();
            JoystickMenu.SetActive(false);
        }

        sceneIndex = num;
        StartAutoMoving = false;
        MovePlayerToPosition();
	}

    private int currentSceneIndex;
    void AutoScene()
    {
        if (UseController)
        {
            ActiveMenuMode();
            JoystickMenu.SetActive(false);
        }

        currentSceneIndex = sceneIndex;
        if (sceneIndex + 1 < PlayerMovement.instance.PlayerTargetPostion.Length)
            sceneIndex++;
        else
            sceneIndex = 0;

        StartAutoMoving = true;
        MovePlayerToPosition();
        Invoke("HideAllMenus", 0.5f); // for hiding the loading icon
    }
    
    void HideAllMenus()
    {
        for (int i = 0; i < GameMenus.Length; i++)
            GameMenus[i].SetActive(false);
    }

    void MovePlayerToPosition()
    {
        GetComponent<NavMeshAgent>().Stop();
        GetComponent<NavMeshAgent>().SetDestination(PlayerTargetPostion[sceneIndex]);
        GetComponent<NavMeshAgent>().Resume();

        transform.GetChild(0).GetComponent<Animator>().Play("Walk", 0, 0f);
        StartMoving = true;
    }

	void Update () 
    {
        if (!UseController)
        {
            MovingByMenu();
        }
        else
        {
            MovingByJoyStick();
        }
	}


    void MovingByMenu()
    {
        if (StartMoving)
         {
             GetComponent<NavMeshAgent>().destination = PlayerTargetPostion[sceneIndex];

             if (GetComponent<NavMeshAgent>().remainingDistance < 0.1f)
             {
                 if (!StartAutoMoving || (StartAutoMoving && currentSceneIndex == sceneIndex))
                 {
                     StartMoving = false;
                     transform.GetChild(0).GetComponent<Animator>().Play("Walk", 0, 0.0f);
                     transform.GetChild(0).GetComponent<Animator>().Play("Stop", 0, 0.0f);
                     GetComponent<NavMeshAgent>().Stop();

                     if (StartAutoMoving)
                     {
                         StartAutoMoving = false;
                         for (int i = 0; i < GameMenus.Length; i++)
                             GameMenus[i].SetActive(true);
                     }
                 }
                 else
                 {
                     if (sceneIndex + 1 < PlayerMovement.instance.PlayerTargetPostion.Length)
                         sceneIndex++;
                     else
                         sceneIndex = 0;

                     MovePlayerToPosition();
                 }
             }
        }
    }


    void MovingByJoyStick()
    {
        //transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y - VRCamera.localRotation.y, transform.localRotation.z, transform.localRotation.w);

       // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - VRCamera.localEulerAngles.y, transform.localEulerAngles.z);


        if (Input.GetButtonDown("Fire1"))
        {
            if (JoystickMenu.activeSelf)
                JoystickMenu.SetActive(false);
            else
                JoystickMenu.SetActive(true);
        }

        if (controller.isGrounded)
        {

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0,
            Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= MoveSpeed;

            //if (Input.GetButton("Jump"))
           //     moveDirection.y = JumpSpeed;
        }

        moveDirection.y -= Gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}

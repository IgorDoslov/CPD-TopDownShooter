using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    public GunController theGun;
    public GunController theGun1;
    public GameObject theGunObj;
    public GameObject theGun1Obj;

    public VirtualJoystick moveJoystick;
    public VirtualJoystick rotationJoystick;

    public bool usePS4Controller = false;
    public bool useMouseController = false;
    public bool useTouchController = false;
    bool onWeb = false;
    float invertControls = 1.0f;

    public Controls controls;

    private Camera cam;

    void Start()
    {
#if (UNITY_WEBGL)
        onWeb = true;
        invertControls = -1.0f;
#endif

        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();

        //Get input contols and enable them
        controls = new Controls();
        controls.Enable();
        theGunObj.SetActive(true);
        theGun1Obj.SetActive(false);
        theGun.isFiring = false;

    }

    void Update()
    {
        var dirMove = controls.Player.Move.ReadValue<Vector2>();

        
            moveInput = new Vector3(dirMove.x, 0f, dirMove.y * invertControls);
        

        //store direction and speed inside of move velocity. move velocity applied in FixedUpdate()

        if (useTouchController)
        {
            moveInput = moveJoystick.InputDirection;

            Vector3 dirRot = rotationJoystick.InputDirection;
            Vector3 playerDirection = Vector3.right * dirRot.x + Vector3.forward * dirRot.z;

            //returns 1 if any rotation is being inputed from right joystick
            if (playerDirection.sqrMagnitude > 0.0f)
                transform.rotation = Quaternion.LookRotation(playerDirection * Time.deltaTime, Vector3.up);

            if (rotationJoystick.InputDirection != Vector3.zero)
            {
                theGun.isFiring = true;
                theGun1.isFiring = true;
            }
            else
            {
                theGun.isFiring = false;
                theGun1.isFiring = false;
            }

        }

        //Rotate with Mouse
        if (useMouseController)
        {
            if(onWeb)
            moveInput = new Vector3(dirMove.x, 0f, dirMove.y);

            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLength;
            //if raycast is hitting Plane give raylength variable
            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            //If left mouse button down shoot
            if (Input.GetMouseButtonDown(0))
            {
                theGun.isFiring = true;
                theGun1.isFiring = true;
            }
            //if left mouse button is up dont shoot
            if (Input.GetMouseButtonUp(0))
            {
                theGun.isFiring = false;
                theGun1.isFiring = false;
            }

        }
        //Rotate with Controller
        if (usePS4Controller)
        {
            //Get the current rotation from input and stores in a vector 3
            var dirRot = controls.Player.Rotation.ReadValue<Vector2>();

            Vector3 playerDirection = Vector3.right * dirRot.x + Vector3.forward * dirRot.y * invertControls;

            if (playerDirection.sqrMagnitude > 0.0f)
                transform.rotation = Quaternion.LookRotation(playerDirection * Time.deltaTime, Vector3.up);

            if (playerDirection.sqrMagnitude > 0.0f)
            {
                theGun.isFiring = true;
                theGun1.isFiring = true;
            }

            //If player has no current rotation selected with right joystick dont shoot
            if (playerDirection.sqrMagnitude == 0.0f)
            {
                theGun.isFiring = false;
                theGun1.isFiring = false;
            }

           
        }
        moveVelocity = moveInput * moveSpeed;
    }


    private void FixedUpdate()
    {
        //add velocity to player
        rb.velocity = moveVelocity;
    }

}

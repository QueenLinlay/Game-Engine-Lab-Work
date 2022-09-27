using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    //Player Movement
    public PlayerAction inputAction;
    Vector2 move;
    Vector2 rotate;
    private float walkSpeed = 5f;
    private float degrees = 90.0f;
    // public Camera playerCamera;
    //Vector3 cameraRotation;

    //Player Jump
    Rigidbody rb; //rigid body
    private float distanceToGround;
    private bool isGrounded = true;
    public float jump = 5f;

    //Player Animation
    Animator playerAnimator;
    private bool isWalking = false;

    //Projectile Bullets
    public GameObject bullet;
    public Transform projectilePos;
    public GameObject Character;

    private void OnEnable()
    {
        inputAction.Player.Enable();
    }
    private void OnDisable()
    {
        inputAction.Player.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        inputAction = new PlayerAction();

        inputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        inputAction.Player.Move.performed += cntxt => IsAction();

        inputAction.Player.Move.canceled += cntxt => move = Vector2.zero;
        inputAction.Player.Move.canceled += cntxt => IsNoAction();

        inputAction.Player.Jump.performed += cntxt => Jump();
        inputAction.Player.Shoot.performed += cntxt => Shoot();
        inputAction.Player.Rotate.performed += cntxt => RotateCP();
        inputAction.Player.Exit.performed += cntxt => Exit();

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;

    }

    private void IsAction()
    {
        playerAnimator.SetBool("Walking", true);
    }

    private void IsNoAction()
    {
        playerAnimator.SetBool("Walking", false);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            isGrounded = false;
        }
    }

    public void Shoot()
    {
        Rigidbody bulletRb = Instantiate(bullet, projectilePos.position, Quaternion.identity).GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        bulletRb.AddForce(transform.up * 5f, ForceMode.Impulse);
    }

    public void RotateCP()
    {
        Character.transform.Rotate(0.0f, -degrees, 0.0f, Space.World);
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);

        if(Character.transform.position.y < -2)
        {
            SceneManager.LoadScene(0);
        }
    }
}
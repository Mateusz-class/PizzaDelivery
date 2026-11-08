using UnityEngine;

//Modified Code from:
//https://youtu.be/cqATTzJmFDY?si=kAzefp6xjAhPThch
public class ArcadeCarController : MonoBehaviour
{
    public enum PlayerState
    {
        None, Regular, Stunned, Immune
    }
    public PlayerState playerState;

    public Rigidbody RB;
    public float forwardAccel = 3f, reverseAccel = 2f, maxSpeed = 40f, turnStrength = 180, gravityForce = 20f, dragOnGround = 3f;
    private float speedInput, turnInput;
    private bool grounded;
    public LayerMask WhatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;
    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;
    public float stunned, immune;
    public GameObject StunnedText;
    void Start()
    {
        playerState = PlayerState.Regular;
        RB.transform.parent = null;
    }

    void Update()
    {
        if (playerState == PlayerState.Stunned)
        {
            stunned -= Time.deltaTime;
            if(stunned < 0)
            {
                playerState = PlayerState.Immune;
                StunnedText.SetActive(false);
                immune = 3f;
            }
            return;
        }
        else
        {
            Move();
            if (playerState == PlayerState.Stunned)
            {
                immune -= Time.deltaTime;
                if (immune < 0)
                {
                    playerState = PlayerState.Regular;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;
        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, WhatIsGround))
        {
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        if (playerState == PlayerState.Stunned) return;
        if (grounded)
        {
            RB.linearDamping = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                RB.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            RB.linearDamping = 0.1f;
            RB.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }
    private void Move()
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }
        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);
        transform.position = RB.transform.position;
    }
    public void GetStunned()
    {
        playerState = PlayerState.Stunned;
        stunned = 4f;
        RB.linearVelocity = Vector3.zero;
        StunnedText.SetActive(true);
    }
}

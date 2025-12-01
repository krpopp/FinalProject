using System.Collections;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    
    [Header ("Platformer Physics")]
    [Tooltip("Horizontal speed")]
    [SerializeField] private float speed = 7f;
    [Tooltip("Jump force")]
    [SerializeField] private float jumpPower = 5f;
    [Tooltip("Radius to check if the player is touching the ground")]
    [SerializeField] private float groundCheckRadius = 0.25f;
    [Tooltip("Center position of radius you're checking if the player is on the ground")]
    [SerializeField] private Transform groundPoint;
    [Tooltip("Layer platforms should be in")]
    [SerializeField] private LayerMask groundLayer;

    [Header ("Lose Timers")]
    [Tooltip("Time to pause before returning the player to the start")]
    [SerializeField] private float deathTime;
    [Tooltip("Time to pause before letting the player continue")]
    [SerializeField] private float restartTime;

    //Horizontal input direction
    private float dir;
    //Reference to player's body
    public static Rigidbody2D myBody;
    //Bool to check if player should be able to move or not
    public static bool letMove = true;
    
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if we can move, find the input value
        if (letMove) dir = Input.GetAxis("Horizontal");
        //if we press the jump button and we're touching the ground
        if(Input.GetButtonDown("Jump") && CheckGround())
        {
            //trigger the jump juice event
            GetComponent<GameEvents>().JumpEvent();
            //jump
            myBody.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        }
        //continue to check if the player is touching the grount
        CheckGround();
    }

    private void FixedUpdate()
    {
        //if we can move, move
        if(letMove) myBody.linearVelocity = new Vector2(dir * speed, myBody.linearVelocity.y);
    }

    private bool CheckGround()
    {
        //check if there's ground within the circle
        if(Physics2D.OverlapCircle(groundPoint.position, groundCheckRadius, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TriggerRestart();
        }
    }
    
    //reset various movement values and start the restarting timer
    private void TriggerRestart()
    {
        dir = 0;
        myBody.linearVelocity = Vector3.zero;
        letMove = false;
        StartCoroutine(Restart());
    }

    //after a set time, reset the player's position and then after another set time, allow the player to move
    IEnumerator Restart()
    {
        yield return new WaitForSeconds (deathTime);
        transform.position = startPos;
        yield return new WaitForSeconds(restartTime);
        letMove = true;
    }
}

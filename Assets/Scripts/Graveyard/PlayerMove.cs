using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //how fast our player will move
    [SerializeField]
    float speed;

    //reference to camera 'recording' scene
    Camera cam;

    //rate to smoothly rotate the player
    [SerializeField]
    float rotationSmooth;

    //current angle that we want the player to rotate to
    float currentAngle;

    //reference to character controller on the player
    //this is similar to a rigidbody, but provides lots of extra useful features that are commonly needed for 3D player-chars
    //such as moving on slopes
    CharacterController charController;

    [SerializeField]
    bool allowRotate;

    public static Vector3 movementInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main; //get the main camera
        charController = GetComponent<CharacterController>(); //get the character controller on the player
    }

    // Update is called once per frame
    void Update()
    {
        //collect data if the player is pressing an input
        //GetAxisRaw Horizontal/Vertical refers to any default input settings that are typically used
        //for example: horizontal = A and D, left and right arrows, horizontal analog stick movement
        //they output a value from -1 to 1 (-1 meaning left, 1 meaing right, 0 meaning not pressing)
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        //if the player is pressing an input
        //(we need to check a little beyond 0 because analog sticks are sensitive
        //and may be picking up input even if the player isn't touching them)
        if(movementInput.magnitude >= 0.1f)
        {
            if(allowRotate){
                //get the angle between where we are currently facing and where that camera's facing
                float target = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                //smoothly transition to that angle
                currentAngle = Mathf.SmoothDampAngle(currentAngle, target, ref currentAngle, rotationSmooth);
                //set our player-char's direction to that rotation
                transform.rotation = Quaternion.Euler(0, currentAngle, 0);
                //based on that rotation, find where we want to move to
                Vector3 rotateMove = Quaternion.Euler(0, target, 0) * Vector3.forward;
                //move the player-char to that position
                charController.Move(rotateMove * speed * Time.deltaTime);
            } else
            {
                //move the player-char to that position
                charController.Move(movementInput * speed * Time.deltaTime);  
            }
        }
        //transform.position += movementInput * speed * Time.deltaTime;
    }
}

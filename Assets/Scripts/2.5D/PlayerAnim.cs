using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{

    Animator myAnim;
    Camera cam;
    Transform child;

    public enum AnimStates
    {
        Idle,
        MoveLeft,
        MoveRight
    }

    private AnimStates aState = AnimStates.Idle;

    public AnimStates AState
    {
        get
        {
            return aState;
        }
        set
        {
            if(value != aState)
            {
                switch (value)
                {
                    case AnimStates.Idle:
                        myAnim.SetBool("walkRight", false);
                        myAnim.SetBool("walkLeft", false);
                        break;
                    case AnimStates.MoveLeft:
                        myAnim.SetBool("walkRight", false);
                        myAnim.SetBool("walkLeft", true);
                        break;
                    case AnimStates.MoveRight:
                        myAnim.SetBool("walkRight", true);
                        myAnim.SetBool("walkLeft", false);
                        break;
                }
                aState = value;
            }
        }
    } 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(PlayerMove.movementInput.x > 0 || PlayerMove.movementInput.z > 0)
        {
            AState = AnimStates.MoveRight;
        } else if(PlayerMove.movementInput.x < 0 || PlayerMove.movementInput.z < 0)
        {
            AState = AnimStates.MoveLeft;
        } else if(PlayerMove.movementInput.x == 0 && PlayerMove.movementInput.z == 0)
        {
            AState = AnimStates.Idle;
        }
    }
}

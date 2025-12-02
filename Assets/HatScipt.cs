using Unity.Mathematics;
using UnityEngine;

public class HatScipt : MonoBehaviour
{
    private float dir;
    [SerializeField]
    Rigidbody2D playerRb;
    public float dividedBy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new quaternion(0f,0f,playerRb.linearVelocityX/dividedBy,1);
    }
}

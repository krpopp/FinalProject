using UnityEngine;

public class coinspin : MonoBehaviour
{

    public float vector = Mathf.Sin(5);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotate: transform.Rotate(0, 0, vector* Time.deltaTime);
    }
}

using UnityEngine;
using UnityEngine.Rendering;

public class RainbowColor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float Speed = 1;
    private Renderer rend;
    float hue = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hue = Random.Range(0f,1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        hue+=Speed*Time.deltaTime;
        if(hue>1f) hue -=1f;
        spriteRenderer.color = Color.HSVToRGB(hue,1f,1f);
    }
}

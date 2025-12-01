using UnityEngine;

public class InClassDemo : MonoBehaviour
{

    public ParticleSystem goalParticles;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            goalParticles.Play();
        }
    }

}

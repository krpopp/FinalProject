using UnityEngine;

public class InClassDemo : MonoBehaviour
{
    public ParticleSystem goalParticles;

    void onTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            goalParticles.Play();
        }
    }
}

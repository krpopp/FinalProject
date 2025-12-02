using UnityEngine;
using TMPro;

public class GameEvents : MonoBehaviour
{

    [Header ("Goal Hit Effects")]
    [SerializeField] ParticleSystem goalParticles;
    [SerializeField] AudioClip goalAudio;

    [Header ("Enemy Hit Effects")]
    [SerializeField] ParticleSystem enemyParticles;
    [SerializeField] AudioClip enemyAudio;

    [Header ("Collect Hit Effects")]
    [SerializeField] ParticleSystem collectParticles;
    [SerializeField] AudioClip collectAudio;
    [SerializeField] ParticleSystem scoreParticles;

    [Header ("Ground Hit Effects")]
    [SerializeField] ParticleSystem groundParticles;
    [SerializeField] AudioClip groundAudio;
    [SerializeField] float groundPartOffset = 0.5f;

    [Header ("Jump Effects")]
    [SerializeField] AudioClip jumpAudio;

    [Header ("UI")]
    [SerializeField] GameObject winTextObj;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Animator scoreAnim;
    int score = 0;

    AudioSource audioSource;
    Animator playerAnim;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
    }

    public void JumpEvent()
    {
        //play animation and audio
        playerAnim.SetTrigger("jump");
        audioSource.PlayOneShot(jumpAudio);
    }

    private void GoalEvent(GameObject goalObj)
    {
        //stop the player from moving and reset its velocity
        PlatformerPlayer.myBody.linearVelocity = Vector3.zero;
        PlatformerPlayer.letMove = false;
        //play particles, audio, and animation
        if(goalParticles != null) PlayParticles(goalParticles, goalObj.transform.position);
        if(goalAudio != null) audioSource.PlayOneShot(goalAudio);
        playerAnim.SetBool("win", true);
        Debug.Log("hit goal!");
    }

    public void WinGame()
    {
        //turn on "you win" UI text
        winTextObj.SetActive(true);
    }

    private void EnemyEvent()
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        //player particles, audio, and animation
        if(enemyParticles != null) PlayParticles(enemyParticles, transform.position);
        if(enemyAudio != null) audioSource.PlayOneShot(enemyAudio);
        playerAnim.SetTrigger("hit");
        Debug.Log("hit enemy!");
    }

    private void GroundEvent()
    {
        //get the position of the bottom of the player
        Vector3 partPos = transform.position;
        partPos.y -= groundPartOffset;
        //play particles, audio, and animation
        if(groundParticles != null) PlayParticles(groundParticles, partPos);
        if(groundAudio != null) audioSource.PlayOneShot(groundAudio);
        playerAnim.SetTrigger("land");
        Debug.Log("hit ground!");
    }

    private void CollectEvent(GameObject collectObj)
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        transform.GetChild(0).localScale += new Vector3(0.1f, 0.1f, 0.1f);
        //player particles and audio
        if(collectParticles != null) PlayParticles(collectParticles, collectObj.transform.position);
        if(collectAudio != null) audioSource.PlayOneShot(collectAudio);
        //play any score related effects or system changes
        ScoreEvent();
        //destory the collected object
        Destroy(collectObj);
        Debug.Log("hit collectable!");
    }

    private void ScoreEvent()
    {
        //animate the score text, increase the score, and set the score text to the score value
        scoreParticles.Play();
        if (scoreAnim != null) scoreAnim.SetTrigger("scored");
        score++;
        scoreText.text = score.ToString();
    }

    //turn on the passed particles, set the particle's position, play the particles
    private void PlayParticles(ParticleSystem sys, Vector3 newPos)
    {
        sys.gameObject.SetActive(true);
        sys.transform.position = newPos;
        sys.Play();
    }

    //DO NOT TOUCH
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckTag(collision.gameObject);
    }

    //Can only add cases 
    //Checks the tag of the passed object
    private void CheckTag(GameObject hitObj)
    {
        switch (hitObj.tag)
        {
            case "Goal":
                GoalEvent(hitObj);
                break;
            case "Enemy":
                EnemyEvent();
                break;
            case "Ground":
                GroundEvent();
                break;
            case "Collectable":
                CollectEvent(hitObj);
                break;
        }
    }
}

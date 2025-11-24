using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSelect : MonoBehaviour
{

    [SerializeField]
    RuntimeAnimatorController[] allSkins;

    [SerializeField]
    Animator playerAnim;

    int skinTrack = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSkin()
    {
        skinTrack++;
        if(skinTrack >= allSkins.Length)
        {
            skinTrack = 0;
        }
        playerAnim.runtimeAnimatorController = allSkins[skinTrack] as RuntimeAnimatorController;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

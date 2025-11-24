using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public static PlayerSettings referenceSettings; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if (referenceSettings == null)
        {
            referenceSettings = this;
        }
        else if (referenceSettings != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
}

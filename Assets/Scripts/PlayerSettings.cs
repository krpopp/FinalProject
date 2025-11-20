using UnityEngine;

public class PlayerSettings : MonoBehaviour
{

    public static PlayerSettings settings;

    void Awake(){
        if(settings == null){
            settings = this;
        } else if(settings != this){
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

}

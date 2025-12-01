using UnityEngine;
using TMPro;

public class Student : MonoBehaviour
{

    [SerializeField]
    string dialogue;

    [SerializeField]
    GameObject canvas;
    TMP_Text dialogueText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText = canvas.GetComponentInChildren<TMP_Text>();
        dialogueText.text = dialogue;
        canvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Karina"))
        {
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Karina"))
        {
            canvas.SetActive(false);
        }
    }
}

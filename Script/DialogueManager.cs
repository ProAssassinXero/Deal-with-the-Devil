using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public string lines;


    void Awake()
    {
            instance = this;
    }
    public void StartDialogue(string newline)
    {
        dialogueBox.SetActive(true);
        lines = newline;
        Advance();
    }
    public void Advance()
    {

         dialogueText.text = lines;
         Debug.Log(lines);
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
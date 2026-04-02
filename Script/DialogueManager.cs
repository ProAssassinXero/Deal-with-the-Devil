using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public GameObject dialogueBox;
    public int index = 0;



    void Awake()
    {
            instance = this;
    }
    public void StartDialogue(string[] newline)
    {
        lines = newline;
        index = 0;
        Advance();
    }
    public void Advance()
    {
        if (index < lines.Length)
        {
            dialogueText.text = lines[index];
            Debug.Log(lines[index]);
            index++;
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
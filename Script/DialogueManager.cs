using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Enemy enemy;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public string line;
    public bool sent;
    public bool Dbox;

    void Awake()
    {
        instance = this;
    }

    public void StartDialogue(string newline)
    {
        Dbox = true;
        line = newline;
        sent = true;
        dialogueText.text = line;

    }
    public void Advance()
    {
        
        Debug.Log(line);

        if (sent)
        {
            EndDialogue();
            enemy.Agent.SetDestination(enemy.target[1].position);
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public AIMovement aIMovement;
    public TextMeshProUGUI dialogueText; 
    public TextMeshProUGUI orderDisplay;  
    public GameObject dialogueBox;
    public string line;  
    public bool sent;    
    public bool Dbox;    

    public string orderStore;
    public MiniGame_ShakingScript miniGameScript;
    public GameObject miniGame;

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
        orderStore = line;
        orderDisplay.text = "Order: " + line;
        EndDialogue();
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
        miniGame.SetActive(true);
    }

    public void Resetminigame()
    {
        miniGame.SetActive(false);
        miniGameScript.RestartDrink();
        miniGameScript.ResetDrink();
    }
}
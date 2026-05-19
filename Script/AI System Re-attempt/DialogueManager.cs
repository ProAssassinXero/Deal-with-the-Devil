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
    public NPC_OrderScript nPC_OrderScript;
    public GameObject miniGame;

    public AIMovement activeNPC;

    void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (nPC_OrderScript.ClearUI)
        {
            orderDisplay.text = "Order: ";
            line = "";
        }
    }

    public void StartDialogue(string newline, AIMovement npc)
    {
        Dbox = true;
        line = newline;
        sent = true;
        activeNPC = npc;          

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
        orderStore = "";
    }
}
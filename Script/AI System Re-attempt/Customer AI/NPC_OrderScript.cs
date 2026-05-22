using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC_OrderScript : MonoBehaviour
{
    public int pickedOrder;
    public int randomiser;
    public int IndexToRemove;
    public List<string> Order;
    public List<int> orderToNum;
    public DialogueManager dialogueManager;
    public AIMovement aIMovement;
    public AIMovement npcAtCounter;
    public bool ClearUI = true;
    public bool orderIsReceiced;
    public GameObject Counter;
    public bool debounce = false;

    public BoxCollider2D takeOrder;

    public GameObject miniGame;
    public PlayerInteraction playerInteraction;
    public MiniGame_ShakingScript MiniGame_ShakingScript;
    public BoxCollider2D top;
    public BoxCollider2D bottom;
    public BoxCollider2D left;
    public BoxCollider2D right;

    private void Start()
    {
        debounce = false;
        top = playerInteraction.leftCollider;
        bottom = playerInteraction.rightCollider;
        left = playerInteraction.topCollider;
        right = playerInteraction.bottomCollider;
    }

    public void GabiSend()
    {
        if (debounce) return; 
        debounce = true;       

        int length = Order.Count;

        if (length == 0)
        {
            dialogueManager.EndDialogue();
            return;
        }

        randomiser = Random.Range(0, length);
        IndexToRemove = Order.IndexOf(Order[randomiser]);
        pickedOrder = randomiser;
        orderToNum = new List<int>(new int[length - 1]);

        DialogueManager.instance.dialogueBox.SetActive(true);
        DialogueManager.instance.StartDialogue(Order[randomiser], aIMovement);
        FindAnyObjectByType<DialogueManager>().gameObject.SetActive(true);

        Order.RemoveAt(IndexToRemove);
    }

    private void Update()
    {
        bool isTopTouching = playerInteraction.topCollider.IsTouching(takeOrder);
        bool isBottomTouching = playerInteraction.bottomCollider.IsTouching(takeOrder);
        bool isLeftTouching = playerInteraction.leftCollider.IsTouching(takeOrder);
        bool isRightTouching = playerInteraction.rightCollider.IsTouching(takeOrder);

        if (npcAtCounter != null && Counter != null)
        {
            if (npcAtCounter != null)
            {
                if (!debounce && (isTopTouching || isBottomTouching || isLeftTouching || isRightTouching) && Input.GetKeyDown(KeyCode.E))
                {
                    aIMovement = npcAtCounter;
                    GabiSend();
                }
            }
        }
        if (dialogueManager.activeNPC == null && MiniGame_ShakingScript.servedNPC == null)
        {
            debounce = false;
            miniGame.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npcAtCounter = collision.GetComponent<AIMovement>();
            ClearUI = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            if (npcAtCounter == collision.GetComponent<AIMovement>())
            {
                npcAtCounter = null; 
                ClearUI = true;
            }
        }
    }
}
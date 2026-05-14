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
    public GameObject nPC;
    public bool orderIsReceiced;
    public GameObject Counter;  
    public bool debounce = false;     

    public BoxCollider2D takeOrder;   

    public GameObject miniGame;    
    public PlayerInteraction playerInteraction;
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
        int length = Order.Count;
        randomiser = Random.Range(0, length);

        if (length == 0)
        {
            Order = new List<string>();
            dialogueManager.EndDialogue();
            return;
        }

        IndexToRemove = Order.IndexOf(Order[randomiser]);
        pickedOrder = randomiser;
        orderToNum = new List<int>(new int[length - 1]);

        DialogueManager.instance.dialogueBox.SetActive(true);
        DialogueManager.instance.StartDialogue(Order[randomiser], aIMovement);

        FindAnyObjectByType<DialogueManager>().gameObject.SetActive(true);
        Order.RemoveAt(IndexToRemove);
    }

    private void FixedUpdate()
    {
        bool isTopTouching = playerInteraction.topCollider.IsTouching(takeOrder);
        bool isBottomTouching = playerInteraction.bottomCollider.IsTouching(takeOrder);
        bool isLeftTouching = playerInteraction.leftCollider.IsTouching(takeOrder);
        bool isRightTouching = playerInteraction.rightCollider.IsTouching(takeOrder);

        if ((nPC.transform.position - Counter.transform.position).magnitude < 2 && !debounce && (isTopTouching || isBottomTouching || isLeftTouching || isRightTouching) && Input.GetKey(KeyCode.E))
        {
            GabiSend();
            debounce = true;    
            
        }
  
        else if (aIMovement.doneOrder == true)
        {
            debounce = false;
            miniGame.SetActive(false);
        }
    }
}
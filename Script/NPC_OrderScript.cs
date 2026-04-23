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

    private void Start()
    {
        debounce = false;
    }
    public void GabiSend()
    {
        int length = Order.Count;
        randomiser = Random.Range(0, length);

        if (length == 0)
        {
            Order = new List<string>();
            DialogueManager.instance.StartDialogue("I'm good");
            dialogueManager.EndDialogue();
            return;
        }

        IndexToRemove = Order.IndexOf(Order[randomiser]);
        pickedOrder = randomiser;
        orderToNum = new List<int>(new int[length - 1]);
        DialogueManager.instance.dialogueBox.SetActive(true);
        DialogueManager.instance.StartDialogue(Order[randomiser]);

        FindAnyObjectByType<DialogueManager>().gameObject.SetActive(true);
        


        //An order is picked and removed from the list, then the dialogue box is closed
        Order.RemoveAt(IndexToRemove);
    }

    private void FixedUpdate()
    {
        if ((nPC.transform.position - Counter.transform.position).magnitude < 1 && !debounce) 
        {
            debounce = true;
            GabiSend();
            aIMovement.doneOrder = true;
        }
    }
}

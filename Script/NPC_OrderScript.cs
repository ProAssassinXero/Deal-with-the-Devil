using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class NPC_OrderScript : MonoBehaviour
{

    public string[] Order;
    public int[] orderToNum;
    public void GabiSend()
    {
        int length = Order.Length;
        int randomiser = Random.Range(1, length - 1);

        IndexToRemove = Order;

        pickedOrder = randomiser;
        orderToNum = new int[length];
        DialogueManager.instance.StartDialogue(Order[randomiser]);

        Order.RemoveAt(IndexToRemove);

    }
}

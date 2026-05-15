using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BookScript : MonoBehaviour
{
    public GameObject Flip_1;
    public GameObject Flip_2;
    public GameObject Flip_3;
    public GameObject Flip_4;

    public GameObject Book;
    public int CurrentInt;

    public bool On = false;
    Dictionary<int, GameObject> _Index;
    private void Start()
    {
        _Index = new Dictionary<int, GameObject>()
    {
        {1, Flip_1},
        {2, Flip_2},
        {3, Flip_3},
    };
    }
    public void CheckFlip(int Anmount)
    {
        if (CurrentInt + Anmount <= 0)
        {
            return;
        }
        if (CurrentInt + Anmount >= 3)
        {
            return;
        }
        _Index[CurrentInt].SetActive(false);
        CurrentInt += Anmount;
        _Index[CurrentInt].SetActive(true);
    }
    void AbleBook()
    {
        Book.SetActive(true);
        Flip_1.SetActive(true);
        CurrentInt = 1;
    }



    public void BookButtonFunc()
    {
        if (On)
        {
            Book.SetActive(false);
        }
        else
        {
            AbleBook();
        }
    }
}

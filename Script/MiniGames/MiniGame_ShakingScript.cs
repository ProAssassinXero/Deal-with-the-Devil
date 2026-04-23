using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MiniGame_ShakingScript : PhaseManager
{
    public void RestartDrink()
    {
        CurrentType = "";
        CurrentMix = new Dictionary<string, int>();
    }
    public string CurrentType;

    Dictionary<string, int> TypePartLimit = new Dictionary<string, int>()
    {
        {"Mixing", 8},
        {"Shot", 1},
        {"Shake", 8}
    };

    Dictionary<string, int> CurrentMix = new Dictionary<string, int>();

    Dictionary<string, Dictionary<string, int>> Drinks = new Dictionary<string, Dictionary<string, int>>()
    {
        {"Vodka Collins",   new Dictionary<string, int>()
        {
            
        }}
    };

    bool CheckAddPart()
    {
        int Part_Counter = 0;
        int Limit = TypePartLimit[CurrentType];
        foreach (int value in CurrentMix.Values)
        {
            Part_Counter += value;
        }
        if (Part_Counter < Limit)
        {
            return true;
        }
        return false;
    }

    public void AddPart(string NamePart)
    {
        Debug.Log(NamePart);
        if (!CheckAddPart())
        {
            Debug.Log("Can't add part");
            return;
        }
        if (CurrentMix.TryGetValue(NamePart, out int value))
        {
            CurrentMix[NamePart] += 1;
        }
        else
        {
            CurrentMix.Add(NamePart, 1);
        }
        
        Debug.Log(CurrentMix[NamePart]);
    }
}

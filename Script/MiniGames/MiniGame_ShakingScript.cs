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
    public string CurrentDrink;

    Dictionary<string, int> TypePartLimit = new Dictionary<string, int>()
    {
        {"Mixing", 8},
        {"Shot", 1},
        {"Shake", 8}
    };

    Dictionary<string, int> CurrentMix = new Dictionary<string, int>();

    Dictionary<string, Dictionary<string, int>> MixDrinks = new Dictionary<string, Dictionary<string, int>>()
    {
        {"Vodka Collins", new Dictionary<string, int>()
        {
            {"vodka", 3},
            {"simple_syrup", 5}
        } 
        },
    };

    Dictionary<string, Dictionary<string, Dictionary<string, int>>> DrinksType = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

    public void Start()
    {
        DrinksType = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>()
    {
        {"Mixing",MixDrinks},
    };
    }

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

    string IsDrink()
    {
        Dictionary<string, Dictionary<string, int>> FilerType = DrinksType[CurrentType];
        string Name = "something";
        Debug.Log(CurrentMix.Keys );
        foreach (string DrinkName in FilerType.Keys)
        {
            Dictionary<string, int> DrinkIng = FilerType[DrinkName];
            int IngCount = DrinkIng.Count;
            int count = 0;
            foreach (string IngName in DrinkIng.Keys)
            {
                if (CurrentMix.ContainsKey(IngName))
                {
                    if (CurrentMix[IngName] == DrinkIng[IngName])
                    {
                        count++;
                    }
                }
            }
            if (count == IngCount)
            {
                Name = DrinkName;
            }
        }
        return Name;
    }

    public void AddPart(string NamePart)
    {
        Debug.Log(NamePart);
        if (!CheckAddPart())
        {
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
        if (!CheckAddPart())
        {
            string DrinkName = IsDrink();
            Debug.Log(DrinkName);
        }
        Debug.Log(CurrentMix[NamePart]);
    }
}

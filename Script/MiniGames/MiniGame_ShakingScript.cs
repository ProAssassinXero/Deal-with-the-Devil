using System.Collections.Generic;
using UnityEngine;

public class MiniGame_ShakingScript : PhaseManager
{
    public DialogueManager dialogueManager;
    public AIMovement AIMovement;
    public ResetToSelection ResetToSelection;


    public void RestartDrink()
    {
        CurrentType = "";
        CurrentMix = new Dictionary<string, int>();
    }
    public string CurrentType;
    public string CurrentDrink;

    [Header("Mini-Games")]
    public GameObject _Mixing;
    public GameObject _Shaking;
    public GameObject _Pouring;


    Dictionary<string, int> TypePartLimit = new Dictionary<string, int>()
    {
        {"Mixing", 6},
        {"Shots", 1},
        {"Shake", 6}
    };

    Dictionary<string, int> CurrentMix = new Dictionary<string, int>();

    Dictionary<string, Dictionary<string, int>> MixDrinks = new Dictionary<string, Dictionary<string, int>>()
{
    {"Tequila Sunrise Twist", new Dictionary<string, int>()
        {
            {"Tequila", 3},
            {"Cranberry", 2},
            {"Blue Curacao", 1}
        }
    },
    {"Vodka Citrus Cooler", new Dictionary<string, int>()
        {
            {"Vodka", 3},
            {"Triple_Sec", 1},
            {"Lime", 1},
            {"Cranberry", 1}
        }
    },
    {"Curacao Sunset", new Dictionary<string, int>()
        {
            {"Blue_Curacao", 2},
            {"Cranberry", 3},
            {"Lime", 1}
        }
    },
};

    Dictionary<string, Dictionary<string, int>> ShakeDrinks = new Dictionary<string, Dictionary<string, int>>()
{
    {"Classic Cosmopolitan", new Dictionary<string, int>()
        {
            {"Vodka", 2},
            {"Triple_Sec", 1},
            {"Cranberry", 2},
            {"Lime", 1}
        }
    },
    {"Blue Margarita", new Dictionary<string, int>()
        {
            {"Tequila", 2},
            {"Blue_Curacao", 3},
            {"Lime", 1}
        }
    },
    {"Full Six Fusion", new Dictionary<string, int>()
        {
            {"Tequila", 1},
            {"Vodka", 1},
            {"Triple_Sec", 1},
            {"Blue_Curacao", 1},
            {"Lime", 1},
            {"Cranberry", 1}
        }
    },
};

    Dictionary<string, Dictionary<string, int>> ShotsDrinks = new Dictionary<string, Dictionary<string, int>>()
{
    {"Tequila Shot", new Dictionary<string, int>()
        {
            {"Tequila", 1}
        }
    },
    {"Vodka Shot", new Dictionary<string, int>()
        {
            {"Vodka", 1}
        }
    },
    {"Triple Sec Shot", new Dictionary<string, int>()
        {
            {"Triple_Sec", 1}
        }
    },
    {"Blue Curacao Shot", new Dictionary<string, int>()
        {
            {"Blue_Curacao", 1}
        }
    },
    {"Lime Juice Shot", new Dictionary<string, int>()
        {
            {"Lime", 1}
        }
    },
    {"Cranberry Shot", new Dictionary<string, int>()
        {
            {"Cranberry", 1}
        }
    },
};


    Dictionary<string, Dictionary<string, Dictionary<string, int>>> DrinksType = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();

    public void Start()
    {
        DrinksType = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>()
    {
        {"Mixing",MixDrinks},
        {"Shots",ShotsDrinks},
        {"Shake",ShakeDrinks}
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
        string Name = "None";
        Debug.Log(CurrentMix.Keys);
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
            if (count == IngCount && CurrentMix.Count == IngCount)
            {
                Name = DrinkName;
            }
        }
        return Name;
    }

    public void ResetDrink()
    {
        CurrentMix.Clear();
    }

    public void AddPart(string NamePart)
    {
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
            CurrentDrink = IsDrink();
            Debug.Log(CurrentDrink);
            if (CurrentType == "Mixing")
            {
                _Mixing.SetActive(true);
            }
            else if (CurrentType == "Shake")
            {
                _Shaking.SetActive(true);
            }
            _Pouring.SetActive(false);

        }
        Debug.Log(CurrentMix[NamePart]);
    }

    void Update()
    {
        if (CurrentDrink == dialogueManager.orderStore && AIMovement.doneOrder == false && dialogueManager.orderStore != "")
        {
            ResetToSelection.ResetToSelectionMenu();
            dialogueManager.miniGame.SetActive(false);
            AIMovement.doneOrder = true;
            Debug.Log(CurrentDrink);
        }
        else if (CurrentDrink != dialogueManager.orderStore && CurrentDrink != "")
        {
            ResetToSelection.ResetToSelectionMenu();
        }
    }
}
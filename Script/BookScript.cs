using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BookScript : MonoBehaviour
{
    public GameObject Book;

    public GameObject FrontPages;
    public GameObject DrinksPages;

    public GameObject PagesPrefab;

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


    public void AbleBook()
    {
        FrontPages.SetActive(true);
        DrinksPages.SetActive(false);
    }
    Dictionary<int, string> _Index;
    List<GameObject> CurrentPrefabs;

    public void CreateDrinkPrefab()
    {

    }

    public void FrontPagesButtons(string TypeName)
    {
        DrinksPages.SetActive(true);
        FrontPages.SetActive(false);
        _Index.Clear();
        int Count = 0;
        Dictionary<string, Dictionary<string, int>> TypeDrinks = DrinksType[TypeName];
        foreach (string DrinkName in TypeDrinks.Keys)
        {
            Count++;
            _Index.Add(Count, DrinkName);
            Dictionary<string, int> Drink = TypeDrinks[DrinkName];
            Debug.Log("  " + DrinkName);
            
            foreach (string Ing in Drink.Keys)
            {
                Debug.Log(Ing + " " + Drink[Ing]);
            }
        }
        for (var i = 1; i < DrinksPages.transform.childCount; i++)
        {
            Transform Child = DrinksPages.transform.GetChild(i);
            GameObject Clone = Instantiate(PagesPrefab);
            Clone.transform.parent = Child;
            CurrentPrefabs.Add(Clone);
            Clone.SetActive(true);
            GameObject NameHolder = Clone.transform.Find("Name").gameObject;
            GameObject IngPreFab = Clone.transform.Find("Grid").Find("IngName").gameObject;
            NameHolder.GetComponent<TextMeshProUGUI>().text = _Index[i];
            Dictionary<string, int> Drink = TypeDrinks[_Index[i]];
            foreach (string Ing in Drink.Keys)
            {
                IngPreFab.GetComponent<TextMeshProUGUI>().text = Ing;
                IngPreFab = Instantiate(IngPreFab);
                Debug.Log(Ing + " " + Drink[Ing]);
            }

        }
    }

    public void BookButtonFunc()
    {
        if (Book.activeInHierarchy)
        {
            Book.SetActive(false);
        }
        else
        {
            Book.SetActive(true);
            AbleBook();
        }
    }
}

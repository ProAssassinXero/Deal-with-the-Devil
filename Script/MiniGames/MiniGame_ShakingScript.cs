using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MiniGame_ShakingScript : PhaseManager
{
    public void RestartDrink()
    {

    }
    public string CurrentType;

    Dictionary<int, string> CurrentMix = new Dictionary<int, string>();
}

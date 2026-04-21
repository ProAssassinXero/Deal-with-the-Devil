using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MiniGame_ShakingScript : PhaseManager, IPointerUpHandler// These are the interfaces the OnPointerUp method requires.
{
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("The mouse click was released");
    }
}

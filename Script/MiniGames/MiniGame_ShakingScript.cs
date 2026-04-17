using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MiniGame_ShakingScript : PhaseManager, IPointerUpHandler// These are the interfaces the OnPointerUp method requires.
{
    //OnPointerDown is also required to receive OnPointerUp callbacks


    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("The mouse click was released");
    }
}

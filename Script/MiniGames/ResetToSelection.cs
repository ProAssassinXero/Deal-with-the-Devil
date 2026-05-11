using UnityEngine;

public class ResetToSelection : MonoBehaviour
{
    public MiniGame_ShakingScript Manager;

    public GameObject SelectionMenu;   // The shots/mixing/shaking selection screen
    public GameObject MixingStage;     // _Mixing object
    public GameObject ShakingStage;    // _Shaking object
    public GameObject PouringStage;    // _Pouring object

    public void ResetToSelectionMenu()
    {
        // Reset drink state
        Manager.RestartDrink();
        Manager.CurrentDrink = "";

        // Hide all mini-game stages
        if (MixingStage != null) MixingStage.SetActive(false);
        if (ShakingStage != null) ShakingStage.SetActive(false);
        if (PouringStage != null) PouringStage.SetActive(false);

        // Show the selection menu
        SelectionMenu.SetActive(true);
    }
}
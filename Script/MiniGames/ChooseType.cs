using UnityEngine;

public class ChooseType : MonoBehaviour
{

    public MiniGame_ShakingScript Manager;
    public GameObject NextStage;

    public void Choose_Type(string _Type)
    {
        NextStage.SetActive(true);
        Manager.CurrentType = _Type;
        gameObject.SetActive(false);
    }
}

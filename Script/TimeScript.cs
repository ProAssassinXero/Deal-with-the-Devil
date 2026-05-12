using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{
    public int CurrentTime = 11;
    public string FinalText = " : AM";

    public TextMeshProUGUI TextBox;

    public void ChangeDisplayTime()
    {
        TextBox.text = CurrentTime + FinalText;
    }

    public void AddTime(int Amount)
    {
        CurrentTime += Amount;
        if (CurrentTime > 12)
        {
            FinalText = " : PM";
            CurrentTime -= 12;
        }
        ChangeDisplayTime();
    }

    private void Start()
    {
        AddTime(1);
    }

}

using UnityEngine;

public class GabrielDialogueScript : MonoBehaviour
{
    private string[] gabrielDialogue = new string[]
    {
        "Gabriel: Greetings, traveler.",
        "Gabriel: I am Gabriel, the messenger of the divine.",
        "Gabriel: I have come to offer you guidance and protection on your journey.",
        "Gabriel: Will you accept my help and walk the path of righteousness?"
    };

    public void GabiSend()
    {
        DialogueManager.instance.StartDialogue(gabrielDialogue);
    }
}

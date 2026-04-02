using UnityEngine;

public class LuciferDialogueScript : MonoBehaviour
{

    private string[] luciferDialogue = new string[]
    {
        "Lucifer: Welcome to my domain, mortal.",
        "Lucifer: I see you have come seeking power and knowledge.",
        "Lucifer: I can offer you a deal, but be warned, it comes with a price.",
        "Lucifer: Are you willing to make a pact with me?"
    };

    public void LuciSend()
    {
        DialogueManager.instance.StartDialogue(luciferDialogue);
    }
}

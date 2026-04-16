using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Phases CurrentPhase = Phases.Combat;
    public enum Phases
    {
        Bar,
        Combat,
        Clean_Up
    };
}

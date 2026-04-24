using UnityEngine;

public class AIWaypointBar : MonoBehaviour
{
    public Transform frontCounter;

    [Header("Chairs to go to")]
    public Transform[] chairTransform;

    [Header("Center Chairs")]
    public Transform[] c_ChairToGoTo;
    [Header("Lower Center Chairs")]
    public Transform[] lC_ChairToGoTo;


    [Header("Right Chairs")]
    public Transform[] r_ChairToGoTo;
    [Header("Lower Right Chairs")]
    public Transform lR_ChairToGoTo;

    [Header("Left Chairs")]
    public Transform[] l_ChairToGoTo;
    [Header("Left Lower Chairs")]
    public Transform lL_ChairToGoTo;

    [Header("Right Stools")]
    public Transform[] stools;

    [Header("Upper Right Seats")]
    public Transform[] upperChairs;
}

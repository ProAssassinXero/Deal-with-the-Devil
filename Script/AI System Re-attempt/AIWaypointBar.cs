using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIWaypointBar : MonoBehaviour
{
    public AISeatStorage AISeatStorage;

    public Transform frontCounter;

    [Header("Chairs to go to")]
    public List<Transform> chairTransform;

    [Header("Center Chairs")]
    public List<Transform> c_ChairToGoTo;
    [Header("Lower Center Chairs")]
    public List<Transform> lC_ChairToGoTo;


    [Header("Right Chairs")]
    public List<Transform> r_ChairToGoTo;
    [Header("Lower Right Chairs")]
    public Transform lR_ChairToGoTo;

    [Header("Left Chairs")]
    public List<Transform> l_ChairToGoTo;
    [Header("Left Lower Chairs")]
    public Transform lL_ChairToGoTo;

    [Header("Right Stools")]
    public List<Transform> stools;

    [Header("Upper Right Seats")]
    public List<Transform> upperChairs;
}

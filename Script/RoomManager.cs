using UnityEngine;
using System.Collections;
public class RoomManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CameraMovement Camera;
    public GameObject Player;

    [SerializeField] private float desiredDuration = 0.5f;
    [SerializeField] private float elapsedTime;

    [SerializeField] private float Range = 0.2f;

    [SerializeField] private AnimationCurve curve;

    bool Approximate(Vector3 Pos1, Vector3 Pos2, float Range)
    {
        if ((Pos1.x > Pos2.x - Range && Pos1.x < Pos2.x + Range) && (Pos1.y > Pos2.y - Range && Pos1.y < Pos2.y + Range))
        {
            return false;
        }
        return true;
    }

    private IEnumerator Tween(Vector3 startPosition,Vector3 endPosition)
    {
        elapsedTime = 0;
        do
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;
            Camera.Camera.transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percentageComplete));
            yield return new WaitForEndOfFrame();
            
        } while (Approximate(Camera.Camera.transform.position, endPosition, Range));
        Camera.Camera.transform.position = endPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("door"))
        {
            DoorScript Script = collision.GetComponent<DoorScript>();
            Vector3 endPosition;
            Vector3 startPosition = Camera.Camera.transform.position;
            Camera.xClamp = Script.xStoredClamp;
            Camera.yClamp = Script.yStoredClamp;
            Camera.Center = Script.StoredCenter;
            endPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, Camera.transform.position.z);
            if (endPosition.x > Script.xStoredClamp + Script.StoredCenter.x)
                endPosition.x = Script.xStoredClamp + Script.StoredCenter.x;
            else if (endPosition.x < -Script.xStoredClamp + Script.StoredCenter.x)
                endPosition.x = -Script.xStoredClamp + Script.StoredCenter.x;
            if (endPosition.y > Script.yStoredClamp + Script.StoredCenter.y)
                endPosition.y = Script.yStoredClamp + Script.StoredCenter.y;
            else if (endPosition.y < -Script.yStoredClamp + Script.StoredCenter.y)
                endPosition.y = -Script.yStoredClamp + Script.StoredCenter.y;
            StartCoroutine(Tween(startPosition, endPosition));
        }
    }
}

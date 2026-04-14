using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CameraMovement Camera;

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("door"))
        {
            DoorScript Script = collision.GetComponent<DoorScript>();

            float localXClamp = Camera.xClamp;
            float localYClamp = Camera.yClamp;
            Vector2 localCenter = Camera.Center;
            Camera.xClamp = Script.xStoredClamp;
            Camera.yClamp = Script.yStoredClamp;
            Camera.Center = Script.StoredCenter;
            Camera.Camera.transform.position = new Vector3(Script.StoredCenter.x, Script.StoredCenter.y, Camera.transform.position.z);
            Script.xStoredClamp = localXClamp;
            Script.yStoredClamp = localYClamp;
            Script.StoredCenter = localCenter;
        }
    }
}

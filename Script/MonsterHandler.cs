using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterHandler : MonoBehaviour
{
    public List<GameObject> ObjectPlayerFollow;

    public List<GameObject> ObjectSetPos;

    public GameObject Player;

    public List<GameObject> Monsters;

    private void FixedUpdate()
    {
        foreach (GameObject _Objects in ObjectPlayerFollow)
        {
            _Objects.transform.position = Player.transform.position;
        }
    }

    public void AddRandomMonster(Enemy MonsterScript)
    {
        int Choosen_Type = Random.Range(0, 1);
        if (1 == 1)
        {
            int _Random = Random.Range(0,ObjectPlayerFollow.Count);
            foreach (Transform Child in ObjectPlayerFollow[_Random].GetComponentsInChildren<Transform>())
            {
                if (Child.CompareTag("Target"))
                {
                    MonsterScript.targetGroup.Add(Child);
                }
            }
        }
        else
        {

        }
    }
}

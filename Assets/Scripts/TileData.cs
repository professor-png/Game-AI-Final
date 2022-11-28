using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dir { left, right, up, down};

[System.Serializable]
public struct Entrance
{
    public GameObject wall;
    public Transform alignPt;
    public dir direction;
}

public class TileData : MonoBehaviour
{
    public Entrance[] entrances;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateTile(GameObject test)
    {
        GameObject testObj1 = Instantiate(test);

        Vector3 dif = transform.position - entrances[1].alignPt.position;

        foreach (Entrance entrance in testObj1.GetComponent<TileData>().entrances)
        {
            if (entrances[1].direction == dir.right && entrance.direction == dir.left)
                testObj1.transform.position = transform.position + entrance.alignPt.position;
        }
    }
}

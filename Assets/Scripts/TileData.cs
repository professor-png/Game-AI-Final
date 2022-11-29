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

    public void CreateTile(GameObject tile)
    {
        GameObject newTile = Instantiate(tile);


        //randomly select open option
        foreach (Entrance entranceNT in newTile.GetComponent<TileData>().entrances)
        {
            //if (entrances[1].direction == dir.right && entranceNT.direction == dir.left)
            //    newTile.transform.position = transform.position + entranceNT.alignPt.position - entrances[1].alignPt.position;

            //if (entrances[0].direction == dir.left && entranceNT.direction == dir.right)
            //    newTile.transform.position = transform.position + entranceNT.alignPt.position - entrances[0].alignPt.position;


            //if (entrances[0].direction == dir.up && entranceNT.direction == dir.down)
            //    newTile.transform.position = transform.position + entranceNT.alignPt.position - entrances[0].alignPt.position;

            if (entrances[1].direction == dir.down && entranceNT.direction == dir.up)
                newTile.transform.position = transform.position + entranceNT.alignPt.position - entrances[1].alignPt.position;
        }
    }
}

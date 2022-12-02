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

    public GameObject CreateTile(GameObject tile, dir _dir)
    {
        //_dir is from old to new
        GameObject newTile = Instantiate(tile);

        foreach (Entrance entranceNT in newTile.GetComponent<TileData>().entrances)
        {
            foreach(Entrance oldEntrance in entrances)
            {
                if(oldEntrance.direction == _dir)
                {
                    if ((_dir == dir.right && entranceNT.direction == dir.left) 
                        || (_dir == dir.left && entranceNT.direction == dir.right)
                        || (_dir == dir.up && entranceNT.direction == dir.down)
                        || (_dir == dir.down && entranceNT.direction == dir.up))
                        newTile.transform.position = transform.position - entranceNT.alignPt.position + oldEntrance.alignPt.position;
                }
            }
        }
        return newTile;
    }
}

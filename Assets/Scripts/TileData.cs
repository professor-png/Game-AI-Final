using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    public List<CollisionCheck> walls;
    public List<Entrance> entrances;

    public List<CollisionCheck> center;

    public GameObject from = null;
    public Entrance tmpEntrance, test;

    // Start is called before the first frame update
    void Awake()
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
        //rint(newTile.transform.position + " " + newTile.transform.name);
        foreach (Entrance entranceNT in newTile.GetComponent<TileData>().entrances)
        {
            foreach(Entrance oldEntrance in entrances)
            {
                if (oldEntrance.direction == _dir)
                {
                    if ((_dir == dir.right && entranceNT.direction == dir.left)
                        || (_dir == dir.left && entranceNT.direction == dir.right)
                        || (_dir == dir.up && entranceNT.direction == dir.down)
                        || (_dir == dir.down && entranceNT.direction == dir.up))
                    {
                        SetPosition(newTile, entranceNT, oldEntrance.alignPt.position);

                        //if (!newTile.GetComponent<TileData>().CheckSpace())
                        //{
                        //    Destroy(newTile);
                        //    return null;
                        //}

                        tmpEntrance = oldEntrance;
                        newTile.GetComponent<TileData>().tmpEntrance = entranceNT;
                        
                        newTile.GetComponent<TileData>().from = gameObject;
                        
                        Destroy(entranceNT.wall);
                        //Destroy(oldEntrance.wall);

                        newTile.GetComponent<TileData>().entrances.Remove(entranceNT);
                        //entrances.Remove(oldEntrance);
                        
                        return newTile;
                    }
                }
            }
        }
        
        return newTile;
    }

    public bool CheckSpace()
    {
        bool vacant = false;

        foreach(CollisionCheck check in center)
        {
            vacant = check.GetCollision();

            if (vacant)
                break;
        }

        return vacant;
    }

    public void SetPosition(GameObject newTile, Entrance entrance, Vector3 otherPos)
    {
        Vector3 dif = Vector3.zero - entrance.alignPt.position;

        entrance.alignPt.position = otherPos;

        newTile.transform.position = entrance.alignPt.position + dif;
        entrance.alignPt.position = newTile.transform.position - dif;
    }

    public void DeleteEntrance()
    {
        Destroy(tmpEntrance.wall);
        entrances.Remove(tmpEntrance);
    }
}

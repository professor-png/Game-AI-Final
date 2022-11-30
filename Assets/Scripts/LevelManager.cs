using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] tiles;
    public dir testDir;
    [Range(0,2)]
    public int tileSelection = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject test = Instantiate(tiles[2]);
        
        //choose direction to place tile here
        //how to make sure new tile has correct direction?
        test.GetComponent<TileData>().CreateTile(tiles[tileSelection], testDir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

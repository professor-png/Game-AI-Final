using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        GameObject test = Instantiate(tiles[2]);

        test.GetComponent<TileData>().CreateTile(tiles[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

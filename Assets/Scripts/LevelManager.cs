using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
        GameObject test = Instantiate(tile);

        test.GetComponent<TileData>().CreateTile(tile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

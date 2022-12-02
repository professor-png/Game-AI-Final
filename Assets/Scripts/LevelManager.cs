using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> placedTiles;

    public List<GameObject> selectableTile;

    dir movementDir;
    int tileSelection = 0;
    bool validTile = false;
    bool validDirection = false;

    GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(GameObject tile in placedTiles)
            {
                Destroy(tile);
            }

            placedTiles.Clear();
            Generate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GenerateNext();
        }
    }

    private void Generate()
    {
        test = Instantiate(selectableTile[0]);
        placedTiles.Add(test);
    }

    private void GenerateNext()
    {
        checkNewTile(test);

        placedTiles.Add(test.GetComponent<TileData>().CreateTile(selectableTile[tileSelection], movementDir));

        test = placedTiles[placedTiles.Count - 1];
        //Debug.Log(movementDir);
    }

    void getNewDirection(GameObject currentTile)
    {
        validDirection = false;
        do
        {
            //randomly choose direction to go
            movementDir = (dir)Random.Range(0, 3);
            foreach (Entrance directionCheck in currentTile.GetComponent<TileData>().entrances)
            {
                if (movementDir == directionCheck.direction)
                {
                    validDirection = true;
                    break;
                }
            }

        } while (!validDirection);
    }

    void checkNewTile(GameObject currentTile)
    {
        validTile = false;
        do
        {
            getNewDirection(currentTile);

            //randomly choose tile to place
            tileSelection = Random.Range(1, selectableTile.Count);
            print(currentTile.GetComponent<TileData>().entrances.Count);
            //check tile for oppositie direction
            foreach (Entrance tileCheck in selectableTile[tileSelection].GetComponent<TileData>().entrances)
            {
                if ((movementDir == dir.up && tileCheck.direction == dir.down) ||
                    (movementDir == dir.down && tileCheck.direction == dir.up) ||
                    (movementDir == dir.left && tileCheck.direction == dir.right) ||
                    (movementDir == dir.right && tileCheck.direction == dir.left))
                {
                    validTile = true;
                    break;
                }
            }
        } while (!validTile);
    }
}

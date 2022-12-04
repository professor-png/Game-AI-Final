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
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (placedTiles.Count > 0)
            CheckLast();

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
        index = 0;
    }

    private void GenerateNext()
    {
        GameObject tmp = null;

        checkNewTile(ref test);
        tmp = test.GetComponent<TileData>().CreateTile(selectableTile[tileSelection], movementDir);

        placedTiles.Add(tmp);

        test = placedTiles[placedTiles.Count - 1];
        index++;
    }

    void getNewDirection(ref GameObject currentTile)
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
                    index = placedTiles.Count - 1;
                    break;
                }
            }

            if (validDirection == false)
            {
                index--;

                if (index <= 0)
                    index = placedTiles.Count - 1;

                currentTile = placedTiles[index];
            }

        } while (!validDirection);
    }

    void checkNewTile(ref GameObject currentTile)
    {
        validTile = false;
        do
        {
            getNewDirection(ref currentTile);

            //randomly choose tile to place
            tileSelection = Random.Range(1, selectableTile.Count);
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

    void CheckLast()
    {
        GameObject last = placedTiles[placedTiles.Count - 1];

        bool lastSpace = last.GetComponent<TileData>().CheckSpace();

        if (lastSpace)
        {
            print("ass");
            placedTiles.Remove(last);
            Destroy(last);
            test = placedTiles[placedTiles.Count - 1];
        }
        else
        {
            last.GetComponent<TileData>().DeleteEntrance();
        }
    }
}

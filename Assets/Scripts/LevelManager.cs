using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> placedTiles;

    public GameObject[] selectableTile;

    dir movementDir;
    int tileSelection = 0;
    bool validTile = false;
    bool validDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject test = Instantiate(selectableTile[Random.Range(0,3)]);
        placedTiles.Add(test);

        checkNewTile(test);

        test.GetComponent<TileData>().CreateTile(selectableTile[tileSelection], movementDir);
    }

    void getNewDirection(GameObject currentTile)
    {
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
        do
        {
            validDirection = false;
            getNewDirection(currentTile);

            //randomly choose tile to place
            tileSelection = Random.Range(0, selectableTile.Length);

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

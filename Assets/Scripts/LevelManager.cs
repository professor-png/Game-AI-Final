using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> placedTiles;

    public List<GameObject> selectableTile;

    public List<GameObject> deadEnds;

    dir movementDir;
    int tileSelection = 0;
    bool validTile = false;
    bool validDirection = false;

    GameObject test, last;
    int index = 0;

    public float generateTime = 0.2f;
    public float levelLength;
    float time = 0;

    bool generate = true;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    private void FixedUpdate()
    {
        if (generate)
        {
            if (Vector2.Distance(placedTiles[placedTiles.Count - 1].transform.position, Vector2.zero) > levelLength)
            {
                //time = generateTime * 2f;
                //Tick(deadEnds);
                PlaceDeadEnd();
                generate = false;
            }
            else
                Tick(selectableTile);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach(GameObject tile in placedTiles)
            {
                Destroy(tile);
            }
            generate = true;
            placedTiles.Clear();
            Generate();
        }
    }

    private void Generate()
    {
        test = Instantiate(selectableTile[0]);
        placedTiles.Add(test);
        index = 0;
    }

    private void GenerateNext(List<GameObject> tiles)
    {
        checkNewTile(ref test, tiles);
        last = test.GetComponent<TileData>().CreateTile(selectableTile[tileSelection], movementDir);

        placedTiles.Add(last);

        test = placedTiles[placedTiles.Count - 1];
        index++;
    }

    void Tick(List<GameObject> tiles)
    {
        time += Time.deltaTime;

        if (time >= generateTime)
        {
            GenerateNext(tiles);
            StartCoroutine(StartCheck());

            time = 0f;
        }
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

    void checkNewTile(ref GameObject currentTile, List<GameObject> tiles)
    {
        validTile = false;
        do
        {
            getNewDirection(ref currentTile);

            //randomly choose tile to place
            tileSelection = Random.Range(1, tiles.Count);
            //check tile for oppositie direction
            foreach (Entrance tileCheck in tiles[tileSelection].GetComponent<TileData>().entrances)
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

    void PlaceDeadEnd()
    {
        validDirection = false;
        do
        {
            //randomly choose direction to go
            movementDir = (dir)Random.Range(0, 3);
            foreach (Entrance directionCheck in placedTiles[placedTiles.Count - 1].GetComponent<TileData>().entrances)
            {
                if (movementDir == directionCheck.direction)
                {
                    validDirection = true;
                    break;
                }
            }

        } while (!validDirection);

        foreach (GameObject obj in deadEnds)
        {
            Entrance tileCheck = obj.GetComponent<TileData>().entrances[0];

            if ((movementDir == dir.up && tileCheck.direction == dir.down) ||
                    (movementDir == dir.down && tileCheck.direction == dir.up) ||
                    (movementDir == dir.left && tileCheck.direction == dir.right) ||
                    (movementDir == dir.right && tileCheck.direction == dir.left))
            {
                last = test.GetComponent<TileData>().CreateTile(obj, movementDir);

                placedTiles.Add(last);
                break;
            }
        }

        StartCoroutine(StartCheck());
    }

    IEnumerator StartCheck()
    {
        yield return new WaitForFixedUpdate();

        CheckLast();
    }

    void CheckLast()
    {
        //last = placedTiles[placedTiles.Count - 1];

        if (last == null)
        {
            Debug.Log("null");
            return;
        }

        bool lastSpace = last.GetComponent<TileData>().CheckSpace();

        if (lastSpace)
        {
            placedTiles.Remove(last);
            Destroy(last);
            test = placedTiles[placedTiles.Count - 1];
        }
        else
        {
            GameObject tmp = last.GetComponent<TileData>().from;
            tmp.GetComponent<TileData>().DeleteEntrance();
        }
    }
}

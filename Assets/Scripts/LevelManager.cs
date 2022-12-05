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

    GameObject test, last;
    int index = 0;

    public float generateTime = 0.2f;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    private void FixedUpdate()
    {
        Tick();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach(GameObject tile in placedTiles)
            {
                Destroy(tile);
            }

            placedTiles.Clear();
            Generate();
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    GenerateNext();
        //    StartCoroutine(StartCheck());
        //}
    }

    private void Generate()
    {
        test = Instantiate(selectableTile[0]);
        placedTiles.Add(test);
        index = 0;
    }

    private void GenerateNext()
    {
        checkNewTile(ref test);
        last = test.GetComponent<TileData>().CreateTile(selectableTile[tileSelection], movementDir);

        placedTiles.Add(last);

        test = placedTiles[placedTiles.Count - 1];
        index++;
    }

    void Tick()
    {
        time += Time.deltaTime;

        if (time > generateTime)
        {
            GenerateNext();
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

    IEnumerator StartCheck()
    {
        yield return new WaitForSeconds(.1f);

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

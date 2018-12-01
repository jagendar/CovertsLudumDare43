using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

public class PopulateWorld : MonoBehaviour {
    [SerializeField] private Tile waterTile, lavaTile, sandTile, dirtTile, grassTile;

    [SerializeField] private int worldSize, halfSize;
    [SerializeField] private int maxHeight; 

    private static Tile[,] worldArray;
    private World world;

    private GameObject[,] tiles;

    private void Awake()
    {
        worldArray = new Tile[worldSize, worldSize];
        world = new World(worldArray);
    }

    void Start () {
        halfSize = worldSize / 2;

        for (int x = 0; x < worldSize; x++)
        {
            for (int z = 0; z < worldSize; z++)
            {
                Tile thisTile;
                if (worldSize - z < 5 || worldSize - x < 5 || x < 5 || z <5)
                {
                    thisTile = waterTile;
                }
                else if((worldSize - z < 10 && worldSize - z >= 5) || (worldSize - x < 10 && worldSize - x >= 5) || 
                        (x < 10 && x >= 5) || (z < 10 && z >= 5))
                {
                    thisTile = sandTile;
                }
                else
                {
                    thisTile = grassTile;
                }

                world[x, z] = thisTile;
                worldArray[x,z] = Instantiate(world[x, z], new Vector3(x - halfSize, thisTile.Height, z - halfSize), Quaternion.identity, transform);
            }
        }

        SetVolcanoHeights();
    }

    private void SetVolcanoHeights()
    {
        worldArray[halfSize, halfSize].Height = maxHeight;

        SetNeighbors(halfSize, halfSize, maxHeight, 1);

        //float heightSub = maxHeight;
        //for(int i = maxHeight; i >= 0; i-=2)
        //{
        //    SetHeights(i, maxHeight - heightSub);
        //    SetHeights(i - 1, maxHeight - heightSub);
        //    heightSub-= 0.5f;
        //}
    }

    private void SetNeighbors(int i, int j, float height, int layer)
    {
        float newHeight;
        int row_limit = worldArray.Length;

        if (row_limit > 0)
        {
            int column_limit = worldArray.GetLength(0);
            for (int x = Mathf.Max(0, i - layer); x <= Mathf.Min(i + layer, row_limit); x++)
            {
                for (int y = Mathf.Max(0, j - layer); y <= Mathf.Min(j + layer, column_limit); y++)
                {
                    if (x != i || y != j)
                    {
                        int rand = Random.Range(1, 3);
                        if (rand == 2)
                        {
                            newHeight = height - 0.5f;
                        }
                        else
                        {
                            newHeight = height;
                        }
                        worldArray[x, y].Height = newHeight;
                        // Debug.Log(worldArray[x, y].transform.position);
                        //SetNeighbors(x, x, newHeight, layer+1);

                    }
                }
            }
        }

    }


    private void SetHeights(int volcanoLayer, float height)
    {

        //for (int i = worldSize / 2 - volcanoLayer; i < worldSize / 2 + volcanoLayer; i++)
        //{
        //    for(int j = worldSize / 2 - volcanoLayer; j < worldSize / 2 + volcanoLayer; j++)
        //    {
        //        worldArray[i, j].Height = height; 
        //    }
        //}
    }
}

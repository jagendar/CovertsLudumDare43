using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

public class PopulateWorld : MonoBehaviour {
    [SerializeField] private Tile waterTile, lavaTile, sandTile, dirtTile, grassTile;

    [SerializeField] private int worldSize;
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
        Tile thisTile;
        for (int x = 0; x < worldSize; x++)
        {
            for (int z = 0; z < worldSize; z++)
            {
                if (worldSize - z < 3 || worldSize - x < 3 || x < 3 || z < 3)
                {
                    thisTile = sandTile;
                }
                else
                {
                    thisTile = dirtTile;
                }

                thisTile.transform.position = new Vector3(x, thisTile.Height, z);
                world[x, z] = thisTile;
                worldArray[x,z] = Instantiate(world[x, z]);
            }
        }

        SetVolcanoHeights();
    }

    private void SetVolcanoHeights()
    {
        int heightSub = maxHeight;
        for(int i = maxHeight; i >= 0; i-=2)
        {
            SetHeights(i, maxHeight - heightSub);
            SetHeights(i - 1, maxHeight - heightSub);
            heightSub--;
        }
    }

    private void SetHeights(int volcanoLayer, int height)
    {
        for (int i = worldSize / 2 - volcanoLayer; i < worldSize / 2 + volcanoLayer; i++)
        {
            for(int j = worldSize / 2 - volcanoLayer; j < worldSize / 2 + volcanoLayer; j++)
            {
                worldArray[i, j].Height = height;
            }
        }
    }
}

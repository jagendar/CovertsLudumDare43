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
        int halfSize = worldSize / 2;
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
        float heightSub = maxHeight;
        for(int i = maxHeight; i >= 0; i-=2)
        {
            SetHeights(i, maxHeight - heightSub);
            SetHeights(i - 1, maxHeight - heightSub);
            heightSub-= 0.5f;
        }
    }

    private void SetHeights(int volcanoLayer, float height)
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

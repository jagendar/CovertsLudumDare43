using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

public class PopulateWorld : MonoBehaviour {
    [SerializeField] private Tile waterTile, lavaTile, sandTile, dirtTile, grassTile;
    [SerializeField] private int waterWidth, sandWidth;
    [SerializeField] private int worldSize;
    [SerializeField] private int maxHeight;

    private int halfSize;

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
                Tile thisTile = SetTileType(x, z);

                world[x, z] = thisTile;

                Tile tile = Instantiate(world[x, z], new Vector3(x - halfSize, thisTile.Height, z - halfSize), Quaternion.identity, transform);
                tile.Position = new Vector2Int(x, z);
                worldArray[x, z] = tile;
            }
        }

        SetVolcanoHeights();
    }


    private Tile SetTileType(int x, int z)
    {
        Tile tile;
        int sandEdge = waterWidth + sandWidth;
        if (worldSize - z < waterWidth || worldSize - x < waterWidth || x < waterWidth || z < waterWidth)
        {
            tile = waterTile;
        }
        else if ((worldSize - z < sandEdge && worldSize - z > waterWidth) || (worldSize - x < sandEdge && worldSize - x > waterWidth) ||
                (x < sandEdge && x > waterWidth) || (z < sandEdge && z > waterWidth))
        {
            tile = sandTile;
        }
        else if (worldSize - z == waterWidth || worldSize - x == waterWidth || x == waterWidth || z == waterWidth)
        {
            int randomChance = Random.Range(1, 3);
            if (randomChance == 2)
            {
                tile = sandTile;
            }
            else
            {
                tile = waterTile;
            }
        }
        else if (worldSize - z == sandEdge || worldSize - x == sandEdge || x == sandEdge || z == sandEdge)
        {
            int randomChance = Random.Range(1, 3);
            if (randomChance == 2)
            {
                tile = sandTile;
            }
            else
            {
                tile = grassTile;
            }
        }
        else
        {
            tile = grassTile;
        }

        return tile;
    }

    private void SetVolcanoHeights()
    {
        worldArray[halfSize, halfSize].Height = maxHeight;

        SetNeighbors(halfSize, halfSize, maxHeight, 1);
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
                    }
                }
            }
        }

    }
}

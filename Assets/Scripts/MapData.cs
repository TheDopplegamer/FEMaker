using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {

    public Vector2 dimensions;  //The size of the map (length and width)

    public TileData[,] grid;           //The 2D array of tiles that make of the map

    public GameObject grass_tile;

    public bool trigger;


    void Update()
    {
        if (trigger)
        {
            trigger = false;
            Create_Grid();
        }
    }


    //Create a grid with the correct dimensions
    void Create_Grid()
    {
        grid = new TileData[(int)dimensions.x, (int)dimensions.y];

        for(int y = 0;y < (int)dimensions.y; y++)
        {
            for(int x = 0;x< (int)dimensions.x; x++)
            {
                GameObject new_tile = Instantiate(grass_tile);
                new_tile.transform.position = new Vector3(x * 0.32f, y * -0.32f, 0);
                grid[x, y] = new_tile.GetComponent<TileData>();
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

    public Vector2 position;  //The x and y coordinates of the cursor

    MapData map;              //Reference to the map

    UnitData unit;            //The currently selected unit
    public GameObject unit_object;


    int timer = 0;
    SpriteRenderer sprite;
    public Sprite blue_sprite;
    public Sprite oragne_sprite;

    TileData previous_tile;



    void Start()
    {
        map = GameObject.Find("GameManager").GetComponent<MapData>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        if (timer == 0)
        {
            if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            {
                int new_x = 0;
                int new_y = 0;
                if (Input.GetKey("a")) { new_x -= 1; }
                if (Input.GetKey("d")) { new_x += 1; }
                if (Input.GetKey("w")) { new_y -= 1; }
                if (Input.GetKey("s")) { new_y += 1; }
                Move(new_x, new_y);
                timer = 7;
            }
        }
        else
        {
            timer -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(unit_object == null) { Select_Unit(); }
            else                    { Move_Unit();   }
        }
    }




    //Move the cursor based on the parameters
    void Move(int x,int y)
    {
        position.x += x;
        position.y += y;
        //Don't let the cursor leave the map
        Keep_In_Bounds();
        transform.position = new Vector3((position.x-1) * 0.32f, (position.y-1) * -0.32f, 0);
    }

    //Prevent the cursor from going out of bounds of the maps
    void Keep_In_Bounds()
    {
        if(position.x < 1) { position.x = 1; }
        else if(position.x > map.dimensions.x) { position.x = map.dimensions.x; }
        if(position.y < 1) { position.y = 1; }
        else if (position.y > map.dimensions.y) { position.y = map.dimensions.y; }
    }


    //Checks the unit at the currently hovered-over tile and gets a reference if it exists
    void Select_Unit()
    {
        TileData check = map.grid[(int)position.x-1, (int)position.y-1];
        if(check == null)
        {
            Debug.Log("NULL TILE FOUND AT ( " + ((int)position.x - 1).ToString() + " , " + ((int)position.y - 1).ToString() + " )");
        }
        else if(check.unit != null)
        {
            unit = check.unit;
            unit_object = unit.gameObject;
            previous_tile = check;
            sprite.sprite = oragne_sprite;
            Show_Move();
        }
        else
        {
            Debug.Log("NUL UNIT FOUND AT ( " + ((int)position.x - 1).ToString() + " , " + ((int)position.y - 1).ToString() + " )");
        }
    }


    //Clears the unit reference and resets the previously selected unit
    void Deselect_Unit()
    {
        if(unit_object != null)
        {
            unit = null;
            unit_object = null;
        }
        sprite.sprite = blue_sprite;
        Remove_Move();
    }

    //Move a unit to another tile
    void Move_Unit()
    {
        //If clicking the same spot as the unit, simply cancel the move
        if(map.grid[(int)position.x - 1, (int)position.y - 1] == previous_tile) {Deselect_Unit();}
        //Only move the unit if the movement range reaches the current tile
        else if (map.grid[(int)position.x - 1, (int)position.y - 1].Check_Indicator())
        {
            //Do not let the unit stop on an occupied space
            if (map.grid[(int)position.x - 1, (int)position.y - 1].unit == null)
            {
                unit_object.transform.position = transform.position;
                previous_tile.unit = null;
                map.grid[(int)position.x - 1, (int)position.y - 1].unit = unit;
                Deselect_Unit();
            }
        }
    }

    //Show Movement range
    void Show_Move()
    {
        int mov = unit.stats.MOV;
        if(mov > 0)
        {
            //Make sure we don't go out of bounds
            if ((int)position.x > 1)                { Recursive_Move_Spawn((int)position.x - 1, (int)position.y, mov - 1); }
            if ((int)position.x < map.dimensions.x) { Recursive_Move_Spawn((int)position.x + 1, (int)position.y, mov - 1); }
            if ((int)position.y > 1)                { Recursive_Move_Spawn((int)position.x, (int)position.y - 1, mov - 1); }
            if ((int)position.y < map.dimensions.y) { Recursive_Move_Spawn((int)position.x, (int)position.y + 1, mov - 1); }
        }
    }

    //Recursively spawn movement indicators on the map
    void Recursive_Move_Spawn(int x,int y,int mov)
    {
        //Subtract the mov cost of the tile 
        mov -= map.grid[x - 1, y - 1].movement_penalty;
        //If there is an enemy combatant on this tile, it is impassible, so stop the recursion
        if ((map.grid[x - 1, y - 1].unit != null) && map.grid[x - 1, y - 1].unit.Army != unit.Army){}
        else
        {
            if (mov > 0)
            {
                map.grid[x - 1, y - 1].Spawn_Indicator();
            }
            //Keep going if we still have MOV left
            if (mov > 0)
            {
                //Make sure we don't go out of bounds
                if (x > 1) { Recursive_Move_Spawn(x - 1, y, mov - 1); }
                if (x < map.dimensions.x) { Recursive_Move_Spawn(x + 1, y, mov - 1); }
                if (y > 1) { Recursive_Move_Spawn(x, y - 1, mov - 1); }
                if (y < map.dimensions.y) { Recursive_Move_Spawn(x, y + 1, mov - 1); }
            }
        }
    }

    void Remove_Move()
    {
        for(int w = 0;w < map.dimensions.x; w++)
        {
            for(int h = 0;h < map.dimensions.y; h++)
            {
                map.grid[w, h].DeSpawn_Indicator();
            }
        }
       
    }



}

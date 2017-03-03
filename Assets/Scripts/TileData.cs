using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    SpriteRenderer sprite;   //Reference to the sprite renderer
    public Sprite grasssprite;
    public Sprite mountainsprite;
    public UnitData unit;         //The unit currently occupying the tile


    public int movement_penalty;    //The movement cost to move through this tile, ex.) penalty of 1 means the tile costs 1 movement to enter along with the usual movement cost. EG COSTS 2 move to enter
    public int defense_bonus;       //Standing on this tile provides this flat bonus to any defenses in combat
    public int avoid_bonus;         //The amount to affect avoid in combat
    public int heal_amount;         //At the beginning of the player phase, heals unit by this amount


    public Vector2 pos;      //The location of the tile in x and y values

    public GameObject indicator;
    GameObject cur_ind;



    void Start()
    {
        Find_Unit();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        int randtype = Random.Range(0, 100);
        if(randtype < 80) { Set_Type(1); }
        else { Set_Type(2); }
    }




    void Find_Unit()
    {
        GameObject[] unit_list = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0;i < unit_list.Length; i++)
        {
            if(unit_list[i].transform.position == transform.position)
            {
                unit = unit_list[i].GetComponent<UnitData>();
                i = unit_list.Length;
            }
        }
    }

    void Set_Type(int t)
    {
        if(t == 1)
        {
            sprite.sprite = grasssprite;
        }
        else if(t == 2)
        {
            sprite.sprite = mountainsprite;
            movement_penalty = 2;
        }
    }



    public bool Check_Indicator()
    {
        if(cur_ind == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Spawn_Indicator()
    {
        if(cur_ind == null && unit == null)
        {
            cur_ind = Instantiate(indicator);
            cur_ind.transform.position = transform.position;
        }
    }

    public void DeSpawn_Indicator()
    {
        if(cur_ind != null)
        {
            Destroy(cur_ind);
            cur_ind = null;
        }
    }

}

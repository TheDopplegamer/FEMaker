using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour {

    [System.Serializable]
    //Unit's basic combat stats
    public class Stats
    {
        public int MHP;             //Max Health Points
        public int HP;              //Current Health Points
        public int STR;             //Strength, power of physical attacks
        public int MAG;             //Magic, power of magical attacks
        public int SKL;             //Skill, affects hit rate, crit rate, and trigger rates
        public int DEF;             //Defense, reduces power of enemy physical attacks
        public int RES;             //Resistance, reduces power of enemy magical attacks
        public int LCK;             //Luck, affects hit rate, crit rate, and trigger rates (less effective then skill)
        public int MOV;             //Movement, the total amount of spaces a unit can move per turn
    }

    //Unit's stat growth rates
    public class Growths
    {
        float HP;
        float STR;
        float MAG;
        float SKL;
        float DEF;
        float RES;
        float LCK;
    }

    //Name, Class, and allegiance of the unit
    public string Name;
    public string Class;
    public string Army;

    //Unit's current overall level and XP values
    public int Level;
    public int XP;

    public Stats stats;
    public Growths growths;







}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    //private string sceneName;
    //private int health;
    //private bool[] abilities;
    //private bool[] ingredients;
    //private bool[] healthUpgrades;
    public float[] position;
    public int facingDirection;

    public PlayerSaveData(Player player, Vector3 savePosition)
    {
        //this.health = health;
        //this.abilities = abilities;
        //this.ingredients = ingredients;
        //this.healthUpgrades = healthUpgrades;
        //this.position = position;
        this.position = new float[] { savePosition.x, savePosition.y, savePosition.z };
        this.facingDirection = player.FacingDirection;
    }
}

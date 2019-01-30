using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class to describe individual items within the game world.
 * 
 * Author: Joshua Pritchard
 * Version: 07.01.2019
 */
 
public class Item
{


    private string itemName;
    private int turnsLeft;
    private bool beingUsed;

    public Item()
    {
        this.itemName = "";
        this.turnsLeft = 0;
        this.beingUsed = false;
    }

    public Item(string name, int turns, bool beingUsed)
    {
        this.itemName = name;
        this.turnsLeft = turns;
        this.beingUsed = beingUsed;
    }

    /**
     * Return the name of the item
     */ 
    public string getName()
    {
        return this.itemName;
    }

    /**
     * Return the number of turns left of light in this object.
     */ 
    public int getTurnsLeft()
    {
        return this.turnsLeft;
    }

    /**
     * Decreases the number of turns left of light by one
     */ 
    public void decreaseByOne()
    {
        this.turnsLeft--;
    }

    /**
     * Used to tell this object it is being used.
     */ 
    public void turnOn()
    {
        this.beingUsed = true;

        GameStateController.Instance.createLight(this.itemName);
        
    }

    /**
     * Used to tell this object it is not being used
     */ 
    public void turnOff()
    {
        this.beingUsed = false;

        
    }

    /**
     * Sets the number of turns of light left in the object to PARAM: turns
     */ 
    public void setTurnsLeft(int turns)
    {
        this.turnsLeft = turns;
    }

    /**
     * Sets the name of the item to name
     * 
     * PARAM name: the string to set the name of the item to.
     */ 
    public void setName(string name)
    {
        this.itemName = name;
    }

    /**
     * Get whether or not this item is turned on.
     * 
     * RETURN: bool, true = item is on, false = item is off.
     */ 
    public bool IsOn()
    {
        return this.beingUsed;
    }


}

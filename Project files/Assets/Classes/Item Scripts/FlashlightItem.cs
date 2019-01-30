using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightItem : Item {

    /**
     * Create a flashlight.
     */ 
    public FlashlightItem()
    {
        this.setName("Flashlight");
        this.setTurnsLeft(6);
        this.turnOff();
    }
}

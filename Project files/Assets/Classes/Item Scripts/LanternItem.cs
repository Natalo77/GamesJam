using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternItem : Item {

    /**
     * Create a lantern
     */ 
    public LanternItem()
    {
        this.setName("Lantern");
        this.setTurnsLeft(8);
        this.turnOff();
    }

}

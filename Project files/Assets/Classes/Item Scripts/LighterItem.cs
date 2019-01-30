using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterItem : Item {

    public LighterItem()
    {
        this.setName("Lighter");
        this.setTurnsLeft(0);
        this.turnOff();
    }

}

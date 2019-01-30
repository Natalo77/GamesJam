using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchItem : Item {

    /**
     * Create a torch
     */ 
    public TorchItem()
    {
        this.setName("Torch");
        this.setTurnsLeft(4);
        this.turnOff();
    }


}

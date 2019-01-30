using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlitTorchItem : Item {

    /**
     * Create an Unlit torch
     */
    public UnlitTorchItem()
    {
        this.setName("Unlit Torch");
        this.setTurnsLeft(0);
        this.turnOff();
    }
}

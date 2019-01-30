using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabWrapper : MonoBehaviour {

    public Item item;
    public bool isTorch;
    public bool isFlashlight;
    public bool isLantern;
    public bool isUnlitTorch;

	// Use this for initialization
	void Start () {
		if(isTorch)
        {
            item = new TorchItem();
        }

        if(isFlashlight)
        {
            item = new FlashlightItem();
        }

        if(isLantern)
        {
            item = new LanternItem();
        }

        if(isUnlitTorch)
        {
            item = new UnlitTorchItem();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Item getItem()
    {
        return this.item;
    }

    public void setItem(Item item)
    {
        this.item = item;
    }
}

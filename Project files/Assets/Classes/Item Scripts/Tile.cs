using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool lit;

    public bool alwaysLit;

    public bool alwaysUnlit;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Used to set this tile as unlit, unless the tile is set as an always lit tile.
     */ 
    public void unlight()
    {
        if(!alwaysLit)
            this.lit = false;
    }

    /**
     * Used to set this tile as lit.
     */ 
    public void light()
    {
        if(!alwaysUnlit)
            this.lit = true;
    }

    /**
     * Returns whether or not this tile is always lit.
     * 
     * RETURN: a bool representing the alwaysLit status of this tile.
     */ 
    public bool getisAlwaysLit()
    {
        return this.alwaysLit;
    }

}

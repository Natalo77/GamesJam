using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    //Player's Position
    public Vector3 position;


	// Use this for initialization
	void Start () {
        position = gameObject.transform.position;
	}


    /**
     * Gets position of player
     * 
     */
    public Vector2 getCoOrds()
    {
        return new Vector2 (position.x,position.y);
    }

    /**
     * Used to move the player along an axis
     * 
     * PARAM direction: string saying which way to move
     * 
     */
    public void move(InputController.Direction direction)
    {
        if(direction == InputController.Direction.DOWN)
        {
            transform.position = new Vector3(position.x, position.y -1, position.z);
        }

        if (direction == InputController.Direction.UP)
        {
            transform.position = new Vector3(position.x, position.y + 1, position.z);
        }

        if (direction == InputController.Direction.LEFT)
        {
            transform.position = new Vector3(position.x - 1, position.y, position.z);
        }

        if (direction == InputController.Direction.RIGHT)
        {
            transform.position = new Vector3(position.x + 1, position.y, position.z);
        }

        position = gameObject.transform.position;
    }
}

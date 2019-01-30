using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public InputController.Direction directionFaced;
    public Animator animator;
    private SpriteRenderer renderer;
    public RuntimeAnimatorController torch;
    public RuntimeAnimatorController noItem;
    public RuntimeAnimatorController lanternItem;



    // Use this for initialization
    void Start () {
        renderer = gameObject.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        if(GameStateController.Instance.getCurrentItem() == "TorchItem")
        {
            animator.runtimeAnimatorController = torch;
        }

        

        if (GameStateController.Instance.getCurrentItem() == "null")
        {
            animator.runtimeAnimatorController = noItem;
        }


        if (GameStateController.Instance.getCurrentItem() == "LanternItem")
        {
            animator.runtimeAnimatorController = lanternItem;
        }

    }


    public void changeDirection(InputController.Direction direction)
    {
        print("Direction Changed");
        if(direction == InputController.Direction.UP)
        {
            animator.SetInteger("Direction", 0);
        }

        else if (direction == InputController.Direction.DOWN)
        {
            animator.SetInteger("Direction", 1);
        }

        else if (direction == InputController.Direction.LEFT)
        {
            animator.SetInteger("Direction", 2);
            renderer.flipX = true;
        }

        else if (direction == InputController.Direction.RIGHT)
        {
            animator.SetInteger("Direction", 3);
            renderer.flipX = false;
        }
    }
}

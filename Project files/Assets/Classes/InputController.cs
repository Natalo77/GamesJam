using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour {

    /**
     * An enum to be used to specify directions withtin the system.
     */
    public enum Direction { UP, DOWN, LEFT, RIGHT };

    //temp variable for movement script
    public Movement movement;

    //Position wanted
    private Vector2 wantedPosition;

    //Co-ordinates of the player in the form "x,y"
    private string coOrds;

    //Whether or not the player can move.
    private bool canMove;

    //Whether or not the player has a firework they can activate.
    private bool hasFirework;

    //GameHUD
    private HUD gameHUD;

    //Script for animating player
    public PlayerAnimation animator;

    

	// Use this for initialization
	void Start () {
        gameHUD = GameObject.Find("GameStateControllerSingleton").GetComponent<HUD>();
        canMove = true;
	}

    // Update is called once per frame
    void Update ()
    {
        //MP work 07.01

        wantedPosition = movement.getCoOrds();
        if (Input.GetKeyDown(KeyCode.W))
        {
            string x = "" + wantedPosition.x;
            if (x.Length == 1)
            {
                x = "0" + x;
            }
            string y = "" + (wantedPosition.y + 1);
            if (y.Length == 1)
            {
                y = "0" + y;
            }
            coOrds = "" + x + "," + y;
            print(coOrds);
            if (GameStateController.Instance.QueryRequestedTile(coOrds))
            {
                string n = "" + wantedPosition.x;
                if (n.Length == 1)
                {
                    n = "0" + n;
                }
                string m = "" + wantedPosition.y;
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                string pos = "" + n + "," + m;
                directMovement(Direction.UP, pos);
            }

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            string x = "" + wantedPosition.x;
            if (x.Length == 1)
            {
                x = "0" + x;
            }
            string y = "" + (wantedPosition.y - 1);
            if (y.Length == 1)
            {
                y = "0" + y;
            }
            coOrds = "" + x + "," + y;
            print(coOrds);
            if (GameStateController.Instance.QueryRequestedTile(coOrds))
            {
                string n = "" + wantedPosition.x;
                if (n.Length == 1)
                {
                    n = "0" + n;
                }
                string m = "" + wantedPosition.y;
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                string pos = "" + n + "," + m;
                directMovement(Direction.DOWN, pos);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            string x = "" + (wantedPosition.x + 1);
            if (x.Length == 1)
            {
                x = "0" + x;
            }
            string y = "" + (wantedPosition.y);
            if (y.Length == 1)
            {
                y = "0" + y;
            }
            coOrds = "" + x + "," + y;
            print(coOrds);
            if (GameStateController.Instance.QueryRequestedTile(coOrds))
            {
                string n = "" + wantedPosition.x;
                if (n.Length == 1)
                {
                    n = "0" + n;
                }
                string m = "" + wantedPosition.y;
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                string pos = "" + n + "," + m;
                directMovement(Direction.RIGHT, pos);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            string x = "" + (wantedPosition.x - 1);
            if (x.Length == 1)
            {
                x = "0" + x;
            }
            string y = "" + (wantedPosition.y);
            if (y.Length == 1)
            {
                y = "0" + y;
            }
            coOrds = "" + x + "," + y;
            print(coOrds);
            if (GameStateController.Instance.QueryRequestedTile(coOrds))
            {
                string n = "" + wantedPosition.x;
                if (n.Length == 1)
                {
                    n = "0" + n;
                }
                string m = "" + wantedPosition.y;
                if (m.Length == 1)
                {
                    m = "0" + m;
                }
                string pos = "" + n + "," + m;
                directMovement(Direction.LEFT, pos);
            }
        }

        //If user presses F key, activate a flare.
        if (Input.GetKeyDown(KeyCode.F))
        {
            activateFirework();
        }

        //If the user presses R key, refuel a torch.
        if(Input.GetKeyDown(KeyCode.R))
        {
            refuelTorch();
        }

        //If the user presses the E key, pickup an item.
        if(Input.GetKeyDown(KeyCode.E))
        {
            pickup();
        }

        //if the user presses the ESC key, toggles the pause menu.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }

        //if the user presses the V key, drop the current equipped item.
        if(Input.GetKeyDown(KeyCode.V))
        {
            drop();
        }

        //If the user presses the 'M' key, use a match.
        if(Input.GetKeyDown(KeyCode.M))
        {
            //todo: implement actual direction.
            throwMatch(Direction.UP);
        }

        //If the yser presses the 'L' key, use the moonlight power.
        if(Input.GetKeyDown(KeyCode.P))
        {
            activateMoonlight();
        }

        //If the user presses the Q key, swap the item in the inventory with the item equipped.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            swap();
        }

        //if the user presses the left key, rotate the flashlight left.
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateFlashlight(PlayerLighting.Rotation.LEFT);
        }

        //if the user presses the right key, rotate the flashlight left.
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateFlashlight(PlayerLighting.Rotation.RIGHT);
        }
	}

    /**
     * Used to prescribe movement in a specific direction.
     * 
     * PARAM direction: An enum specifying the direction in which to move.
     * PARAM from: a string representing the co-ords the player is moving from.
     */ 
    private void directMovement(Direction direction, string from)
    {
        if (canMove)
        {
            movement.move(direction);
            animator.changeDirection(direction);
            GameStateController.Instance.reactNewTile(from, true);
        }
    }


    /**
     * Used to activate the firework powerup.
     */ 
    private void activateFirework()
    {
        if (GameStateController.Instance.getHasFirework())
        {
            GameStateController.Instance.player.GetComponent<PlayerLighting>().activateFirework();
            GameStateController.Instance.setUsedFirework();
        }
    }

    private void activateFlare()
    {
        gameHUD.removeFlare();
        GameStateController.Instance.setUsedFlare();
    }

    /**
     * Used to activate the Moonlight powerup.
     */ 
    private void activateMoonlight()
    {
        if(GameStateController.Instance.getHasMoonlight())
        {
            //activate moonlight.
            gameHUD.removeMoonlight();
            Debug.Log("Moonlight activated");
            GameStateController.Instance.setUsedMoonlight();
        }
    }

    /**
     * Used to throw a match powerup.
     * 
     * PARAM direction: The direction to throw a match in.
     */ 
    private void throwMatch(Direction direction)
    {
        if(GameStateController.Instance.canThrowMatch())
        {
            //throw match
            gameHUD.removeMatch();
            GameStateController.Instance.matchUsed();
            Debug.Log("Match Thrown");
        }
    }

    /**
     * Used to pickup the current item on the ground.
     */ 
    private void pickup()
    {
        GameStateController.Instance.pickup();
    }

    /**
     * Used to drop the item in the current inventory on to the ground
     */ 
    private void drop()
    {
        GameStateController.Instance.dropHeldItem();
    }

    /**
     * Used to process the refuel torch action.
     */ 
    public void refuelTorch()
    {
        GameStateController.Instance.refuelTorch();
    }

    /**		
	* Used to pause the game		
	*/		
    public void pause()
    {		
	    gameHUD.pause();		
	    canMove = !canMove;			
    }		
	
    /**
     * Used to trigger a victory on the current map.
     */ 
    public void victory()
    {		
	    canMove = false;		
	    GameStateController.Instance.player.GetComponent<PlayerLighting>().activateFirework();		
	    gameHUD.victory();		
    }

    /**
     * Used to trigger an item swap on the current player.
     */ 
    public void swap()
    {
        GameStateController.Instance.swap();
    }

    public void rotateFlashlight(PlayerLighting.Rotation direction)
    {
        GameStateController.Instance.rotateFlashlight(direction);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : Singleton<GameStateController> {

    //A reference to the player.
    public GameObject player;

    //Storage for determining the direction the torch is pointing in.
    public InputController.Direction flashlightDirection;

    //Storage for the passable&Overlay tiles on the current map.
    private ArrayList tiles;

    //The item currently being used by the player.
    private Item itemCurrentlyUsing;
    
    //The item being reserved in the player's inventory.
    private Item iteminInventory;

    //Storage for whether the previous tile the player was standing on was always lit.
    private bool previousMoveWasOnAlwaysLitTile;

    //Whether or not the player has a firework powerup they can use.
    private bool hasFirework;

    //Whether or not the player has a moonlight powerup they can use.
    private bool hasMoonlight;

    //Whether or not the player has a flare powerup they can use.
    private bool hasFlare;

    //How many matches the player has already used.
    private int matchesUsed;

    //The number of matches powerups available to the player.
    private const int MAX_NUMBER_MATCHES = 3;

    //Level Unlock booleans		
	public bool level1;		
	public bool level2;		
	public bool level3;		
			
	//GameHUD ref		
	private HUD gameHUD;		
			
	//InputController ref		
	private InputController inputController;

    //References to each of the Item prefabs.
    public GameObject torch;
    public GameObject unlitTorch;
    public GameObject flashlight;
    public GameObject lantern;

    //Used when dropping an item by the ReactNewTile script.
    private Item droppedItem;

    //Instance storage for the current level.
    public int level;

    /**
     * If any levels need to be ran from their own scene, uncomment this function.
     */
    public void Start()
    {
        /*
        ///Find a reference to the player if it does not already exist.
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        if (player != null)
        {
            //placeholder - remove when UI implemented.
            player.transform.position = new Vector3(2, 0, 0);
            player.GetComponent<Movement>().position = player.transform.position;

            //Set the currently used item to a torch.
            Item torch = new TorchItem();
            torch.setTurnsLeft(5);
            torch.turnOn();
            itemCurrentlyUsing = torch;

            //Remove any existing light on the player.
            player.GetComponent<PlayerLighting>().removeLight();

            //Activate the lighting of a torch on the player.
            player.GetComponent<PlayerLighting>().addTorchLight();

            //Clear the inventory.
            iteminInventory = null;


            tiles = new ArrayList();
            tiles.Clear();
            //Fill the tiles storage with non light source overlay tiles and passable tiles.
            foreach (Transform trans in GameObject.Find("Tiles/Overlay Tiles/NonLightSources").GetComponentsInChildren<Transform>())
            {
                tiles.Add(trans.gameObject);
            }
            foreach (Transform trans in GameObject.Find("Tiles/Base Tiles/Tiles").GetComponentsInChildren<Transform>())
            {
                tiles.Add(trans.gameObject);
            }*/


            //MP stuf: pleas keep in start (:		
            //gameHUD = this.GetComponent<HUD>();
            //inputController = this.GetComponent<InputController>();

            /*
            //Initialize the previousMoveAlwaysLit storage.
            previousMoveWasOnAlwaysLitTile = false;

            //Run the reaction to a new tile so that tiles are classified as lit/unlit.
            reactNewTile("" + player.transform.position.x + "," + player.transform.position.y);
            
        }*/


    }

    /**
     * Used to obtain whether or not the player has a firework they can use.
     * 
     * RETURN: bool representing whether the player has a firework powerup or not.
     */
    public bool getHasFirework()
    {
        return Instance.hasFirework;
    }


    public void setUsedFirework()
	{		
	    hasFirework = false;		
	}

    /**
     * Used to obtain whether or not the player has a moonlight they can use.
     * 
     * RETURN: bool representing whether the player has a moonlight powerup or not.
     */
    public bool getHasMoonlight()
    {
        return Instance.hasMoonlight;
    }

    public void setUsedMoonlight()
	{		
	    hasMoonlight = false;		
	}		
			
	public bool getHasFlare()
	{		
	    return Instance.hasFlare;		
	}		
			
	public void setUsedFlare()
	{		
	    hasFlare = false;		
	}

    /**
     * Used to obtain whether or not the player can throw a match.
     * 
     * RETURN: A bool representing if the number of matches used is less than the maximum number of matches.
     */ 
    public bool canThrowMatch()
    {
        return Instance.matchesUsed < MAX_NUMBER_MATCHES;
    }

    public void matchUsed()
	{		
	    if (matchesUsed<MAX_NUMBER_MATCHES)		
	    {		
	        matchesUsed++;		
	    }		
	    print(matchesUsed);		
	}

    /**
     * Used to pickup an item on the ground
     * 
     */
    public void pickup()
    {
        //check for an item on the ground.
        //. get the current player position.
        //. find a reference to the tile the player is on.
        //. If there are any children transforms then there is an item
        //if there is.
        //. If there is an item already in inventory.
        //.. Drop it.
        //. Get the item prefab wrapper component.
        //. Get the item.
        //. Copy this to the inventory storage.

        //Get the current player position.
        string n = "" + player.transform.position.x;
        if (n.Length == 1)
        {
            n = "0" + n;
        }
        string m = "" + player.transform.position.y;
        if (m.Length == 1)
        {
            m = "0" + m;
        }
        string playerPos = "" + n + "," + m;

        //Find a reference to the tile the player is on.
        GameObject currentTile = null;
        foreach(GameObject obj in tiles)
        {
            if(obj.name.Equals(playerPos))
            {
                currentTile = obj;
                break;
            }
        }

        //If there are any children transforms.
        Transform item = null;
        if (currentTile.transform.childCount > 0)
        {
           item = currentTile.transform.GetChild(0).GetComponentInChildren<Transform>();
        }

        if(item != null)
        {
            //If there is an item already in the inventory.
            if(iteminInventory != null)
            {
                //Drop it.
                dropInvItem(currentTile);
            }

            //Get the item from the item prefab wrapper component.
            iteminInventory = item.GetComponentInChildren<ItemPrefabWrapper>().getItem();

            //ensure the item in the inventory is off.
            iteminInventory.turnOff();

            //Delete the Gameobject from the map.
            GameObject.Destroy(item.gameObject);
        }


        Debug.Log("GameStateController Line 205: Inv after pickup");
        if (itemCurrentlyUsing != null)
        {
            Debug.Log(itemCurrentlyUsing.getName());
        }
        else
        {
            Debug.Log("Null");
        }
        if (iteminInventory != null)
        {
            Debug.Log(iteminInventory.getName());
        }
        else
        {
            Debug.Log("Null");
        }
    }

    /**
     * Used to drop the currently reserved item on to the current tile
     */
    public void dropInvItem(GameObject currentTile)
    {
        //Create an item gameobject based on the current inventory item.
        //. Instantiate a gameobject chosen by the name of the current inventory item.
        //Set its properties correctly.
        //. Get the game object's item object and change it's properties based on the current inventory item.
        //Make it a child of the current tile.
        //. parent setting.
        //Delete the current inv object.

        //Instantiate an Item and gameobject chosen by the name of the current inventory item.
        GameObject obj = null;
        Item item = null;
        {
            if (iteminInventory.getName().Equals("Torch"))
            {
                item = new TorchItem();
                obj = Instantiate(torch, currentTile.transform);
            }
            if (iteminInventory.getName().Equals("Unlit Torch"))
            {
                item = new UnlitTorchItem();
                obj = Instantiate(unlitTorch, currentTile.transform);
            }
            if (iteminInventory.getName().Equals("Flashlight"))
            {
                item = new FlashlightItem();
                obj = Instantiate(flashlight, currentTile.transform);
            }
            if (iteminInventory.getName().Equals("Lantern"))
            {
                item = new LanternItem();
                obj = Instantiate(lantern, currentTile.transform);
            }
        }

        //Set it's properties correctly.
        item.setTurnsLeft(iteminInventory.getTurnsLeft());
        item.turnOff();

        //Make it a child of the current tile.
        obj.GetComponent<ItemPrefabWrapper>().setItem(item);
        obj.transform.position = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, 0);

        //Delete the current inv object.
        iteminInventory = null;

        //update the inventory UI.
        updateInventoryUI();
        

    }

    public void dropHeldItem()
    {
        //Find the current player position.

        //Create an item gameobject based on the current inventory item.
        //. Instantiate a gameobject chosen by the name of the current equipped item.
        //Set its properties correctly.
        //. Get the game object's item object and change it's properties based on the current equipped item.
        //Make it a child of the current tile.
        //. parent setting.
        //Delete the current equipped object.

        if (itemCurrentlyUsing != null)
        {
            //Find the current player position.
            string x = "" + player.transform.position.x;
            if (x.Length == 1)
            {
                x = "0" + x;
            }
            string y = "" + player.transform.position.y;
            if (y.Length == 1)
            {
                y = "0" + y;
            }
            string currentPos = "" + x + "," + y;
            GameObject currentTile = null;
            foreach (GameObject tile in tiles)
            {
                if (tile.name.Equals(currentPos))
                {
                    currentTile = tile;
                    break;
                }
            }

            GameObject obj = null;
            //Instantiate an item and gameobject chosen by the name of the current inventory item.
            Item item = null;
            {
                if (itemCurrentlyUsing.getName().Equals("Torch"))
                {
                    item = new TorchItem();
                    obj = Instantiate(torch, currentTile.transform);
                }
                if (itemCurrentlyUsing.getName().Equals("Unlit Torch"))
                {
                    item = new UnlitTorchItem();
                    obj = Instantiate(unlitTorch, currentTile.transform);
                }
                if (itemCurrentlyUsing.getName().Equals("Flashlight"))
                {
                    item = new FlashlightItem();
                    obj = Instantiate(flashlight, currentTile.transform);
                }
                if (itemCurrentlyUsing.getName().Equals("Lantern"))
                {
                    item = new LanternItem();
                    obj = Instantiate(lantern, currentTile.transform);
                }
            }

            //Set it's properties correctly.
            item.setTurnsLeft(itemCurrentlyUsing.getTurnsLeft());
            item.turnOff();

            //Make it a child of the current tile.
            obj.GetComponent<ItemPrefabWrapper>().setItem(item);
            obj.transform.position = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, 0);

            //Record that an item was dropped
            droppedItem = itemCurrentlyUsing;

            //Delete the current equipped object.
            itemCurrentlyUsing = null;

            //Remove the light from the player.
            player.GetComponent<PlayerLighting>().removeLight();

            Debug.Log("GameStateController Line 330: Inv after drop.");
            if (itemCurrentlyUsing != null)
            {
                Debug.Log(itemCurrentlyUsing.getName());
            }
            else
            {
                Debug.Log("Null");
            }
            if (iteminInventory != null)
            {
                Debug.Log(iteminInventory.getName());
            }
            else
            {
                Debug.Log("Null");
            }


            //React to a new tile, false so that the previous item is the item dropped.
            reactNewTile(currentPos, false);
        }

        //Update the inventory UI.
        updateInventoryUI();
    }

    /**
     * Swaps the currently held and item in inventory.
     */ 
    public void swap()
    {
        //is the inventory null?
        //is the equipped null?

        //if inventory is null, then temp is null, else inventory.
        //if equipped is null, inventory is null, else equipped.
        //inventory is temp.

        //If inventory is a lit torch.
        //change to an unlit torch.
        //call hud popup for unlit torch to inventory warning.


        //Is the inventory null?
        bool inventoryNull = (iteminInventory == null);
        //is the equipped null?
        bool equippedNull = (itemCurrentlyUsing == null);

        Item temp = null;
        //if the inventory is not null
        if(!inventoryNull)
        {
            //temp is inventory.
            temp = iteminInventory;
        }
        //if equipped is null
        if(equippedNull)
        {
            //inventory is null
            iteminInventory = null;
        }
        else
        {
            //else equipped.
            iteminInventory = itemCurrentlyUsing;
        }

        //equipped is temp.
        itemCurrentlyUsing = temp;

        //If inventory is a lit torch
        if (iteminInventory != null && iteminInventory.getName().Equals("Torch"))
        {
            //Change this to an unlit torch with the same number of moves.
            //temp = item in inventory.
            //item in inventory is new Unlit torch.
            //item in inventory turns left is temp turns left.

            //temp is item in inventory.
            temp = iteminInventory;

            //item in inventory is new unlit torch.
            iteminInventory = new UnlitTorchItem();

            //item in inventory turns left is temp turns left.
            iteminInventory.setTurnsLeft(temp.getTurnsLeft());

            //Call the HUD popup for unlit torch put into inventory warning.

        }

        player.GetComponent<PlayerLighting>().removeLight();

        //Set the on and off states of the new inventory items.
        if (itemCurrentlyUsing != null)
        {
            itemCurrentlyUsing.turnOn();
        }

        if(iteminInventory != null)
        {
            iteminInventory.turnOff();
        }

        Debug.Log("GameStateController Line 437: Inventory after swap");
        if (itemCurrentlyUsing != null)
        {
            Debug.Log(itemCurrentlyUsing.getName());
        }
        else
        {
            Debug.Log("Null");
        }
        if (iteminInventory != null)
        {
            Debug.Log(iteminInventory.getName());
        }
        else
        {
            Debug.Log("Null");
        }

        //Update the inventory Ui.
        updateInventoryUI();

        

    }

    /**
     * Ran to process reactions to the new tile the player has moved on to
     * 
     * PARAM previous: A string representing the co-ords of the tile the player was last on.
     */ 
    public void reactNewTile(string previous, bool currentlyHoldingItem)
    {
        //Record the item that was being used before this move was made.
        Item previousItem = null;
        if (currentlyHoldingItem)
        {
            previousItem = itemCurrentlyUsing;
        }
        else
        {
            previousItem = droppedItem;
        }
        

        //Storage for if the player is standing on an always lit tile.
        bool adjacentToBonfire = standingOnAlwaysLit();

        //decrease the turns of the item, if it is activated and the player was not standing next to a bonfire.
        if (itemCurrentlyUsing != null && !previousMoveWasOnAlwaysLitTile)
        {
            if (itemCurrentlyUsing.IsOn())
                itemCurrentlyUsing.decreaseByOne();
        }

        //if the item has ran out
        if (itemCurrentlyUsing != null)
        {
            if (itemCurrentlyUsing.getTurnsLeft() == 0)
            {
                //delete the current item, if it's not a torch (these can be refuelled).
                if (!(itemCurrentlyUsing.getName().Equals("Torch") || itemCurrentlyUsing.getName().Equals("Unlit Torch")))
                {
                    itemCurrentlyUsing = null;
                }
                else
                {
                    itemCurrentlyUsing.setName("Unlit Torch");
                }

                //Remove the light from the player entity regardless (The torch will have ran out).
                player.GetComponent<PlayerLighting>().removeLight();

                
            }
        }

        //if there is an item in the inventory, and no item being used, then autoactivate this.
        if (iteminInventory != null && itemCurrentlyUsing == null)
        {
            //autoactivate the item in the inventory.
            autoActivate();
        }

        //Record the item that is now being used after the move was made.
        Item newItem = itemCurrentlyUsing;

        //Update the lighting system using this information
        updateLightingSystem(previousItem, newItem, previous);
        

        //do concealment checks.

        //Update the previous tile's always lit record.
        previousMoveWasOnAlwaysLitTile = adjacentToBonfire;

        //query current tile and deal with this.
        queryCurrentTile();

        
        
    }

    public void updateLightingSystem(Item previousItem, Item newItem, string previous)
    {
        //Storage for the tiles to be modified.
        LinkedList<string> tilesCoOrds = new LinkedList<string>();
        //Storage for the tiles in the map.

        //Update the flashlight direction.
        flashlightDirection = player.GetComponent<PlayerLighting>().getFlashLightDirection();

        if (previousItem != null)
        {
            //Get previous tiles based on previous item and previous position.
            {
                if (previousItem.getName().Contains("Torch"))
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            int a = int.Parse(previous.Substring(0, 2)) + x;
                            int b = int.Parse(previous.Substring(3, 2)) + y;
                            string n = "" + a;
                            if (n.Length == 1)
                            {
                                n = "0" + n;
                            }
                            string m = "" + b;
                            if (m.Length == 1)
                            {
                                m = "0" + m;
                            }
                            tilesCoOrds.AddLast("" + n + "," + m);
                        }
                    }
                }
                if (previousItem.getName().Contains("Lantern"))
                {
                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            int a = int.Parse(previous.Substring(0, 2)) + x;
                            int b = int.Parse(previous.Substring(3, 2)) + y;
                            string n = "" + a;
                            if (n.Length == 1)
                            {
                                n = "0" + n;
                            }
                            string m = "" + b;
                            if (m.Length == 1)
                            {
                                m = "0" + m;
                            }
                            tilesCoOrds.AddLast("" + n + "," + m);
                        }
                    }
                }
                if (previousItem.getName().Contains("Flashlight"))
                {
                    if (flashlightDirection == InputController.Direction.UP)
                    {
                        for (int y = 0; y <= 3; y++)
                        {
                            int a = int.Parse(previous.Substring(0, 2));
                            int b = int.Parse(previous.Substring(3, 2)) + y;
                            string n = "" + a;
                            if (n.Length == 1)
                            {
                                n = "0" + n;
                            }
                            string m = "" + b;
                            if (m.Length == 1)
                            {
                                m = "0" + m;
                            }
                            tilesCoOrds.AddLast("" + n + "," + m);
                        }
                    }
                    if (flashlightDirection == InputController.Direction.DOWN)
                    {
                        for (int y = 0; y >= -3; y--)
                        {
                            int a = int.Parse(previous.Substring(0, 2));
                            int b = int.Parse(previous.Substring(3, 2)) + y;
                            string n = "" + a;
                            if (n.Length == 1)
                            {
                                n = "0" + n;
                            }
                            string m = "" + b;
                            if (m.Length == 1)
                            {
                                m = "0" + m;
                            }
                            tilesCoOrds.AddLast("" + n + "," + m);
                        }

                    }
                    if (flashlightDirection == InputController.Direction.LEFT)
                    {
                        for (int x = 0; x >= -3; x--)
                        {
                            int a = int.Parse(previous.Substring(0, 2)) + x;
                            int b = int.Parse(previous.Substring(3, 2));
                            string n = "" + a;
                            if (n.Length == 1)
                            {
                                n = "0" + n;
                            }
                            string m = "" + b;
                            if (m.Length == 1)
                            {
                                m = "0" + m;
                            }
                            tilesCoOrds.AddLast("" + n + "," + m);
                        }
                    }
                    if (flashlightDirection == InputController.Direction.RIGHT)
                    {
                        for (int x = 0; x <= 3; x++)
                        {
                            int a = int.Parse(previous.Substring(0, 2)) + x;
                            int b = int.Parse(previous.Substring(3, 2));
                            string n = "" + a;
                            if (n.Length == 1)
                            {
                                n = "0" + n;
                            }
                            string m = "" + b;
                            if (m.Length == 1)
                            {
                                m = "0" + m;
                            }
                            tilesCoOrds.AddLast("" + n + "," + m);
                        }

                    }
                }
            }
        }

        //Unlight previous tiles
        foreach (string str in tilesCoOrds)
        {
            //For each tile in the passable/overlay tiles
            foreach (GameObject obj in tiles)
            {
                //if the name contains the co-ords for the tile to be lit, light it.
                if (obj.name.Contains(str))
                    obj.GetComponent<Tile>().unlight();
            }
        }

        //Get new tiles based on new item and new position.
        tilesCoOrds.Clear();
        if (newItem != null)
        {
            if (newItem.getName().Equals("Torch"))
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        string n = "" + (int)(player.transform.position.x + x);
                        if (n.Length == 1)
                        {
                            n = "0" + n;
                        }
                        string m = "" + (int)(player.transform.position.y + y);
                        if (m.Length == 1)
                        {
                            m = "0" + m;
                        }
                        tilesCoOrds.AddLast("" + n + "," + m);
                    }
                }
            }
            if (newItem.getName().Contains("Lantern"))
            {
                for (int x = -2; x <= 2; x++)
                {
                    for (int y = -2; y <= 2; y++)
                    {
                        string n = "" + (int)(player.transform.position.x + x);
                        if (n.Length == 1)
                        {
                            n = "0" + n;
                        }
                        string m = "" + (int)(player.transform.position.y + y);
                        if (m.Length == 1)
                        {
                            m = "0" + m;
                        }
                        tilesCoOrds.AddLast("" + n + "," + m);
                    }
                }
            }
            if (newItem.getName().Contains("Flashlight"))
            {
                if (flashlightDirection == InputController.Direction.UP)
                {
                    for (int y = 0; y <= 3; y++)
                    {
                        string n = "" + player.transform.position.x;
                        if (n.Length == 1)
                        {
                            n = "0" + n;
                        }
                        string m = "" + (int)(player.transform.position.y + y);
                        if (m.Length == 1)
                        {
                            m = "0" + m;
                        }
                        tilesCoOrds.AddLast("" + n + "," + m);
                    }
                }
                if (flashlightDirection == InputController.Direction.DOWN)
                {
                    for (int y = 0; y >= -3; y--)
                    {
                        string n = "" + player.transform.position.x;
                        if (n.Length == 1)
                        {
                            n = "0" + n;
                        }
                        string m = "" + (int)(player.transform.position.y + y);
                        if (m.Length == 1)
                        {
                            m = "0" + m;
                        }
                        tilesCoOrds.AddLast("" + n + "," + m);
                    }

                }
                if (flashlightDirection == InputController.Direction.LEFT)
                {
                    for (int x = 0; x >= -3; x--)
                    {
                        string n = "" + (int)(player.transform.position.x + x);
                        if (n.Length == 1)
                        {
                            n = "0" + n;
                        }
                        string m = "" + player.transform.position.y;
                        if (m.Length == 1)
                        {
                            m = "0" + m;
                        }
                        tilesCoOrds.AddLast("" + n + "," + m);
                    }
                }
                if (flashlightDirection == InputController.Direction.RIGHT)
                {
                    for (int x = 0; x <= 3; x++)
                    {
                        string n = "" + (int)(player.transform.position.x + x);
                        if (n.Length == 1)
                        {
                            n = "0" + n;
                        }
                        string m = "" + player.transform.position.y;
                        if (m.Length == 1)
                        {
                            m = "0" + m;
                        }
                        tilesCoOrds.AddLast("" + n + "," + m);
                    }

                }
            }
        }

        //Light new tiles.
        foreach (string str in tilesCoOrds)
        {
            //For each tile in the passable/overlay tiles
            foreach (GameObject obj in tiles)
            {
                //if the name contains the co-ords for the tile to be lit, light it.
                if (obj.name.Contains(str))
                    obj.GetComponent<Tile>().light();
            }
        }
    }

    /**
     * Returns a bool representing whether or not the player is currently on an always lit tile.
     * 
     * RETURN: bool representing if the player is currently standing on a tile flagged as always lit.
     */ 
    private bool standingOnAlwaysLit()
    {
        //A string representing the player's current position in tile co-ords.
        string n = "" + player.transform.position.x;
        if (n.Length == 1)
        {
            n = "0" + n;
        }
        string m = "" + player.transform.position.y;
        if (m.Length == 1)
        {
            m = "0" + m;
        }
        string currentPos = "" + n + "," + m;

        //check the player position against each object in the passable/overlay tiles
        foreach (GameObject obj in tiles)
        {
            //if the player is standing on an always lit tiles
            if (obj.name.Contains(currentPos) && obj.GetComponent<Tile>().getisAlwaysLit())
            {
                //return true.
                return true;
            }
        }
        
        //return false if the player is not standing on an always lit tile.
        return false;
    }

    /*
     * Used to query the status of the current tile the player is standing on
     */ 
    public void queryCurrentTile()
    {
        //Storage for the current tile that the player is on.
        GameObject currentTile = null;

        //Storage for a string representing the current tile position of the player.
        string n = "" + player.transform.position.x;
        if (n.Length == 1)
        {
            n = "0" + n;
        }
        string m = "" + player.transform.position.y;
        if (m.Length == 1)
        {
            m = "0" + m;
        }
        string currentPos = "" + n + "," + m;

        //Find a reference to the tile the player is standing on via searching the passable and overlay tiles.
        //INEFFICIENT - REWORK TO USE SECONDARY DATA STRUCTURE THAT DOESN'T CHECK OVERLAY TILES.
        foreach(GameObject obj in tiles)
        {
            if(obj.name.Contains(currentPos))
            {
                currentTile = obj;
                break;
            }
        }

        //If the current tile is not lit then process the player's death.
        if (currentTile != null)
        {
            if (!currentTile.GetComponent<Tile>().lit)
            {
                //Process death.
                Debug.Log("Dead");

                //reload the level.
                reloadLevel();
            }

            //if the current tile is the goal tile
            //if it is, brings up the victory screen, disables movement, and sets off fireworkds (:
            if(currentTile.tag == "GoalTile")
            {
                inputController.victory();
                //Go back to menu.
            }
        }
    }

    public void updateInventoryUI()
    {
        if (itemCurrentlyUsing != null)
        {
            if (itemCurrentlyUsing.getName().Equals("Torch"))
            {
                GameStateController.Instance.GetComponent<HUD>().setEquipItem(HUD.Item.TORCH);
            }
            else if (itemCurrentlyUsing.getName().Equals("Unlit Torch"))
            {
                GameStateController.Instance.GetComponent<HUD>().setEquipItem(HUD.Item.UNLIT_TORCH);
            }
            else if (itemCurrentlyUsing.getName().Equals("Lantern"))
            {
                GameStateController.Instance.GetComponent<HUD>().setEquipItem(HUD.Item.LANTERN);
            }
            else if (itemCurrentlyUsing.getName().Equals("Flashlight"))
            {
                GameStateController.Instance.GetComponent<HUD>().setEquipItem(HUD.Item.FLASHLIGHT);
            }
        }
        else
        {
            GameStateController.Instance.GetComponent<HUD>().setEquipItem(HUD.Item.NOTHING);
        }

        if(iteminInventory != null)
        {
            if(iteminInventory.getName().Equals("Torch"))
            {
                GameStateController.Instance.GetComponent<HUD>().setInvImage(HUD.Item.TORCH);
            }
            else if(iteminInventory.getName().Equals("Unlit Torch"))
            {
                GameStateController.Instance.GetComponent<HUD>().setInvImage(HUD.Item.UNLIT_TORCH);
            }
            else if(iteminInventory.getName().Equals("Flashlight"))
            {
                GameStateController.Instance.GetComponent<HUD>().setInvImage(HUD.Item.FLASHLIGHT);
            }
            else if (iteminInventory.getName().Equals("Lantern"))
            {
                GameStateController.Instance.GetComponent<HUD>().setInvImage(HUD.Item.LANTERN);
            }
        }
        else
        {
            GameStateController.Instance.GetComponent<HUD>().setInvImage(HUD.Item.NOTHING);
        }

    }

    /**
     * Used to reload the level in case of a death.
     */
    private void reloadLevel()
    {
        SceneManager.LoadScene(GameStateController.Instance.level);

        //DEPRECATED
        //OnLevelWasLoaded(-1);
    }

    /**
     * Used to query the concealing status of the tile below the player.
     * 
     * Returns: a bool representing the concealing status of the tile below the player.
     */ 
    private bool checkIfBelowTileConcealing()
    {
        bool concealing = false;

        return concealing;
    }

    /**
     * Used to 'light' a tile up
     */ 
    public void lightTile()
    {

    }

    /**
     * used to make a tile 'dark'
     */ 
    public void unlightTile()
    {

    }


    
    /**
     * Used to check if the tile attempting to be moved to is passable.
     * 
     * Returns: true if passable, false if impassable.
     */ 
    public bool QueryRequestedTile(string position)
    {
        //JP Work 08.01
        {
            //Search each impassable tile.
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Impassable"))
            {
                //If its name contains the position of the requested tile
                if(obj.name.Contains(position))
                {
                    //return false.
                    return false;
                }
            }

           //if no matching impassable tiles are found then return false.
            return true;
        }
    }

    /**
     * Used to autoactivate the reserved item
     */ 
    public void autoActivate()
    {

    }

    /**
     * Used to activate the reserved item
     */ 
    public void activate()
    {

    }

    /**
     * Will be used when loading levels from a menu in place of the current Start() system.
     * 
     * PARAM level: an int representing the level being loaded.
     */ 
    private void OnLevelWasLoaded(int level)
    {
        //Find a reference to the player if it does not already exist.
        player = GameObject.Find("Player");

        GameObject obj = GameObject.Find("GameStateControllerSingleton");
        if (obj != null)
        {
            gameHUD = obj.GetComponent<HUD>();
        }

        if (player != null)
        {
            //placeholder - remove when UI implemented.
            player.transform.position = new Vector3(2, 0, 0);
            player.GetComponent<Movement>().position = player.transform.position;

            //Remove any existing light on the player.
            player.GetComponent<PlayerLighting>().removeLight();

            //Set the currently used item to a torch.
            Item torch = new TorchItem();
            torch.setTurnsLeft(5);
            torch.turnOn();
            itemCurrentlyUsing = torch;

            //Clear the inventory.
            iteminInventory = null;


            tiles = new ArrayList();
            tiles.Clear();
            //Fill the tiles storage with non light source overlay tiles and passable tiles.
            //foreach (Transform trans in GameObject.Find("Tiles/Overlay Tiles/NonLightSources").GetComponentsInChildren<Transform>())
            //{
            //    if (trans.parent == GameObject.Find("Tiles/Overlay Tiles/NonLightSources").transform)
                //{
                //    tiles.Add(trans.gameObject);
                //}
            //}
            foreach (Transform trans in GameObject.Find("Tiles/Base Tiles/Tiles").GetComponentsInChildren<Transform>())
            {
                if (trans.parent == GameObject.Find("Tiles/Base Tiles/Tiles").transform)
                {
                    tiles.Add(trans.gameObject);
                }
            }



            //MP stuf: pleas keep in start (:		
            gameHUD = GameObject.Find("GameStateControllerSingleton").GetComponent<HUD>();
            inputController = GameObject.Find("GameStateControllerSingleton").GetComponent<InputController>();

            //Sets hasFirework and hasMoonlight to true, player starts with one of each. 		
            hasFirework = true;
            hasMoonlight = true;
            hasFlare = true;

            matchesUsed = 0;

            //Resets the UI
            gameHUD.resetUI();

            //Initialize the previousMoveAlwaysLit storage.
            previousMoveWasOnAlwaysLitTile = false;

            //Run the reaction to a new tile so that tiles are classified as lit/unlit.
            string x = "" + player.transform.position.x;
            if(x.Length == 1)
            {
                x = "0" + x;
            }
            string y = "" + player.transform.position.y;
            if(y.Length == 1)
            {
                y = "0" + y;
            }
            string pos = "" + x + "," + y;
            reactNewTile(pos, true);
        }

        //Set the gamestatecontrollers instance prefab variables.
        GameStateController.Instance.torch = torch;
        GameStateController.Instance.unlitTorch = unlitTorch;
        GameStateController.Instance.flashlight = flashlight;
        GameStateController.Instance.lantern = lantern;

        //Set the gamestatecontrollers instance level variable.
        GameStateController.Instance.level = level;

    }

    /**
     * Called when refuelling a torch next to a bonfire.
     */ 
    public void refuelTorch()
    {
        //if the player is standing next to a bonfire and they are currently carrying a torch.
        if(standingOnAlwaysLit() && (itemCurrentlyUsing.getName().Equals("Torch") || itemCurrentlyUsing.getName().Equals("Unlit Torch")))
        {
            //if the item had no turns left then the light was destroyed so create a new light.
            if (itemCurrentlyUsing.getTurnsLeft() <= 0)
            {
                player.GetComponent<PlayerLighting>().addTorchLight();
            }

            //refuel the torch.
            itemCurrentlyUsing.setTurnsLeft(4);

            //change the name of the torch.
            itemCurrentlyUsing.setName("Torch");

            //ensure the torch is turned on.
            itemCurrentlyUsing.turnOn();

            //change the model used.

        }
        else
        {
            //display an error message.
            Debug.Log("You don't have a torch equipped!");
        }
    }

    public void createLight(string lightName)
    {
        if (player != null)
        {
            if (lightName.Equals("Torch"))
            {
                player.GetComponent<PlayerLighting>().addTorchLight();
            }
            else if (lightName.Equals("Flashlight"))
            {
                player.GetComponent<PlayerLighting>().addFlashLight();
            }
            else if (lightName.Equals("Lantern"))
            {
                player.GetComponent<PlayerLighting>().addLantern();
            }
        }
    }

    public void rotateFlashlight(PlayerLighting.Rotation rotation)
    {
        if(itemCurrentlyUsing.getName().Equals("Flashlight"))
        {
            player.GetComponent<PlayerLighting>().rotateFlashLight(rotation);
            updateLightingSystem(new FlashlightItem(), new FlashlightItem(), getPlayerCurrentPos());
        }
    }

    public string getPlayerCurrentPos()
    {
        string n = "" + player.transform.position.x;
        if (n.Length == 1)
        {
            n = "0" + n;
        }
        string m = "" + player.transform.position.y;
        if (m.Length == 1)
        {
            m = "0" + m;
        }
        return "" + n + "," + m;
    }

    public string getCurrentItem()
    {
        if (itemCurrentlyUsing == null)
        {
            return ("null");
        }
        else
        {
            return itemCurrentlyUsing.ToString();
        }
    }

}

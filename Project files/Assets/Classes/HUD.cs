using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum Item { TORCH, UNLIT_TORCH, FLASHLIGHT, LANTERN, NOTHING };

    //pausemenu
    private GameObject pauseMenu;

    //victorymenu
    private GameObject victoryMenu;

    //Match UI objects
    private GameObject[] matches;

    //Moonlight UI object
    private GameObject moonlight;

    //Flare UI object
    private GameObject flare;

    //References to the inventory image renderers.
    private Image inventoryImage;
    private Image equippedImage;

    //matches used
    private int usedMatches;

    //Storage for the item sprites.
    public Sprite torch;
    public Sprite lantern;
    public Sprite unlitTorch;
    public Sprite flashlight;
    public Sprite nothing;
    // Use this for initialization
    void Start()
    {
        pauseMenu = GameObject.Find("Canvas/PauseMenu");
        pauseMenu.SetActive(false);
        victoryMenu = GameObject.Find("Canvas/VictoryMenu");
        victoryMenu.SetActive(false);
        matches = new GameObject[3];
        matches[2] = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Matches/Match1");
        matches[1] = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Matches/Match2");
        matches[0] = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Matches/Match3");
        moonlight = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Moonlight");
        flare = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Flare");

        inventoryImage = GameObject.Find("Canvas/HUD/Cirlces/Log3/Image_Inv").GetComponent<Image>();
        equippedImage = GameObject.Find("Canvas/HUD/Circles/Log1/Image_Equip").GetComponent<Image>();
    }

    public void setInvImage(Item itemType)
    {
        if(itemType == Item.TORCH)
        {
            inventoryImage.sprite = torch;
        }
        else if(itemType == Item.UNLIT_TORCH)
        {
            inventoryImage.sprite = unlitTorch;
        }
        else if(itemType == Item.LANTERN)
        {
            inventoryImage.sprite = lantern;
        }
        else if(itemType == Item.FLASHLIGHT)
        {
            inventoryImage.sprite = flashlight;
        }
        else if(itemType == Item.NOTHING)
        {
            inventoryImage.sprite = nothing;
        }
    }

    public void setEquipItem(Item itemType)
    {
        if (itemType == Item.TORCH)
        {
            equippedImage.sprite = torch;
        }
        else if (itemType == Item.UNLIT_TORCH)
        {
            equippedImage.sprite = unlitTorch;
        }
        else if (itemType == Item.LANTERN)
        {
            equippedImage.sprite = lantern;
        }
        else if (itemType == Item.FLASHLIGHT)
        {
            equippedImage.sprite = flashlight;
        }
        else if(itemType == Item.NOTHING)
        {
            equippedImage.sprite = nothing;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Used to display the popup describing the current item
     * 
     * PARAM item: The item to display information about.
     */
    public void popup(Item item)
    {

    }

    /**
     * Used to hide the popup describing the current item
     */
    public void hideItemPopup()
    {

    }

    /**
     * Used to display a generic popup
     * 
     * PARAM message: the string message to display.
     */
    public void popup(string message)
    {

    }

    /**
     * Used to hide a generic popup
     */
    public void hideGenericPopup()
    {

    }

    /**
     * Updates the lightCounter canvas with the current Item and the reserved Item
     */
    public void updateCounter(Item currentItem, Item reservedItem)
    {

    }

    /**
     * Decreases the displayed counter for the current item by one.
     */
    public void decreaseCounterByOne()
    {

    }

    public void pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void victory()
    {
        victoryMenu.SetActive(true);
        if ((SceneManager.GetActiveScene().name) == "TutorialLevel")
        {
            GameStateController.Instance.level1 = true;
            print("Level 1 is " + GameStateController.Instance.level1);
        }
        if ((SceneManager.GetActiveScene().name) == "Level1")
        {
            GameStateController.Instance.level2 = true;
        }
        if ((SceneManager.GetActiveScene().name) == "Level2")
        {
            GameStateController.Instance.level3 = true;
        }
    }

    //Reset UI, sets all UI power-ups to be visible
    public void resetUI()
    {
        //matches = new GameObject[3];
        //matches[2] = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Matches/Match1");
        //matches[1] = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Matches/Match2");
        //matches[0] = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Matches/Match3");
        //moonlight = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Moonlight");
        //flare = GameObject.Find("Canvas/HUD/Boxes/ItemBoxes/Flare");
        //matches[0].SetActive(true);
        //matches[1].SetActive(true);
        //matches[2].SetActive(true);
        //moonlight.SetActive(true);
        //flare.SetActive(true);
    }

    //Sets torch objects as inactive
    public void removeMatch()
    {
        matches[usedMatches].SetActive(false);
        usedMatches++;
    }

    //Sets moonlight UI object as inactive
    public void removeMoonlight()
    {
        moonlight.SetActive(false);
    }

    public void removeFlare()
    {
        flare.SetActive(false);
    }
}

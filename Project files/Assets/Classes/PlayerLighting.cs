using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLighting : MonoBehaviour {

    public enum Rotation { LEFT, RIGHT };
    private InputController.Direction flashlightDirection;

    public Light torchPrefab;
    public Light lanternPrefab;
    public Light flashlightPrefab;

    public bool playerHasLight;

    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    public void addTorchLight()
    {
        if (!playerHasLight)
        {
            Light torch = Instantiate(torchPrefab, this.transform);
            torch.intensity = 10;
            playerHasLight = true;
        }
    }

    public void addFlashLight()
    {
        if(!playerHasLight)
        {
            Light flashLight = Instantiate(flashlightPrefab, this.transform);
            flashlightDirection = InputController.Direction.UP;
            playerHasLight = true;
        }
    }

    public void rotateFlashLight(Rotation direction)
    {
        //Calculate the new bearing of the the flashlight.
        {
            if (flashlightDirection == InputController.Direction.UP)
            {
                if (direction == Rotation.RIGHT)
                {
                    flashlightDirection = InputController.Direction.RIGHT;
                }
                else if (direction == Rotation.LEFT)
                {
                    flashlightDirection = InputController.Direction.LEFT;
                }
            }
            else if (flashlightDirection == InputController.Direction.LEFT)
            {
                if (direction == Rotation.RIGHT)
                {
                    flashlightDirection = InputController.Direction.UP;
                }
                else if (direction == Rotation.LEFT)
                {
                    flashlightDirection = InputController.Direction.DOWN;
                }
            }
            else if (flashlightDirection == InputController.Direction.RIGHT)
            {
                if (direction == Rotation.RIGHT)
                {
                    flashlightDirection = InputController.Direction.DOWN;
                }
                else if (direction == Rotation.LEFT)
                {
                    flashlightDirection = InputController.Direction.UP;
                }
            }
            else if (flashlightDirection == InputController.Direction.DOWN)
            {
                if (direction == Rotation.RIGHT)
                {
                    flashlightDirection = InputController.Direction.LEFT;
                }
                else if (direction == Rotation.LEFT)
                {
                    flashlightDirection = InputController.Direction.RIGHT;
                }
            }
        }

        //place the light object in it's correct position and rotation.
        placeFlashLight();
    }

    private void placeFlashLight()
    {
        if(flashlightDirection == InputController.Direction.UP)
        {
            GameObject obj = gameObject.GetComponentInChildren<Light>().gameObject;
            obj.transform.position = new Vector3(GameStateController.Instance.player.transform.position.x, GameStateController.Instance.player.transform.position.y - 1, -1);
            obj.transform.rotation = Quaternion.Euler(-62.0f, 0.0f, -90.0f);
        }
        else if(flashlightDirection == InputController.Direction.LEFT)
        {
            GameObject obj = gameObject.GetComponentInChildren<Light>().gameObject;
            obj.transform.position = new Vector3(GameStateController.Instance.player.transform.position.x + 1, GameStateController.Instance.player.transform.position.y, -1);
            obj.transform.rotation = Quaternion.Euler(0.0f, -62.0f, 0.0f);
        }
        else if(flashlightDirection == InputController.Direction.RIGHT)
        {
            GameObject obj = gameObject.GetComponentInChildren<Light>().gameObject;
            obj.transform.position = new Vector3(GameStateController.Instance.player.transform.position.x - 1, GameStateController.Instance.player.transform.position.y, -1);
            obj.transform.rotation = Quaternion.Euler(0.0f, 62.0f, -180.0f);
        }
        else
        {
            GameObject obj = gameObject.GetComponentInChildren<Light>().gameObject;
            obj.transform.position = new Vector3(GameStateController.Instance.player.transform.position.x, GameStateController.Instance.player.transform.position.y + 1, -1);
            obj.transform.rotation = Quaternion.Euler(62.0f, 0.0f, 270.0f);
        }
    }

    public InputController.Direction getFlashLightDirection()
    {
        return this.flashlightDirection;
    }

    public void addLantern()
    {
        if(!playerHasLight)
        {
            Light lantern = Instantiate(lanternPrefab, this.transform);
            playerHasLight = true;
        }
    }

    public void removeLight()
    {
        if (playerHasLight)
        {
            GameObject obj = gameObject.GetComponentInChildren<Light>().gameObject;
            GameObject.Destroy(obj);
            playerHasLight = false;
        }
    }

    /**
     * Used when activating fireworks to display the effect.
     */
    public void activateFirework()
    {
        //Create a point light 
        //Start a coroutine which deals with the light effects over time.
        //Do this a few times with different colours.

        //Create a temporary colours array to use for randomly determining a colour.
        Color[] colours = new Color[6];
        colours[0] = Color.blue;
        colours[1] = Color.cyan;
        colours[2] = Color.green;
        colours[3] = Color.magenta;
        colours[4] = Color.red;
        colours[5] = Color.yellow;

        //Create an array of lights to use as the fireworks.
        GameObject[] lights = new GameObject[5];
        lights[0] = new GameObject();
        lights[0].AddComponent<Light>();
        lights[1] = new GameObject();
        lights[1].AddComponent<Light>();
        lights[2] = new GameObject();
        lights[2].AddComponent<Light>();
        lights[3] = new GameObject();
        lights[3].AddComponent<Light>();
        lights[4] = new GameObject();
        lights[4].AddComponent<Light>();

        //Debug.Log(lights[0].ToString());

        //Set up each light in the lights array.
        for (int x = 0; x < 5; x++)
        {
            lights[x].GetComponent<Light>().enabled = false;
            lights[x].GetComponent<Light>().type = LightType.Point;
            lights[x].GetComponent<Light>().color = colours[Random.Range(0, 6)];
            lights[x].GetComponent<Light>().intensity = Random.Range(8, 10);
            lights[x].GetComponent<Light>().range = Random.Range(15, 21);
            lights[x].transform.position = new Vector3(Random.Range(0, 8), Random.Range(0, 8), -10);

        }

        //run the firework explosion iterator.
        StartCoroutine(FireworksExplode(lights));
    }
    private IEnumerator FireworksExplode(GameObject[] lights)
    {
        //Run each firework explosion with a delay inbetween each.
        foreach (GameObject light in lights)
        {
            StartCoroutine(fireworkFade(light.GetComponent<Light>()));
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
    private IEnumerator fireworkFade(Light light)
    {
        light.enabled = true;

        for (float f = light.intensity; f >= 0; f -= 0.1f)
        {
            light.intensity = f;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}

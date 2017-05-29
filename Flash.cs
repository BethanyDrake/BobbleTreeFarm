using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Flash : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (flashing)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange>=timeBetweenFlashes)
            {
                flash();
            }

            timeSinceBegun += Time.deltaTime;
            if (timeSinceBegun >= flashtime)
            {
                stopFlashing();
            }
        }
	
	}

    private bool flashing = false;
    public float timeBetweenFlashes;
    private float timeSinceLastChange = 0;
    private float timeSinceBegun = 0;
    private float flashtime = 0;


    private void startFlashing()
    {
        flashing = true;
        flash();
    }

    public void stopFlashing()
    {
        flashing = false;
        if (!GetComponent<Image>().color.Equals(main))
        {
            GetComponent<Image>().color = main;

        }
    }


    public bool isFlashing()
    {
        return flashing;
    }
    public void flashFor(float time)
    {

        if (flashing) stopFlashing();

        flashtime = time;
        timeSinceBegun = 0;
        startFlashing();
    }

    

    public Color main;
    public Color flashColour;


    private void flash()
    {
       if ( GetComponent<Image>().color.Equals(main))
        {
            GetComponent<Image>().color = flashColour;

        }
        else
        {
            GetComponent<Image>().color = main;
        }

        timeSinceLastChange = 0;
    }
}

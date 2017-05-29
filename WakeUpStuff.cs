using UnityEngine;
using System.Collections;

public class WakeUpStuff : MonoBehaviour {

    public Almanac almanac;
    

	// Use this for initialization
	void Start () {
        if(!almanac.isActiveAndEnabled)
        {
            almanac.gameObject.SetActive(true);
            //almanac.gameObject.SetActive(false);
        }

        //TO-DO: load game data, if any exists

        
        SaveGame g = new SaveGame();
        g.data = 50;
        SaveGameSystem.SaveGame(g, "b");
        SaveGame h = SaveGameSystem.LoadGame("b");
        if (h != null) Debug.Log("wpring? " +h.forest.Length);
        else Debug.Log("doesnt work");

        



    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Saver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	    savePath = "savePath3";
    }
	

    private bool loaded = false;
	// Update is called once per frame
	void Update () {
        
	    if (!loaded)
        {
            loaded = true;
            Debug.Log("loading");
            load();
            loadAlmanac();
        }
        
	}


    public void loadAlmanac()
    {
        SaveGame prev_save;
        if (SaveGameSystem.DoesSaveGameExist(savePath) && SaveGameSystem.LoadGame(savePath) != null)
        {

            prev_save = SaveGameSystem.LoadGame(savePath);
            Debug.Log("found prev save. data: " + prev_save.data + "forestSize: " + prev_save.forest.Length);
            
            //get saved almanac
            if (prev_save.almanac != null)
            {
                for (int i = 0; i < prev_save.almanac.Length; i++)
                {
                    if (prev_save.almanac[i])
                    {
                        Almanac.loadFoundSpecies((Species)Almanac.speciesTypes[i]);
                    }
                }
            }


        }
        else
        {
            Debug.Log("no prev save found");
            
        }

    }

    public void load()
    {
        SaveGame prev_save;
        if (SaveGameSystem.DoesSaveGameExist(savePath) && SaveGameSystem.LoadGame(savePath) != null)
        {
            
            prev_save = SaveGameSystem.LoadGame(savePath);
            Debug.Log("found prev save. data: " + prev_save.data + "forestSize: " + prev_save.forest.Length);
            //if no trees are planted in the forest, generate a new bunch
            if (prev_save.forest[0] == null && treeGen.forest[0] == null)
            {
                treeGen.makeAForest();
            }

            else
            {
                treeGen.plantForest(prev_save.forest);
            }

          


        }
        else
        {
            Debug.Log("no prev save found");
            treeGen.makeAForest();
        }

        


    }
    public static string savePath;

    public GameObject flashPanel;

    public void SaveForest()
    {
        //get save file
        SaveGame prev_save;
        if (SaveGameSystem.DoesSaveGameExist(savePath) && SaveGameSystem.LoadGame(savePath) != null)
        {
            prev_save = SaveGameSystem.LoadGame(savePath);
            
        }

        else
        {
            prev_save = new SaveGame();
            prev_save.data = 500;
            prev_save.forest = new TreeData[treeGen.forestSize];
        }


        //update save file
        prev_save.data += 1;
        foreach (Tree tree in treeGen.forest)
        {
            prev_save.forest[tree.potNum] = new TreeData(tree);
        }
        SaveGameSystem.SaveGame(prev_save, savePath);
        Debug.Log("saved forest");
        flashPanel.GetComponent<Flash>().flashFor(0.5f);

    }

    public void clearAlmanac()
    {
        //get save file
        SaveGame prev_save;
        if (SaveGameSystem.DoesSaveGameExist(savePath) && SaveGameSystem.LoadGame(savePath) != null)
        {
            prev_save = SaveGameSystem.LoadGame(savePath);

        }

        else
        {
            prev_save = new SaveGame();
            prev_save.data = 500;
            prev_save.forest = new TreeData[treeGen.forestSize];
        }

        //update save file
        prev_save.almanac = new bool[Almanac.speciesTypes.Count];
        for (int i = 0; i < Almanac.speciesTypes.Count; i++)
        {
           
              prev_save.almanac[i] = false;
           ((Species) Almanac.speciesTypes[i]).found = false;
            
        }
        SaveGameSystem.SaveGame(prev_save, savePath);
        Debug.Log("cleared almanac");
    }

    public static void SaveAlmanac()
    {
        //get save file
        SaveGame prev_save;
        if (SaveGameSystem.DoesSaveGameExist(savePath) && SaveGameSystem.LoadGame(savePath) != null)
        {
            prev_save = SaveGameSystem.LoadGame(savePath);

        }

        else
        {
            prev_save = new SaveGame();
            prev_save.data = 500;
            prev_save.forest = new TreeData[treeGen.forestSize];
        }

        //update save file
        prev_save.almanac = new bool[Almanac.speciesTypes.Count];
        for (int i = 0; i < Almanac.speciesTypes.Count;i++)
        {
            if (((Species)Almanac.speciesTypes[i]).found)
            {
                prev_save.almanac[i] = true;
            }
            else
            {
                prev_save.almanac[i] = false;
            }
        }
        SaveGameSystem.SaveGame(prev_save, savePath);
        Debug.Log("saved almanac");


    }

}

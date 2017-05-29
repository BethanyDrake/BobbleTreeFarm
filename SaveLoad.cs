
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class SaveGame
{
    public int data = 8;
    //looks I'm restricted to numbers. I can store the layer counts and other data instead.
    public TreeData[] forest;
    public bool[] almanac;
    public SaveGame()
    {
        forest = new TreeData[treeGen.forestSize];
        
    }

}

[Serializable]
public class TreeData
{
    
    public int[] layerCount; //length = num extra layers, each item gives the num braches per layer
    
    public float sizeN;
    public float sizeD;
    
    public float[] seedCol;
    
    public float[] bobbleCol;

    public float generalVar;

    public float seedPosX;
    
    
    public TreeData(Tree tree)
    {
       
        int numLayers = tree.layerCount.Count;
        layerCount = new int[numLayers];

        for(int i = 0; i< numLayers; i++)
        {
            layerCount[i] = (int)tree.layerCount[i];
        }

        sizeN = tree.sizeN;
        sizeD = tree.sizeD;

        seedCol = new float[3];
        seedCol[0] = tree.seedCol.r;
        seedCol[1] = tree.seedCol.g;
        seedCol[2] = tree.seedCol.b;
        
        bobbleCol = new float[3];
        bobbleCol[0] = tree.bobbleCol.r;
        bobbleCol[1] = tree.bobbleCol.g;
        bobbleCol[2] = tree.bobbleCol.b;
        Debug.Log(bobbleCol);

        generalVar = tree.generalVar;

        seedPosX = tree.seed.transform.position.x;
        
    }


}


public static class SaveGameSystem
{
    public static bool SaveGame(SaveGame saveGame, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
        {
            try
            {
                formatter.Serialize(stream, saveGame);
            }
            catch (Exception)
            {
                return false;
            }
        }

        return true;
    }

    public static SaveGame LoadGame(string name)
    {
        if (!DoesSaveGameExist(name))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream stream = new FileStream(GetSavePath(name), FileMode.Open))
        {
            try
            {
                return formatter.Deserialize(stream) as SaveGame;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    public static bool DeleteSaveGame(string name)
    {
        try
        {
            File.Delete(GetSavePath(name));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static bool DoesSaveGameExist(string name)
    {
        return File.Exists(GetSavePath(name));
    }

    private static string GetSavePath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".sav");
    }
}

using UnityEngine;
using System.Collections;

public abstract class Species {
    public bool found = false;
    public string name;
    public GameObject display;
    public abstract bool isSpecies(Tree tree);
    public Sprite standardImg;
    

}

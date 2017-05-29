using UnityEngine;
using System.Collections;
using System;

public class Grow : MonoBehaviour {

    public float growSpeed = -1;
    public float maxSize = 1;
    public Tree motherTree;

	// Use this for initialization
	void Start () {
        transform.localScale = Vector3.one * 0.1f;
	    
	}
    public void makeSmall()
    {
        transform.localScale = Vector3.one * 0.1f;
    }

    public void makeSmall(float maxSize)
    {
        this.maxSize = maxSize;
        makeSmall();
    }

    public void makeSmall(float maxSize, float growSpeed)
    {
        this.growSpeed = growSpeed;
        makeSmall(maxSize);
    }

    // Update is called once per frame
    void Update () {
        if (growSpeed>0 && transform.localScale.x < maxSize)
        {
            transform.localScale += Vector3.one * growSpeed*Time.deltaTime;

            
            if (isFullyGrown())
            {
                motherTree.notifyGrown();
            }
            
                 
        }

    }

    public bool isFullyGrown()
    {
        return (transform.localScale.x >= maxSize);
        
    }
}

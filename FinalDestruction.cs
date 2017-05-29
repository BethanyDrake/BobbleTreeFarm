using UnityEngine;
using System.Collections;

public class FinalDestruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public float destroyHeight = 10;

    public Tree tree;
	// Update is called once per frame
	void Update () {

        if (transform.position.y > destroyHeight)
        {

            //destroyImgs();
            GameObject.Destroy(transform.gameObject);


        }
	
	}
    /*
    public void destroyImgs()
    {
        

       // Replanter.highlight.SetActive(false);
            foreach (GameObject img in Replanter.imgList)
            {
                if (img != null)
                {
                    GameObject.Destroy(img);
                   // Debug.Log("destroyed image");
                }
            }

            for (int i = 0; i < Replanter.imgList.Count; i++)
            {
                if (Replanter.imgList[i] == null)
                {
                    Replanter.imgList.RemoveAt(i);
                }
            }
            
            GameObject.Destroy(transform.gameObject);

        Replanter.destroyer = null;
        
    }
    */
}

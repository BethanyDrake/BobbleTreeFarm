using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Replanter : MonoBehaviour {

    Tree t1;
    Tree t2;
    Tree t3;

    public static Tree destroyer;
    public Camera c2;

    //public static GameObject highlight;

    //public GameObject highlightPanel;


    // Use this for initialization
    void Start () {
        //highlight = highlightPanel;

        p1Pos = new Vector3(150, 45, 0);
        p2Pos = new Vector3(263, 45, 0);
        trashPos = new Vector3(425, 45, 0);

    }

    public  Vector3 p1Pos = new Vector3(150, 45, 0);
    public  Vector3 p2Pos = new Vector3(210, 45, 0);
    public  Vector3 trashPos = new Vector3(310, 45, 0);

    public RectTransform p1Transform;
    public RectTransform p2Transform;


    public GameObject breedPanel1;
    public GameObject breedPanel2;

    public static ArrayList imgList = new ArrayList { };

    // Update is called once per frame
    void Update () {
        

        //debug: display tree data
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.gameObject.GetComponent<Grow>() != null)
                    {
                        Tree t = hitInfo.transform.gameObject.GetComponent<Grow>().motherTree;
                        Debug.Log(t);


                    }

                }

            }
        }
        

        if (flashing && !breedPanel1.GetComponent<Flash>().isFlashing())
        {
            flashing = false;
            
            destroyImages();
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.GetComponent<Grow>()!=null)
                {   if (flashing)
                    {
                        stopFlashing();
                        destroyImages();
                    }
                    Tree t = hitInfo.transform.gameObject.GetComponent<Grow>().motherTree;
                    //c2.GetComponent<PictureScript>().displayTreeAt(t, p1Pos);
                    if (t1 == null)
                    {
                        c2.GetComponent<PictureScript>().displayTreeAt(t, p1Transform);
                        t1 = t;
                    }
                    else if (t2 == null)
                    {
                        c2.GetComponent<PictureScript>().displayTreeAt(t, p2Transform);
                        t2 = t;
                    }
                    else if (t3 == null)
                    {
                        //c2.GetComponent<PictureScript>().displayTreeAt(t, trashPos);
                        t3 = t;
                        breed();
                    }


                }
                
            }
            
                
        }


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.GetComponent<Grow>() != null)
                {
                    Tree t = hitInfo.transform.gameObject.GetComponent<Grow>().motherTree;
                    Vector3 pos = t.seed.transform.position;
                    int pot = t.potNum;
                    t.destroy();
                    Tree aTree = new Tree(true);
                    aTree.plantSeed(pot);
                    aTree.addFirstBobble();


                }

            }
        }

    }
    public void destroyImages()
    {


        // Replanter.highlight.SetActive(false);
        foreach (GameObject img in imgList)
        {
            if (img != null)
            {
                GameObject.Destroy(img);
                // Debug.Log("destroyed image");
            }
        }

        for (int i = 0; i < imgList.Count; i++)
        {
            if (imgList[i] == null)
            {
                imgList.RemoveAt(i);
            }
        }

       

       

    }

    private void breed()
    {
        highlight();
        Vector3 newPos = t3.seed.transform.position;
        t3.destroy();
        Tree t = new Tree(t1, t2);
        t.plantSeed(t3.potNum);
        t.addFirstBobble();
        //GameObject.Destroy(t1.img);
        //GameObject.Destroy(t2.img);
        destroyer = t3;
        t1 = t2 = t3 = null;
    }


    public float flashTime = 2;//0.5f;
    private bool flashing = false;
    

    private void highlight()
    {
        flashing = true;
        breedPanel1.GetComponent<Flash>().flashFor(flashTime);
        breedPanel2.GetComponent<Flash>().flashFor(flashTime);

    }

    private void stopFlashing()
    {
        flashing = false;
        breedPanel1.GetComponent<Flash>().stopFlashing();
        breedPanel2.GetComponent<Flash>().stopFlashing();

    }

}

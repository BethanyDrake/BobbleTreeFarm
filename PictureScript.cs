using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PictureScript : MonoBehaviour {

    public Camera straightCam;
    public GameObject Panel;
    public GameObject DisplayCase;

    // Use this for initialization
    void Start () {
        Tree.straitCam = straightCam;
    }
	
	// Update is called once per frame
	void Update () {

        if (this.isActiveAndEnabled && !grab)
        {
            transform.gameObject.SetActive(false);
        }
	
	}


    private bool almanacEntry;
    private bool displaying;
    private RectTransform targetPos;

    public void displayTreeAt(Tree tree, RectTransform pos, GameObject displayer = null)
    {
        DisplayCase = displayer;
        displaying = true;
        targetPos = pos;
        takePicture(tree);
        
    }

    public void takePicture(Tree tree)
    {
        transform.gameObject.SetActive(true);
        frame(tree);
        target = tree;
        grab = true;

    }

    private Tree target;

    public void frame(Tree tree)
    {

        transform.position = transform.position + (tree.seed.transform.position.x - transform.position.x) * Vector3.right ;
       

        float height = Mathf.Max(2, tree.getHeight())/2;

        GetComponent<Camera>().orthographicSize = height;
        transform.position = transform.position + (height/2 - transform.position.y) * Vector3.up;
        /*
        Vector3 worldTopLeft = tree.seed.transform.position + tree.getHeight() * Vector3.up + (tree.getWidth() / 2) * Vector3.left;
        Vector3 worldTopRight = tree.seed.transform.position + tree.getHeight() * Vector3.up + (tree.getWidth() / 2) * Vector3.right;
        Vector3 worldBottomLeft = tree.seed.transform.position + (tree.getWidth() / 2) * Vector3.left;
        Vector3 worldBottomRight = tree.seed.transform.position + (tree.getWidth() / 2) * Vector3.right;

        Vector3 pos = GetComponent<Camera>().WorldToScreenPoint(worldBottomLeft);
        Vector3 size = (GetComponent<Camera>().WorldToScreenPoint(worldTopRight)) - pos;

        Vector2 realPos = new Vector2(pos.x, pos.y);
        Vector2 realSize = new Vector2(size.x, size.y);
        

        GetComponent<Camera>().rect = new Rect(realPos, realSize);
        */

    }

    public bool grab;
    public Renderer display;
    void OnPostRender()
    {
        Debug.Log("on post render");
        
        if (grab)
        {
            Texture2D tex = new Texture2D(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight);
            Rect r = new Rect(GetComponent<Camera>().rect.x*Screen.width, GetComponent<Camera>().rect.y*Screen.height, GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight);
            tex.ReadPixels(r, 0, 0);
            tex.Apply();
            //display.material.mainTexture = tex;
            grab = false;
            Sprite sprite = Sprite.Create(tex, new Rect(0,0, tex.width, tex.height), Vector2.zero);
            
            GameObject img = new GameObject(); 
            img.AddComponent<Image>().sprite = sprite;
            if (DisplayCase!=null)
            {
                /*
                img.transform.SetParent(DisplayCase.transform, false);
                
                Debug.Log(targetPos.transform);
                
                img.transform.position -= 10 * Vector3.up;
                Debug.Log(img.transform.position);
                */
                img.transform.SetParent(targetPos, false);
            }
            else
            {
                /*
                img.transform.SetParent(Panel.transform, true);
                img.transform.position = targetPos.position;
                */
                img.transform.SetParent(targetPos, false);
            }

            //img.AddComponent<RectTransform>() 


            if (DisplayCase == null) Replanter.imgList.Add(img);
            //img.GetComponent<RectTransform>().position = Vector3.zero;
            img.transform.localScale *= 0.7f;
            if(DisplayCase!=null)target.img = img;
            //img.transform.parent = Panel.transform;



        }
        
    }
}

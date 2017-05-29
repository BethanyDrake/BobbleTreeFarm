using UnityEngine;
using System.Collections;

public class treeGen : MonoBehaviour {

    public static Tree[] forest;
    public static int forestSize = 7;

	// Use this for initialization
	void Start () {
        
        

        Tree.mat = mat;
        forest = new Tree[forestSize];
        //genNewTree();
        //makeAForest();
    }
    
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void makeAForest()
    {
        //Debug.Log("making forest");
        //Debug.Log("forest sizev " + forestSize);
        for (int i = 0; i<forestSize;i++)
        {
            //Debug.Log("making tree");
            
            Tree aTree = new Tree(true);
            aTree.plantSeed(i);
            aTree.addFirstBobble();
        } 

    }
    
    public static void plantForest(TreeData[] dataForest)
    {
        Debug.Log("planting saved forest");
        for (int i = 0; i< dataForest.Length; i++)
        {
            Tree tree;
            if (dataForest[i] ==null)
            {
                Debug.Log("tree not found, planting new shrub");
                tree = new Tree(true);
                
            }
            else
            {
                tree = new Tree(dataForest[i]);
            }
            if (forest[i] != null)
            {
                forest[i].destroy();
            }
            tree.plantSeed(i);
            tree.addFirstBobble();
        }

    }
    

    public Material mat;

    void genNewTree()
    {
        Tree aTree = new Tree();
        aTree.plantSeed(0);
        aTree.addFirstBobble();
        /*
        Material mat2 = new Material(mat);
        //mat2.color = Color.red;
        GameObject seed = GameObject.CreatePrimitive(PrimitiveType.Cube);
        seed.GetComponent<Renderer>().material = mat2;
        seed.transform.Rotate(new Vector3(45, 0, 45));
        seed.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        seed.transform.position = new Vector3(0, 0, 0);
        seed.AddComponent<Rigidbody>();
        seed.GetComponent<Rigidbody>().isKinematic = true;
        */
        /*
        Material mat3 = new Material(mat);
        mat3.color = Color.green;
        GameObject bobble = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bobble.GetComponent<Renderer>().material = mat3;
        bobble.AddComponent<Rigidbody>();
        bobble.AddComponent<SpringJoint>();
        bobble.GetComponent<SpringJoint>().connectedBody = seed.GetComponent<Rigidbody>();
        bobble.GetComponent<SpringJoint>().anchor = Vector3.zero;
        bobble.GetComponent<SpringJoint>().connectedAnchor = Vector3.zero;
        bobble.GetComponent<SpringJoint>().enableCollision = true;
        bobble.GetComponent<SpringJoint>().spring = 100;
        */

    }


}

using UnityEngine;
using System.Collections;
using System;

public class Tree{
    
    public static int maxLayers = 6;
    public static int maxBranches = 4;
    public static int shrubMaxLayers = 2;
    public static int shrubMaxBranches = 2;
    public static Material mat;

    public ArrayList layerCount; //length = num extra layers, each item gives the num braches per layer
    public float sizeN;
    public float sizeD;

    public Color seedCol;
    public Color bobbleCol;
    

    public float generalVar;
    public int potNum;
    
    public Tree(bool shrub = false)
    {
        
        //basic tree
        seedCol = MyColors.randomBrown();
        bobbleCol = MyColors.randomGreen();
        
        generalVar = 0.2f;
        sizeN = 4;
        sizeD = 5;

        if (shrub)
        {
            layerCount = new ArrayList();
            {

                int numLayers = UnityEngine.Random.Range(1, shrubMaxLayers+1);
                for (int j = 0; j < numLayers; j++)
                {
                    layerCount.Add(UnityEngine.Random.Range(1, shrubMaxBranches+1));
                }

            }
        }
        else
        {
            layerCount = new ArrayList();
            {

                int numLayers = UnityEngine.Random.Range(1, maxLayers+1);
                for (int j = 0; j < numLayers; j++)
                {
                    layerCount.Add(UnityEngine.Random.Range(1, maxBranches+1));
                }

            }
        }

        
    }
    public Tree(Tree parent)
    {
        //clone of parent
    }

    public Tree(TreeData data)
    {


        seedCol = new Color(data.seedCol[0], data.seedCol[1], data.seedCol[2]);
        bobbleCol = new Color(data.bobbleCol[0], data.bobbleCol[1], data.bobbleCol[2]);

        generalVar = data.generalVar;
        sizeN = data.sizeN;
        sizeD = data.sizeD;
      
        layerCount = new ArrayList();

        foreach (int i in data.layerCount)
        {
            layerCount.Add(i);
        }
        bobbles = new ArrayList { };
        
    }

    
    public override string ToString()
    {

        String s = "pot number: " + potNum + "\nlayer counts: ";
        foreach (int layer in layerCount)
        {
            s = s + layer + ", ";
        }
        s = s + "\nsize: " + sizeN + "/" + sizeD + "\n";
        s = s + "volitility: " + generalVar + "\n";
        s += "Color: " + bobbleCol + " : " + MyColors.getColor(bobbleCol)+ "\n";
        Species spec = Almanac.getSpecies(this);
        if (spec!=null)
        {
            s = s + spec.name;
        }

        
        
        return s;
    }

    public Tree(Tree p1, Tree p2)
    {
        //mix of parents genetics, plus mutations
        /*
        public ArrayList layerCount; //length = num extra layers, each item gives the num braches per layer
        public float sizeN;
        public float sizeD;

        public Color seedCol;
        public Color bobbleCol;
        public int bobbleColVar;
        */


        //general variance
        generalVar = myRange(p1.generalVar, p2.generalVar);
        if (commonMute()) generalVar += UnityEngine.Random.Range(-generalVar / 10, generalVar / 10);
        if (rareMute()) generalVar += UnityEngine.Random.Range(-generalVar / 2, generalVar / 2);
        //sanitation
        if (generalVar >= 1 || generalVar <= 0) generalVar = 0.5f;


        //sizeN
        sizeN = myRange(p1.sizeN, p2.sizeN);
        if (commonMute()) sizeN += UnityEngine.Random.Range(-sizeN / 10, sizeN / 10);
        if (rareMute()) sizeN += UnityEngine.Random.Range(-sizeN / 2, sizeN / 2);
        //sizeD
        sizeD = myRange(p1.sizeD, p2.sizeD);
        if (commonMute()) sizeD += UnityEngine.Random.Range(-sizeD / 10, sizeD / 10);
        if (rareMute()) sizeD += UnityEngine.Random.Range(-sizeD / 2, sizeD / 2);
        ///sanitation
        if (sizeN < 1) sizeN = 1;
        if (sizeD < sizeN + 1) sizeD = sizeN + 1;

        //colors
        seedCol = MyColors.Varient(p1.seedCol, p2.seedCol, generalVar);
        bobbleCol = MyColors.Varient(p1.bobbleCol, p2.bobbleCol, generalVar);

        bobbles = new ArrayList { };
        layerCount = new ArrayList { };
        int i;
        for (i = 0; i<Mathf.Min(p1.layerCount.Count, p2.layerCount.Count);i++)
        {
            if (UnityEngine.Random.Range(0.0f,1.0f)<0.5)
            {
                layerCount.Add(p1.layerCount[i]);
            }
            else
            {
                layerCount.Add(p2.layerCount[i]);
            }
        }
        i++;
        while (heads())
        {
            if (p1.layerCount.Count > i)
            {
                layerCount.Add(p1.layerCount[i]);
            }
            else if (p2.layerCount.Count > i)
            {
                layerCount.Add(p2.layerCount[i]);
            }
            else break;
            i++;
        }


        mutateStructure();
        


    }
    private bool heads()
    {
        return (UnityEngine.Random.Range(0.0f, 1.0f) < 0.5);
    }

    private void mutateStructure()
    {
        if (commonMute())
        {   
            addOrRemoveLayer();
        }
        if (rareMute())
        {
            addOrRemoveLayer();
        }

        //add or remove branches
        for (int i = 0; i < layerCount.Count;i++)
        {
            if (commonMute())
            {
                if (heads())
                {
                    if ((int)layerCount[i] < maxBranches)
                    {
                        layerCount[i] = (int)layerCount[i] + 1;
                    }
                }
                else
                {
                    if ((int)layerCount[i] > 1)
                    {
                        layerCount[i] = (int)layerCount[i] - 1;
                    }
                }
            }
        }

    }

    private void addOrRemoveLayer()
    {
        if (heads())
        {
            if (layerCount.Count < maxLayers)
            {
                //Debug.Log("addedLayer. numlayers = "+ layerCount.Count);
                addLayer();
            }
        }
        else
        {
            if (layerCount.Count > 0)
            {
                removeLayer();
            }
        }

    }

    private void addLayer()
    {
        int index = UnityEngine.Random.Range(0, layerCount.Count+1);
        int value = UnityEngine.Random.Range(1, shrubMaxBranches + 1);
        layerCount.Insert(index, value);
    }
    private void removeLayer()
    {
        int index = UnityEngine.Random.Range(0, layerCount.Count);
        layerCount.RemoveAt(index);
        layerCount.TrimToSize();
    }


    public void destroy()
    {
        if (particles != null) GameObject.Destroy(particles);
        foreach (ArrayList layer in bobbles)
        {
            foreach(GameObject bobble in layer)
            {
                GameObject.Destroy(bobble);
            }
        }
        GameObject.Destroy(seed);
        GameObject.Destroy(firstBobble.GetComponent<Grow>());
        firstBobble.AddComponent<FinalDestruction>().tree = this;
    }

    private bool commonMute()
    {
        if (UnityEngine.Random.Range(0.0f, 1.0f) < generalVar)
        {
            //Debug.Log("common mutation");
            return true;
        }
        
        return false;
    }

    private bool rareMute()
    {
        if (UnityEngine.Random.Range(0.0f, 1.0f) < generalVar * generalVar)
        {
            //Debug.Log("rare mutation");
            return true;
        }
        return false;
    }
    public static float myRange(float a, float b)
    {
        return UnityEngine.Random.Range(Mathf.Min(a, b), Mathf.Max(a, b));
    }



    /*
    public void plantSeed(Vector3 pos)
    {
       
        Material seedMat = new Material(mat);
        seedMat.color = seedCol;
        seed = GameObject.CreatePrimitive(PrimitiveType.Cube);
        seed.GetComponent<Renderer>().material = seedMat;
        seed.transform.Rotate(new Vector3(45, 0, 45));
        seed.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        seed.transform.position = pos - Vector3.up * pos.y; //ignore y pos
        seed.AddComponent<Rigidbody>();
        seed.GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Destroy(seed.GetComponent<Collider>());
        seed.AddComponent<SphereCollider>();
            
        
    }*/

    public void plantSeed(int potNum)
    {
        this.potNum = potNum;
        treeGen.forest[potNum] = this;
        Vector3 pos = new Vector3((potNum - 3) * 2, 0, 0);

        Material seedMat = new Material(mat);
        seedMat.color = seedCol;
        seed = GameObject.CreatePrimitive(PrimitiveType.Cube);
        seed.GetComponent<Renderer>().material = seedMat;
        seed.transform.Rotate(new Vector3(45, 0, 45));
        seed.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        seed.transform.position = pos - Vector3.up * pos.y; //ignore y pos
        seed.AddComponent<Rigidbody>();
        seed.GetComponent<Rigidbody>().isKinematic = true;
        GameObject.Destroy(seed.GetComponent<Collider>());
        seed.AddComponent<SphereCollider>();


    }


    public GameObject particles;

    public void highlight()
    {
        //make some partical effects around the tree to highlight that you've discovered a new species
        //remember to destroy the effect when you destroy the tree
        particles = GameObject.Instantiate(Almanac.particlePrefab);
        particles.transform.position = seed.transform.position;

        //Debug.Log("highlighting tree at " + seed.transform.position);
    }

    public bool mature;
    
    public void onMature()
    {
        Almanac.checkNewSpecies(this);
    }

    internal void notifyGrown()
    {
        //Debug.Log("notifiying grown ");// + Time.time);
        //if the latest layer is fully grown, make a new one.
        try
        {
            if (bobbles.Count != 0 && ((ArrayList)bobbles[bobbles.Count - 1]).Count == 0)
            {
                //Debug.Log("layer with zero bobbles");
            }
            if (bobbles.Count == 0 || ((GameObject)((ArrayList)bobbles[bobbles.Count - 1])[0]).GetComponent<Grow>().isFullyGrown())
            {
                if (bobbles.Count < layerCount.Count)
                {
                    addNextLayer();
                }
                else
                {
                    mature = true;
                    onMature();

                }



            }
        }
        catch
        {
            Debug.Log("error here");
            Debug.Log("bobbles count = " + bobbles.Count);
            Debug.Log("last bobble count = " + ((ArrayList)bobbles[bobbles.Count-1]).Count);
        }
        
    }

    public GameObject seed;
    public GameObject firstBobble;
    public ArrayList bobbles = new ArrayList { }; //2d array, where each item is a layer

    public void addNextLayer()
    {
        //Debug.Log("adding next Layer");
        
        if (bobbles.Count >= layerCount.Count) return;
        ArrayList prevLayer;
        if(bobbles.Count == 0)
        {
            //Debug.Log("bobbles.Count == 0");
            prevLayer = new ArrayList { firstBobble };
        }
        else
        {
            //Debug.Log("bobbles.Count != 0");
            prevLayer = (ArrayList) bobbles[bobbles.Count-1];
        }

        ArrayList newLayer = new ArrayList { };
       
        foreach (GameObject bobble in prevLayer)
        {
            for(int i = 0; i<(int)layerCount[bobbles.Count];i++)
            {
                GameObject newBobble = GameObject.Instantiate(firstBobble);
                newBobble.transform.position = bobble.transform.position;
                newBobble.GetComponent<Grow>().maxSize = bobble.GetComponent<Grow>().maxSize * (sizeN/sizeD);
                newBobble.GetComponent<Rigidbody>().mass = bobble.GetComponent<Rigidbody>().mass* (sizeN / sizeD) * (sizeN / sizeD) * (sizeN / sizeD);
                newBobble.GetComponent<Grow>().makeSmall(); //set maxsize here
                newBobble.GetComponent<SpringJoint>().connectedBody = bobble.GetComponent<Rigidbody>();
                newBobble.GetComponent<SpringJoint>().spring = 100* newBobble.GetComponent<Rigidbody>().mass; //make spring strength proportional to mass
                newBobble.GetComponent<Grow>().motherTree = this;

                //make a random brown color
                Material newMat = new Material(mat);
                newMat.color = MyColors.Varient(bobble.GetComponent<Renderer>().material.color, generalVar);
                newBobble.GetComponent<Renderer>().material = newMat;

                firstBobble.GetComponent<SpringJoint>().spring += 100 * newBobble.GetComponent<Rigidbody>().mass;
                newLayer.Add(newBobble);
            }
        }
        bobbles.Add(newLayer);
        //Debug.Log("finnished adding next Layer");
    }

    public void addFirstBobble()
    {
        Material mat3 = new Material(mat);
        mat3.color = bobbleCol;
        GameObject bobble = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bobble.transform.position = seed.transform.position + Vector3.up;
        bobble.GetComponent<Renderer>().material = mat3;
        bobble.AddComponent<Rigidbody>();
        bobble.AddComponent<SpringJoint>();
        bobble.GetComponent<SpringJoint>().autoConfigureConnectedAnchor = false;
        bobble.GetComponent<SpringJoint>().connectedBody = seed.GetComponent<Rigidbody>();
        bobble.GetComponent<SpringJoint>().anchor = Vector3.zero;
        bobble.GetComponent<SpringJoint>().connectedAnchor = Vector3.zero;
        bobble.GetComponent<SpringJoint>().enableCollision = true;
        bobble.GetComponent<SpringJoint>().spring = 100;
        //bobble.GetComponent<SpringJoint>().damper = 10000.0f;
        bobble.AddComponent<Grow>();
        bobble.GetComponent<Grow>().growSpeed = 0.1f;
        bobble.GetComponent<Grow>().motherTree = this;
        firstBobble = bobble;
    }


    public float getWidth()
    {
        float width = 0;

        foreach (ArrayList layer in bobbles)
        {
            foreach(GameObject bobble in layer)
            {
                Vector3 pos = bobble.transform.position - bobble.transform.position.y * Vector3.up;
                float d = Vector3.Distance(pos, Vector3.zero) + bobble.transform.localScale.x;
                if (d > width) width = d;
            }
        }
        return width;

    }
    public float getHeight()
    {
        float height = 1;

        foreach (ArrayList layer in bobbles)
        {
            foreach (GameObject bobble in layer)
            {
                
                float d = bobble.transform.position.y + bobble.transform.localScale.x;
                if (d > height) height = d;
            }
        }
        return height+1;

    }

    public float getSizeOfTopLayer()
    {
        GameObject topBobble = (GameObject)((ArrayList)bobbles[bobbles.Count - 1])[0];

        return topBobble.GetComponent<Grow>().maxSize;
    }

    public int getNumBobblesInTopLayer()
    {
        return ((ArrayList)bobbles[bobbles.Count - 1]).Count;
    }

    public float getCalculatedHeight()
    {
        float sum = 1; //height of first bobble
        for (int i=0; i<layerCount.Count; i++)
        {
            float a = sizeN / sizeD;
            for (int j = 0; j<i;j++)
            {
                a*= sizeN / sizeD;
            }
            sum += a;
        }
        return sum;
    }

    public float getNumBobbles()
    {   //includes firstBobble
        float c = 1;

        foreach (ArrayList layer in bobbles)
        {
            foreach (GameObject bobble in layer)
            {

                c++;
            }
        }
        return c;

    }
    public float getNumBobbles(int layer)
    {   //includes firstBobble
        float c = 0;
        if (layer >= bobbles.Count) return c;
        
            foreach (GameObject bobble in (ArrayList)bobbles[layer])
            {

                c++;
            }
        
        return c;

    }

    public static Camera straitCam;

    public GameObject img;

    public void takeNewPic()
    {

    }

    //should only be called through the 
    public void displayAt(Vector3 pos, bool takeNewPic = false)
    {

    }

}

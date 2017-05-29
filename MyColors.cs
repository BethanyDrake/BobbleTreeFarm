using UnityEngine;
using System.Collections;

public class MyColors {


    public static Vector3 brownMin = new Vector3(50, 20, 0);
    public static Vector3 brownMax = new Vector3(120, 150, 40);

    public static Vector3 greenMin = new Vector3(0, 20, 0);
    public static Vector3 greenMax = new Vector3(100, 250, 100);

    public static Color randomBrown()
    {
        Color C = new Color();
        C.r = Random.Range(brownMin.x/256, brownMax.x/256);
        C.g = Random.Range(brownMin.y / 256, brownMax.y / 256);
        C.b = Random.Range(brownMin.z / 256, brownMax.z / 256);
        return C;
    }
    public static Color randomGreen()
    {
        Color C = new Color();
        C.r = Random.Range(greenMin.x / 256, greenMax.x / 256);
        C.g = Random.Range(greenMin.y / 256, greenMax.y / 256);
        C.b = Random.Range(greenMin.z / 256, greenMax.z / 256);
        return C;
    }
    /* returns varient of original colour
     * O is original colour*/
    public static Color Varient(Color O, float var)
    {
        if (var < 0) var = 0;
        

        Color C = new Color();
        C.r = (O.r + Random.Range(-var, var));
        C.g = (O.g + Random.Range(-var, var));
        C.b = (O.b + Random.Range(-var, var));
        //Debug.Log(O + " became " + C);
        return sanitise(C);
    }

    public static Color Varient(Color c1, Color c2, float var)
    {
        Color C = new Color();
        C.r = Tree.myRange(c1.r, c2.r);
        C.g = Tree.myRange(c1.g, c2.g);
        C.b = Tree.myRange(c1.b, c2.b);
        C.r += Random.Range(-var, var);
        C.g += Random.Range(-var, var);
        C.b += Random.Range(-var, var);

        return sanitise(C);


    }


    public static string getColor(Color C)
    {
        string s = "";
        if (isPurple(C)) s += "Purple, ";
        if (isBrown(C)) s += "Brown, ";
        if (isGreen(C)) s += "Green, ";
        return s;
    }

    public static bool isPurple(Color C)
    {
        /*
         if (C.g < 40.0 / 255 && C.b > 70.0 / 255) return true;

         return false;
         */
        if (C.r >= 0.1 && C.r <= 0.3 && C.b >= 0.08571429 && C.b <= 0.7 && C.g >= 0 && C.g <= 0.1374999) { return true; }
        if (C.r >= 0.3 && C.r <= 0.5 && C.b >= 0.2928571 && C.b <= 1 && C.g >= 0 && C.g <= 0.3124999) { return true; }
        if (C.r >= 0.5 && C.r <= 0.7 && C.b >= 0.6928571 && C.b <= 1 && C.g >= 0 && C.g <= 0.5) { return true; }
        return false;

    }


    public static bool isBrown(Color C)
    {
        /*
        if (C.r < brownMax.x && C.r > brownMin.x
            && C.g < brownMax.y && C.g > brownMin.y
            && C.b < brownMax.z && C.g > brownMin.z)
        {
            return true;
        }
        return false;
        */
        if (C.r >= 0.1 && C.r <= 0.3 && C.b >= 0 && C.b <= 0.1 && C.g >= 0 && C.g <= 0.3624999) { return true; }
        if (C.r >= 0.3 && C.r <= 0.5 && C.b >= 0 && C.b <= 0.3071429 && C.g >= 0.1499999 && C.g <= 0.2999999) { return true; }
        if (C.r >= 0.5 && C.r <= 0.7 && C.b >= 0 && C.b <= 0.3071429 && C.g >= 0.3374998 && C.g <= 0.5374999) { return true; }
        return false;
    }

    public static bool isGreen(Color C)
    {
        /*
        if (C.r + C.b < C.g) return true;
        return false;
        */
        if (C.r >= -0.1 && C.r <= 0.1 && C.b >= 0 && C.b <= 0.5071428 && C.g >= 0.2999998 && C.g <= 1) { return true; }
        if (C.r >= 0.1 && C.r <= 0.3 && C.b >= 0 && C.b <= 0.2928571 && C.g >= 0.2999998 && C.g <= 1) { return true; }
        if (C.r >= 0.3 && C.r <= 0.5 && C.b >= 0 && C.b <= 0.4714286 && C.g >= 0.525 && C.g <= 1) { return true; }
        if (C.r >= 0.5 && C.r <= 0.7 && C.b >= 0 && C.b <= 0.4714286 && C.g >= 0.8624998 && C.g <= 1) { return true; }
        return false;

    }
    private static Color sanitise(Color C)
    {
        if (C.r < 0) C.r = 0;
        if (C.r > 1) C.r = 1;
        if (C.g < 0) C.g = 0;
        if (C.g > 1) C.g = 1;
        if (C.b < 0) C.b= 0;
        if (C.b > 1) C.b = 1;
        return C;
    
    }
}

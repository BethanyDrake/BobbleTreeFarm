using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour {

	// Use this for initialization
	void Start () {
        openSpeed = 1;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            Close();
        }

        if (opening)
        {
            transform.localScale += Vector3.one * openSpeed *Time.deltaTime;
            if (transform.localScale.x >= 1)
            {
                opening = false;
                transform.localScale = Vector3.one;
            }
        }
	
	}

    public float openSpeed;
    bool opening;
    bool closing;

    public void Open()
    {
        opening = true;
        transform.localScale = Vector3.one * 0.01f;
        transform.gameObject.SetActive(true);
            
    }

    public void Close()
    {

        transform.gameObject.SetActive(false);
    }

}

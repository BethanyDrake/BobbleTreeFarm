using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow)) left();
        if (Input.GetKey(KeyCode.RightArrow)) right();
        if (Input.GetKey(KeyCode.UpArrow)) up();
        if (Input.GetKey(KeyCode.DownArrow)) down();

    }

    public float speed = 2;
    public float stop = 10;

    private void left()
    {
        Vector3 point = Vector3.up * transform.position.y;
        transform.RotateAround(point, Vector3.up, speed);
    }
    private void right()
    {
        
        Vector3 point = Vector3.up * transform.position.y;
        transform.RotateAround(point, Vector3.up, -speed);
    }
    private void up()
    {
        Vector3 point = Vector3.zero;
        Vector3 axis = Vector3.Cross(transform.position, Vector3.up);
        float angle = Vector3.Angle(transform.position, Vector3.up);
        if (angle < stop) return;
        transform.RotateAround(point, axis, speed);

    }
    private void down()
    {
        Vector3 point = Vector3.zero;
        Vector3 axis = Vector3.Cross(transform.position, Vector3.up);
        float angle = Vector3.Angle(transform.position, transform.position - transform.position.y * transform.up);
        if (angle < stop) return;
        transform.RotateAround(point, axis, -speed);
    }



}

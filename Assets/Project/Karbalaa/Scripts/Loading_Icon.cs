using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Loading_Icon : MonoBehaviour {


    void Start()
    {
        GetComponent<RawImage>().transform.Rotate(new Vector3(0,0,0));
    }

	void Update () 
	{
        GetComponent<RawImage>().transform.Rotate(new Vector3(0, 0, 10));
	}
}

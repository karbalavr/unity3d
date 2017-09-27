using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNGToPlayer : MonoBehaviour {


    public Transform target;
    private float x, z, w;


	void Start () 
    {
        x = transform.rotation.x;
        z = transform.rotation.z;
        w = transform.rotation.w;
	}
	

	void Update () 
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1.2f);
        transform.rotation = new Quaternion(x, transform.rotation.y, z, w);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextOverlayTracking : MonoBehaviour {

    public Transform tTrack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = tTrack.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(tTrack.forward, Vector3.up), 0.1f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundModifier : MonoBehaviour {

    AudioSource source;
    public float pitch;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        if (source)
            pitch = source.pitch = Random.Range(2.0f, 4.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

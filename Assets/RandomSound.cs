using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour {

    public AudioClip[] clips;

	// Use this for initialization
	void Start () {
        PlayRandomClip();
	}

    public void PlayRandomClip()
    {
        if (clips.Length > 0)
        {
            int index = Random.Range(0, clips.Length);
            AudioClip clip = clips[index];
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}

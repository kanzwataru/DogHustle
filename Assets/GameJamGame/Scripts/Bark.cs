using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {

    public float fireRate = 1.0f;
    private float nextFire;
    //public AudioClip barkSound;
    private AudioSource source;

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    void Update () {
		
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            //source.PlayOneShot(barkSound);
            nextFire = Time.time + fireRate;
        }

	}
}

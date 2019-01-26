using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {

    //BARKING AND INTERACT WITH ENVIRONMENT

    public TaskManager taskManager;
    public float barkRate = 1.0f;
    private float nextFire;
    //public AudioClip barkSound;
    private AudioSource source;
    private string action = "";

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (other.gameObject.tag == "FoodBowl")
            {
                action = "food";
                print("Interacted with Food Bowl");
            }
            else if (other.gameObject.tag == "WaterBowl")
            {
                action = "water";
                print("Interacted with Water Bowl");
            }
            else if (other.gameObject.tag == "BackDoor")
            {
                action = "back";
                print("Interacted with Back Door");
            }
            else if (Time.time > nextFire)
            {
                //source.PlayOneShot(barkSound);
                nextFire = Time.time + barkRate;
            }

            taskManager.CheckTask(action);
        }

    }

}

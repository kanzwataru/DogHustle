using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {

    //BARKING AND INTERACT WITH ENVIRONMENT

    public GameObject barkEffect;
    public TaskManager taskManager;
    public float barkRate = 1.0f;
    private float nextBark;
    //public AudioClip barkSound;
    private AudioSource source;
    private string action = "";

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    //Effects: Sound and Visual
    private void BarkEffects()
    {
        //source.PlayOneShot(barkSound);
        StartCoroutine(BarkBlink());
    }

    IEnumerator BarkBlink()
    {
        barkEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        barkEffect.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        barkEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        barkEffect.SetActive(false);
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

            taskManager.CheckTask(action);
        }

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextBark)
        {
            nextBark = Time.time + barkRate;
            BarkEffects();
        }
    }
}

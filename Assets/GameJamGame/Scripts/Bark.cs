using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bark : MonoBehaviour {

    //BARKING AND INTERACT WITH ENVIRONMENT

    public GameObject barkEffect;
    public TaskManager taskManager;
    public float barkRate = 2f;
    private float nextBark;
    public AudioClip barkSound;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    private AudioSource source;
    private string action = "";
    private bool inTriggerZone = false; //for free barking

    private void Start()
    {
        source = this.GetComponent<AudioSource>();
        EventBus.AddListener<PauseEvent>(HandleEvent);
        EventBus.AddListener<GameOverEvent>(HandleEvent);
    }

    //Effects: Sound and Visual
    private void BarkEffects()
    {
        source.pitch = Random.Range(0.95f, 1.05f);
        source.PlayOneShot(barkSound);
        StartCoroutine(BarkBlink());
    }

    private void DrinkWaterSound()
    {
        if (Random.Range(0,2) == 0)
        {
            source.PlayOneShot(drinkSound1);
        }
        else
        {
            source.PlayOneShot(drinkSound2);
        }
    }

    private void EatFoodSound()
    {
        if (Random.Range(0, 2) == 0)
        {
            source.PlayOneShot(eatSound1);
        }
        else
        {
            source.PlayOneShot(eatSound2);
        }
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
        yield return new WaitForSeconds(0.2f);
        barkEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        barkEffect.SetActive(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Space) && inTriggerZone && Time.time > nextBark)
        {
            if (other.gameObject.tag == "FoodBowl")
            {
                EatFoodSound();
                action = "food";
            }
            else if (other.gameObject.tag == "WaterBowl" || other.gameObject.tag == "Toilet")
            {
                DrinkWaterSound();
                action = "water";
            }
            else if (other.gameObject.tag == "Cat")
            {
                BarkEffects();
                action = "barkcat";
            }
            else if (other.gameObject.tag == "Hydrant")
            {
                action = "hydrant";
            }

            nextBark = Time.time + barkRate;
            taskManager.CheckTask(action);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inTriggerZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTriggerZone = false;
    }

    private void Update()
    {
        if (!inTriggerZone)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time > nextBark)
            {
                nextBark = Time.time + barkRate;
                BarkEffects();
            }
        }
    }

    private void HandleEvent(PauseEvent msg)
    {
        enabled = !enabled;
    }

    private void HandleEvent(GameOverEvent msg)
    {
        enabled = !enabled;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happy : MonoBehaviour {

    //Happy script attached to every human
    //Proximity to dog will increase their happy status
    //Maxing out happiness changes human state and happy counter increases
    //After 10 seconds, state is reset and happy status returns to zero

    public static int happyCounter = 0;

    private float happyStatus = 0f;
    private float max = 10f;
    private bool imHappy = false;
    private bool isPaused = false;

    private void Start()
    {
        EventBus.AddListener<PauseEvent>(HandleEvent);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (happyStatus < max && !isPaused)
            {
                happyStatus += Time.deltaTime;
            }
        }

    }

    IEnumerator HappyToSad()
    {
        for (int i = 0; i < 16; i++)
        {
            while (isPaused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }

        happyStatus = 0; //reset happy status
        imHappy = false;
    }

    private void Update()
    {
        if (happyStatus >= max && imHappy == false)
        {
            imHappy = true;
            happyCounter++;
            StartCoroutine(HappyToSad());
            EventBus.Emit<HappyEvent>(new HappyEvent()); //call animations
        }
    }

    private void HandleEvent(PauseEvent msg)
    {
        isPaused = !isPaused;
    }

    /*
    private void HandleEvent(HappyEvent msg)
    {
       
    }
    */
}

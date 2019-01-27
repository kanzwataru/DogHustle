using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happy : MonoBehaviour {

    //Happy script attached to every human
    //Proximity to dog will increase their happy status
    //Maxing out happiness changes human state and happy counter increases
    //After 10 seconds, state is reset and happy status returns to zero
    public static int happyCounter = 0; /* would be better to have this in a GameManager of some kind, instead of static */

    private float happyStatus = 0f;
    private float max = 10f;
    private bool isHappy = false;
    private bool isPaused = false;

    private GameObject bubble;

    private void Start()
    {
        EventBus.AddListener<PauseEvent>(HandleEvent);
        bubble = transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (happyStatus < max && !isPaused)
            {
                happyStatus += Time.deltaTime;
            }

            Debug.Log("Getting happy..." + happyStatus);
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
        isHappy = false;
        EventBus.Emit<HappinessChangedEvent>(new HappinessChangedEvent() {person = transform.parent, happy = isHappy});
        bubble.SetActive(!isHappy);
    }

    private void Update()
    {
        if (happyStatus >= max && isHappy == false)
        {
            isHappy = true;
            EventBus.Emit<HappinessChangedEvent>(new HappinessChangedEvent() {person = transform.parent, happy = isHappy});
            bubble.SetActive(!isHappy);
            
            happyCounter++;
            StartCoroutine(HappyToSad());
        }
    }

    private void HandleEvent(PauseEvent msg)
    {
        isPaused = !isPaused;
    }
}

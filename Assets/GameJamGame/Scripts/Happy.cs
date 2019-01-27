using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happy : MonoBehaviour {

    //Happy script attached to every human
    //Proximity to dog will increase their happy status
    //Maxing out happiness changes human state and happy counter increases
    //After 10 seconds, state is reset and happy status returns to zero
    public static int happyCounter = 0; /* would be better to have this in a GameManager of some kind, instead of static */
    public Material personMat;

    public float max = 4f;

    private float happyStatus = 0f;
    private bool isHappy = false;
    private bool isPaused = false;

    private GameObject bubble;
    private GameObject cheer;

    private void Start()
    {
        EventBus.AddListener<PauseEvent>(HandleEvent);
        bubble = transform.GetChild(0).gameObject;
        cheer = transform.GetChild(1).gameObject;

        EventBus.Emit<HappinessChangedEvent>(new HappinessChangedEvent() {person = transform.parent, happy = isHappy});
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (happyStatus < max && !isPaused)
            {
                happyStatus += Time.deltaTime;
                bubble.SetActive(false);
                cheer.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(happyStatus < max && !isPaused)
                bubble.SetActive(true);
                cheer.SetActive(false);
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
            EventBus.Emit<HappyEvent>(new HappyEvent());
            bubble.SetActive(!isHappy);
            StartCoroutine(HappyToSad());

            cheer.SetActive(false);
        }

        if(personMat != null)
            personMat.SetFloat("_Fade", Mathf.Lerp(0.6f, 0.0f, UtilFuncs.remap(happyStatus, 0.0f, max, 0.0f, 1.0f)));
    }

    private void HandleEvent(PauseEvent msg)
    {
        isPaused = !isPaused;
    }
}

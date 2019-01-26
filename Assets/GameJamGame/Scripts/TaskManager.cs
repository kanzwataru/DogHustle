using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour {

    public class Task
    {
        public string description;
        public float timer;
        public System.Action<Vector3> function;

        public Task(string description, float timer, System.Action<Vector3> function)
        {
            this.description = description;
            this.timer = timer;
            this.function = function;
        }
    }

    //TIMED TASKS THE PLAYER MUST DO

    private GameObject taskBox;
    private Text taskText;
    private Text timerText;

    private Vector3 backDoorPos;
    private Vector3 waterBowlPos;
    private Vector3 foodBowlPos;

    private List<Task> tasks = new List<Task>();
    private float timer;
    private int task;
    private bool timerOn = true;

    void Start () {

        //set-up transforms
        backDoorPos = GameObject.FindGameObjectWithTag("BackDoor").GetComponent<Transform>().position;
        waterBowlPos = GameObject.FindGameObjectWithTag("WaterBowl").GetComponent<Transform>().position;
        foodBowlPos = GameObject.FindGameObjectWithTag("FoodBowl").GetComponent<Transform>().position;

        //Get UI:
        taskText = GameObject.FindGameObjectWithTag("TaskText").GetComponent<Text>();
        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
        taskBox = GameObject.FindGameObjectWithTag("TaskBox");

        //Fill tasks arraylist:
        tasks.Add(new Task("Bark at front door", 15f, BarkAt));
        tasks.Add(new Task("Eat some food", 20f, EatAt));
        tasks.Add(new Task("Drink some water", 20f, DrinkAt));

        GetNewTask(); //tasks all have their own text, times, and RULES
    }

    public void GetNewTask()
    {
        Task currentTask = TaskRandomizer();
        taskBox.SetActive(true); //show task to player
        taskText.text = tasks[task].description; //set task text
        currentTask.function(backDoorPos); //set current task rules
        timer = currentTask.timer; //start new timer
    }

    //Randomly picks a task from list
    private Task TaskRandomizer()
    {
        task = Random.Range(0, tasks.Count);
        return tasks[task];
    }

    private void TaskComplete()
    {
        timerOn = false;
        taskBox.SetActive(false);
        StartCoroutine(TaskDelay());
    }

    private IEnumerator TaskDelay()
    {
        yield return new WaitForSeconds(2f);
        timerOn = true;
        GetNewTask();
    }

    //TASK RULES

    public void BarkAt(Vector3 location)
    {

    }

    private void DrinkAt(Vector3 location)
    {

    }

    private void EatAt(Vector3 location)
    {

    }

    //Update for timer
    private void Update()
    {
        if (timerOn)
        {
            timerText.text = "" + timer.ToString("F2");
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                //Mission failed
                timerOn = false;
            }
        }
    }

}

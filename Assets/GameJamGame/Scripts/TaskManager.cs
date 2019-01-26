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

    public GameObject taskBox;

    public Transform doorTransform;

    private List<Task> tasks = new List<Task>();
    private Text taskText;
    private float timer = 0.0f;
    private int task;
    private bool completed = false;

    void Start () {

        //Get taskText:
        taskText = taskBox.GetComponentInChildren<Text>();

        //Fill tasks arraylist:
        tasks.Add(new Task("Bark at front door", 15f, BarkAt));
        tasks.Add(new Task("Eat some food", 20f, EatAt));
        tasks.Add(new Task("Drink some water", 20f, DrinkAt));

        GetNewTask(); //tasks all have their own text, times, and RULES
    }

    public void GetNewTask()
    {
        Task currentTask = TaskRandomizer();
        currentTask.function(doorTransform.position);
        //start counting up to task timer
    }

    //Randomly picks a task from list
    private Task TaskRandomizer()
    {
        task = Random.Range(0, tasks.Count);
        taskText.text = tasks[task].description;
        print(tasks[task].description);
        return tasks[task];
    }

    private void TaskComplete()
    {
        completed = true;
        StartCoroutine(TaskDelay());
    }

    private IEnumerator TaskDelay()
    {
        yield return new WaitForSeconds(2f);
        completed = false;
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

	
}

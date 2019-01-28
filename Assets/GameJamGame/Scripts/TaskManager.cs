using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour {

    public class Task
    {
        public string description;
        public Texture image;
        public Transform target;
        public float timer;

        public Task(string description, Texture image, float timer, Transform target)
        {
            this.description = description;
            this.image = image;
            this.timer = timer;
            this.target = target;
        }
    }

    //TIMED TASKS THE PLAYER MUST DO

    //UI
    private GameObject taskBox;
    private Animator iconAnimator;
    public Texture foodImage;
    public Texture waterImage;
    public Texture barkCatImage;
    public Texture fireHydrantImage;
    private GameObject currentImageObject;
    private RawImage currentImage;
    private RectTransform timerBar;
    private Vector3 startWidth = new Vector3(1, 1, 1);
    private Vector3 endWidth = new Vector3(0, 1, 1);
    private bool timerOn = true;
    private Image gameOverScreen;
    private bool gameOver = false;
    private bool isPaused = false;
    private Text happyCounterText;
    public int happyCounter;
    private GameObject gameOverPanel;

    //Positions
    private Transform catPos;
    private Transform waterBowlPos;
    private Transform foodBowlPos;
    public static string taskLocation = "";

    //Tasks
    private Task currentTask;
    private List<Task> tasks = new List<Task>();
    private int task;

    //Misc
    private Camera cam;
    private Canvas canvas;

    void Start () {
        EventBus.AddListener<PauseEvent>(HandleEvent);
        EventBus.AddListener<HappyEvent>(HandleEvent);

        cam = Camera.main;
        canvas = transform.GetChild(0).GetComponent<Canvas>();

        //set-up transforms
        catPos = GameObject.FindGameObjectWithTag("Cat").GetComponent<Transform>();
        waterBowlPos = GameObject.FindGameObjectWithTag("WaterBowl").GetComponent<Transform>();
        foodBowlPos = GameObject.FindGameObjectWithTag("FoodBowl").GetComponent<Transform>();

        //Get UI:
        currentImageObject = GameObject.FindGameObjectWithTag("TaskImage");
        currentImage = currentImageObject.GetComponent<RawImage>();
        taskBox = GameObject.FindGameObjectWithTag("TaskBox");
        timerBar = GameObject.FindGameObjectWithTag("TimerBar").GetComponent<RectTransform>();
        iconAnimator = taskBox.GetComponentInChildren<Animator>();
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen").GetComponent<Image>();
        happyCounterText = GameObject.FindGameObjectWithTag("HappyCounter").GetComponent<Text>();
        gameOverPanel = GameObject.FindGameObjectWithTag("GameOver");

        //Fill tasks arraylist:
        tasks.Add(new Task("barkcat", barkCatImage, 15f, GameObject.FindGameObjectWithTag("Cat").GetComponent<Transform>()));
        tasks.Add(new Task("food", foodImage, 20f, GameObject.FindGameObjectWithTag("FoodBowl").GetComponent<Transform>()));
        tasks.Add(new Task("water", waterImage, 20f, GameObject.FindGameObjectWithTag("WaterBowl").GetComponent<Transform>()));
        tasks.Add(new Task("NewHydrant", fireHydrantImage, 20f, GameObject.FindGameObjectWithTag("NewHydrant").GetComponent<Transform>())); //fire hydrant pos?

        GetNewTask(); //tasks all have their own text, times, and RULES
    }

    public void GetNewTask()
    {
        currentTask = TaskRandomizer();
        print("New Task");
        currentImage.texture = currentTask.image;
        StartCoroutine(LerpTimer());
    }

    public void CheckTask(string action)
    {
        if (currentTask != null)
        {
            if (string.Compare(action, currentTask.description) == 0)
            {
                TaskComplete();
            }
        }
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
        currentImageObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        iconAnimator.SetBool("Urgent", false);
        StartCoroutine(TaskDelay());
    }

    private IEnumerator TaskDelay()
    {
        yield return new WaitForSeconds(1.8f);
        timerOn = true;
        GetNewTask();
        yield return new WaitForSeconds(0.1f);
        taskBox.SetActive(true); //show task to player
    }

    IEnumerator LerpTimer()
    {
        float progress = 0;
        float speed = currentTask.timer / 500;

        while (progress <= 1)
        {
            while (isPaused)
            {
                yield return null;
            }

            timerBar.localScale = Vector3.Lerp(startWidth, endWidth, progress);
            progress += Time.deltaTime * speed;
            yield return null;

            if (timerOn == false)
            {
                yield break;
            }

            if (progress >= 0.5)
            {
                iconAnimator.SetBool("Urgent", true);
            }

        }
        timerBar.localScale = endWidth;
        GameOver();
    }

    public void GameOver()
    {
        timerOn = false;
        gameOver = true;
        taskBox.SetActive(false);
        StartCoroutine(GameOverDelay());
        EventBus.Emit<GameOverEvent>(new GameOverEvent()); //game over
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1f);
        gameOverPanel.SetActive(true);
    }

    public void ResetGame() //for call by the death screen button
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (gameOver)
        {
            //GAME OVER
            Color color = gameOverScreen.color;
            color.a = Mathf.MoveTowards(color.a, 255, Time.deltaTime);
            gameOverScreen.color = color;
        }

        Vector3 view_pos = cam.WorldToScreenPoint(currentTask.target.position);
        currentImageObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            view_pos.x,
            view_pos.y
        );
    }

    private void HandleEvent(PauseEvent msg)
    {
        isPaused = !isPaused;
    }

    private void HandleEvent(HappyEvent msg)
    {
        print("handled happiness");
        happyCounter++;
        happyCounterText.text = "Happy: " + happyCounter;
    }

}

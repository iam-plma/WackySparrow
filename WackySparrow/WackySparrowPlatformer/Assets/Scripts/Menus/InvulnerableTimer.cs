using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    public Slider invulnerableTimerSlider;
    public Text timerText;
    public float gameTime = 2f;

    private bool stopTimer;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        invulnerableTimerSlider.maxValue = gameTime;
        invulnerableTimerSlider.value = gameTime;
        currentTime = 0;


    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        float time = gameTime - currentTime;

        //string textTime = time.ToString("0");

        if(time <= 0)
        {
            stopTimer = true;
            Destroy(gameObject);
        }

        if(stopTimer == false)
        {
            //timerText.text = textTime;
            invulnerableTimerSlider.value = time;
        }

    }
}

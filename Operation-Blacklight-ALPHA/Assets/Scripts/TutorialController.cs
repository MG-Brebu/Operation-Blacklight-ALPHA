using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private float pause = 0f;
    private float play = 1f;
    public GameObject tutorial;

    void Start()
    {
        Time.timeScale = pause;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F DOWN");
            Time.timeScale = play;
            tutorial.SetActive(false);
        }
    }
}

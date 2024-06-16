using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI passedLabel;
    [SerializeField]
    private GameObject alert;
    [SerializeField]
    private TMPro.TextMeshProUGUI alertLabel;
    [SerializeField]
    private GameObject lifePrefab;

    private Rigidbody2D rigidBody; // Посилання на інший компонент того ж ГО, що й скріпт
    private int score;
    private int lives;
    private bool needClear;
    private bool gameover;

    void Start()
    {
        Debug.Log("BirdScript Start");
        // пошук компоненту та одержання посилання на нього
        rigidBody = GetComponent<Rigidbody2D>();
        score = 0;
        lives = 3;
        needClear = false;
        gameover = false;
        HideAlert();
        DrawLives(lives);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(new Vector2(0, 300) * Time.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (alert.activeSelf)
            {
                HideAlert();
            }
            else
            {
                ShowAlert("Paused");
            }
        }
    }

    /* Подія, що виникає при перетині колайдерів-тригерів  */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Collision!! " + other.gameObject.name);
            rigidBody.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ShowAlert("UOOPS!");
            needClear = true;

            if (lives > 1)
            {
                DrawLives(--lives);
            }
            else
            {
                ShowAlert("Game over!");
                gameover = true;
            }
        }   
        
        if(other.gameObject.CompareTag("bonus"))
        {
            Debug.Log("Bonus!");
            Destroy(other.gameObject);
            DrawLives(++lives);
        }
    }
    /* Подія, що виникає при роз'єднанні колайдерів-тригерів  */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pass"))
        {
            Debug.Log("+1");
            score++;
            passedLabel.text = score.ToString("D3");
        }
    }

    private void ShowAlert(string message)
    {
        alert.SetActive(true);
        alertLabel.text = message;
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void HideAlert()
    {
        if (gameover)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        alert.SetActive(false);
        Time.timeScale = 1f;
        rigidBody.transform.localScale = new Vector3(1f, 1f, 1f);

        if (needClear)
        {
            foreach (var pipe in GameObject.FindGameObjectsWithTag("Pass"))
            {
                GameObject.Destroy(pipe);
            }
            needClear = false;
        }
    }

    private void DrawLives (int lives)
    {

        foreach(var life in GameObject.FindGameObjectsWithTag("life"))
        {
            Destroy(life);
        }
        

        for (int i = 0; i < lives; i++)
        {
            var life = Instantiate(lifePrefab);
            life.transform.position = new Vector3(6.2f - 0.5f * i, 3.2f, 0);
        }
    }
}

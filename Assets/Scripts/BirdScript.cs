using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BirdScript : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI passedLabel;
    [SerializeField]
    private GameObject alert;
    [SerializeField]
    private TMPro.TextMeshProUGUI alertLabel;

    private Rigidbody2D rigidBody; // Посилання на інший компонент того ж ГО, що й скріпт
    private int score;
    private bool needClear;
    void Start()
    {
        Debug.Log("BirdScript Start");
        // пошук компоненту та одержання посилання на нього
        rigidBody = GetComponent<Rigidbody2D>();
        score = 0;
        needClear = false;
        HideAlert();
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
            ShowAlert("UOOPS!");
            needClear = true;
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
        alert.SetActive(false);
        Time.timeScale = 1f;

        if (needClear)
        {
            foreach (var pipe in GameObject.FindGameObjectsWithTag("Pass"))
            {
                GameObject.Destroy(pipe);
            }
            needClear = false;
        }
    }
}

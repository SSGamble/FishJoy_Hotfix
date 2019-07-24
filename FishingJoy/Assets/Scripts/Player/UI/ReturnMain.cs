using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ReturnMain : MonoBehaviour {

    private Button but;

    // Use this for initialization
    void Start()
    {
        but = GetComponent<Button>();
        but.onClick.AddListener(StartGames);
    }

    public void StartGames()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}


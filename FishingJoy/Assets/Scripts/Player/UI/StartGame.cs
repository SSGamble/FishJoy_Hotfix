using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    private Button but;

    void Start() {
        but = GetComponent<Button>();
        but.onClick.AddListener(StartGames);
    }

    private void StartGames() {
        SceneManager.LoadScene(1);
    }

    void Update() {

    }
}

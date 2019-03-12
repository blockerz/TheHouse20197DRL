using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataCollection : MonoBehaviour
{
    public InputField nameField;
    public Slider difficultySlider;
    private int difficulty = 1;

    // Start is called before the first frame update
    void Start()
    {
        nameField.text = GameData.Data.PlayerName;
        difficultySlider.value = GameData.Data.Difficulty;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            PlayPressed();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void PlayPressed()
    {
        GameData.Data.PlayerName = nameField.text;
        GameData.Data.Difficulty = (int)difficultySlider.value;
        SceneManager.LoadScene("MainScene");

    }
}

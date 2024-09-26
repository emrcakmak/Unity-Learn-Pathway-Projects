using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;
    private Button button;
    private GameManager gm;
    public GameObject diffPanel;
    void Start()
    {
        button = GetComponent<Button>();
        gm = FindObjectOfType<GameManager>();

        button.onClick.AddListener(SetDifficulty);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetDifficulty()
    {
        gm.StartGame(difficulty);
        diffPanel.SetActive(false);
    }
}

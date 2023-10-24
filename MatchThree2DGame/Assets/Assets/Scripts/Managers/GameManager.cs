using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _destroyCandyText;
    private int _score = 0;
    private int _destroyCandySize = 0;
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    public void ScoreIncrease(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }
    public void DestroyCandyIncrease(int destroyCandySize)
    {
        _destroyCandySize += destroyCandySize;
        _destroyCandyText.text = _destroyCandySize.ToString();
    }
}

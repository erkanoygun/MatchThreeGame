using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    private int _destroyCandySize = 0;
    public static GameManager instance;

    [Header("GUI")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _destroyCandyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

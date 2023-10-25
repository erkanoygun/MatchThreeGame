using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 6;
    public int height = 8;
    [SerializeField] GameObject[] _dotPrefabs;
    [SerializeField] private GameObject _backGroundTilePrefab;
    static public GameObject[,] allDots;

    public bool isDragible = true;
    private EffectController _effectCtrlScr;
    private SoundEffectController _soundEffectCtrlScr;

    Coroutine _myCorutine;

    void Start()
    {
        allDots = new GameObject[width, height];
        _effectCtrlScr = GetComponent<EffectController>();
        _soundEffectCtrlScr = GetComponent<SoundEffectController>();
        Setup();
    }

    private void Update()
    {
        DotMoveDownSlot();

        if (_myCorutine == null)
            _myCorutine = StartCoroutine(CreateNewDotEnumerator());

    }

    IEnumerator CreateNewDotEnumerator()
    {
        yield return new WaitForSeconds(0.3f);
        CreateNewDot();

    }

    private void CreateNewDot()
    {
        for (int i = 0; i < width; i++)
        {
            if (allDots[i, (height - 1)] == null)
            {

                int dotToUse;

                dotToUse = Random.Range(0, _dotPrefabs.Length);
                Vector2 dotPosition = new Vector2(i, (height - 1));

                if (i > 0)
                {
                /*We prevent randomly generated tiles in the top row from having the same triplet next to each other.*/
                    while (_dotPrefabs[dotToUse].tag == allDots[(i - 1), (height - 1)].tag)
                    {
                        dotToUse = Random.Range(0, _dotPrefabs.Length);
                    }
                }

                GameObject dot = Instantiate(_dotPrefabs[dotToUse], dotPosition, Quaternion.identity);
                dot.transform.parent = transform;
                dot.name = $"({i},{(height - 1)})";
                allDots[i, (height - 1)] = dot;
            }
        }
        _myCorutine = null;
    }

    /*We slide the newly created candies into the empty slots below.*/
    private void DotMoveDownSlot()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject dot = allDots[i, j];
                if (j < height && j > 0)
                {
                    GameObject downDot = allDots[i, j - 1];
                    if (downDot == null && allDots[i, j] != null)
                    {
                        allDots[i, j - 1] = allDots[i, j];
                        Vector2 newPos = new Vector2(allDots[i, j].transform.position.x, j - 1);
                        allDots[i, j].GetComponent<Dot>().targetPos = newPos;
                        allDots[i, j] = null;
                        isDragible = false;
                    }
                }
            }
        }
    }

    private void Setup()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 backgroundTilePos = new Vector2(i, j);
                GameObject backGroundPrfb = Instantiate(_backGroundTilePrefab, backgroundTilePos, Quaternion.identity);
                backGroundPrfb.transform.parent = transform;
                backGroundPrfb.name = $"bg_({i},{j})";
            }
        }


        GameObject dot;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //int lenght = Random.Range(0,5);
                int dotToUse = Random.Range(0, _dotPrefabs.Length);
                do
                {
                    dotToUse = Random.Range(0, _dotPrefabs.Length);
                } while ((i > 0 && _dotPrefabs[dotToUse].tag == allDots[i - 1, j].tag) || (j > 0 && _dotPrefabs[dotToUse].tag == allDots[i, j - 1].tag));

                Vector2 dotPosition = new Vector2(i, j);

                dot = Instantiate(_dotPrefabs[dotToUse], dotPosition, Quaternion.identity);
                dot.transform.parent = transform;
                dot.name = $"({i},{j})";
                allDots[i, j] = dot;

            }
        }
    }

    private void DestroyDot(float posX, float posY)
    {
        GameObject _dot = Board.allDots[(int)posX, (int)posY];

        ChangeUIandEffectTrigger(_dot);

        Destroy(_dot);
        Board.allDots[(int)posX, (int)posY] = null;
    }

    public void GetMatchDot()
    {
        _soundEffectCtrlScr.PlaySoundEffect(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject _dot = Board.allDots[i, j];
                if (_dot != null && _dot.GetComponent<Dot>().isMatched)
                {
                    DestroyDot(_dot.transform.position.x, _dot.transform.position.y);
                }
            }
        }
    }

    private void ChangeUIandEffectTrigger(GameObject _dot)
    {
        _effectCtrlScr.CandyFragmentationEffect(_dot);
        _effectCtrlScr.PointTextEffect(10, _dot);
        GameManager.instance.ScoreIncrease(10);
        GameManager.instance.DestroyCandyIncrease(1);
    }
}

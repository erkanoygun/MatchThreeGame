using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 6;
    public int height = 8;
    [SerializeField] GameObject[] _dotPrefabs;
    [SerializeField] GameObject testPrefabDot, testPrefabDot2;
    static public GameObject[,] allDots;

    public bool isDragible = true;

    Coroutine _myCorutine;

    void Start()
    {
        allDots = new GameObject[width, height];
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
        yield return new WaitForSeconds(.3f);
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
        GameObject dot;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {

                int dotToUse = Random.Range(0, _dotPrefabs.Length);
                //int dotToUse;
                do
                {
                    dotToUse = Random.Range(0, _dotPrefabs.Length);
                } while ((i > 0 && _dotPrefabs[dotToUse].tag == allDots[i - 1, j].tag) || (j > 0 && _dotPrefabs[dotToUse].tag == allDots[i, j - 1].tag));

                Vector2 dotPosition = new Vector2(i, j);

                /*
                if (i == 0 && j == 7)
                    dot = Instantiate(testPrefabDot, dotPosition, Quaternion.identity);
                else if (i == 1 && j == 7)
                    dot = Instantiate(testPrefabDot, dotPosition, Quaternion.identity);
                else if (i == 2 && j == 6)
                    dot = Instantiate(testPrefabDot, dotPosition, Quaternion.identity);
                else if (i == 3 && j == 7)
                    dot = Instantiate(testPrefabDot, dotPosition, Quaternion.identity);

                else if (i == 2 && j == 7)
                    dot = Instantiate(testPrefabDot2, dotPosition, Quaternion.identity);
                else
                    dot = Instantiate(_dotPrefabs[dotToUse], dotPosition, Quaternion.identity);*/

                dot = Instantiate(_dotPrefabs[dotToUse], dotPosition, Quaternion.identity);
                dot.transform.parent = transform;
                dot.name = $"({i},{j})";
                allDots[i, j] = dot;

            }
        }
    }

    private void DestroyDot(float posX, float posY)
    {
        Destroy(Board.allDots[(int)posX, (int)posY]);
        Board.allDots[(int)posX, (int)posY] = null;
    }

    public void GetMatchDot()
    {
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
}

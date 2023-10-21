using UnityEngine;

public class FindMatches : MonoBehaviour
{

    private GameObject _boardGO;
    private Board _boardScript;
    private Dot _dotScr;

    private void Start() {
        _boardGO = GameObject.FindWithTag("Board");
        _boardScript = _boardGO.GetComponent<Board>();
        _dotScr = GetComponent<Dot>();
    }


    public void FindMatchesDot()
    {

        GameObject otherDotRight;
        GameObject otherDotLeft;
        GameObject otherDotTop;
        GameObject otherDotDown;

        if (gameObject.transform.position.x < (_boardScript.width - 1f) && gameObject.transform.position.x > 0)
        {
            if (!_dotScr.dotIsMoving)
            {
                otherDotRight = Board.allDots[(int)gameObject.transform.position.x + 1, (int)gameObject.transform.position.y];
                otherDotLeft = Board.allDots[(int)gameObject.transform.position.x - 1, (int)gameObject.transform.position.y];

                if (otherDotRight != null && otherDotLeft != null)
                {
                    if (otherDotRight.tag == gameObject.tag && otherDotLeft.tag == gameObject.tag)
                    {
                        _dotScr.isMatched = true;
                        otherDotRight.GetComponent<Dot>().isMatched = true;
                        otherDotLeft.GetComponent<Dot>().isMatched = true;
                    }
                }

            }
        }

        if (gameObject.transform.position.y < (_boardScript.height - 1) && gameObject.transform.position.y > 0)
        {
            if (!_dotScr.dotIsMoving)
            {
                otherDotTop = Board.allDots[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y + 1];
                otherDotDown = Board.allDots[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y - 1];

                if (otherDotTop != null && otherDotDown != null)
                {
                    if (otherDotTop.tag == gameObject.tag && otherDotDown.tag == gameObject.tag)
                    {
                        _dotScr.isMatched = true;
                        otherDotTop.GetComponent<Dot>().isMatched = true;
                        otherDotDown.GetComponent<Dot>().isMatched = true;
                    }
                }

            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private Vector2 startMousePos;
    private Vector2 endMousePos;

    public Vector2 targetPos;
    
    private float _dotMoveSpeed = 15f;
    public float mouseDownDotPosX;
    public float mouseDownDotPosY;
    public bool dotIsMoving = false;
    public bool isMatched;

    private DotDragController _dotDragCtrlScr;
    private GameObject _boardGO;
    private Board _boardScript;


    void Start()
    {
        targetPos = transform.position;
        _boardGO = GameObject.FindWithTag("Board");
        _boardScript = _boardGO.GetComponent<Board>();
        _dotDragCtrlScr = GetComponent<DotDragController>();
    }

    private void Update()
    {
        /*We make sure not to make a match check while the stone is moving.*/
        if (_boardScript.isDragible)
            FindMatches();

        DotMove();

        /*Even if "isMatched" is provided, we are waiting for the end of the dragging action so that all 
        objects next to each other disappear at the same time, we control this situation with the 
        "_boardScript.isDragible" variable that ensures that the new object is draggable.*/
        if (isMatched && _boardScript.isDragible)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.2f);
            StartCoroutine(DotDestroyMethodEnumerator());
        }

    }

    private void DotMove()
    {
        if (Mathf.Abs(targetPos.x - transform.position.x) > 0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, _dotMoveSpeed * Time.deltaTime);
            dotIsMoving = true;
        }
        else if (Mathf.Abs(targetPos.y - transform.position.y) > 0.02f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, _dotMoveSpeed * Time.deltaTime);
            dotIsMoving = true;
        }
        else
        {
            transform.position = new Vector2(targetPos.x, targetPos.y);
            if (dotIsMoving)
            {
                _boardScript.isDragible = true;
                dotIsMoving = false;
                StartCoroutine(DotBackOldPositionCheck());
            }

        }
    }

    /*When the same tiles are next to each other, we wait for 0.1 seconds to make sure that the 
    "FindMatches()" method is running and the "isMatch" values are "true" for all tiles with the 
    same tag before the tiles start to disappear.
    */
    IEnumerator DotDestroyMethodEnumerator()
    {
        yield return new WaitForSeconds(.1f);
        _boardScript.GetMatchDot();
    }

    IEnumerator DotBackOldPositionCheck()
    {
        yield return new WaitForSeconds(0.1f);
        if (!isMatched && _dotDragCtrlScr.otherGO != null)
        {
            if (!_dotDragCtrlScr.otherGO.GetComponent<Dot>().isMatched)
            {
                if (_dotDragCtrlScr.dragDirection == DragDirection.RightDrag) _dotDragCtrlScr.DragDot(DragDirection.LeftDrag);
                else if (_dotDragCtrlScr.dragDirection == DragDirection.LeftDrag) _dotDragCtrlScr.DragDot(DragDirection.RightDrag);
                else if (_dotDragCtrlScr.dragDirection == DragDirection.TopDrag) _dotDragCtrlScr.DragDot(DragDirection.DownDrag);
                else if (_dotDragCtrlScr.dragDirection == DragDirection.DownDrag) _dotDragCtrlScr.DragDot(DragDirection.TopDrag);
                _dotDragCtrlScr.otherGO = null;
            }
        }

    }

    private void OnMouseDown()
    {
        if (_boardScript.isDragible)
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mouseDownDotPosX = gameObject.transform.position.x;
            mouseDownDotPosY = gameObject.transform.position.y;
        }

    }

    private void OnMouseUp()
    {
        if (_boardScript.isDragible)
        {
            endMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _dotDragCtrlScr.CalculateDragDirection(endMousePos, startMousePos);
        }

    }

    private void FindMatches()
    {

        GameObject otherDotRight;
        GameObject otherDotLeft;
        GameObject otherDotTop;
        GameObject otherDotDown;

        if (gameObject.transform.position.x < (_boardScript.width - 1f) && gameObject.transform.position.x > 0)
        {
            if (!dotIsMoving)
            {
                otherDotRight = Board.allDots[(int)gameObject.transform.position.x + 1, (int)gameObject.transform.position.y];
                otherDotLeft = Board.allDots[(int)gameObject.transform.position.x - 1, (int)gameObject.transform.position.y];

                if (otherDotRight != null && otherDotLeft != null)
                {
                    if (otherDotRight.tag == gameObject.tag && otherDotLeft.tag == gameObject.tag)
                    {
                        isMatched = true;
                        otherDotRight.GetComponent<Dot>().isMatched = true;
                        otherDotLeft.GetComponent<Dot>().isMatched = true;
                    }
                }

            }
        }

        if (gameObject.transform.position.y < (_boardScript.height - 1) && gameObject.transform.position.y > 0)
        {
            if (!dotIsMoving)
            {
                otherDotTop = Board.allDots[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y + 1];
                otherDotDown = Board.allDots[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y - 1];

                if (otherDotTop != null && otherDotDown != null)
                {
                    if (otherDotTop.tag == gameObject.tag && otherDotDown.tag == gameObject.tag)
                    {
                        isMatched = true;
                        otherDotTop.GetComponent<Dot>().isMatched = true;
                        otherDotDown.GetComponent<Dot>().isMatched = true;
                    }
                }

            }
        }

    }


}

using UnityEngine;

public class DotDragController : MonoBehaviour
{
    public DragDirection dragDirection;
    public GameObject otherGO;

    private Board _boardScript;
    private Dot _dotScr;
    private GameObject _boardGO;
    private SoundEffectController _soundEffectCtrlScr;

    private void Start()
    {
        _boardGO = GameObject.FindWithTag("Board");
        _boardScript = _boardGO.GetComponent<Board>();
        _dotScr = GetComponent<Dot>();
        _soundEffectCtrlScr = _boardGO.GetComponent<SoundEffectController>();
    }
    public void CalculateDragDirection(Vector2 endMousePos, Vector2 startMousePos)
    {
        Vector2 dragDirection = endMousePos - startMousePos;

        // We calculate the angle in radians.
        float angleRadians = Mathf.Atan2(dragDirection.y, dragDirection.x);

        // We turn it into a degree.
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        
        if (Mathf.Abs(angleDegrees) > 0.1f)
        {
            if (angleDegrees > -45f && angleDegrees < 45f && _dotScr.mouseDownDotPosX < (_boardScript.width - 1))
            {
                this.dragDirection = DragDirection.RightDrag;
                DragDot(this.dragDirection);
            }
            else if ((angleDegrees > 135f || angleDegrees < -135f) && _dotScr.mouseDownDotPosX > 0)
            {
                this.dragDirection = DragDirection.LeftDrag;
                DragDot(this.dragDirection);
            }
            else if (angleDegrees > 45f && angleDegrees < 135f && _dotScr.mouseDownDotPosY < (_boardScript.height - 1))
            {
                this.dragDirection = DragDirection.TopDrag;
                DragDot(this.dragDirection);
            }
            else if (angleDegrees < -45f && angleDegrees > -135f && _dotScr.mouseDownDotPosY > 0)
            {
                this.dragDirection = DragDirection.DownDrag;
                DragDot(this.dragDirection);
            }
        }


    }

    public void DragDot(DragDirection dragDirection)
    {

        if (dragDirection == DragDirection.RightDrag)
        {
            _dotScr.mouseDownDotPosX += 1;
            otherGO = Board.allDots[(int)_dotScr.mouseDownDotPosX, (int)gameObject.transform.position.y];

            /*We perform this check to avoid the error of a tile being replaced by another tile that is 
            more than one unit away from it for various reasons.*/
            if (Vector2.Distance(otherGO.transform.position, gameObject.transform.position) == 1)
            {
                Board.allDots[(int)(gameObject.transform.position.x), (int)gameObject.transform.position.y] = otherGO;
                otherGO.GetComponent<Dot>().targetPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

                Board.allDots[(int)(_dotScr.mouseDownDotPosX), (int)gameObject.transform.position.y] = gameObject;
                _dotScr.targetPos = new Vector2(_dotScr.mouseDownDotPosX, gameObject.transform.position.y);

                _boardScript.isDragible = false;

                _soundEffectCtrlScr.PlaySoundEffect(0);
            }
        }

        else if (dragDirection == DragDirection.LeftDrag)
        {
            _dotScr.mouseDownDotPosX -= 1;
            otherGO = Board.allDots[(int)_dotScr.mouseDownDotPosX, (int)gameObject.transform.position.y];

            if (Vector2.Distance(otherGO.transform.position, gameObject.transform.position) == 1)
            {
                Board.allDots[(int)(gameObject.transform.position.x), (int)gameObject.transform.position.y] = otherGO;
                otherGO.GetComponent<Dot>().targetPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

                Board.allDots[(int)(_dotScr.mouseDownDotPosX), (int)gameObject.transform.position.y] = gameObject;
                _dotScr.targetPos = new Vector2(_dotScr.mouseDownDotPosX, gameObject.transform.position.y);

                _boardScript.isDragible = false;

                _soundEffectCtrlScr.PlaySoundEffect(0);
            }
        }

        else if (dragDirection == DragDirection.TopDrag)
        {
            _dotScr.mouseDownDotPosY += 1;
            otherGO = Board.allDots[(int)gameObject.transform.position.x, (int)_dotScr.mouseDownDotPosY];

            if (Vector2.Distance(otherGO.transform.position, gameObject.transform.position) == 1)
            {
                Board.allDots[(int)gameObject.transform.position.x, (int)(gameObject.transform.position.y)] = otherGO;
                otherGO.GetComponent<Dot>().targetPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

                Board.allDots[(int)(gameObject.transform.position.x), (int)(_dotScr.mouseDownDotPosY)] = gameObject;
                _dotScr.targetPos = new Vector2(gameObject.transform.position.x, _dotScr.mouseDownDotPosY);

                _boardScript.isDragible = false;

                _soundEffectCtrlScr.PlaySoundEffect(0);
            }

        }

        else if (dragDirection == DragDirection.DownDrag)
        {
            _dotScr.mouseDownDotPosY -= 1;
            otherGO = Board.allDots[(int)gameObject.transform.position.x, (int)_dotScr.mouseDownDotPosY];

            if (Vector2.Distance(otherGO.transform.position, gameObject.transform.position) == 1)
            {
                Board.allDots[(int)gameObject.transform.position.x, (int)(gameObject.transform.position.y)] = otherGO;
                otherGO.GetComponent<Dot>().targetPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

                Board.allDots[(int)(gameObject.transform.position.x), (int)(_dotScr.mouseDownDotPosY)] = gameObject;
                _dotScr.targetPos = new Vector2(gameObject.transform.position.x, _dotScr.mouseDownDotPosY);

                _boardScript.isDragible = false;

                _soundEffectCtrlScr.PlaySoundEffect(0);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class Swipe : Singleton<Swipe>
{
    Vector3 first;
    Vector3 currentPosition;
    Tiles currentTile;
    Match match;

    public void Awake()
    {
        match = FindObjectOfType<Match>().GetComponent<Match>();
    }
    public void Select(Tiles tile)
    {
        if (currentTile != null) return;
        currentTile = tile;
        first = Input.mousePosition;
        currentPosition = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, currentTile.transform.position.z);
    }

    public void unSelect()
    {
        if (currentTile == null) return;
        bool isDrag = isDragging(Input.mousePosition);
        currentTile.transform.position = currentPosition;
        if (isDrag)
        {
            Position position = currentTile.pos;
            switch (CalculateAngle(first, Input.mousePosition))
            {
                case Direction.Up:
                    if (currentTile.pos.y == Board.Instance.columns - 1) return;
                    StartCoroutine(match.Action(Board.Instance.tiles[position.x,position.y], Board.Instance.tiles[position.x, position.y+1]));
                    break;
                case Direction.Down:
                    if (currentTile.pos.y == 0) return;
                    StartCoroutine(match.Action(Board.Instance.tiles[position.x, position.y], Board.Instance.tiles[position.x, position.y - 1]));
                    break;
                case Direction.Left:
                    if (currentTile.pos.x == 0) return;
                    StartCoroutine(match.Action(Board.Instance.tiles[position.x, position.y], Board.Instance.tiles[position.x - 1, position.y]));
                    break;
                case Direction.Right:
                    if (currentTile.pos.x == Board.Instance.rows - 1) return;
                    StartCoroutine(match.Action(Board.Instance.tiles[position.x, position.y], Board.Instance.tiles[position.x + 1, position.y]));
                    break;
            }
        }
        currentTile = null;
        Match3State.Instance.state = Match3State.State.Null;
    }
    private void Update()
    {
        bool isDrag = isDragging(Input.mousePosition);
        if (Match3State.Instance.state == Match3State.State.Select && isDrag)
        {
            switch (CalculateAngle(first, Input.mousePosition))
            {
                case Direction.Up:
                    if (currentTile.pos.y == Board.Instance.columns - 1) return;
                    currentTile.transform.position = new Vector3(currentPosition.x,currentPosition.y+.25f,currentPosition.z);
                    break;
                case Direction.Down:
                    if (currentTile.pos.y == 0) return;
                    currentTile.transform.position = new Vector3(currentPosition.x, currentPosition.y-.25f, currentPosition.z);
                    break;
                case Direction.Left:
                    if (currentTile.pos.x == 0) return;
                    currentTile.transform.position = new Vector3(currentPosition.x-.25f, currentPosition.y, currentPosition.z);
                    break;
                case Direction.Right:
                    if (currentTile.pos.x == Board.Instance.rows - 1) return;
                    currentTile.transform.position = new Vector3(currentPosition.x+.25f, currentPosition.y, currentPosition.z);
                    break;
            }
        }
        else if (Match3State.Instance.state == Match3State.State.Select && !isDrag)
        {
            currentTile.transform.position = currentPosition;
        }
    }

    Direction CalculateAngle(Vector3 first, Vector3 final)
    {
        float angle;
        angle = Mathf.Atan2(final.y - first.y, final.x - first.x) * 180 / Mathf.PI;
        if (angle <= 135 && angle > 45)
        {
            return Direction.Up;
        }
        else if (angle <= 45 && angle > -45)
        {
            return Direction.Right;
        }
        else if (angle <= -45 && angle > -135)
        {
            return Direction.Down;
        }
        else
        {
            return Direction.Left;
        }
    }

    bool isDragging(Vector3 inputMouse)
    {
        Vector3 dir = ((Vector3)inputMouse - first);
        if (dir.magnitude > 32) return true;
        return false;
    }
}

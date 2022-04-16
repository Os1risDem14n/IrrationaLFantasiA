using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    private bool matchFound = false;
    private bool isSwapBack = true;
    private enum Directions
    {
        up,
        down,
        right,
        left
    }

    private Direction Direction;
    public IEnumerator Action(GameObject _1,GameObject _2)
    {
        isSwapBack = true;
        yield return StartCoroutine(Swap(_1, _2));
        StartCoroutine(ClearMatch(_2));
        StartCoroutine(ClearMatch(_1));
        if (isSwapBack) StartCoroutine(Swap(_1, _2));
    }

    public IEnumerator Swap(GameObject object1,GameObject object2)
    {
        Tiles tile1 = object1.GetComponent<Tiles>();
        Tiles tile2 = object2.GetComponent<Tiles>();
        Sprite tempSprite = tile1.render.sprite;
        Board.ItemID tempID = tile1.ID;

        tile1.UpdateInfo(tile2.ID, tile2.render.sprite);
        tile2.UpdateInfo(tempID, tempSprite);

        IEnumerator swapAnim = null;
        if (swapAnim != null)
        {
            StopCoroutine(swapAnim);
        }
        swapAnim = SwapAnim(object1, object2, 0.1f);
        StartCoroutine(swapAnim);
        yield return null;
    }

    private IEnumerator SwapAnim(GameObject startobj, GameObject endobj, float time)
    {
        GameObject clone1 = Instantiate(startobj);
        GameObject clone2 = Instantiate(endobj);

        clone1.transform.position = startobj.transform.position;
        clone2.transform.position = endobj.transform.position;

        Vector3 startPos = clone1.transform.position;
        Vector3 endPos = clone2.transform.position;

        startobj.GetComponent<SpriteRenderer>().enabled = false;
        endobj.GetComponent<SpriteRenderer>().enabled = false;

        for (float t = 0; t <= 1 * time; t += Time.deltaTime)
        {
            clone1.transform.position = Vector3.Lerp(startPos, endPos, t / time);
            yield return null;
        }
        clone1.transform.position = clone2.transform.position;

        for (float t = 0; t <= 1 * time; t += Time.deltaTime)
        {
            clone2.transform.position = Vector3.Lerp(endPos, startPos, t / time);
            yield return null;
        }
        clone2.transform.position = clone1.transform.position;

        Destroy(clone1);
        Destroy(clone2);

        startobj.GetComponent<SpriteRenderer>().enabled = true;
        endobj.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(time + 0.1f);

    }

    private List<GameObject> FindMatch(Direction direction, GameObject current)
    {
        List<GameObject> matched = new List<GameObject>();
        List<GameObject> hit = new List<GameObject>();
        hit = ReturnListObjectDirections(current, direction);
        if (hit.Count > 0)
        {
            for (int i = 0; i < hit.Count; i++)
            {
                if (hit[i].GetComponent<Tiles>().ID == current.GetComponent<Tiles>().ID)
                {
                    matched.Add(hit[i]);
                }
                else
                {
                    break;
                }
            }
        }
        return matched;
    }

    private DefineMatch CheckMatch(GameObject current)
    {
        List<GameObject> vertical = new List<GameObject>();
        List<GameObject> horizontal = new List<GameObject>(); 

        vertical.AddRange(FindMatch(Direction.Up, current));
        vertical.AddRange(FindMatch(Direction.Down, current));

        horizontal.AddRange(FindMatch(Direction.Right, current));
        horizontal.AddRange(FindMatch(Direction.Left, current));

        if (vertical.Count < 2 && horizontal.Count < 2)
        {
            matchFound = false;
            return new DefineMatch(DefineMatch.Type.None, null);
        }
        else
        {
            matchFound = true;
            if (vertical.Count >= 2 && horizontal.Count >= 2)
            {
                List<GameObject> matched = new List<GameObject>();
                matched.AddRange(vertical);
                matched.AddRange(horizontal);
                matched.Add(current);

                if (vertical.Count == 4 || horizontal.Count == 4)
                    return new DefineMatch(DefineMatch.Type._5x3, matched);
                if (vertical.Count == 3 || horizontal.Count == 3)
                    return new DefineMatch(DefineMatch.Type._4x3, matched);
                return new DefineMatch(DefineMatch.Type._3x3, matched);
            }
            else if (vertical.Count >= 2)
            {
                List<GameObject> matched = new List<GameObject>();
                matched.AddRange(vertical);
                matched.Add(current);
                if (vertical.Count == 4)
                    return new DefineMatch(DefineMatch.Type._5x1, matched);
                if (vertical.Count == 3)
                    return new DefineMatch(DefineMatch.Type._4x1, matched);
                return new DefineMatch(DefineMatch.Type._3x1, matched);
            }
            else if (horizontal.Count >= 2)
            {
                List<GameObject> matched = new List<GameObject>();
                matched.AddRange(horizontal);
                matched.Add(current);
                if (horizontal.Count == 4)
                    return new DefineMatch(DefineMatch.Type._5x1, matched);
                if (horizontal.Count == 3)
                    return new DefineMatch(DefineMatch.Type._4x1, matched);
                return new DefineMatch(DefineMatch.Type._3x1, matched);
            }
        }
        return null;
    }

    public IEnumerator ClearMatch(GameObject current)
    {
        if (current.GetComponent<Tiles>().ID == Board.ItemID.Null)
            yield break;
        DefineMatch matched = CheckMatch(current);

        if (!matchFound)
        {
            yield break;
        }
          

        Board.ItemID itemID = matched.matched[0].GetComponent<Tiles>().ID;

        if (matched.type == DefineMatch.Type._5x3 || matched.type == DefineMatch.Type._4x3 || matched.type == DefineMatch.Type._3x3)
        {
            for (int i = 0; i < matched.matched.Count; i++)
            {
                matched.matched[i].GetComponent<Tiles>().ID = Board.ItemID.Null;
                matched.matched[i].GetComponent<Tiles>().render.sprite = null;
                matched.matched[i].GetComponent<Tiles>().ClearMatchAnim(0);          
            }
        }
        else if (matched.type == DefineMatch.Type._3x1)
        {
            for (int i = 0; i < matched.matched.Count; i++)
            {
                matched.matched[i].GetComponent<Tiles>().ClearMatchAnim(2);
                matched.matched[i].GetComponent<Tiles>().ID = Board.ItemID.Null;
                matched.matched[i].GetComponent<Tiles>().render.sprite = null;
            }
        }
        else if (matched.type == DefineMatch.Type._4x1 || matched.type == DefineMatch.Type._5x1)
        {
            for (int i = 0; i < matched.matched.Count; i++)
            {
                matched.matched[i].GetComponent<Tiles>().ClearMatchAnim(1);
                matched.matched[i].GetComponent<Tiles>().ID = Board.ItemID.Null;
                matched.matched[i].GetComponent<Tiles>().render.sprite = null;
            }
        }

        matchFound = false;
        isSwapBack = false;

        yield return new WaitForSeconds(0.2f);
  

        if (!BattleManager.Instance.isTransfered)
        {
            switch (matched.type)
            {
                case DefineMatch.Type._3x1:
                    StartCoroutine(BattleManager.Instance.PlayerTurn(itemID, 1));
                    break;
                case DefineMatch.Type._4x1:
                    StartCoroutine(BattleManager.Instance.PlayerTurn(itemID, 2));
                    break;
                case DefineMatch.Type._3x3:
                    StartCoroutine(BattleManager.Instance.PlayerTurn(itemID, 3));
                    break;
                case DefineMatch.Type._5x1:
                    StartCoroutine(BattleManager.Instance.PlayerTurn(itemID, 5));
                    break;
                case DefineMatch.Type._4x3:
                    StartCoroutine(BattleManager.Instance.PlayerTurn(itemID, 8));
                    break;
                case DefineMatch.Type._5x3:
                    StartCoroutine(BattleManager.Instance.PlayerTurn(itemID, 13));
                    break;
            }
            BattleManager.Instance.isTransfered = true;
        }
        

        yield return new WaitForSeconds(2f);
        StopCoroutine(Board.Instance.FindNullTiles());
        StartCoroutine(Board.Instance.FindNullTiles());
    }

    private List<GameObject> ReturnListObjectDirections(GameObject current, Direction direction)
    {
        List<GameObject> gameObjects = new List<GameObject>();
        int y = current.GetComponent<Tiles>().pos.y;
        int x = current.GetComponent<Tiles>().pos.x;
        switch (direction)
        {
            case Direction.Up:
                for (int i = y+1;i < Board.Instance.rows; i++)
                {
                    gameObjects.Add(Board.Instance.tiles[x, i]);
                }
                break;
            case Direction.Down:
                for (int i = y-1; i > -1; i--)
                {
                    gameObjects.Add(Board.Instance.tiles[x, i]);
                }
                break;
            case Direction.Left:
                for (int i = x - 1; i > -1; i--)
                {
                    gameObjects.Add(Board.Instance.tiles[i, y]);
                }
                break;
            case Direction.Right:
                for (int i = x + 1; i < Board.Instance.columns; i++)
                {
                    gameObjects.Add(Board.Instance.tiles[i, y]);
                }
                break;
        }
        return gameObjects;
    }
}

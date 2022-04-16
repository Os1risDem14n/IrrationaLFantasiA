using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Board.ItemID ID;

    private static Color selectedColor = new Color(.5f, .5f, .5f, 1f);
    private static Item previousSelected = null;
    private List<GameObject> moveableDir = new List<GameObject>();
    private SpriteRenderer render;
    private bool isSelected = false;
    private bool canSwap = false;
    private bool matchFound = false;
    private int matchCount = 0;
    private IEnumerator swapCoroutine;
    private Vector2[] Directions = new Vector2[]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Select()
    {
        if (BattleManager.Instance.state == BattleState.PLAYER)
        {
            isSelected = true;
            render.color = selectedColor;
            previousSelected = gameObject.GetComponent<Item>();
        }
    }

    private void unSelect()
    {
        isSelected = false;
        render.color = Color.white;
        previousSelected = null;
        matchFound = false;
        matchCount = 0;
    }

    private void OnMouseDown()
    {
        if (render.sprite == null || Board.Instance.IsShifting)
        {
            return;
        }

        if (isSelected)
        {
            unSelect();
        }
        else
        {
            if (previousSelected == null)
            {
                Select();
            }
            else
            {
                moveableDir = Moveable();
                if (moveableDir.Contains(gameObject))
                {
                    canSwap = true;
                }
                if (canSwap)
                {
                    Swap(previousSelected.gameObject);
                    Invoke("Action", 0.5f);
                }
                else
                {
                    previousSelected.unSelect();
                    Select();
                }
            }
        }
    }

    private void Action()
    {
        if (FindAllMatches())
        {
            previousSelected.ClearMatches();
            ClearMatches();
            previousSelected.unSelect();
        }
        else
        {
            unSwap(previousSelected.gameObject);
            previousSelected.unSelect();
        }
        canSwap = false;
    }
    private void Swap(GameObject gameObject2)
    {
        if (swapCoroutine != null)
        {
            StopCoroutine(swapCoroutine);
        }

        swapCoroutine = SwapCoroutine(gameObject, previousSelected.gameObject, 0.1f);
        StartCoroutine(swapCoroutine);

        Sprite temp = gameObject2.GetComponent<SpriteRenderer>().sprite;
        gameObject2.GetComponent<SpriteRenderer>().sprite = render.sprite;
        render.sprite = temp;

        Board.ItemID tempID = gameObject2.GetComponent<Item>().ID;
        gameObject2.GetComponent<Item>().ID = ID;
        ID = tempID;
    }

    private void unSwap(GameObject gameObject2)
    {

        Sprite temp = render.sprite;
        render.sprite = gameObject2.GetComponent<SpriteRenderer>().sprite;
        gameObject2.GetComponent<SpriteRenderer>().sprite = temp;

        Board.ItemID tempID = ID;
        ID = gameObject2.GetComponent<Item>().ID;
        gameObject2.GetComponent<Item>().ID = tempID;


    }

    private GameObject MoveableDir(Vector2 dir)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(previousSelected.transform.position, dir);
        if (hit.Length > 1)
        {
            return hit[1].collider.gameObject;
        }
        return null;
    }

    private List<GameObject> Moveable()
    {
        List<GameObject> moveableDir = new List<GameObject>();
        for (int i = 0; i < Directions.Length; i++)
        {
            moveableDir.Add(MoveableDir(Directions[i]));
        }
        return moveableDir;
    }

    private List<GameObject> FindMatch(Vector2 dir)
    {
        List<GameObject> match = new List<GameObject>();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, dir);
        if (hit.Length > 1)
        {
            for (int i = 1; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject.GetComponent<Item>().ID == ID && hit[i].collider.gameObject != gameObject)
                {
                    match.Add(hit[i].collider.gameObject);
                }
                else
                {
                    break;
                }
            }
        }
        return match;
    }

    private List<GameObject> GetAllMatchDir(int dir, GameObject current, GameObject target)
    {
        List<GameObject> matchDir = new List<GameObject>();
        if (dir == 1)
        {
            matchDir.AddRange(FindMatchv2(Directions[0], current, target));
            matchDir.AddRange(FindMatchv2(Directions[1], current, target));
        }
        else if (dir == 2)
        {
            matchDir.AddRange(FindMatchv2(Directions[2], current, target));
            matchDir.AddRange(FindMatchv2(Directions[3], current, target));
        }

        return matchDir;
    }


    private bool FindAllMatches()
    {
        List<GameObject> allMatches = new List<GameObject>();
        for (int i = 1; i < 3; i++)
        {
            allMatches = GetAllMatchDir(i, gameObject, gameObject);
            if (allMatches.Count >= 2)
            {
                matchFound = true;
                matchCount = allMatches.Count + 1;
                if (!BattleManager.Instance.isTransfered && BattleManager.Instance.state == BattleState.PLAYER)
                {
                    StartCoroutine(BattleManager.Instance.PlayerTurn(ID, matchCount));
                    BattleManager.Instance.isTransfered = true;
                }
                for (int j = 0; j < allMatches.Count; j++)
                {
                    allMatches[j].GetComponent<SpriteRenderer>().sprite = null;
                    allMatches[j].GetComponent<Item>().ID = Board.ItemID.Null;
                }
            }
        }
        return matchFound;
    }

    public void ClearMatches()
    {
        if (render.sprite == null)
            return;

        FindAllMatches();

        if (matchFound)
        {
            render.sprite = null;
            Debug.Log(ID + " * " + matchCount);
            ID = Board.ItemID.Null;
            matchFound = false;
            StopCoroutine(Board.Instance.FindNullTiles());
            StartCoroutine(Board.Instance.FindNullTiles());
        }
    }

    private List<GameObject> FindMatchv2(Vector2 dir, GameObject current, GameObject target)
    {
        List<GameObject> match = new List<GameObject>();
        RaycastHit2D[] hit = Physics2D.RaycastAll(target.transform.position, dir);
        if (hit.Length > 1)
        {
            for (int i = 1; i < hit.Length; i++)
            {
                if (hit[i].collider.gameObject.GetComponent<Item>().ID == current.GetComponent<Item>().ID && hit[i].collider.gameObject != current)
                {
                    match.Add(hit[i].collider.gameObject);
                }
                else
                {
                    break;
                }
            }
        }
        return match;
    }
    private bool isPossibleMatches(GameObject current, GameObject target)
    {
        List<GameObject> allMatches = new List<GameObject>();
        for (int i = 1; i < 3; i++)
        {
            allMatches = GetAllMatchDir(i, current, target);
            if (allMatches.Count >= 2)
            {
                return true;
            }
        }
        return false;
    }

    public bool isPossibleMove()
    {
        foreach (GameObject moved in moveableDir)
        {
            if (isPossibleMatches(gameObject, moved))
                return true;
        }
        return false;
    }

    private IEnumerator SwapCoroutine(GameObject startobj, GameObject endobj, float time)
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
        //startobj.GetComponent<SpriteRenderer>().sprite = clone2.GetComponent<SpriteRenderer>().sprite;

        for (float t = 0; t <= 1 * time; t += Time.deltaTime)
        {
            clone2.transform.position = Vector3.Lerp(endPos, startPos, t / time);
            yield return null;
        }
        clone2.transform.position = clone1.transform.position;
        //endobj.GetComponent<SpriteRenderer>().sprite = clone1.GetComponent<SpriteRenderer>().sprite;

        Destroy(clone1);
        Destroy(clone2);

        startobj.GetComponent<SpriteRenderer>().enabled = true;
        endobj.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(time + 0.1f);

    }

    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

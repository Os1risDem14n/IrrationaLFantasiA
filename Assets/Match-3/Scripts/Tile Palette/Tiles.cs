using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Board.ItemID ID;
    public SpriteRenderer render;
    public Position pos;

    public GameObject[] vfx;

    public Tiles(Board.ItemID newID,Sprite newSprite)
    {
        ID = newID;
        render.sprite = newSprite;
    }

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    private void OnMouseDown()
    {
        if (BattleManager.Instance.state != BattleState.PLAYER) return;
        BattleManager.Instance.isTransfered = false;
        render.color = Color.gray;
        if (Match3State.Instance.state != Match3State.State.Null) return;
        Match3State.Instance.state = Match3State.State.Select;
        Swipe.Instance.Select(this);
    }

    private void OnMouseUp()
    {
        render.color = Color.white;
        Swipe.Instance.unSelect();
    }

    public void InitTile(Position position)
    {
        pos = position;
        transform.name = "Node [" + pos.x + ", " + pos.y + "]";
    }

    public void UpdateInfo(Board.ItemID newID, Sprite newRender)
    {
        ID = newID;
        render.sprite = newRender;
    }

    public void ClearMatchAnim(int id)
    {
        Instantiate(vfx[id], transform);
    }
}

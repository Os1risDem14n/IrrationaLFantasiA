using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Singleton<Board>
{
	[Header("Tile Prefabs")]
	public List<GameObject> items = new List<GameObject>();
	[Header("Config Board")]
	public int rows;
	public int columns;
	public Match match;

	public enum ItemID
	{
		Attack,
		Shield,
		HP,
		MP,
		Magic,
		Null
	}

	public bool IsShifting { get; set; }

	public GameObject[,] tiles;

    private void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        tiles = new GameObject[columns, rows];

        float initialX = transform.position.x;
        float initialY = transform.position.y;

        GameObject[] previousLeft = new GameObject[rows];
        GameObject previousDown = null;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                List<GameObject> possibleItems = new List<GameObject>();
                possibleItems.AddRange(items);

                possibleItems.Remove(previousLeft[y]);
                possibleItems.Remove(previousDown);

                GameObject newObject = possibleItems[Random.Range(0, possibleItems.Count)];

                GameObject newTile = Instantiate(newObject, new Vector3(initialX + 1.4f* x, initialY + 1.4f * y, 0),newObject.transform.rotation);
                tiles[x, y] = newTile;
				Tiles temptiles = newTile.GetComponent<Tiles>();
				tiles[x, y].GetComponent<Tiles>().InitTile(new Position(x,y));
                newTile.transform.parent = transform;

                previousLeft[y] = newObject;
                previousDown = newObject;
            
            }
        }
    }

	private void DestroyBoard()
    {
		int childs = transform.childCount;
		for (int i = childs - 1; i > 0; i--)
		{
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
	}
	public IEnumerator FindNullTiles()
	{
		for (int x = 0; x < columns; x++)
		{
			for (int y = 0; y < rows; y++)
			{
				if (tiles[x, y].GetComponent<Tiles>().ID == ItemID.Null)
				{
					yield return StartCoroutine(ShiftTilesDown(x, y));
					break;
				}
			}
		}

		for (int x = 0; x < columns; x++)
		{
			for (int y = 0; y < rows; y++)
			{
				StartCoroutine(match.ClearMatch(tiles[x, y]));
			}
		}
	}

	private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = 0.07f)
	{
		IsShifting = true;
		List<GameObject> shifttiles = new List<GameObject>();
		int nullCount = 0;

		for (int y = yStart; y < rows; y++)
		{
			GameObject shifttile = tiles[x, y];
			if (shifttile.GetComponent<Tiles>().ID == ItemID.Null)
			{
				nullCount++;
			}
			shifttiles.Add(shifttile);
		}

		for (int i = 0; i < nullCount; i++)
		{
			if (shifttiles.Count >= 2)
            {
				yield return new WaitForSeconds(shiftDelay);
				for (int k = 0; k < shifttiles.Count - 1; k++)
				{
					shifttiles[k].GetComponent<Tiles>().UpdateInfo(shifttiles[k + 1].GetComponent<Tiles>().ID, shifttiles[k + 1].GetComponent<SpriteRenderer>().sprite);
					GameObject @tempobject = GetNewObject(x, rows - 1);
					shifttiles[k+1].GetComponent<Tiles>().UpdateInfo(@tempobject.GetComponent<Tiles>().ID, @tempobject.GetComponent<SpriteRenderer>().sprite);
				}
			}
			else if (shifttiles.Count == 1)
            {
				yield return new WaitForSeconds(shiftDelay);
				GameObject @tempobject = GetNewObject(x, rows - 1);
				shifttiles[0].GetComponent<Tiles>().UpdateInfo(@tempobject.GetComponent<Tiles>().ID, @tempobject.GetComponent<SpriteRenderer>().sprite);
			}
		}
		IsShifting = false;
	}

	private GameObject GetNewObject(int x, int y)
	{
		List<GameObject> possibleItems = new List<GameObject>();
		possibleItems.AddRange(items);

		if (x > 0)
		{
			possibleItems.Remove(tiles[x - 1, y]);
		}
		if (x < columns - 1)
		{
			possibleItems.Remove(tiles[x + 1, y]);
		}
		if (y > 0)
		{
			possibleItems.Remove(tiles[x, y - 1]);
		}

		return possibleItems[Random.Range(0, possibleItems.Count)];
	}

	private List<GameObject> NearMatch()
    {
		List<GameObject> nearmatch = new List<GameObject>();


		return nearmatch;

    }

}

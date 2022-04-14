using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : Singleton<SetCursor>
{
    public Texture2D crosshair;

    void Start()
    {
        DontDestroyOnLoad(this);
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
    }
}
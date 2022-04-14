using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3State : Singleton<Match3State>
{
    public enum State
    {
        Null,
        Select,
        Swipe,
        Match,
        ShiftTilesDown
    }
    public State state;

    private void Awake()
    {
        state = State.Null;
    }
}

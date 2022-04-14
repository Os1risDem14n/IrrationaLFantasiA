using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineMatch
{
    public enum Type
    {
        None,
        _3x1,
        _4x1,
        _3x3,
        _5x1,
        _4x3,
        _5x3
    }
    public Type type;
    public List<GameObject> matched;

    public DefineMatch(Type _type,List<GameObject> _gameObjects)
    {
        type = _type;
        matched = _gameObjects;
    }
}

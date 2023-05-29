using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject
{

        public Vector2 Position;
        public Vector2 Direction;
        public float chanceToChange;

    public WalkerObject(Vector2 pos, Vector2 dir, float chance)
    {
        Position = pos;
        Direction = dir;
        chanceToChange = chance;
    }
}

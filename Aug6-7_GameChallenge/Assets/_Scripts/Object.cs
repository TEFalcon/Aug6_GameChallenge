using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Object
{
    public enum objectState
    {
        Uninitialized,
        Falling,
        Waiting
    }

    public void SetToWait();

    public void SetToFall();

    public void SetToUninitialized();
}

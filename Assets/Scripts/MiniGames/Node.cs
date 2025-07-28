using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isUsable;

    public GameObject ingredient;

    public Node(bool _isUsable, GameObject _ingredient)
    {
        isUsable = _isUsable;
        ingredient = _ingredient;
    }
}

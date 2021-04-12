using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChores : MonoBehaviour
{
    private GameObject[] _childrenObjs;
    public int numberOfOptions = 2;
    private float _ySpacing = 200;

    private int[] _chosenChores;
    private List<int> _usedValues = new List<int>();

    private void Awake()
    {
        _childrenObjs = new GameObject[transform.childCount];
        _chosenChores = new int[numberOfOptions];
        int i = 0;
        foreach (Transform childObj in transform)
        {
            _childrenObjs[i] = childObj.gameObject;
            i++;
        }
        i = 0;
        foreach (int randomNum in _chosenChores)
        {
            _chosenChores[i] = UniqueRandomInt(0, _childrenObjs.Length);
            Transform chosenChild = transform.GetChild(_chosenChores[i]);
            chosenChild.gameObject.SetActive(true);
            if (numberOfOptions % 2 == 0) chosenChild.localPosition = new Vector3(0, ((((float)i + 0.5f) - ((float)numberOfOptions / 2)) * _ySpacing), 0);
            else chosenChild.localPosition = new Vector3(0, ((i - (numberOfOptions % 2)) * _ySpacing), 0);
            i++;
        }
    }

    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (_usedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        _usedValues.Add(val);
        return val;
    }
}

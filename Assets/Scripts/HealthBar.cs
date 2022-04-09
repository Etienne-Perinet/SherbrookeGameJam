using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static enum Color { RED, GREEN, BLUE };
    [SerializedField] private int colorRadius;
    [SerializedField] private Vector3 colorsValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddColor(Color color, int val) {
        this.colorsValue[color] += val;
    }

    void SubColor(Color color, int val) {
        this.colorsValue[color] -= val;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("test");
    }
}

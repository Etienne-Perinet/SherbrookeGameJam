using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HealthBar : MonoBehaviour 
{
    public enum Color { RED, GREEN, BLUE, LIGHT };

    [SerializeField] private int range;

    [SerializeField] private Vector2 ratio;
    [SerializeField] private float ratioYAxisTop;
    [SerializeField] private float ratioYAxisBottom;

    [SerializeField] private Vector2[] dots;

    [SerializeField] private Transform cursor;

    [SerializeField] private Vector2 initialPos;

    [SerializeField] private Vector2[] vertexs;

    void Start()
    {
        dots = new Vector2[3];
        

        ratio = GetComponent<SpriteRenderer>().bounds.size;
        ratio.x = ratio.x / 2;
        ratio.y = ratio.y / 2;


        
        vertexs = new Vector2[] {  
            new Vector2(ratio.x, -ratio.y),
            new Vector2(0, ratio.y),
            new Vector2(-ratio.x, -ratio.y),
        };

        initialPos = new Vector2(0, -ratio.y/3);

        ratioYAxisTop = ratio.y/2 - initialPos.y;
        ratioYAxisBottom = ratio.y - ratioYAxisTop;
        AddColor(Color.BLUE, 0f);
    }

    private Vector2 Centroid() 
    {
        float count = 0, totalX = 0, totalY = 0;
        foreach (Vector2 d in dots) 
        {
            if (d != new Vector2()) 
            {
                count++;
                totalX += d.x;
                totalY += d.y;
            }
                
        }
  
        return  count == 0 
            ? new Vector2()
            : new Vector2(totalX / count, totalY / count);
    }


    private float Sign(Vector2 v1, Vector2 v2, Vector2 v3) {
        return (v1.x - v3.x) * (v2.y - v3.y) - (v2.x - v3.x) * (v1.y - v3.y);
    }

    private bool IsPointInTriangle(Vector2 point) {
        float d1, d2, d3;
        bool hasNeg, hasPos;

        d1 = Sign(point, vertexs[0], vertexs[1]);
        d2 = Sign(point, vertexs[1], vertexs[2]);
        d3 = Sign(point, vertexs[2], vertexs[0]);

        hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
        hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

        return !(hasNeg && hasPos);
    }

    public bool AddColor(Color color, float val) 
    {
        val /= range;
        switch (color) 
        {
            case Color.GREEN:
                dots[(int)color].y += (val * ratioYAxisTop * 1.5f);
                break;
            case Color.RED:
                dots[(int)color].x += val * ratio.x;
                dots[(int)color].y -= (val * ratioYAxisBottom * 4);
                break;
            case Color.BLUE:
                dots[(int)color].x -= val * ratio.x;
                dots[(int)color].y -= (val * ratioYAxisBottom * 4);
                break;
        }

        Vector2 newPos = Centroid();
        if (!IsPointInTriangle(newPos + initialPos)) 
        {
            return false;

        }

        cursor.localPosition = newPos;
        cursor.position = (Vector2)cursor.position + initialPos;
                return true;

    }
}
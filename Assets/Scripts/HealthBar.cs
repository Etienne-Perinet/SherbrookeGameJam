using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/**
 * function positionnement pour les différente couleurs: 
 *   (pour val: compteur)
 * Green:   x = -val, y = val
 * Red:     x = val, y = val
 * Blue:    x = 0, y = -val
 */
public class HealthBar : MonoBehaviour
{
    public enum Color { RED, GREEN, BLUE };
    [SerializeField] private int colorRadius;
    [SerializeField] private Vector2[] dots;

    [SerializeField] private Transform vPoint;

    private Vector2 dotCorrection = new Vector2(0, .36f);

    private Vector2 ratio;
    private float maxContainerSize;

 
    // Start is called before the first frame update
    void Start()
    {
        dots = new Vector2[3];
        colorRadius = 20;

        Vector3 container = GetComponent<SpriteRenderer>().bounds.extents;
        ratio = new Vector2(container.x, container.y);
        maxContainerSize = colorRadius / container.y;

        AddColor(Color.RED, 20);
    }

    void AddColor(Color color, float val) 
    {
        val /= colorRadius;
        switch (color) 
        {
            case Color.GREEN:
                dots[(int)color].x -= val;
                dots[(int)color].y += val;
                break;
            case Color.RED:
                dots[(int)color].x += val;
                dots[(int)color].y += val;
                break;
            case Color.BLUE:
                dots[(int)color].y -= val;
                break;
        }

        Vector2 newPos = Centroid() * ratio + dotCorrection;
        if (IsDead(newPos)) {
            throw new System.Exception("yooo t mort dude");
        }

        vPoint.position = newPos;
    }

    void SubColor(Color color, float val) 
    {
        AddColor(color, -val);
    }

    bool IsDead(Vector2 pos) {
        pos -= dotCorrection;
        return pos.x >= maxContainerSize || pos.x <= -maxContainerSize 
            || pos.y >= maxContainerSize || pos.y <= -maxContainerSize;
    }

    Vector2 Centroid() 
    {
        float count = 0, totalX = 0, totalY = 0;
        foreach (Vector2 d in dots) 
        {
            if (d != new Vector2()) {
                count++;
                totalX += d.x;
                totalY += d.y;
            }
                
        }
  
        return  count == 0 ? new Vector2() : new Vector2(totalX / count, totalY / count);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
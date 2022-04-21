using UnityEngine;

public enum HealthBarColor { RED, GREEN, BLUE };

public class HealthBarResponse
{
  public Color color { get; }

  public bool isEnd { get; }

  public HealthBarResponse(Color color, bool isEnd)
  {
    this.color = color;
    this.isEnd = isEnd;
  }
}

public class HealthBar : MonoBehaviour
{

  [SerializeField] private byte health;
  [SerializeField] private Transform cursor;

  private Vector2 ratio;
  private float ratioYAxisTop;

  private float ratioYAxisBottom;

  private Vector2[] dots;

  private Vector2 initialPos;

  private Vector3 scale;

  private Vector2[] vertexs;

  [SerializeField] private ColorsVec colors;

  private class ColorsVec
  {
    [SerializeField] private float redSlope;
    [SerializeField] private float greenSlope;
    [SerializeField] private float blueSlope;

    private float step;
    private const byte rgbMax = 255;

    public ColorsVec(float step)
    {
      this.step = step;
    }

    private Color GetColor()
    {
      Debug.Log(redSlope + ", " + greenSlope + ", " + blueSlope);

      float r = rgbMax - (redSlope * step);
      float g = rgbMax - (greenSlope * step);
      float b = rgbMax - (blueSlope * step);


      return new Color(r/rgbMax, g/rgbMax, b/rgbMax);
    }

    public Color AppendColor(HealthBarColor color, float x)
    {
      float rest;

      switch (color) {
        case HealthBarColor.RED:
          if (redSlope == 0)
          {
            greenSlope += x;
            blueSlope += x;
            return GetColor();
          }
          if (x <= redSlope)
          {
            redSlope -= x;
            return GetColor();
          }
          rest = x - redSlope;
          redSlope = 0;
          greenSlope += rest;
          blueSlope += rest;
          break;
          
        case HealthBarColor.GREEN:
          if (greenSlope == 0)
          {
            redSlope += x;
            blueSlope += x;
            return GetColor();
          }
          if (x <= greenSlope)
          {

            greenSlope -= x;
            return GetColor();
          }
          rest = x - greenSlope;
          redSlope += rest;
          greenSlope = 0;
          blueSlope += rest;
          break;

        case HealthBarColor.BLUE:
          if (blueSlope == 0)
          {
            redSlope += x;
            greenSlope += x;
            return GetColor();
          }
          if (x <= blueSlope)
          {
            blueSlope -= x;
            return GetColor();
          }
          rest = x - blueSlope;
          redSlope += rest;
          greenSlope += rest;
          blueSlope = 0;
          break;
      }

      return GetColor();
    }
  }

  private void Start() {
    SpriteRenderer healthBarSprite = GetComponent<SpriteRenderer>();

    ratio = healthBarSprite.bounds.size;
    scale = healthBarSprite.transform.lossyScale;
    ratio.x /= 2;
    ratio.y /= 2;

    initialPos = new Vector2(0, -ratio.y/3);

    ratioYAxisTop = ratio.y/2 - initialPos.y;
    ratioYAxisBottom = ratio.y - ratioYAxisTop;

    vertexs = new Vector2[] {  
      new Vector2(ratio.x, -ratio.y),
      new Vector2(0, ratio.y),
      new Vector2(-ratio.x, -ratio.y),
    };

    dots = new Vector2[3];
    colors = new ColorsVec(health);
  }

  public HealthBarResponse AddColor(HealthBarColor color, float val)
  {
    float vertexVal = val / (health * scale.x);
    switch (color)
    {
      case HealthBarColor.RED:
        dots[(int)color].x += vertexVal * ratio.x;
        dots[(int)color].y -= (vertexVal * ratioYAxisBottom * 4);
        break;
      case HealthBarColor.GREEN:
        dots[(int)color].y += (vertexVal * ratioYAxisTop * 1.5f);
        break;
      case HealthBarColor.BLUE:
        dots[(int)color].x -= vertexVal * ratio.x;
        dots[(int)color].y -= (vertexVal * ratioYAxisBottom * 4);
        break;
    }

    Vector2 newPos = Centroid();

    cursor.localPosition = newPos;
    cursor.position = (Vector2)cursor.position + initialPos;

    return new HealthBarResponse(colors.AppendColor(color, val / 4), !IsPointInTriangle(newPos + initialPos));
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

  private float Sign(Vector2 v1, Vector2 v2, Vector2 v3) 
  {
    return (v1.x - v3.x) * (v2.y - v3.y) - (v2.x - v3.x) * (v1.y - v3.y);
  }

   private bool IsPointInTriangle(Vector2 point) 
  {
    float d1, d2, d3;
    bool hasNeg, hasPos;

    d1 = Sign(point, vertexs[0], vertexs[1]);
    d2 = Sign(point, vertexs[1], vertexs[2]);
    d3 = Sign(point, vertexs[2], vertexs[0]);

    hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
    hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

    return !(hasNeg && hasPos);
  }
}

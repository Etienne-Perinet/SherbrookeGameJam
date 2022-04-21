using UnityEngine;

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
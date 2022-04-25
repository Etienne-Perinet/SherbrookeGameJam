using UnityEngine;

public class HealthBarResponse
{
  public Color BarColor { get; }

  public bool IsEnd { get; }

  public HealthBarResponse(Color color, bool isEnd)
  {
    this.BarColor = color;
    this.IsEnd = isEnd;
  }
}
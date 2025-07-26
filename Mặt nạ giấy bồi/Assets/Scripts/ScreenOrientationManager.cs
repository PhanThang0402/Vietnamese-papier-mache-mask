using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour
{
    public enum OrientationType
    {
        Portrait,
        Landscape
    }

    public OrientationType orientation = OrientationType.Portrait;

    void Start()
    {
        ApplyOrientation();
    }

    void ApplyOrientation()
    {
        switch (orientation)
        {
            case OrientationType.Portrait:
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case OrientationType.Landscape:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
        }
    }
}

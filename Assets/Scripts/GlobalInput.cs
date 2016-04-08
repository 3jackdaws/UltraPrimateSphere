using UnityEngine;
using System.Collections;

public static class GlobalInput
{

    public static float CamXSensitivity = 2;
    public static float CamYSensitivity = 2;

    public static void SetCameraSensitivity(float val)
    {
        CamXSensitivity = CamYSensitivity = val;
    }
}

using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using UnityEditor.ShaderGraph.Internal;

public static class Utils
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool LayerInMask(this LayerMask mask, int layer)
    {
        if ((mask & (1 << layer)) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsInLayerMask(LayerMask mask, int layer)
    {
        if ((mask & (1 << layer)) != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static float RemapRange(float value, float min_a, float max_a, float min_b, float max_b)
    {
        return min_b + (value - min_a) * (max_b - min_b) / (max_a - min_a);
    }
}

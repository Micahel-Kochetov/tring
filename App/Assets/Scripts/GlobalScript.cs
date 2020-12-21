using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static int trackingObjIndex = -1;
    private static int curSel = 0;
    public static int ringSel = 0;
    public static int braceletSel = -1;
    public static bool pickToneState = false;
    public static Color fingerColor;
    public static int pickToneNum = 0;
    public static Material handMaterial;
    public static bool isRotate = false;
    public Material _handMaterial;
    [SerializeField] private Color[] toneCols;
    // Use this for initialization
    void Start()
    {
        toneCols = new Color[15];
        toneCols[0] = new Color(221.0f / 255, 181.0f / 255, 148.0f / 255, 1);
        toneCols[1] = new Color(235.0f / 255, 171.0f / 255, 127.0f / 255, 1);
        toneCols[2] = new Color(211.0f / 255, 153.0f / 255, 114.0f / 255, 1);
        toneCols[3] = new Color(225.0f / 255, 184.0f / 255, 153.0f / 255, 1);
        toneCols[4] = new Color(223.0f / 255, 182.0f / 255, 155.0f / 255, 1);
        toneCols[5] = new Color(226.0f / 255, 185.0f / 255, 143.0f / 255, 1);
        toneCols[6] = new Color(234.0f / 255, 189.0f / 255, 157.0f / 255, 1);
        toneCols[7] = new Color(223.0f / 255, 185.0f / 255, 151.0f / 255, 1);
        toneCols[8] = new Color(238.0f / 255, 207.0f / 255, 180.0f / 255, 1);
        toneCols[9] = new Color(229.0f / 255, 194.0f / 255, 152.0f / 255, 1);
        toneCols[10] = new Color(219.0f / 255, 144.0f / 255, 101.0f / 255, 1);
        toneCols[11] = new Color(206.0f / 255, 150.0f / 255, 124.0f / 255, 1);
        toneCols[12] = new Color(202.0f / 255, 144.0f / 255, 105.0f / 255, 1);
        toneCols[13] = new Color(204.0f / 255, 141.0f / 255, 99.0f / 255, 1);
        toneCols[14] = new Color(217.0f / 255, 152.0f / 255, 109.0f / 255, 1);


        handMaterial = _handMaterial;
    }

    public void ChangeToneColor(Color col)
    {
        handMaterial.SetColor("_Color", col);
    }

    public static int СurSel
    {
        get
        {
            return curSel;
        }
        set
        {
            curSel = value;
        }
    }
}

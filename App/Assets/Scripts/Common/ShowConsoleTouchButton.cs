using UnityEngine;

public class ShowConsoleTouchButton : MonoBehaviour
{
    private float tapTime = 3;
    private float timeout;
    private int tapsCount;
    [SerializeField]
    public GameObject console;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (timeout < Time.time)
            tapsCount = 0;

        if (Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
        {
            if (tapsCount == 0)
                timeout = Time.time + tapTime;
            if (Time.time < timeout)
            {
                tapsCount++;
            }
        }

        if (tapsCount == 3)
        {
            ShowConsole();
            timeout = 0;
        }
    }

    private void ShowConsole()
    {
        console.SetActive(!console.activeSelf);
    }
}

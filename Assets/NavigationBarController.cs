using UnityEngine;
using UnityEngine.UI;

public class NavigationBarController : MonoBehaviour
{
    // private Image _image;
    public GameObject[] menu;
    private RectTransform rectTransform;

    public float rectWidth;

    public float rectHeight;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectWidth = rectTransform.rect.width;
        rectHeight = rectTransform.rect.height;

        for (int i = 0; i < menu.Length; i++)
        {
            Image itemRect = menu[i].GetComponent<Image>();
            itemRect.rectTransform.sizeDelta = new Vector2(rectWidth / menu.Length, rectHeight);
            if (i == 0)
            {
                RectTransform itemRectTransform = menu[i].GetComponent<RectTransform>();
                itemRectTransform.anchoredPosition = new Vector2(rectWidth / menu.Length / 2, rectHeight / 2);
            }
            else
            {
                RectTransform itemRectTransform = menu[i].GetComponent<RectTransform>();
                RectTransform itemRectTransformBef = menu[i - 1].GetComponent<RectTransform>();
                float positionX = itemRectTransformBef.position.x + itemRectTransform.rect.width;
                itemRectTransform.anchoredPosition = new Vector2(positionX, rectHeight / 2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // _image.rectTransform.sizeDelta = new Vector2(30, 30);
    }
}
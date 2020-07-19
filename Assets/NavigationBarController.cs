using System.Collections;
using UnityEngine;

public class NavigationBarController : MonoBehaviour
{
    /*
     * Sorry for unreadable code,
     * never code for UI animation before,
     * and the time is not enough to clean this mess :(
     *
     * Notes : will revised it soon
     */

    public GameObject[] menu; // Item menu container
    private float _rectWidth;
    private float _itemWidth;
    private int _currentMenu;
    [SerializeField] private float animationDelay = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        _currentMenu = menu.Length / 2;
        _rectWidth = GetComponent<RectTransform>().rect.width;
        _itemWidth = _rectWidth / (menu.Length + 1);
        RelayoutMenuItem(_currentMenu);
    }

    private void RelayoutMenuItem(int currentMenu)
    {
        float left = 0;
        float right = _rectWidth - _itemWidth;
        for (int i = 0; i < menu.Length; i++)
        {
            RectTransform itemRect = menu[i].GetComponent<RectTransform>();
            RectTransform menuIcon = menu[i].transform.Find("Menu Icon").GetComponent<RectTransform>();
            GameObject menuName = menu[i].transform.Find("Menu Name").gameObject; 
            // Resizing rect position (left, bottom)
            itemRect.offsetMin = new Vector2(left, 0);
            // Resizing rect position (right, top)
            itemRect.offsetMax = new Vector2(-right, 0);
            menuIcon.anchoredPosition = new Vector2(0,0);
            menuName.SetActive(false);
            
            // If next menu is selected menu
            if ((i + 1) == currentMenu)
            {
                right -= (_itemWidth * 2);
                left += _itemWidth;
            }
            // If this menu is current selected menu  
            else if (i == currentMenu)
            {
                menuIcon.anchoredPosition = new Vector2(0,50f);
                menuName.SetActive(true);
                right -= (_itemWidth);
                left += (_itemWidth * 2);
                // If current selected menu is first menu
                if (i == 0)
                {
                    itemRect.offsetMax = new Vector2(-right, 0);
                    right -= (_itemWidth);
                }
            }
            // Set next menu size as normal size
            else
            {
                right -= _itemWidth;
                left += _itemWidth;
            }
        }
    }

    IEnumerator SwitchAnimation(int selectedMenu)
    {
        // If selected menu is lesser than current menu, do backward looping
        if (selectedMenu < _currentMenu)
        {
            for (int i = _currentMenu; i >= selectedMenu; i--)
            {
                RelayoutMenuItem(i);
                _currentMenu = i;
                yield return new WaitForSeconds(animationDelay);
            }
        }
        // If selected menu is lesser than current menu, do forward looping
        else if (selectedMenu > _currentMenu)
        {
            for (int i = _currentMenu; i <= selectedMenu; i++)
            {
                RelayoutMenuItem(i);
                _currentMenu = i;
                yield return new WaitForSeconds(animationDelay);
            }
        }
    }

    public void SwitchMenu(int selectedMenu)
    {
        StartCoroutine(SwitchAnimation(selectedMenu));
    }
}
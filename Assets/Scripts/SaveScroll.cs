using UnityEngine;
using UnityEngine.UI;

public class SaveScroll : MonoBehaviour
{
    public int startIndex = 0; // Set the start index for this scene
    private ScrollRect scrollRect;
    private float scrollPosition = 0f;

    void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        DontDestroyOnLoad(gameObject); // Prevent the game object from being destroyed when the scene changes
    }

    void OnEnable()
    {
        // Load the saved scroll position from PlayerPrefs when the script is enabled
        scrollPosition = PlayerPrefs.GetFloat("ScrollPosition", 0f);
        scrollRect.horizontalNormalizedPosition = scrollPosition;

        // Calculate the scroll position based on the index of the first item in the list
        int itemCount = scrollRect.content.childCount;
        if (itemCount > 0)
        {
            float itemWidth = scrollRect.content.GetChild(0).GetComponent<RectTransform>().rect.width;
            float contentWidth = itemWidth * itemCount;
            float startX = itemWidth / 2f + (startIndex * itemWidth);
            float scrollX = startX / contentWidth;
            scrollRect.horizontalNormalizedPosition = Mathf.Clamp01(scrollX);
        }
    }

    void OnDisable()
    {
        // Save the scroll position to PlayerPrefs when the script is disabled
        PlayerPrefs.SetFloat("ScrollPosition", scrollRect.horizontalNormalizedPosition);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (scrollRect.horizontalNormalizedPosition != scrollPosition)
        {
            // Save the scroll position every time the user scrolls
            scrollPosition = scrollRect.horizontalNormalizedPosition;
            PlayerPrefs.SetFloat("ScrollPosition", scrollPosition);
            PlayerPrefs.Save();
        }
    }
}

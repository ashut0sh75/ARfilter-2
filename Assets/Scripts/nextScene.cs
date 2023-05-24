using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScene : MonoBehaviour
{
    [SerializeField] private RectTransform panelRectTransform;
    [SerializeField] private float minDistanceForSwipe = 20f;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    private void Start()
    {
        if (panelRectTransform == null)
        {
            panelRectTransform = GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && IsTouchWithinPanel(touch))
            {
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended && IsTouchWithinPanel(touch))
            {
                fingerUpPosition = touch.position;

                if (Vector2.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe)
                {
                    Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;

                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Swipe left or right
                        if (swipeDirection.x < 0)
                        {
                            // Swipe left, load next scene
                            SceneManager.LoadScene("scene1");
                        }
                        else
                        {
                            // Swipe right, load previous scene
                            SceneManager.LoadScene("camera scene");
                        }
                    }
                }
            }
        }
    }

    private bool IsTouchWithinPanel(Touch touch)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, touch.position, Camera.main, out localPoint);
        return panelRectTransform.rect.Contains(localPoint);
    }
}


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollViewItem : MonoBehaviour
{
    public int itemIndex;
    public string[] sceneNames;
    public ScrollRect scrollRect;
    public float snapThreshold = 0.05f;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenScene);
    }

    void Update()
    {
        if (IsItemInCenter())
        {
            SceneManager.LoadScene(sceneNames[itemIndex]);
        }
    }

    bool IsItemInCenter()
    {
        // Get the viewport height of the scroll rect
        float viewportHeight = scrollRect.viewport.rect.height;

        // Get the position of the item relative to the viewport
        Vector3 itemViewportPos = scrollRect.viewport.InverseTransformPoint(transform.position);

        // Calculate the distance of the item from the center of the viewport
        float distanceFromCenter = Mathf.Abs(itemViewportPos.y - viewportHeight / 2);

        // Check if the item is within the snap threshold
        return distanceFromCenter < snapThreshold * viewportHeight;
    }

    void OpenScene()
    {
        SceneManager.LoadScene(sceneNames[itemIndex]);
    }
}

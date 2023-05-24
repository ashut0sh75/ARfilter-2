using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Android;
using UnityEngine.UI;

public class CameraPhoto : MonoBehaviour
{
    private bool hasStoragePermission = false;
    

    private void Start()
    {
        //Asking for permission to write on external Storage
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        else
        {
            hasStoragePermission = true;
        }
    }


    IEnumerator TakeScreenshot()
    {

       
         GameObject screenswipe = GameObject.Find("screenswipe 1");
          GameObject camera = GameObject.Find("camera");
         

    // Find the button GameObject
   GameObject buttonGO = screenswipe.GetComponentInChildren<Canvas>(true).gameObject;
    

    // Hide the button GameObject
    buttonGO.SetActive(false);
    camera.SetActive(false);
  
    
        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        byte[] bytes = screenshotTexture.EncodeToPNG();

        string filename = "ARFilter" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";

        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(screenshotTexture, "MyGallery", filename, (success, path) => {
            if (success)
            {
                Debug.Log("Saved screenshot to gallery: " + path);

                // Prompt the user before quitting the application
                Application.Quit();
            }
            else
            {
                Debug.Log("Failed to save screenshot to gallery");
                
                // Show the button GameObject and ScrollView GameObject
                buttonGO.SetActive(true);
                camera.SetActive(true);
               
            }
            
        });

        // Hide the button GameObject and ScrollView GameObject
        buttonGO.SetActive(false);
        camera.SetActive(false);
       
        
    }

// Button function to Set On Click Listener
    public void OnButtonClick()
    {
        if (hasStoragePermission)
        {
            StartCoroutine(TakeScreenshot());
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ScreenSizeImagePlacer : MonoBehaviour
{
    public Sprite imageFor2204;
    public Sprite imageForOtherSizes;
    
    private void Start()
    {
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        
        if (screenHeight == 2208 && screenWidth == 1242)
        {
#if UNITY_EDITOR
            
#endif
            GetComponent<Image>().sprite = imageFor2204;
        }
        else
        {
#if UNITY_EDITOR
            
#endif
            GetComponent<Image>().sprite = imageForOtherSizes;
        }
    }
}

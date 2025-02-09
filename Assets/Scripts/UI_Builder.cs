using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class UI_Builder : MonoBehaviour
{
    [SerializeField]
    private AssetLabelReference sprites_leader_label_reference;
    
    
    GameObject ui_builder_canvas;

    List<GameObject> leader_images = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadAssetsAsync<Sprite>(sprites_leader_label_reference, (sprite) =>
        {
            GameObject temp = new GameObject(sprite.name);
            temp.transform.SetParent(ui_builder_canvas.transform, false);
            temp.AddComponent<Image>();
            temp.GetComponent<Image>().sprite = sprite;
            leader_images.Add(temp);
            Debug.Log(sprite);
        });


        ui_builder_canvas = new GameObject("UI_Builder_Canvas");
        ui_builder_canvas.AddComponent<Canvas>();
        ui_builder_canvas.AddComponent<CanvasScaler>();
        ui_builder_canvas.AddComponent<GraphicRaycaster>();
        ui_builder_canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        ui_builder_canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        ui_builder_canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);

        
    }

    void Leader_Image_Add(string leader_name)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

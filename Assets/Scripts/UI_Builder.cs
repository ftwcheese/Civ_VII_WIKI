using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEditor.Rendering;
using UnityEngine.U2D;


public class UI_Builder : MonoBehaviour
{
    [SerializeField]
    private AssetLabelReference sprites_leader_label_reference;

    [SerializeField]
    private int leader_images_width;
    [SerializeField]
    private int leader_images_height;

    private int padding_screen_left = 100;
    private int padding_screen_top  = 100;
    private int padding_column      = 50;
    private int padding_row         = 30;


    private int leader_images_column_count = 4;


    GameObject ui_builder_canvas;
    GameObject ui_builder_canvas_background;

    List<GameObject> leader_images = new List<GameObject>();

    // Start is called before the first frame update
    async void Start()
    {
        AsyncOperationHandle<IList<Sprite>> asyncOperationHandle = Addressables.LoadAssetsAsync<Sprite>(sprites_leader_label_reference, (sprite) =>
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

        ui_builder_canvas_background = new GameObject("UI_Builder_Canvas_Background");
        ui_builder_canvas_background.transform.SetParent(ui_builder_canvas.transform, false);
        ui_builder_canvas_background.AddComponent<Image>();
        ui_builder_canvas_background.GetComponent<Image>().color = new Color32(90,130,142,255);
        ui_builder_canvas_background.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        ui_builder_canvas_background.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        ui_builder_canvas_background.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        asyncOperationHandle.Completed += Leader_images_position;


    }

    private void Leader_images_position(AsyncOperationHandle<IList<Sprite>> asyncOperationHandle)
    {
        int row = 0;
        int column = 0;
        for(int i = 0; i<leader_images.Count;i++) {

            leader_images[i].GetComponent<RectTransform>().sizeDelta = new Vector2(leader_images_width, leader_images_height);
            leader_images[i].GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            leader_images[i].GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);

            int pos_x = column*leader_images_width + padding_screen_left + padding_column*column;
            int pos_y = row * leader_images_height + padding_screen_top + padding_row * row;
            leader_images[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(pos_x, -pos_y);



            column++;
            row += column / leader_images_column_count;
            column = column % leader_images_column_count;
        }

        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

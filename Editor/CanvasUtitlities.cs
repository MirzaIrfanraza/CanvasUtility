#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
[InitializeOnLoad]
public static class CanvasUtitlities
{
    [MenuItem("CanvasUtility / SetAnchoreToCenter")]
    public static void SetAnchoreToCenter()
    {
        if (Selection.gameObjects.Length > 0)
        {
            for (int indexOfObject = 0; indexOfObject < Selection.gameObjects.Length; indexOfObject++)
            {
                RectTransform targetObjectTransform = Selection.gameObjects[indexOfObject].GetComponent<RectTransform>();
                RectTransform targetObjectParentTransform = targetObjectTransform.parent.GetComponent<RectTransform>();
                Vector2 parentSize = targetObjectParentTransform.sizeDelta;



                if (parentSize.x == 0f)
                {
                    parentSize.x = targetObjectTransform.GetComponentInParent<CanvasScaler>().referenceResolution.x;
                }

                if (parentSize.y == 0f)
                {
                    parentSize.x = targetObjectTransform.GetComponentInParent<CanvasScaler>().referenceResolution.y;
                }


                Debug.Log(parentSize.ToString());


                Vector2 anchoredPosition = targetObjectTransform.anchoredPosition;



                Vector2 targetPivot = Vector2.one * .5f;
                Vector2 targetAnchore = new Vector2(anchoredPosition.x / 1920f, anchoredPosition.y / 100f);




                Vector2 minAnchoredPosition = new Vector2((targetObjectTransform.anchorMin.x * 1920f) + anchoredPosition.x, (targetObjectTransform.anchorMin.y * 100f) + anchoredPosition.y);
                Vector2 maxAnchoredPosition = new Vector2((targetObjectTransform.anchorMax.x * 1920f) + anchoredPosition.x, (targetObjectTransform.anchorMax.y * 100f) + anchoredPosition.y); 



                targetObjectTransform.pivot = targetPivot;
                targetObjectTransform.anchorMin = new Vector2(minAnchoredPosition.x / 1920f, minAnchoredPosition.y / 100f);
                targetObjectTransform.anchorMax = new Vector2(maxAnchoredPosition.x / 1920f, maxAnchoredPosition.y / 100f);
                targetObjectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }
    [MenuItem("CanvasUtility/EnableSelectedCanvas _g")]
    public static void EnableSelectedCanvas()
    {
        if (Selection.gameObjects.Length == 1)
        {
            Canvas[] objectInScene = GameObject.FindObjectsOfType<Canvas>();
            List<Canvas> tempCanvas = new List<Canvas>();
            tempCanvas.AddRange(Selection.gameObjects[0].GetComponents<Canvas>());
            tempCanvas.AddRange(Selection.gameObjects[0].GetComponentsInChildren<Canvas>());
            tempCanvas.AddRange(Selection.gameObjects[0].GetComponentsInParent<Canvas>());

            foreach (Canvas canvas in objectInScene)
            {
                foreach (Canvas selectedCanvas in tempCanvas)
                {
                    if (canvas != selectedCanvas)
                    {
                        canvas.enabled = false;
                    }
                    else
                    {
                        canvas.enabled = true;
                    }
                }
            }
        }
    }

    [MenuItem("CanvasUtility/SetAnchoreToCorner _j")]
    public static void SetAnchoreToCorner()
    {
        if (Selection.gameObjects.Length == 1)
        {
            RectTransform tempRect = Selection.gameObjects[0].GetComponent<RectTransform>();

            Vector2 minAnchore = Vector2.zero;
            Vector2 maxAnchore = Vector2.zero;

            Vector2 minAnchorePos = new Vector2(tempRect.anchoredPosition.x - (tempRect.sizeDelta.x / 2), tempRect.anchoredPosition.y - (tempRect.sizeDelta.y / 2));
            Vector2 maxAnchorePos = new Vector2(tempRect.anchoredPosition.x + (tempRect.sizeDelta.x / 2), tempRect.anchoredPosition.y + (tempRect.sizeDelta.y / 2));

            CanvasScaler scaler = tempRect.GetComponentInParent<CanvasScaler>();

            float height = scaler.referenceResolution.y;
            float width = scaler.referenceResolution.x;

            tempRect.anchorMin = new Vector2(.5f + (minAnchorePos.x / width), .5f + (minAnchorePos.y / height));
            tempRect.anchorMax = new Vector2(.5f + (maxAnchorePos.x / width), .5f + (maxAnchorePos.y / height));

            tempRect.offsetMax = Vector3.zero;
            tempRect.offsetMin = Vector3.zero;
        }
    }

    [MenuItem("CanvasUtility/SetGeneralSpriteSettings _k")]
    public static void SetGeneralSpriteSettings()
    {
        Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();

        Debug.Log(sprites.Length);
        Debug.Log("Texture height : " + sprites[0].name);
        foreach (Sprite sprite in sprites)
        {
            if (AssetDatabase.GetAssetPath(sprite).Contains("Assets"))
            {
                Debug.Log("Texture Path : " + AssetDatabase.GetAssetPath(sprite));
                TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath(AssetDatabase.GetAssetPath(sprite));
                textureImporter.spritePixelsPerUnit = 300;
                FileInfo fInfo = new FileInfo(AssetDatabase.GetAssetPath(sprite));
                string dirName = fInfo.Directory.Name;
                textureImporter.spritePackingTag = dirName;


                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 32)
                {
                    textureImporter.maxTextureSize = 32;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 64)
                {
                    textureImporter.maxTextureSize = 64;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 128)
                {
                    textureImporter.maxTextureSize = 128;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 256)
                {
                    textureImporter.maxTextureSize = 256;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 512)
                {
                    textureImporter.maxTextureSize = 512;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 1024)
                {
                    textureImporter.maxTextureSize = 1024;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 2048)
                {
                    textureImporter.maxTextureSize = 2048;
                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 4096)
                {
                    textureImporter.maxTextureSize = 4096;

                    textureImporter.SaveAndReimport();
                    continue;
                }
                if (Mathf.Max(sprite.texture.width, sprite.texture.height) <= 8192)
                {
                    textureImporter.maxTextureSize = 8192;
                    textureImporter.SaveAndReimport();
                    continue;
                }
            }
        }
    }

    [MenuItem("CanvasUtility/SetPixelPerUnitOfImageInScene #P")]
    public static void SetPixelPerUnitOfImageInScene()
    {
        Image[] images = GameObject.FindObjectsOfType<Image>();
        foreach (Image image in images)
        {
            image.pixelsPerUnitMultiplier = 1f;
        }
    }

}

#endif

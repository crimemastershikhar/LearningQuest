using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;
using TMPro;

public class GitHubImageLoader : MonoBehaviour
{
    public RawImage targetImage;
    public TextMeshProUGUI[] labelDisplays;
    
    [HideInInspector]
    public TextMeshProUGUI[] descriptionDisplays;
    
    private string githubJsonUrl;
    
    [SerializeField] private GameObject[] canvasGame;
    
    void Start()
    {
        githubJsonUrl = GitHubUrl.SharedUrl;
        StartCoroutine(FetchDataFromGitHub());
    }

    IEnumerator FetchDataFromGitHub()
    {
        UnityWebRequest jsonRequest = UnityWebRequest.Get(githubJsonUrl);
        yield return jsonRequest.SendWebRequest();

        if (jsonRequest.result == UnityWebRequest.Result.Success)
        {
            ImageData data = JsonUtility.FromJson<ImageData>(jsonRequest.downloadHandler.text);

            UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(data.image_url);
            yield return imageRequest.SendWebRequest();

            if (imageRequest.result == UnityWebRequest.Result.Success)
            {
                canvasGame[0].SetActive(false);
                canvasGame[1].SetActive(true);
                targetImage.texture = ((DownloadHandlerTexture)imageRequest.downloadHandler).texture;
                LoadData(data);
                
            }
            else
            {
                Debug.LogError("Image load failed: " + imageRequest.error);
            }
        }
        else
        {
            Debug.LogError("failed: ");
        }
    }

    private void LoadData(ImageData imgData)
    {
        for (int i = 0; i < imgData.strings.Length; i++)
        {
            labelDisplays[i].text =  i + " --> " + imgData.strings[i].heading;
            descriptionDisplays[i].text = imgData.strings[i].description;
        }
    }
    
    
}

[System.Serializable]
public class ImageData {
    public string image_url;
    public StringPair[] strings;
}

[System.Serializable]
public class StringPair {
    public string heading;
    public string description;
}



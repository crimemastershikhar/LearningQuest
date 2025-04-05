using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;
using TMPro; // For DateTime

public class GitHubImageLoader : MonoBehaviour
{
    public RawImage targetImage; // Assign in Inspector
    public TextMeshProUGUI[] labelDisplays;  // Assign 4 UI Text elements
    public TextMeshProUGUI[] descriptionDisplays;  // Assign 4 UI Text elements
    public TextMeshProUGUI loadingText;     // Assign a UI Text for "Loading..." message
    private string githubJsonUrl = "https://raw.githubusercontent.com/crimemastershikhar/LearningQuest/refs/heads/MilestoneOne/image_data.json";

    void Start()
    {
        StartCoroutine(FetchDataFromGitHub());
    }

    IEnumerator FetchDataFromGitHub()
    {
        // Start time tracking
        DateTime startTime = DateTime.Now;
        loadingText.text = "Loading...";

        // Step 1: Fetch JSON
        UnityWebRequest jsonRequest = UnityWebRequest.Get(githubJsonUrl);
        yield return jsonRequest.SendWebRequest();

        if (jsonRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Raw JSON: " + jsonRequest.downloadHandler.text);
            // Step 2: Parse JSON
            ImageData data = JsonUtility.FromJson<ImageData>(jsonRequest.downloadHandler.text);

            // Step 3: Fetch Image
            UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(data.image_url);
            yield return imageRequest.SendWebRequest();

            if (imageRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("SHIKHAR Loaded");
                // Display Image
                targetImage.texture = ((DownloadHandlerTexture)imageRequest.downloadHandler).texture;

                // Display Strings
                
                LoadData(data);
                
                // Calculate and show load time
                TimeSpan elapsedTime = DateTime.Now - startTime;
                loadingText.text = $"Loaded in {elapsedTime.TotalSeconds} seconds!";
            }
            else
            {
                loadingText.text = "Image load failed!";
                Debug.LogError("Image load failed: " + imageRequest.error);
            }
        }
        else
        {
            loadingText.text = "Failed to fetch data!";
            Debug.LogError("JSON fetch failed: " + jsonRequest.error);
        }
    }

    private void LoadData(ImageData imgData)
    {
        for (int i = 0; i < imgData.strings.Length; i++)
        {
            labelDisplays[i].text = imgData.strings[i].heading;
            descriptionDisplays[i].text = imgData.strings[i].description;
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
    
    
}



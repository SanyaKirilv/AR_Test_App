using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Downloader : JSONParcer
{
    public Text messageText;
    public Button goARButton;
    public GameObject loadingCircle;


    private void Start()
    {
        UseMemory(Target());
        UseMemory(Model());
    }

    private void Update()
    {
        if(CheckMemory(Target()) && CheckMemory(Model()))
        {
            messageText.text = "Done!\nNow you can enter to play mode";
            loadingCircle.SetActive(false);
            goARButton.interactable = true;
        }
    }

    IEnumerator LoadFromWeb(ObjectData data)
    {
        UnityWebRequest _request = data.typeOf == "Target" ? 
            UnityWebRequestTexture.GetTexture(data.url) :
            UnityWebRequest.Get(data.url);

        yield return _request.SendWebRequest();

        if (_request.result == UnityWebRequest.Result.ConnectionError)
        {
            messageText.text = "Connection error!\nPlease retry later.";
            loadingCircle.SetActive(false);
            goARButton.interactable = false;
        }
        else
        {
            switch (data.typeOf)
            {
                case "Target":
                    SaveTarget(_request, data.fileName);
                    break;
                case "Model":
                    SaveModel(_request, data.fileName);
                    break;
            }
        }
    }

    private void SaveTarget(UnityWebRequest request, string fileName)
    {
        Texture2D loadedTexture = DownloadHandlerTexture.GetContent(request);
        byte[] textureBytes = loadedTexture.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, 
            fileName), textureBytes);
        Debug.Log($"File {fileName} saved!");
    }

    private void SaveModel(UnityWebRequest request, string fileName)
    {
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, 
            fileName), request.downloadHandler.data);
        Debug.Log($"File {fileName} saved!");
    }

    private void UseMemory(ObjectData data) 
    {
        if(!CheckMemory(data))
            StartCoroutine(LoadFromWeb(data));
        else 
            Debug.Log($"File {data.fileName} already exists!");
    }

    private bool CheckMemory(ObjectData data) => File.Exists(Path.Combine(
        Application.persistentDataPath, data.fileName));

    public void ForceDownload()
    {
        messageText.text = "Please wait!\nLoading process...";
        loadingCircle.SetActive(true);
        goARButton.interactable = false;
        File.Delete(Path.Combine(Application.persistentDataPath, Target().fileName));
        File.Delete(Path.Combine(Application.persistentDataPath, Model().fileName));
        StartCoroutine(LoadFromWeb(Target()));
        StartCoroutine(LoadFromWeb(Model()));
    }
}

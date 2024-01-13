using UnityEngine;

[System.Serializable]
public class ObjectDataJSON 
{
    public ObjectData[] objectData;
}

[System.Serializable]
public class ObjectData {
    public string typeOf;
    public string fileName;
    public string url;
}

public class JSONParcer : MonoBehaviour
{
    public TextAsset textJSON;
    private ObjectDataJSON objectData = new();
    private void Awake() => objectData = JsonUtility.FromJson<ObjectDataJSON>(textJSON.text);
    public ObjectData Target() => objectData.objectData[0];
    public ObjectData Model() => objectData.objectData[1];
}

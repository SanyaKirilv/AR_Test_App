using System.IO;
using UnityEngine;
using Siccity.GLTFUtility;

public class Loader : JSONParcer
{
    private Texture2D target;
    private GameObject model;
    public Texture2D GetImage()
    {
        string _fileName = Target().fileName;

        byte[] textureBytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath,
            _fileName));

        Debug.Log(textureBytes != null ? $"Texture {_fileName} loaded!" :
            $"Error loading {_fileName}!");

        Texture2D loadedTexture = new(0, 0);
        loadedTexture.LoadImage(textureBytes);
        target = loadedTexture;
        return target;
    }

    public GameObject GetModel()
    {
        string _fileName = Model().fileName;

        model = Importer.LoadFromFile(Path.Combine(Application.persistentDataPath, _fileName));
        
        Debug.Log(model != null ? $"Texture {_fileName} loaded!" :
            $"Error loading {_fileName}!");

        return model;
    }
}

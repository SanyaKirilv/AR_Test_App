using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageManager : MonoBehaviour
{
    [SerializeField] public Texture2D imageTarget;
    [SerializeField] public GameObject trackedModel;
    [SerializeField] public GameObject trackedAnchor;
    private XRReferenceImageLibrary runtimeImageLibrary;
    private ARTrackedImageManager trackImageManager;
    private Loader loader;

    private void Start()
    {
        loader = GetComponent<Loader>();
        imageTarget = loader.GetImage();
        SetUpModel();
        SetUpARTrackedImageManager();
    }

    private GameObject SetUpModel()
    {
        trackedModel = loader.GetModel();
        trackedModel.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        return trackedModel;
    }

    private void SetUpARTrackedImageManager()
    {
        trackImageManager = gameObject.AddComponent<ARTrackedImageManager>();
        trackImageManager.referenceLibrary = trackImageManager.CreateRuntimeLibrary(runtimeImageLibrary);
        trackImageManager.requestedMaxNumberOfMovingImages = 1;
        trackImageManager.trackedImagePrefab = trackedAnchor;
        AddImage();
    }

    public void AddImage()
    {
        trackImageManager.enabled = true;
        StartCoroutine(AddImageJob(imageTarget));
    }

    private IEnumerator AddImageJob(Texture2D texture2D)
    {
        yield return null;

        var firstGuid = new SerializableGuid(0,0);
        var secondGuid = new SerializableGuid(0,0);
        
        XRReferenceImage newImage = new(firstGuid, secondGuid, 
            new Vector2(0.1f,0.1f), Guid.NewGuid().ToString(), texture2D);
        
        try
        {
            MutableRuntimeReferenceImageLibrary mutableRuntimeReferenceImageLibrary = trackImageManager.referenceLibrary as MutableRuntimeReferenceImageLibrary;

            var jobHandle = mutableRuntimeReferenceImageLibrary.ScheduleAddImageJob(
                texture2D, Guid.NewGuid().ToString(), 0.1f);
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}

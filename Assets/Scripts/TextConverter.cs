using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextConverter : MonoBehaviour
{
    [SerializeField]
    private string textFilePath = "Texts/";

    private const string defaultFile = "Scenario";

    public string LoadTextFile(string path)
    {
        if (Resources.Load<TextAsset>(path).IsUnityNull())
        {
            path = defaultFile;
        }

        TextAsset textAsset = Resources.Load<TextAsset>(textFilePath + path);
        return textAsset.text.Replace("\n", "").Replace("\r", "");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

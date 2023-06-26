using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextConverter : MonoBehaviour
{
    [SerializeField]
    private string textFilePath = "Texts/";

    private const string defaultFile = "test";

    public string LoadTextFile(string path)
    {
        if (Resources.Load<TextAsset>(textFilePath + path).IsUnityNull())
        {
            path = defaultFile;
        }

        TextAsset textAsset = Resources.Load<TextAsset>(textFilePath + path);
        return textAsset.text.Replace("\n", "").Replace("\r", "");
    }

    public bool ParameterToBool(string parameter)
    {
        string p = parameter.Replace(" ", "");

        return p.Equals("true") || p.Equals("TRUE");
    }

    public Vector3 ParameterToVector3(string parameter)
    {
        string[] ps = parameter.Replace(" ", "").Split(',');

        return new Vector3(float.Parse(ps[0]), float.Parse(ps[1]), float.Parse(ps[2]));
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

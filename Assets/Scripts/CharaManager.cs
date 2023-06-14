using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharaManager : MonoBehaviour
{
    private const string OPTION_SIZE = "_size";
    private const string OPTION_POSITION = "_pos";
    private const string OPTION_ROTATION = "_rotate";
    private const string OPTION_ACTIVE = "_active";
    private const string OPTION_DELETE = "_delete";

    [SerializeField]
    private string prefabsDirectory = "Prefabs/";

    [SerializeField]
    private GameObject characterImages;
    private List<Image> _charaImageList = new List<Image>();

    public void SetCharacterImage( string cmd, string name, string parameter)
    {
        name = name.Substring(name.IndexOf('"') + 1, name.LastIndexOf('"') - name.IndexOf('"') - 1);
        Image image = _charaImageList.Find(n => n.name == name);
        if (image == null)
        {
            image = Instantiate(Resources.Load<Image>(prefabsDirectory + name), characterImages.transform);
            image.name = name;
            _charaImageList.Add(image);
        }
        CharaOption(cmd, parameter, image);
    }

    private void CharaOption(string cmd, string parameter, Image image)
    {
        cmd = cmd.Replace(" ", "");
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        switch (cmd)
        {
            case OPTION_SIZE:
                image.GetComponent<RectTransform>().sizeDelta = ParameterToVector3(parameter);
                break;
            case OPTION_POSITION:
                image.GetComponent<RectTransform>().anchoredPosition = ParameterToVector3(parameter);
                break;
            case OPTION_ROTATION:
                image.GetComponent<RectTransform>().eulerAngles = ParameterToVector3(parameter);
                break;
            case OPTION_ACTIVE:
                image.gameObject.SetActive(ParameterToBool(parameter));
                break;
            case OPTION_DELETE:
                _charaImageList.Remove(image);
                Destroy(image.gameObject);
                break;
            //case OPTION_ANIM:
            //    ImageSetAnimation(image, parameter);
            //    break;
            default:
                break;
        }
    }
    private bool ParameterToBool(string parameter)
    {
        string p = parameter.Replace(" ", "");

        return p.Equals("true") || p.Equals("TRUE");
    }

    private Vector3 ParameterToVector3(string parameter)
    {
        string[] ps = parameter.Replace(" ", "").Split(',');

        return new Vector3(float.Parse(ps[0]), float.Parse(ps[1]), float.Parse(ps[2]));
    }
}

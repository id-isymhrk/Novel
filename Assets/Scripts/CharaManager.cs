using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharaManager : MonoBehaviour
{
    private const string OPTION_SPRITE = "_sprite";
    private const string OPTION_SIZE = "_size";
    private const string OPTION_POSITION = "_pos";
    private const string OPTION_ROTATION = "_rotate";
    private const string OPTION_ACTIVE = "_active";
    private const string OPTION_DELETE = "_delete";

    [SerializeField]
    private string prefabsDirectory = "Prefabs/character/";

    [SerializeField]
    private GameObject characterImages;
    private List<Image> _charaImageList = new List<Image>();

    private TextConverter _textConverter;

    public void SetCharacterImage( string cmd, string name, string parameter)
    {
        name = name.Substring(name.IndexOf('"') + 1, name.LastIndexOf('"') - name.IndexOf('"') - 1);
        Image image = _charaImageList.Find(n => n.name == name);
        if (image == null)
        {
            image = Instantiate(Resources.Load<Image>(prefabsDirectory + name), characterImages.transform);
            image.name = name;
            image.sprite = Instantiate(Resources.Load<Sprite>(prefabsDirectory + name));
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
                image.GetComponent<RectTransform>().sizeDelta = _textConverter.ParameterToVector3(parameter);
                break;
            case OPTION_POSITION:
                image.GetComponent<RectTransform>().anchoredPosition = _textConverter.ParameterToVector3(parameter);
                break;
            case OPTION_ROTATION:
                image.GetComponent<RectTransform>().eulerAngles = _textConverter.ParameterToVector3(parameter);
                break;
            case OPTION_ACTIVE:
                image.gameObject.SetActive(_textConverter.ParameterToBool(parameter));
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

    private void Start()
    {
        _textConverter= new TextConverter();
    }
}

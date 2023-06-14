using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private string spritesDirectory = "Sprite/";

    [SerializeField]
    private Image backgroundImage;

    private const string OPTION_SPRITE = "_sprite";
    private const string OPTION_COLOR = "_color";

    public void SetBackgroundImage(string cmd, string parameter)
    {
        SetImage(cmd, parameter, backgroundImage);
    }

    private void SetImage(string cmd, string parameter, Image image)
    {
        cmd = cmd.Replace(" ", "");
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        switch (cmd)
        {
            case OPTION_SPRITE:
                image.sprite = LoadSprite(parameter);
                break;
            case OPTION_COLOR:
                image.color = ParameterToColor(parameter);
                break;
            default:
                break;
        }
    }

    private Sprite LoadSprite(string name)
    {
        return Instantiate(Resources.Load<Sprite>(spritesDirectory + name));
    }
    private Color ParameterToColor(string parameter)
    {
        string[] ps = parameter.Replace(" ", "").Split(',');
        if (ps.Length > 3)
            return new Color32(byte.Parse(ps[0]), byte.Parse(ps[1]),
                                            byte.Parse(ps[2]), byte.Parse(ps[3]));
        else
            return new Color32(byte.Parse(ps[0]), byte.Parse(ps[1]),
                                            byte.Parse(ps[2]), 255);
    }
}

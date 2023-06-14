
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using System;

// MonoBehaviourを継承することでオブジェクトにコンポーネントとして
// アタッチすることができるようになる
public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI mainText;
    [SerializeField]
    public TextMeshProUGUI nameText;


    private const char SIGN_PAGE = '&';
    private const char SIGN_SUBSCENE = '#';

    private TextConverter _textConverter;
    private CharaManager _charaManager;
    private BackgroundManager _backgroundManager;

    private Queue<string> _pageQueue;
    private Queue<char> _charQueue;

    private Dictionary<string, Queue<string>> _subScenes =
           new Dictionary<string, Queue<string>>();

    private string _text = "";



    //初期化
    private void Init()
    {
        _textConverter = GetComponent<TextConverter>();
        _charaManager = GetComponent<CharaManager>();
        _backgroundManager = GetComponent<BackgroundManager>();

        _text = _textConverter.LoadTextFile(""); //異常時はデフォルトのファイルを読み込む

        Queue<string> subScenes = DivideSIGN(_text, SIGN_SUBSCENE);
        foreach (string subScene in subScenes)
        {
            if (subScene.Equals(""))
            {
                continue;
            }

            Queue<string> pages = DivideSIGN(subScene, SIGN_PAGE);
            //ページの最初に格納されている文字列がシーン名
            _subScenes[pages.Dequeue()] = pages;
        }

        _pageQueue = _subScenes.First().Value;
        //最初のシーン（ページ）を表示
        ShowNextPage();
    }
    private void Start()
    {
        //ReadLine(_text);
        Init();
    }
    private void Update()
    {
        // 左(=0)クリックされたらOnClickメソッドを呼び出し
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    public bool ShowNextPage()
    {
        if (_pageQueue.Count <= 0)
        {
            return false;
        }

        // オブジェクトを非表示にする
        nextPageIcon.SetActive(false);

        ReadLine(_pageQueue.Dequeue());//TextReader.cs

        return true;
    }
    //クリック時の処理
    private void OnClick()
    {
        if (_charQueue.Count > 0)//全ての文字を表示しきっていない
        {
            OutputAllChar();
            nextPageIcon.SetActive(true);
        }
        else
        {
            if (ButtonNum() > 0)
                return;

            if (!ShowNextPage())
            {
                // UnityエディタのPlayモードを終了する
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }

    public void JumpTo(string parameter)
    {
        parameter = parameter.Substring(parameter.IndexOf('"') + 1, parameter.LastIndexOf('"') - parameter.IndexOf('"') - 1);
        _pageQueue = _subScenes[parameter];
    }
}
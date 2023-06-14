using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{

    private const char SIGN_MAIN_START = '「';
    private const char SIGN_MAIN_END = '」';
    private const char SIGN_COMMAND = '!';
    private const char SIGN_COMMAND_ARG = '=';

    private const string COMMAND_CHARACTER_IMAGE = "charaimg";
    private const string COMMAND_BACKGROUND = "background";
    private const string COMMAND_SELECT = "select";
    private const string COMMAND_JUMP = "jump_to";

    private void ReadLine(string text)
    {
        if (text[0].Equals(SIGN_COMMAND))
        {
            ReadCommand(text);

            if (ButtonNum() <= 0)
            {
                ShowNextPage();
            }

            return;
        }

        string[] ts = text.Split(SIGN_MAIN_START);
        string name = ts[0];
        string main = ts[1].Remove(ts[1].LastIndexOf(SIGN_MAIN_END));

        if (name.Equals(""))
        {
            nameText.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            nameText.text = name;
            nameText.transform.parent.gameObject.SetActive(true);
        }

        mainText.text = "";
        _charQueue = SeparateString(main);

        StartCoroutine(ShowChars());
    }

    // 1行を読み出す
    private void ReadCommand(string cmdLine)
    {
        cmdLine = cmdLine.Remove(0, 1);
        Queue<string> cmdQueue = DivideSIGN(cmdLine, SIGN_COMMAND);

        foreach (string cmd in cmdQueue)
        {
            string[] cmds = cmd.Split(SIGN_COMMAND_ARG);

            if (cmds[0].Contains(COMMAND_BACKGROUND))
            {
                _backgroundManager.SetBackgroundImage(cmds[0].Replace(COMMAND_BACKGROUND, ""), cmds[1]);
            }
            if (cmds[0].Contains(COMMAND_CHARACTER_IMAGE))
            {
                _charaManager.SetCharacterImage(cmds[0].Replace(COMMAND_CHARACTER_IMAGE, ""), cmds[1], cmds[2]);
            }
            if (cmds[0].Contains(COMMAND_JUMP))
            {
                JumpTo(cmds[1]);
            }
            if (cmds[0].Contains(COMMAND_SELECT))
            {
                SetSelectButton(cmds[0].Replace(COMMAND_SELECT, ""), cmds[1], cmds[2]);
            }
        }
    }

    public Queue<string> DivideSIGN(string text, char sign)
    {
        string[] strs = text.Split(sign);
        Queue<string> queue = new Queue<string>();

        foreach (string l in strs)
        {
            queue.Enqueue(l);
        }

        return queue;
    }

    // 文字列をchar型の配列にする = 1文字ごとに区切る
    private Queue<char> SeparateString(string str)
    {
        char[] chars = str.ToCharArray();
        Queue<char> charQueue = new Queue<char>();
        // foreach文で配列charsに格納された文字を全て取り出し
        // キューに加える
        foreach (char c in chars)
        {
            charQueue.Enqueue(c);
        }

        return charQueue;
    }
}

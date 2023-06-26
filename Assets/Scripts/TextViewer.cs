using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    [SerializeField]
    private float captionSpeed = 0.01f;

    private bool OutputChar()
    {
        // キューに何も格納されていなければfalseを返す
        if (_charQueue.Count <= 0)
        {
            nextPageIcon.SetActive(true);
            return false;
        }

        mainText.text += _charQueue.Dequeue();
        return true;
    }
    //一度に表示する
    private void OutputAllChar()
    {
        // コルーチンをストップ
        StopCoroutine(ShowChars());
        // キューが空になるまで表示
        while (OutputChar()) ; 

        _waitTime = 0;
        nextPageIcon.SetActive(true);
    }
    private IEnumerator ShowChars()
    {
        // OutputCharメソッドがfalseを返す(=キューが空になる)までループする
        while (OutputChar())
            // wait秒だけ待機
            yield return new WaitForSeconds(captionSpeed);
        // コルーチンを抜け出す
        yield break;
    }
}

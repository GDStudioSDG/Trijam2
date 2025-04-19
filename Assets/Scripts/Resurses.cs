using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Resurses : MonoBehaviour
{
    private int wood     = 0;
    private int water    = 0;
    private int stone    = 0;

    private int woodBuf  = 0;
    private int waterBuf = 0;
    private int stoneBuf = 0;

    private int woodBufCor = 0;
    private int waterBufCor = 0;
    private int stoneBufCor = 0;

    Color c;
    Coroutine coroutine = null;


    #region UI
    /*
    [SerializeField] private Text woodUI;
    [SerializeField] private Text waterUI;
    [SerializeField] private Text stoneUI;
    */
    GameObject Res;
    #endregion
    IEnumerator Fade()
    {
        woodBufCor = woodBuf; waterBufCor = waterBuf; stoneBufCor = stoneBuf;
        woodBuf = 0; waterBuf = 0; stoneBuf = 0;
        c.a = 1;
        ResUpdateInside();
        yield return new WaitForSeconds(1f);

        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            ResUpdateInside();
            yield return new WaitForSeconds(0.2f);
        }
        c.a = 0f;
        ResUpdateInside();


    }

        private void Awake()
    {
        c = Color.green;
        TurnManager.ResUpdate += ResUpdate;
        Res = GameObject.FindWithTag("Player1");
    }

    private void ResUpdateInside()
    {
        Res.transform.Find("WoodFon/Wood").GetComponent<Text>().text = "<color=#d56a1aff>Wood:" + wood + "</color><color=#" + ColorUtility.ToHtmlStringRGBA(c) + "> + " + woodBufCor + "</color>";
        Res.transform.Find("WaterFon/Water").GetComponent<Text>().text = "<color=aqua>Water:" + water + "</color><color=#" + ColorUtility.ToHtmlStringRGBA(c) + "> + " + waterBufCor + "</color>";
        Res.transform.Find("StoneFon/Stone").GetComponent<Text>().text = "<color=gray>Stone:" + stone + "</color><color=#" + ColorUtility.ToHtmlStringRGBA(c) + "> + " + stoneBufCor + "</color>";
    }
    private void ResUpdate()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        coroutine = StartCoroutine(Fade());
    }

    public void AddWater(int kol)
    {
        water += kol;
        waterBuf += kol;
    }
    public void AddWood(int kol)
    {
        wood += kol;
        woodBuf += kol;
    }
    public void AddStone(int kol)
    {
        stone += kol;
        stoneBuf += kol;
    }
}

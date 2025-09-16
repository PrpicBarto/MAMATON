using QTEPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTETest : MonoBehaviour
{
    [SerializeField] public QuickTimeEvent QTE;

    private void Start()
    {
        QTE.Hide();
    }
    public void Show()
    {

        QTE.ShowQTE(new Vector2(200f, 200f), 1, 0);

    }

    public void Hide()
    {

        QTE.Hide();
    }
}

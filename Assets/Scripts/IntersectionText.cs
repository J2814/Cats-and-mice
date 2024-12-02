using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntersectionText : MonoBehaviour
{
    private Text text;
    private float keyTextDefaultScale;
    private float keyTextPunchScaleMultiplier;
    private Color keyTextPunchColor;
    private Color keyTextDefaultColor;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        keyTextDefaultScale = transform.localScale.x;
        keyTextPunchScaleMultiplier = VisualSettings.instance.IntersectionTextPunchScale;
        keyTextDefaultColor = text.color;
        keyTextPunchColor = VisualSettings.instance.IntersectionColorPunch;
    }

    public void PunchAnim()
    {
        DOTween.Kill(transform);

        transform.DOScale(keyTextDefaultScale, 0);
        float punchScale = keyTextDefaultScale * keyTextPunchScaleMultiplier;
        Vector3 punchVec = new Vector3(punchScale, punchScale, punchScale);

        transform.DOPunchScale(punchVec, 0.2f, 0, 0);

        //ColorPunch
        Sequence colorSeq = DOTween.Sequence();
        colorSeq.Append(DOTween.To(() => text.color, x => text.color = x, keyTextPunchColor, 0.1f));
        colorSeq.Append(DOTween.To(() => text.color, x => text.color = x, keyTextDefaultColor, 0.1f));
       

    }

    public void SetText(string keyString)
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }

        string resultText = keyString;

        if (resultText.IndexOf("alpha", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            resultText = System.Text.RegularExpressions.Regex.Replace(resultText, "alpha", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        resultText = "[" + resultText + "]";
        text.text = resultText;
    }
}

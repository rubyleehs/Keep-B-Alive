using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ViewsManager : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public float maxFontSizeDelta, maxColorLerpDelta;
    public float fontSizeAfterChange;
    public Color gainViewsColor, loseViewsColor;

    public Transform boner;
    public Vector2 bonerHBounds;
    public float viewsLimitOnBoner;
    public Vector2 bonerScaleBounds;

    public int startViews;
    public Vector2 viewsFuzzerMultiplier;

    public Vector2Int positiveViewChange;
    public Vector2Int negativeViewChange;

    public Vector2 donationChanceBounds;
    public float viewsLimitOnDonations;

    private int currentViews;
    private int requestCount = 0;
    private Color defaultTextColor;
    private float defaultFontSize;

    private void Awake()
    {
        currentViews = startViews;
        defaultTextColor = textMesh.color;
        defaultFontSize = textMesh.fontSize;
    }

    public void RotateBoner(Slider slider)
    {
        boner.parent.rotation = Quaternion.Euler(Vector3.forward * -slider.value);
    }

    public void ScaleBonerX(Slider slider)
    {
        boner.localScale = new Vector3(Mathf.Lerp(bonerScaleBounds.x, bonerScaleBounds.y, slider.value), boner.localScale.y, 1);
    }

    public void ScaleBonerY(Slider slider)
    {
        boner.localScale = new Vector3(boner.localScale.x, Mathf.Lerp(bonerScaleBounds.x, bonerScaleBounds.y, slider.value), 1);
    }

    private void Update()
    {
        if(textMesh.fontSize != defaultFontSize) textMesh.fontSize = Mathf.MoveTowards(textMesh.fontSize, defaultFontSize, maxFontSizeDelta * Time.deltaTime);
        if(textMesh.color != defaultTextColor)
        {
            textMesh.color = Color.Lerp(textMesh.color, defaultTextColor, maxColorLerpDelta * Time.deltaTime);
        }
    }

    public void IncreaseViews()
    {
        currentViews += (int)Mathf.Lerp(positiveViewChange.x, positiveViewChange.y, Random.Range(0f, 1f));
        textMesh.color = gainViewsColor;
        textMesh.fontSize = fontSizeAfterChange;
        UpdateViews();
    }

    public void DecreaseViews()
    {
        currentViews += (int)Mathf.Lerp(negativeViewChange.x, negativeViewChange.y, Random.Range(0f, 1f));
        textMesh.color = loseViewsColor;
        UpdateViews();
    }

    public void UpdateViews()
    {
        if(currentViews <= 0)
        {
            currentViews = 0;
            GameManager.instance.GameOver();
        }
        textMesh.text = (int)(currentViews * Mathf.Lerp(viewsFuzzerMultiplier.x, viewsFuzzerMultiplier.y, Random.Range(0f, 1f))) + " viewing";

        boner.localPosition = Vector2.up * Mathf.Lerp(bonerHBounds.x, bonerHBounds.y, (float)currentViews / viewsLimitOnBoner);
    }

    public bool IsDonationIncoming()
    {
        UpdateViews();

        float chance = Mathf.Lerp(donationChanceBounds.x, donationChanceBounds.y, (float)currentViews / viewsLimitOnDonations);
        if (requestCount < 10) chance *= 1 + (10 - requestCount) * 0.035f;
        if (requestCount < 30) chance *= 1 + (30 - requestCount) * 0.03f;

        if (Random.Range(0f, 1f) <= chance)
        {
            requestCount++;
            return true;
        }
        else return false;
    }


}

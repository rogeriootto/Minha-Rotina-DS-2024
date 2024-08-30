using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{   
    private Vector2 startPosition;
    private Vector3 startScale;
    public bool shouldReturnToStartPosition;
    public bool isConnected = false;
    public bool alreadyPlayedParticle = false;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject rightPieceVFX;
    public Color color;
    public AudioClip correctSFX;
    public bool hint = false;
    private static float breathTime = 0f;
    private Vector3 breathIn = new Vector3(0.9f, 0.9f, 0.9f);
    private Vector3 breathOut = new Vector3(0.7f, 0.7f, 0.7f);
    private bool breathingIn = true;
    private float expandDuration = 0.3f;
    private bool hintState = false;

    // Da pra mudar transparencia com Canvas Group alpha
    // Aula 1 do Programatche
    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Update() {
        if (hint) {
            Vector3 targetScale = breathingIn ? breathIn : breathOut;
            Vector3 startScale = breathingIn ? breathOut : breathIn;

            breathTime += Time.deltaTime;
        
            float lerpfactor = breathTime / expandDuration;

            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, lerpfactor);
            if (lerpfactor >= 1) {
                breathingIn = !breathingIn;
                breathTime = 0;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isConnected) {
            startPosition = rectTransform.anchoredPosition;
            startScale = rectTransform.localScale;
            rectTransform.localScale = new Vector3(1f, 1f, 1f);
            shouldReturnToStartPosition = true;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            hintState = hint;
            hint = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (shouldReturnToStartPosition) {
            rectTransform.anchoredPosition = startPosition;
            rectTransform.localScale = startScale;
            GameManager.howManyWrong++;
            hint = hintState;
        }
        else if (!alreadyPlayedParticle) {
            Instantiate(rightPieceVFX, new Vector3(transform.position.x, transform.position.y, 50), Quaternion.identity);
            alreadyPlayedParticle = true;
            SetIsAlreadySet(true);
            if (hintState) {
                GameManager.howManyHints++;
            }
            hint = false;
            GameManager.hintTimeSpent = 0;
            AudioSource.PlayClipAtPoint(correctSFX, Camera.main.transform.position);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        GameManager.howManyTries++;
    }

    public void SetIsAlreadySet(bool isSet) {
        switch (gameObject.tag) {
            case "armario-certo":
                GameManager.isArmarioSet = isSet;
                break;
            case "criança-certo":
                GameManager.isCriancaSet = isSet;
                break;
            case "vaso-certo":
                GameManager.isVasoSet = isSet;
                break;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (!isConnected) {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }
}
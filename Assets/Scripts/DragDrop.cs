using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{   
    private Vector2 startPosition;
    private Vector3 startScale;
    public bool shouldReturnToStartPosition;
    public bool isConnected = false;
    public bool alreadyPlayedParticle = false;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject rightPieceVFX;

    public AudioClip correctSFX;

    // Da pra mudar transparencia com Canvas Group alpha
    // Aula 1 do Programatche
    public void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (shouldReturnToStartPosition) {
            rectTransform.anchoredPosition = startPosition;
            rectTransform.localScale = startScale;
            GameManager.howManyWrong++;
        }
        else if (!alreadyPlayedParticle) {
            Instantiate(rightPieceVFX, new Vector3(transform.position.x, transform.position.y, 50), Quaternion.identity);
            alreadyPlayedParticle = true;
            AudioSource.PlayClipAtPoint(correctSFX, Camera.main.transform.position);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        GameManager.howManyTries++;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isConnected) {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

}
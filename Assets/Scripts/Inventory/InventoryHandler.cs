using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Transform targetTransform; // �̵��� UI

    private Vector2 beginPoint;
    private Vector2 moveBegin;

    private void Awake()
    {
        // �̵� ��� UI�� �������� ���� ���, �ڵ����� �θ�� �ʱ�ȭ
        if (targetTransform == null)
            targetTransform = transform.parent;
    }

    // �巡�� ���� ��ġ ����
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        beginPoint = targetTransform.position;
        moveBegin = eventData.position;
    }
    
    // �巡�� : ���콺 Ŀ�� ��ġ�� �̵�
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        targetTransform.position = beginPoint + (eventData.position - moveBegin);
    }
}

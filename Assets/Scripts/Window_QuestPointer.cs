using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_QuestPointer : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    public GameObject SpaceShip;

    private void Awake()
    {

        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    private void Update()
    {
        targetPosition = SpaceShip.transform.position;
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

        if(isOffScreen == true)
        {
            transform.Find("Pointer").GetComponent<Image>().enabled = true;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if(cappedTargetScreenPosition.x <= 0)
            {
                cappedTargetScreenPosition.x = 20f;
            }
            if (cappedTargetScreenPosition.x >= Screen.width)
            {
                cappedTargetScreenPosition.x = Screen.width - 20f;
            }
            if (cappedTargetScreenPosition.y <= 0)
            {
                cappedTargetScreenPosition.y = 20f;
            }
            if (cappedTargetScreenPosition.y >= Screen.height)
            {
                cappedTargetScreenPosition.y = Screen.height - 20f;
            }
            //Debug.Log("Is off screen: " + isOffScreen);
            pointerRectTransform.localPosition = new Vector3(cappedTargetScreenPosition.x, cappedTargetScreenPosition.y, 0f);
        }
        else
        {
            //Debug.Log("Is off screen: " + isOffScreen);
            pointerRectTransform.localPosition = new Vector3(targetPositionScreenPoint.x, targetPositionScreenPoint.y, 0f);
            transform.Find("Pointer").GetComponent<Image>().enabled = false;

        }

    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}

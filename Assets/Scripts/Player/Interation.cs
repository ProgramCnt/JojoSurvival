using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interation : MonoBehaviour
{
    public float checkRate = 0.05f;
    public float maxDistance;
    public LayerMask layerMask;
    public TextMeshProUGUI promptText;
    float lastCheckTime;

    GameObject curInteractGO;
    IInteractable curInteractable;

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        CharacterManager.Instance.Player.controller.actionInteract += OnInteraction;
    }

    void Update()
    {
        CheckInteract();
    }

    void CheckInteract()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            Ray ray = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGO)
                {
                    curInteractGO = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();

                    SetPromptText();
                }
            }
            else
            {
                curInteractGO = null;
                curInteractable = null;

                promptText.gameObject.SetActive(false);
            }
        }
    }

    void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteraction()
    {
        if (curInteractGO)
        {
            // 아이템 상호작용 호출
            curInteractable.OnInteract();

            // 현재 아이템 지워주기
            curInteractGO = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}

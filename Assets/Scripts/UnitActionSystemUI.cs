using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    private void Start()
    {
        UnitActionSystem.Instance.onSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons()
    {
        foreach(Transform transform in actionButtonContainerTransform)
        {
            Destroy(transform.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach(BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(baseAction);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender , EventArgs e)
    {
        CreateUnitActionButtons();
    }
}

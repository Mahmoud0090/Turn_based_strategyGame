using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public event EventHandler onSelectedUnitChanged;  
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask selectedUnitLayerMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, selectedUnitLayerMask))
        {
            Unit unit = raycastHit.transform.GetComponent<Unit>();
            if(unit != null)
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        if(onSelectedUnitChanged != null)
        {
            onSelectedUnitChanged(this, EventArgs.Empty);
        }
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}

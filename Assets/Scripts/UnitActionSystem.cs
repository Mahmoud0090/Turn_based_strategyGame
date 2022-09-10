using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }


    public event EventHandler onSelectedUnitChanged;  
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask selectedUnitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than UnitActionSystem" + transform + "-" + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (isBusy) { return; }


        if (TryHandleUnitSelection()) return;
        

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            switch (selectedAction)
            {
                case MoveAction moveAction:
                    if (moveAction.IsvalidActionGridPosition(mouseGridPosition))
                    {
                        SetBusy();
                        moveAction.Move(mouseGridPosition, ClearBusy);
                    }
                    break;
                case SpinAction spinAction:
                    SetBusy();
                    spinAction.Spin(ClearBusy);
                    break;
            }
        }
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0)) 
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
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());

        if(onSelectedUnitChanged != null)
        {
            onSelectedUnitChanged(this, EventArgs.Empty);
        }
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}

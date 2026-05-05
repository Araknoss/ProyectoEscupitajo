using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicButtonNavigation : MonoBehaviour
{
    [System.Serializable]
    public class ButtonRow
    {
        public List<Button> buttons = new List<Button>();
    }

    [Header("Button Layout")]
    [SerializeField] private List<ButtonRow> rows = new List<ButtonRow>();

    [Header("Navigation Settings")]
    [SerializeField] private bool loopHorizontal = false;
    [SerializeField] private bool loopVertical = false;

    [Header("Selection")]
    [SerializeField] private Button defaultButton;

    private void OnEnable()
    {
        RefreshMenuNavigation();
    }

    public void RefreshMenuNavigation()
    {
        RefreshNavigation();
        //EnsureValidSelection();
    }

    public void RefreshNavigation()
    {
        List<List<Button>> activeRows = GetActiveRows();

        for (int rowIndex = 0; rowIndex < activeRows.Count; rowIndex++)
        {
            List<Button> row = activeRows[rowIndex];

            for (int columnIndex = 0; columnIndex < row.Count; columnIndex++)
            {
                Button currentButton = row[columnIndex];

                Navigation navigation = currentButton.navigation;
                navigation.mode = Navigation.Mode.Explicit;

                navigation.selectOnLeft = GetLeftButton(activeRows, rowIndex, columnIndex);
                navigation.selectOnRight = GetRightButton(activeRows, rowIndex, columnIndex);
                navigation.selectOnUp = GetVerticalButton(activeRows, rowIndex, columnIndex, -1);
                navigation.selectOnDown = GetVerticalButton(activeRows, rowIndex, columnIndex, 1);

                currentButton.navigation = navigation;
            }
        }
    }

    private List<List<Button>> GetActiveRows()
    {
        List<List<Button>> activeRows = new List<List<Button>>();

        foreach (ButtonRow row in rows)
        {
            List<Button> activeButtons = new List<Button>();

            foreach (Button button in row.buttons)
            {
                if (IsValidButton(button))
                {
                    activeButtons.Add(button);
                }
            }

            if (activeButtons.Count > 0)
            {
                activeRows.Add(activeButtons);
            }
        }

        return activeRows;
    }

    private Button GetLeftButton(List<List<Button>> activeRows, int rowIndex, int columnIndex)
    {
        List<Button> row = activeRows[rowIndex];

        if (columnIndex > 0)
        {
            return row[columnIndex - 1];
        }

        if (loopHorizontal && row.Count > 1)
        {
            return row[row.Count - 1];
        }

        return null;
    }

    private Button GetRightButton(List<List<Button>> activeRows, int rowIndex, int columnIndex)
    {
        List<Button> row = activeRows[rowIndex];

        if (columnIndex < row.Count - 1)
        {
            return row[columnIndex + 1];
        }

        if (loopHorizontal && row.Count > 1)
        {
            return row[0];
        }

        return null;
    }

    private Button GetVerticalButton(
        List<List<Button>> activeRows,
        int rowIndex,
        int columnIndex,
        int direction)
    {
        if (activeRows.Count == 0)
        {
            return null;
        }

        int targetRowIndex = rowIndex + direction;

        if (loopVertical)
        {
            if (targetRowIndex < 0)
            {
                targetRowIndex = activeRows.Count - 1;
            }
            else if (targetRowIndex >= activeRows.Count)
            {
                targetRowIndex = 0;
            }
        }
        else
        {
            if (targetRowIndex < 0 || targetRowIndex >= activeRows.Count)
            {
                return null;
            }
        }

        List<Button> targetRow = activeRows[targetRowIndex];

        if (targetRow.Count == 0)
        {
            return null;
        }

        int targetColumnIndex = Mathf.Clamp(columnIndex, 0, targetRow.Count - 1);

        return targetRow[targetColumnIndex];
    }

    private void EnsureValidSelection()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        if (selectedObject != null)
        {
            Button selectedButton = selectedObject.GetComponent<Button>();

            if (IsValidButton(selectedButton))
            {
                return;
            }
        }

        SelectDefaultOrFirstAvailableButton();
    }

    private void SelectDefaultOrFirstAvailableButton()
    {
        if (IsValidButton(defaultButton))
        {
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
            return;
        }

        //foreach (ButtonRow row in rows)
        //{
        //    foreach (Button button in row.buttons)
        //    {
        //        if (IsValidButton(button))
        //        {
        //            EventSystem.current.SetSelectedGameObject(button.gameObject);
        //            return;
        //        }
        //    }
        //}

        //EventSystem.current.SetSelectedGameObject(null);
    }

    private bool IsValidButton(Button button)
    {
        return button != null &&
               button.gameObject.activeInHierarchy &&
               button.interactable;
    }
}

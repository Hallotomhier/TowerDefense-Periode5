using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup

{
    public int rows;
    public int columns;
    public Vector2 cellSize;

    public Vector2 spacing;

    public enum Padding
    {
        Left,
        Right,
        Top,
        Bottom
    }
    public enum FitType
    {
        Uniform,
        Width,
        Heigth,
        FixedRows,
        FixedColumns
    }
    public FitType fitType;

    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Heigth || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;

            //Calculate the number of rows and columns:
            float squarRoot = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(squarRoot);
            columns = Mathf.CeilToInt(squarRoot);
        }

        //Calculate the amount of rows or columns when one of the two is a given int.
        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.Heigth || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        //Grab the width and height of the space:
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;


        //Automatically decide the size of the children:
        float cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * (columns - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        //Automatically assign the size of the children:
        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        //Create a variable for the amount of rows and columns
        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];
            //Create a variable for the x and y position of the cells/children
            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            //Set children accordingly alongside both axis
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);

        }
    }
    public override void CalculateLayoutInputVertical()
    {

    }
    public override void SetLayoutHorizontal()
    {

    }
    public override void SetLayoutVertical()
    {

    }
}

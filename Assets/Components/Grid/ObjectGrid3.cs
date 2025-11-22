using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ObjectGrid3 : MonoBehaviour
{

    (GameObject space, Block child)[] field;

    (int width, int height) dimentions;

    Image parentPanel;

    public Vector3 cellLocalScale = Vector3.one;

    public ObjectGrid3((int width, int height) newDimentions, Image panel)
    {
        dimentions = newDimentions;
        parentPanel = panel;
        this.cellLocalScale = new Vector3(GetUnit().x, GetUnit().y, GetUnit().x * 2);
    }


    public (int width, int height) GetDimentions()
    {
        return dimentions;
    }

    public (GameObject space, Block child)[] GetField()
    {
        return field;
    }

    public ObjectGrid3 setField((GameObject space, Block child)[] newField)
    {
        field = newField;
        return this;
    }

    public Image GetPanel()
    {
        return parentPanel;
    }

    public Vector2 GetUnit()
    {
        RectTransform rect = parentPanel.GetComponent<RectTransform>();
        return new Vector2(rect.sizeDelta.x / dimentions.width, rect.sizeDelta.y / dimentions.height);
    }

    public ObjectGrid3 createField()
    {
        (int width, int height) dimentions = this.GetDimentions();
        (GameObject space, Block child)[] newField = new (GameObject, Block)[dimentions.width * dimentions.height];
        Vector2 halfUnit = new Vector2((this.GetUnit().x / 2), (this.GetUnit().y / 2));
        (float X, float Y) location = (0, 0);

        for (int i = 0; i < newField.Length; i++)
        {
            newField[i].space = new GameObject();
            newField[i].space.transform.parent = this.GetPanel().transform;
            newField[i].space.transform.localPosition = new Vector3(location.X * this.GetUnit().x + halfUnit.x, location.Y * this.GetUnit().y + halfUnit.y, 0);
            newField[i].child = null;
            //this will determine the location
            location.X = location.X + 1;
            if (location.X % dimentions.width == 0)
            {
                location = (0, location.Y + 1);
            }
        }
        return this.setField(newField);
    }
}

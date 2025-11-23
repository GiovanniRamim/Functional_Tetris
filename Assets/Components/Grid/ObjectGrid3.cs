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

    GameObject container;

    private Vector3 cellLocalScale;

    public Vector3 CellLocalScale { get => this.settings.CellLocalScale; }

    ObjectGrid3_Settings settings;

    public bool debugMode = false;


    //TRANSFORMAR EM CLASSE DE CONFIGURACAO
    public ObjectGrid3 init(ObjectGrid3_Settings settings)
    {
        this.settings = settings;
        this.createField();
        return this;
    }

    public (int width, int height) GetDimentions()
    {
        return this.settings.Dimentions;
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
        return this.settings.GetUnit();
    }

    private ObjectGrid3 createField()
    {
        (int width, int height) dimentions = this.GetDimentions();
        (GameObject space, Block child)[] newField = new (GameObject, Block)[dimentions.width * dimentions.height];
        Vector2 halfUnit = new Vector2((this.GetUnit().x / 2), (this.GetUnit().y / 2));
        (float X, float Y) location = (0, 0);

        for (int i = 0; i < newField.Length; i++)
        {
            newField[i].space = new GameObject();
            newField[i].space.transform.parent = this.transform;
            newField[i].space.AddComponent<Grid_Cell>();
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

    public static ObjectGrid3 initialize(ObjectGrid3_Settings settings)
    {
        GameObject _groupObject = new GameObject();
        _groupObject.name = settings.NamePrefix + settings.Name;
        _groupObject.transform.parent = settings.Container.transform;
        _groupObject.transform.localPosition = settings.GroupLocalPosition;

        ObjectGrid3 newGrid = _groupObject.AddComponent<ObjectGrid3>();
        newGrid.init(settings);

        return newGrid;
    }
}

[CreateAssetMenu(fileName = "New_ObjectGrid3_Settings", menuName = "ObjectGrid3/Settings", order = 1)]
public class ObjectGrid3_Settings : ScriptableObject
{
    [SerializeField] private int _width;

    [SerializeField] private int _height;

    [SerializeField] private string _namePrefix;

    [SerializeField] private string _name;

    [SerializeField] private Vector3 _groupLocalPosition = Vector3.zero;

    private GameObject _container;

    private Vector3 cellLocalScale = Vector3.one;

    public GameObject Container { get => _container; set => _container = value; }

    public (int width, int height) Dimentions { get => (_width, _height); set {
            _height = value.height;
            _width = value.width;
        }
    }

    public string NamePrefix { get => _namePrefix; set => _namePrefix = value; }

    public string Name { get => _name; set => _name = value; }

    public Vector3 CellLocalScale { get => new Vector3(GetUnit().x, GetUnit().y, GetUnit().x * 2); set => cellLocalScale = value; }

    public Vector3 GroupLocalPosition { get => _groupLocalPosition; set => _groupLocalPosition = value; }

    public Vector2 GetUnit()
    {
        RectTransform rect = this._container.GetComponent<RectTransform>();
        return new Vector2(rect.sizeDelta.x / this._width, rect.sizeDelta.y / this._height);
    }
}

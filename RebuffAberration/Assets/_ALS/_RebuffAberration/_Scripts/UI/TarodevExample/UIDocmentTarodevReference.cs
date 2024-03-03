using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDocmentTarodevReference : MonoBehaviour
{
    [SerializeField] UIDocument _document;
    [SerializeField] StyleSheet _styleSheet;
    [SerializeField] float _lol;

    public static event Action<float> ScaleChanged;
    public static event Action SpinClicked;

    #region example 1;
 //   void Start()
 //   {
 //       Generate();
 //   }

	//private void OnValidate()
	//{
 //       if (Application.isPlaying) return;
 //       Generate();
	//}

	//void Generate()
	//{
 //       var root = _document.rootVisualElement;
 //       root.Clear();

 //       root.styleSheets.Add(_styleSheet);

 //       var titleLabel = new Label("Hello World");

 //       root.Add(titleLabel);

 //       var redBoi = new VisualElement();
 //       redBoi.AddToClassList("red-boi");

 //       root.Add(redBoi);

 //       var blueBoi = new VisualElement();
 //       blueBoi.AddToClassList("blue-boi");

 //       root.Add(blueBoi);
 //   }
    #endregion

    void Start()
    {
        StartCoroutine(Generate());
    }

    private void OnValidate()
    {
        if (Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
	{
        yield return null;
        var root = _document.rootVisualElement;
        root.Clear();
        root.styleSheets.Add(_styleSheet);

        var container = Create("container");

        var viewBox = Create("view-box", "bordered-box");
        container.Add(viewBox);

        var controlBox = Create("control-box", "bordered-box");

        var spinBtn = Create<Button>();
        spinBtn.text = "Spin";
        spinBtn.clicked += SpinClicked;
        controlBox.Add(spinBtn);

        var scaleSlider = Create<Slider>();
        scaleSlider.lowValue = 0.5f;
        scaleSlider.highValue = 2f;
        scaleSlider.RegisterValueChangedCallback(v => ScaleChanged.Invoke(v.newValue));
        controlBox.Add(scaleSlider);

        container.Add(controlBox);
        

        root.Add(container);

        if (Application.isPlaying)
        {
            var targetPosition = container.worldTransform.GetPosition();
            var startPosition = targetPosition + Vector3.up * 100;

            controlBox.experimental.animation.Position(targetPosition, 2000).from = startPosition;
            controlBox.experimental.animation.Start(0, 1, 2000, (e, v) => e.style.opacity = new StyleFloat(v));
        }
	}

    VisualElement Create(params string[] className)
	{
        return Create<VisualElement>(className);
	}

    T Create<T>(params string[] classNames) where T : VisualElement, new()
	{
        var ele = new T();
        foreach(var className in classNames)
		{
            ele.AddToClassList(className);
        }
        return ele;
	}

}

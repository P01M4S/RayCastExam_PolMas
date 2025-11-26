using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{

    InputAction _clickAcion;

    InputAction _positionAction;

    Vector2 _mousePoition;

    SceneManager _changeScene;

    Canvas _canvas;
    Text _text;

    [SerializeField] private Text numberText;
    private int hitCount = 0;

    [SerializeField] private float timerDuration = 5f;
    private Coroutine timerCoroutine;
    [SerializeField] private string sceneToLoadByName = "";
    [SerializeField] private int sceneToLoadByBuildIndex = -1;


    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _clickAcion = InputSystem.actions["Attack"];
        _positionAction = InputSystem.actions ["MousePosition"];
        if (numberText != null)
        {
            numberText.text = "0";
        }
    }

    void Update()
    {
        _mousePoition = _positionAction.ReadValue<Vector2>();

        if(_clickAcion.WasPerformedThisFrame())
        {
            ShootRaycast();
        }
    }

    void ShootRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePoition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.layer == 3)
            {
                Debug.Log(hit.transform.name);
                StartTimer();
            }
        }
    }

    void StartTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(CountdownCoroutine());
        
    }

    IEnumerator CountdownCoroutine()
    {
        float remaining = timerDuration;
        while (remaining > 0f)
        {
            if (numberText != null)
            {
                numberText.text = Mathf.CeilToInt(remaining).ToString();
            }
            remaining -= Time.deltaTime;
            yield return null;
        }
        if (numberText != null)
        {
            numberText.text = "0";
        }
    }
}


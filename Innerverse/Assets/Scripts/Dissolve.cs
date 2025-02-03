using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dissolve : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;

    private int _dissolveAmount = Shader.PropertyToID("DissolveLevel");
    private int _verticalDissolveLevel = Shader.PropertyToID("VerticalDissolve");


    // Start is called before the first frame update
    private void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _materials = new Material[_spriteRenderers.Length];
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
    }
    
    private IEnumerator Disappear(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float LerpedDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / _dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _materials.Length; i++)
            {
                if (useDissolve)
                    _materials[i].SetFloat(_dissolveAmount, LerpedDissolve);

                if (useVertical)
                    _materials[i].SetFloat(_verticalDissolveLevel, lerpedVerticalDissolve);
            }
            yield return null;
        }
    }

    private IEnumerator Appear(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float LerpedDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / _dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _materials.Length; i++)
            {
                if (useDissolve)
                {
                    _materials[i].SetFloat(_dissolveAmount, LerpedDissolve);
                }

                if (useVertical)
                {
                    _materials[i].SetFloat(_verticalDissolveLevel, lerpedVerticalDissolve);
                }
            }
        }
        yield return null;
    }
}



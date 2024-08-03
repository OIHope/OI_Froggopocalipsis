using System.Collections;
using UnityEngine;
using Utilities;

public class BasicEnemyBehaviour : MonoBehaviour, IDamagable
{
    [SerializeField] private int _hp = 20;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private AnimationCurve _colorChangeCurve;

    private IEnumerator ShowTakingDamage(Color startColor, Color endColor)
    {
        float elapsedTime = 0f;
        float duration = AnimationCurveDuration.Duration(_colorChangeCurve);

        while (elapsedTime < duration)
        {
            _spriteRenderer.material.color = Color.Lerp(startColor, endColor, _colorChangeCurve.Evaluate(elapsedTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _spriteRenderer.color = endColor;
    }

    public void TakeDamage(int damageValue)
    {
        _hp -= 10;
        StopAllCoroutines();

        if (_hp <= 0)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(ShowTakingDamage(Color.red, Color.white));
    }
}

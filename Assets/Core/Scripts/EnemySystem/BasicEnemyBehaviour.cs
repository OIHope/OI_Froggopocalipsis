using Components;
using System.Collections;
using UnityEngine;
using Utilities;

public class BasicEnemyBehaviour : MonoBehaviour, IDamagable
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHP;

    [SerializeField] private ProgressBar _hpBar;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private AnimationCurve _colorChangeCurve;

    private void Awake()
    {
        _hpBar.UpdateProgressBar(_hp, _maxHP);
    }
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
        Debug.Log("Received: " +  damageValue + " dmg");
        _hp -= damageValue;
        _hpBar.UpdateProgressBar(_hp, _maxHP);
        StopAllCoroutines();

        if (_hp <= 0)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(ShowTakingDamage(Color.red, Color.white));
    }
}

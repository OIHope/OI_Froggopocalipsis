using Components;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

public class BasicEnemyBehaviour : MonoBehaviour, IDamagable
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHP;

    [SerializeField] private ProgressBarComponent _hpBar;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private AnimationCurve _colorChangeCurve;
    [Space]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;

    private void Awake()
    {
        _hpBar.UpdateProgressBar(_hp, _maxHP);
        SetPath();
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
    private void FixedUpdate()
    {
        if (_agent.pathStatus == NavMeshPathStatus.PathComplete) Debug.Log("Agent get to the point");
    }

    private void SetPath()
    {
        NavMeshPath path = new();
        _agent.CalculatePath(_target.position, path);
        _agent.SetPath(path);
    }
}

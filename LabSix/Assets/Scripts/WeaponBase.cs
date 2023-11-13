using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Base Stats")]
    //protected means "our children can access this field"
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float chargeUpTime;
    [SerializeField, Range(0, 1)] protected float minChargePercent;
    [SerializeField] private bool isFullyAuto;

    //private variables should have underscores before them 
    private Coroutine _currentFireTimer;
    private bool _isOnCooldown;

    private WaitForSeconds _coolDownWait;
    private WaitUntil _coolDownEnforce;
    private float _currentChargeTime;


    private void Start()
    {
        _coolDownWait = new WaitForSeconds(timeBetweenAttacks);
        _coolDownEnforce = new WaitUntil(() => !_isOnCooldown);
    }

    public void StartShooting()
    {
        _currentFireTimer = StartCoroutine(RefireTimer());
    }

    public void StopShooting()
    {
        StopCoroutine(_currentFireTimer);

        float percent = _currentChargeTime / chargeUpTime;

        //you can write if statements like this if only one thing is happening
        if (percent != 0) TryAttack(percent);
    }

  

    
    private IEnumerator CooldownTimer()
    {
        _isOnCooldown = true;
        yield return _coolDownWait;
        _isOnCooldown = false;
    }

    //IEnumerator only works when it needs to work 
    private IEnumerator RefireTimer()
    {
        print("waiting for cooldown");
        //wait until we are not longer on cooldown
        yield return _coolDownEnforce;
        print("post cooldown");

        while (_currentChargeTime < chargeUpTime)
        {
            _currentChargeTime += Time.deltaTime;
            yield return null;
        }
        
        TryAttack(1);

        yield return null;
    }

    private void TryAttack(float percent)
    {
        _currentChargeTime = 0;
        if (!CanAttack(percent)) return;

        Attack(percent);

        StartCoroutine(CooldownTimer());

        if (isFullyAuto && percent >= 1) _currentFireTimer = StartCoroutine(RefireTimer()); //Auto refire
        

    }

    
    protected virtual bool CanAttack(float percent)
    {
        Vector3 math = 50 * Time.deltaTime * Vector3.one;

        return !_isOnCooldown && percent >= minChargePercent;
    }

    //the child will say what it this function does
    protected abstract void Attack(float percent);
}

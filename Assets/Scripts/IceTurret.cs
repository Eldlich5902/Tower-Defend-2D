﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IceTurret : MonoBehaviour
{
    [Header("Referecnces")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;//Khoảng cách tối đa mà tháp có thể bắn
    [SerializeField] private float fireRate = 2f;//Tốc độ bắn của tháp
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;
    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if(timeUntilFire >= 1f / fireRate)
        {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }
    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
        (Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0) {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);
        em.UpdateSpeed(0.5f);
        StartCoroutine(ResetEnemySpeed(em));
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}

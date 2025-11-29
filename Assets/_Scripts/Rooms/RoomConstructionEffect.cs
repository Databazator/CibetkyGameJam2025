using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomConstructionEffect : MonoBehaviour
{
    public List<Transform> FloorTiles;
    public List<Transform> Obstacles;
    public List<Transform> AdditionalObjects;
    public bool buildOnStart = true;
    
    public float ItemConstructionDuration;
    public float PhaseDuration;
    public float EarlyNextPhaseTime;

    private float _itemsStartHeight = -250f;

    public Action RoomConstructed;
    private void Start()
    {
        //hide room
        foreach(Transform t in FloorTiles)
        { t.gameObject.SetActive(false); }
        foreach(Transform t in Obstacles)
        { t.gameObject.SetActive(false); }
        foreach(Transform t in AdditionalObjects)
        { t.gameObject.SetActive(false); }

        if (buildOnStart)
        {
            DOVirtual.DelayedCall(1f, () => ConstructRoom());
        }
    }

    public void ConstructRoom()
    {
        //floor phase
        float delay = 0;
        ConstructionPhase(FloorTiles, delay);
        delay += PhaseDuration - EarlyNextPhaseTime;
        ConstructionPhase(Obstacles, delay);
        delay += PhaseDuration;
        ConstructionPhase(AdditionalObjects, delay);
        delay += PhaseDuration;
        RoomConstructed?.Invoke();
    }

    void ConstructionPhase(List<Transform> items, float startDelay)
    {
        foreach (Transform t in items)
        {
            t.gameObject.SetActive(true);
            float itemTargetHeight = t.position.y;
            t.position = new Vector3(t.position.x, _itemsStartHeight, t.position.z);
            float itemPopDelay = UnityEngine.Random.Range(startDelay, startDelay + PhaseDuration - ItemConstructionDuration);
            t.DOMoveY(itemTargetHeight, ItemConstructionDuration).SetEase(Ease.OutExpo).SetDelay(itemPopDelay);
        }
    }

}

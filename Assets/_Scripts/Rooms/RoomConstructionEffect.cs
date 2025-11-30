using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RoomConstructionEffect : MonoBehaviour
{
    public List<Transform> FloorTiles;
    public List<Transform> Obstacles;
    public List<Transform> AdditionalObjects;
    public List<AudioClip> SoundEffects;
    
    public bool buildOnStart = true;
    
    public float ItemConstructionDuration;
    public float PhaseDuration;
    public float EarlyNextPhaseTime;

    private float _itemsStartHeight = -250f;

    public Action RoomConstructed;

    public bool IsRoomConstructed = false;

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
        if(IsRoomConstructed)
        {
            Debug.LogWarning("Trying to construct a room that was already plink-plonked up. no good buddy.");
            return;
        }
        //floor phase.
        float delay = 0;
        ConstructionPhase(FloorTiles, delay);
        delay += PhaseDuration - EarlyNextPhaseTime;
        ConstructionPhase(Obstacles, delay);
        delay += PhaseDuration;
        ConstructionPhase(AdditionalObjects, delay);
        delay += PhaseDuration;
        IsRoomConstructed = true;
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
            // Play sound
            DOVirtual.DelayedCall(itemPopDelay, () => {
                var effect = SoundEffects[UnityEngine.Random.Range(0, SoundEffects.Count)];
                // Change pitch to random offset
                this.PlayClipAt(effect, t.position);
            });
        }
    }

    private void PlayClipAt(AudioClip clip, Vector3 pos)
    {
        var tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        var aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        // set other aSource properties here, if desired
        aSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        aSource.Play(); // start the sound
        StartCoroutine(AudioFade.FadeOut(new Sound()
        {
            name="",
            clip=clip,
            volume=1f,
            pitch=aSource.pitch,
            loop=false,
            source=aSource
        }, 0.4f, Mathf.SmoothStep));
        Destroy(tempGO, clip.length); // destroy object after clip duration
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayPlayerSoundEffects : MonoBehaviour
{
    [SerializeField] private AudioClip _onDashClip, _onBounceClip;
    private PlayerMovementFinite playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovementFinite>();
        playerMovement.On_Dashing += playerMovement_On_Dashing_Action;
        playerMovement.On_Bouncing += playerMovement_On_Bouncing_Action;
    }

    void playerMovement_On_Dashing_Action(object sender, EventArgs args)
    {
        SoundManager.Instance.PlaySound(_onDashClip);
    }
    void playerMovement_On_Bouncing_Action(object sender, EventArgs args)
    {
        SoundManager.Instance.PlaySound(_onBounceClip);
    }
}

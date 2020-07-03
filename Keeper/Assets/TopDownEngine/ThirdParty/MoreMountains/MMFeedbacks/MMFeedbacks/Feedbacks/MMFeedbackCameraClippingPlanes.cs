﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MoreMountains.Feedbacks
{
    /// <summary>
    /// This feedback lets you control a camera's clipping planes over time. You'll need a MMCameraClippingPlanesShaker on your camera.
    /// </summary>
    [AddComponentMenu("")]
    [FeedbackPath("Camera/Clipping Planes")]
    [FeedbackHelp("This feedback lets you control a camera's clipping planes over time. You'll need a MMCameraClippingPlanesShaker on your camera.")]
    public class MMFeedbackCameraClippingPlanes : MMFeedback
    {
        /// sets the inspector color for this feedback
        public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.CameraColor; } }
        /// returns the duration of the feedback
        public override float FeedbackDuration { get { return Duration; } }

        [Header("Clipping Planes Feedback")]
        /// the channel to emit on
        public int Channel = 0;
        /// the duration of the shake, in seconds
        public float Duration = 2f;
        /// whether or not to reset shaker values after shake
        public bool ResetShakerValuesAfterShake = true;
        /// whether or not to reset the target's values after shake
        public bool ResetTargetValuesAfterShake = true;
        /// whether or not to add to the initial value
        public bool RelativeClippingPlanes = false;

        [Header("Near Plane")]
        /// the curve used to animate the intensity value on
        public AnimationCurve ShakeNear = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        /// the value to remap the curve's 0 to        
        public float RemapNearZero = 0.3f;
        /// the value to remap the curve's 1 to        
        public float RemapNearOne = 100f;

        [Header("Far Plane")]
        /// the curve used to animate the intensity value on
        public AnimationCurve ShakeFar = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        /// the value to remap the curve's 0 to        
        public float RemapFarZero = 0.3f;
        /// the value to remap the curve's 1 to        
        public float RemapFarOne = 100f;

        /// <summary>
        /// Triggers the corresponding coroutine
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attenuation"></param>
        protected override void CustomPlayFeedback(Vector3 position, float attenuation = 1.0f)
        {
            if (Active)
            {
                MMCameraClippingPlanesShakeEvent.Trigger(ShakeNear, Duration, RemapNearZero, RemapNearOne, 
                    ShakeFar, RemapFarZero, RemapFarOne,
                    RelativeClippingPlanes,
                    attenuation, Channel, ResetShakerValuesAfterShake, ResetTargetValuesAfterShake);
            }
        }
    }
}

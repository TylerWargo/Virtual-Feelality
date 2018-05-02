/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

namespace Leap.Unity {
  /**
   * LeapHandController uses a Factory to create and update HandRepresentations based on Frame's received from a Provider  */
  public class LeapHandController : MonoBehaviour {
    protected LeapProvider provider;
    protected HandPool pool;

    protected Dictionary<int, HandRepresentation> graphicsHandReps = new Dictionary<int, HandRepresentation>();
    protected Dictionary<int, HandRepresentation> physicsHandReps = new Dictionary<int, HandRepresentation>();

    protected bool graphicsEnabled = true;
    protected bool physicsEnabled = true;

    public bool GraphicsEnabled {
      get {
        return graphicsEnabled;
      }
      set {
        graphicsEnabled = value;
      }
    }

    public bool PhysicsEnabled {
      get {
        return physicsEnabled;
      }
      set {
        physicsEnabled = value;
      }
    }

    protected virtual void OnEnable() {
      provider = requireComponent<LeapProvider>();
      pool = requireComponent<HandPool>();

      provider.OnUpdateFrame += OnUpdateFrame;
      provider.OnFixedFrame += OnFixedFrame;
    }

    protected virtual void OnDisable() {
      provider.OnUpdateFrame -= OnUpdateFrame;
      provider.OnFixedFrame -= OnFixedFrame;
    }

    /** Updates the graphics HandRepresentations. */
    protected virtual void OnUpdateFrame(Frame frame) {
      if (frame != null && graphicsEnabled) {
        UpdateHandRepresentations(graphicsHandReps, ModelType.Graphics, frame);
      }
    }

    /** Updates the physics HandRepresentations. */
    protected virtual void OnFixedFrame(Frame frame) {
      if (frame != null && physicsEnabled) {
        UpdateHandRepresentations(physicsHandReps, ModelType.Physics, frame);
      }
    }

    /** 
    * Updates HandRepresentations based in the specified HandRepresentation Dictionary.
    * Active HandRepresentation instances are updated if the hand they represent is still
    * present in the Provider's CurrentFrame; otherwise, the HandRepresentation is removed. If new
    * Leap Hand objects are present in the Leap HandRepresentation Dictionary, new HandRepresentations are 
    * created and added to the dictionary. 
    * @param all_hand_reps = A dictionary of Leap Hand ID's with a paired HandRepresentation
    * @param modelType Filters for a type of hand model, for example, physics or graphics hands.
    * @param frame The Leap Frame containing Leap Hand data for each currently tracked hand
    */
    protected virtual void UpdateHandRepresentations(Dictionary<int, HandRepresentation> all_hand_reps, ModelType modelType, Frame frame) {
      for (int i = 0; i < frame.Hands.Count; i++) {
        var curHand = frame.Hands[i];
        HandRepresentation rep;
        if (!all_hand_reps.TryGetValue(curHand.Id, out rep)) {
          rep = pool.MakeHandRepresentation(curHand, modelType);
          if (rep != null) {
            all_hand_reps.Add(curHand.Id, rep);
          }
        }
        if (rep != null) {
          rep.IsMarked = true;
          rep.UpdateRepresentation(curHand);
          rep.LastUpdatedTime = (int)frame.Timestamp;
        }
      }

      /** Mark-and-sweep to finish unused HandRepresentations */
      HandRepresentation toBeDeleted = null;
      for (var it = all_hand_reps.GetEnumerator(); it.MoveNext();) {
        var r = it.Current;
        if (r.Value != null) {
          if (r.Value.IsMarked) {
            r.Value.IsMarked = false;
          } else {
            /** Initialize toBeDeleted with a value to be deleted */
            //Debug.Log("Finishing");
            toBeDeleted = r.Value;
          }
        }
      }
      /**Inform the representation that we will no longer be giving it any hand updates 
       * because the corresponding hand has gone away */
      if (toBeDeleted != null) {
        all_hand_reps.Remove(toBeDeleted.HandID);
        toBeDeleted.Finish();
      }
    }

    private T requireComponent<T>() where T : Component {
      T component = GetComponent<T>();
      if (component == null) {
        string componentName = typeof(T).Name;
        Debug.LogError("LeapHandController could not find a " + componentName + " and has been disabled.  Make sure there is a " + componentName + " on the same gameObject.");
        enabled = false;
      }
      return component;
    }
  }
}

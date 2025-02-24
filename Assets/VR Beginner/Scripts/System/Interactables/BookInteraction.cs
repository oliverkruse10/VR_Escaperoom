using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Small modification of the classic XRGrabInteractable that will keep the position and rotation offset between the
/// grabbed object and the controller instead of snapping the object to the controller. Better for UX and the illusion
/// of holding the thing (see Tomato Presence : https://owlchemylabs.com/tomatopresence/)
/// </summary>
public class BookInteraction : XRGrabInteractable
{
    class SavedTransform
    {
        public Vector3 OriginalPosition;
        public Quaternion OriginalRotation;
    }

    Dictionary<XRBaseInteractor, SavedTransform> m_SavedTransforms = new Dictionary<XRBaseInteractor, SavedTransform>();
    Rigidbody m_Rb;

    public GameObject exitDoor;
    public TaskCompletionSound taskCompletionSound;




    protected override void Awake()
    {
        base.Awake();

        // The base class already grabs it but doesn't expose it so we have to grab it again
        m_Rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs evt)
    {
        var interactor = evt.interactor;
        if (interactor is XRDirectInteractor)
        {
            SavedTransform savedTransform = new SavedTransform();

            savedTransform.OriginalPosition = interactor.attachTransform.localPosition;
            savedTransform.OriginalRotation = interactor.attachTransform.localRotation;

            m_SavedTransforms[interactor] = savedTransform;

            bool haveAttach = attachTransform != null;

            interactor.attachTransform.position = haveAttach ? attachTransform.position : m_Rb.worldCenterOfMass;
            interactor.attachTransform.rotation = haveAttach ? attachTransform.rotation : m_Rb.rotation;
        }

        base.OnSelectEntered(evt);

        if (exitDoor != null)
        {
            if (exitDoor.activeSelf)
            {
                taskCompletionSound.PlayCompletionSound();
                exitDoor.SetActive(false);
                
            }
            else
            {
                Debug.Log("exitDoor was already inactive.");
            }
        }
        








    }

    protected override void OnSelectExited(SelectExitEventArgs evt)
    {
        var interactor = evt.interactor;
        if (interactor is XRDirectInteractor)
        {
            SavedTransform savedTransform = null;
            if (m_SavedTransforms.TryGetValue(interactor, out savedTransform))
            {
                interactor.attachTransform.localPosition = savedTransform.OriginalPosition;
                interactor.attachTransform.localRotation = savedTransform.OriginalRotation;

                m_SavedTransforms.Remove(interactor);
            }
        }

        base.OnSelectExited(evt);
    }

    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        int interactorLayerMask = 1 << interactor.gameObject.layer;
        return base.IsSelectableBy(interactor) && (interactionLayerMask.value & interactorLayerMask) != 0;
    }
}

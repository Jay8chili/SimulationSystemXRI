using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectMovementHelper : MonoBehaviour
{
    #region  PublicDeclarations
    public bool ResetThisObjectOnRelease;
    //move to detect
    public UnityEvent onDestinationReached;
    #endregion

    #region  MoveToHelperDeclarations
    private int _resetDelay = 2;
   
    #endregion

    #region  RessetableDeclarations
    private XRGrabInteractable _event;
    private bool _setupDone;
    private MyTransform ResetTransform;
    private Task PrevTask;

    private TaskCompletionSource<bool> _moveToPositionTaskCompletionSource;
    private CancellationTokenSource _moveToPositionCancellationToken;
    #endregion

    #region  PrivateMethods

    private void Awake()
    {
        //**Done**  //! Why this? This creates an empty gameObject in the scene. Can be changed to a Transform instead.
      
        SetupObject();
        RegisterObject();

       // onDestinationReached.AddListener(()=> { GetComponent<PointableCanvasUnityEventWrapper>().enabled = true; });
    }

    private void OnDisable()
    {
      _moveToPositionCancellationToken?.Cancel();
    }
    private void OnApplicationQuit()
    {
      _moveToPositionCancellationToken?.Cancel();
    }
    private void OnDestroy()
    {
      _moveToPositionCancellationToken?.Cancel();
    }
    private void RegisterObject()
    {
        //AddListeners
        _event = GetComponent<XRGrabInteractable>();
        if (_event != null)
        {
            _event.selectExited.AddListener(delegate { BeginReset(); });
            _event.selectEntered.AddListener(delegate { _moveToPositionCancellationToken?.Cancel(); });

        }

    }
    private void DeRegisterObject()
    {
        ResetThisObjectOnRelease = false;
        _event.selectExited.RemoveListener(delegate
        {
            BeginReset();
         
        });

    }

    public async void MoveToPosition(TransformContainer moveToTransform, bool needsDelay)
    {
        // Cancel the previous task if it exists
        _moveToPositionCancellationToken?.Cancel();
        _moveToPositionCancellationToken = new CancellationTokenSource();

        // Create a new task completion source
        _moveToPositionTaskCompletionSource = new TaskCompletionSource<bool>();

        try
        {
            if (needsDelay)
            {
                await Task.Delay(_resetDelay * 1000);
            }

            await transform.MoveToPositionAsync(moveToTransform, _moveToPositionCancellationToken.Token);

            // Notify that the destination has been reached
            onDestinationReached?.Invoke();
        }
        catch (OperationCanceledException)
        {
            // Handle cancellation if needed
        }
        finally
        {
            // Complete the task completion source
            _moveToPositionTaskCompletionSource.TrySetResult(true);
        }
    }
    private void SetupObject()
    {
        if (_setupDone) return;
        //else
        ResetTransform = new MyTransform(this.transform);
        _setupDone = true;
    }
    private void BeginReset()
    {
        if (ResetThisObjectOnRelease)
        {
            MoveToPosition(ResetTransform.GetThisTransform(), true);
        }

    }

    #endregion

    #region  Public Methods
    
    public void ForceUpdateResettablePos(Transform ResetToThisTransform)
    {
        ResetTransform = new MyTransform(ResetToThisTransform);
    }
    public void ForceResetNow()
    {
        if (GetComponent<XRGrabInteractable>())
        {
            var TempGrabbable = GetComponent<XRGrabInteractable>();
            if (TempGrabbable.isActiveAndEnabled)
            {
                TempGrabbable.ForceUnGrab();
                MoveToPosition(ResetTransform.GetThisTransform(), true);
            }
            else
            { MoveToPosition(ResetTransform.GetThisTransform(), true); }
        }
        else MoveToPosition(ResetTransform.GetThisTransform(), true);
    }

    public void DeregisterResettable()
    {
        DeRegisterObject();
    }
    public void MoveObjectToPosition(Transform moveToTransform)
    {
        TransformContainer TempTransform = new TransformContainer();
        TempTransform.Position = moveToTransform.position;
        TempTransform.Rotation = moveToTransform.rotation;
        TempTransform.localScale = moveToTransform.localScale;
        MoveToPosition(TempTransform, false);
    }
    public void ChangeParent(Transform newParentTransform)
    {
        transform.parent = newParentTransform;
    }

    #endregion

}

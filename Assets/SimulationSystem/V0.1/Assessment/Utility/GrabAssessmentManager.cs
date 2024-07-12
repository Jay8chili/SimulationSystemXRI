
using SimulationSystem.V0._1.Simulation;
using UnityEngine.XR.Interaction.Toolkit;

namespace SimulationSystem.V0._1.Assessment.Utility
{
    public static class GrabAssessmentManager
    {
        public static void CheckForGrabError(XRGrabInteractable grabbable)
        {
            if (!SimulationManager.instance.currentState.stateGrabbables.Contains(grabbable) &&
                !SimulationManager.instance.currentState.allowedStateGrabbables.Contains(grabbable))
            {
                AssessmentManager.DeductScore(AssessmentType.Grab);
            }
        }
    }
}
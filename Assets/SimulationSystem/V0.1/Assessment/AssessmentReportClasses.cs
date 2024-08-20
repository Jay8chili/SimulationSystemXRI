using System;
using System.Collections.Generic;
namespace SimulationSystem.V0._1.Assessment
{
    [Serializable]
    public class AssessmentStep
    {
        public string name;
        public float score;
    }
    [Serializable]
    public class AssessmentSheet
    {
        public string mode;
        public string start_ts;
        public List<AssessmentStep> steps;
    }
    [Serializable]
    public class AssessmentResult
    {
        public string error_msg = " ";
        public string event_ts;
        public int id;
        public float score;
        public AssessmentStatus status;
    }

    [System.Serializable]
    public class AuthTokenResponse
    {
        public string auth_token;
    }
    [Serializable]
    public enum AssessmentStatus
    {
        Success,
        Error,
        Failure,
        Undone
    }
}
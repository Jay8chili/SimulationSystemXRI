using UnityEngine;

namespace SimulationSystem.V0._1.Utility.ToBeRefactored
{
    public class PlayAudioAnimated : MonoBehaviour
    {
        private AudioSource _audioFX;
        private Animator _anim;
        [SerializeField] private bool _shouldPlay;
        private void Awake()
        {
            if (TryGetComponent<Animator>(out _anim))
            {

            }

            if (TryGetComponent<AudioSource>(out _audioFX))
            {

            }
        }

        public void PlayAudioFromAnim()
        {
            _shouldPlay = true;
            _audioFX.PlayOneShot(_audioFX.clip);
        }

        public void StopAudioFromAnim()
        {
            _shouldPlay = false;
        }
    }
}

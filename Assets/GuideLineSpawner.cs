using UnityEngine;

public class GuideLineSpawner : MonoBehaviour
{
    [SerializeField] private Transform _guidLineL, _guideLineR;
    [SerializeField] private float _offset;
    [SerializeField] private int _count;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < _count; i++)
        {
            Vector3 offset = new(_offset * i, 0, 0);
            GameObject l = Instantiate(_guidLineL.gameObject, _guidLineL.position + offset, _guidLineL.rotation, this.transform);
            GameObject r = Instantiate(_guideLineR.gameObject, _guideLineR.position + offset, _guideLineR.rotation, this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

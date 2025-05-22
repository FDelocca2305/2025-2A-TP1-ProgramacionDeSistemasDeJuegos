using UnityEngine;

namespace Excercise1
{
    public class Player : Character
    {
        [SerializeField] private float frequency = 1;
        [SerializeField] private float amplitude = 1;

        private void Reset()
            => id = nameof(Player);
        
        private void Awake() {
            if (frequency <= 0) {
                Debug.LogWarning($"[{name}] frequency should be > 0, using 1 by default.");
                frequency = 1;
            }
        }

        private void Update()
        {
            transform.position = new Vector3(Mathf.Cos(Time.time * frequency) * amplitude,
                                             Mathf.Sin(Time.time * frequency) * amplitude,
                                             transform.position.z);
        }
    }
}
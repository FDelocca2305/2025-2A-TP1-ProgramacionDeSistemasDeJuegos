using System;
using UnityEngine;

namespace Excercise1
{
    public class Enemy : Character
    {
        private const string DefaultPlayerId = "Player";
        [SerializeField] private float speed = 5;
        [SerializeField] private string playerId = DefaultPlayerId;
        private ICharacter _player;
        private string _logTag;

        private void Reset()
            => id = nameof(Enemy);

        private void Awake()
            => _logTag = $"{name}({nameof(Enemy).Colored("#555555")}):";

        protected override void OnEnable()
        {
            base.OnEnable();
            if (CharacterService.Instance == null) {
                Debug.LogError($"{_logTag} No existe CharacterService en la escena.");
                return;
            }
            if (!CharacterService.Instance.TryGetCharacter(playerId, out _player)) {
                Debug.LogError($"{_logTag} No se encontró player con ID ‘{playerId}’");
            }
        }

        private void Update()
        {
            if (_player == null)
                return;
            var direction = _player.transform.position - transform.position;
            transform.position += direction.normalized * (speed * Time.deltaTime);
        }
    }
}
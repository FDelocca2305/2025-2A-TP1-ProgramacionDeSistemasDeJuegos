using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    public class CharacterService : MonoBehaviour
    {
        private readonly Dictionary<string, ICharacter> _charactersById = new();
        public static CharacterService Instance { get; private set; }
        
        void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this) {
                Debug.LogWarning($"[{nameof(CharacterService)}] deleting duplicated instance of {name}");
                Destroy(gameObject);
            }
        }

        public bool TryAddCharacter(string id, ICharacter character)
        {
            if (string.IsNullOrWhiteSpace(id) || character == null) return false;
            return _charactersById.TryAdd(id, character);
        }

        public bool TryRemoveCharacter(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            return _charactersById.Remove(id);
        }

        public bool TryGetCharacter(string id, out ICharacter character)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                character = null; 
                Debug.LogError($"[{nameof(CharacterService)}] TryGetCharacter received a null or empty id.");
                return false;
            }
            
            if (_charactersById.TryGetValue(id, out character))
            {
                return true;
            }
            
            Debug.LogWarning($"[{nameof(CharacterService)}] character was not found with id: '{id}'.");
            return false;
        }
    }
}

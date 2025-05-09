using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    public class CharacterService : MonoBehaviour
    {
        private readonly Dictionary<string, ICharacter> _charactersById = new();
        public static CharacterService Instance { get; private set; }
        
        void Awake()
        {
            if (Instance!=null && Instance!=this) Destroy(gameObject);
            else { Instance = this; DontDestroyOnLoad(gameObject); }
        }
        
        public bool TryAddCharacter(string id, ICharacter character)
            => _charactersById.TryAdd(id, character);
        public bool TryRemoveCharacter(string id)
            => _charactersById.Remove(id);
        public bool TryGetCharacter(string id, out ICharacter c) 
            => _charactersById.TryGetValue(id, out c);
    }
}

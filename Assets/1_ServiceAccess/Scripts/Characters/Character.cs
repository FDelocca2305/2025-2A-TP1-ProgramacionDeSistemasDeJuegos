using System;
using UnityEngine;

namespace Excercise1
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected string id;

        protected virtual void OnEnable()
        {
            if (CharacterService.Instance.TryAddCharacter(id, this))
            {
                Debug.LogError("Error in TryAddCharacter - Can't add character");
            }
        }

        protected virtual void OnDisable()
        {
            if (CharacterService.Instance.TryRemoveCharacter(id))
            {
                Debug.LogError("Error in TryRemoveCharacter - Can't remove character");
            }
        }
    }
}
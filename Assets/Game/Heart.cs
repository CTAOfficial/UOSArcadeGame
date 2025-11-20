using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Glorp.UI
{
    public enum HeartState
    {
        Active,
        Lost
    }

    public sealed class Heart : MonoBehaviour
    {
        public Image Image;

        [SerializeField] Sprite ActiveSprite;
        [SerializeField] Sprite LostSprite;

        public HeartState State { get => _state; set => SetState(value); }
        [SerializeField] HeartState _state;
        
        void SetState(HeartState state)
        {
            _state = state;

            switch (state)
            {
                case HeartState.Active:
                    Image.sprite = ActiveSprite;
                    Image.color = Color.red;
                    break;

                case HeartState.Lost:
                    Image.sprite = LostSprite;
                    Image.color = Color.gray;
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Blazers.UI
{
    public enum HeartState
    {
        Active,
        Lost
    }

    public sealed class Heart : MonoBehaviour
    {
        public Image Image;

        public HeartState State { get => _state; set => SetState(value); }
        [SerializeField] HeartState _state;
        
        void SetState(HeartState state)
        {
            _state = state;

            switch (state)
            {
                case HeartState.Active:
                    //.. change image
                    gameObject.SetActive(true);
                    break;

                case HeartState.Lost:
                    //.. change image
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}

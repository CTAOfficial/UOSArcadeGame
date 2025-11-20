using System;
using System.Collections;
using UnityEngine;

namespace Glorp
{
    public sealed partial class Timer : MonoBehaviour
    {
        public event Action OnTime;        

        public bool Resets { get; set; }
        public float Time
        {
            get => _time;
            set
            {
                _time = value;
                _startTime = _time;
                Debug.Log($"{name}: Time set to {Time}");
            }
        }
        float _time;


        float _timer;
        float _startTime;

        public IEnumerator StartTimer()
        {
            enabled = true;
            _timer = Time;
            yield return new WaitForSeconds(_timer);
            EndTimer();
        }

        public void EndTimer()
        {
            OnTime.Invoke();

            if (Resets) 
            { 
                _timer = _startTime; 
                StartCoroutine(StartTimer());
                return;
            }
            ForceStop();
        }

        public void ForceStop()
        {
            StopCoroutine(StartTimer());
            Resets = false;
            enabled = false;
        }
    }
}

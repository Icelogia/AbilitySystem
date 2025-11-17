using System;
using UnityEngine;

namespace ICGames.Core
{
    [Serializable]
    public class Duration
    {
        public Action OnEndOfDuration;
        public bool IsFinished { get; set; } = false;

        [SerializeField] private float duration;
        private float timer = 0f;

        public Duration(float duration, bool isFinishedInit = false, Action callback = null)
        {
            this.duration = duration;
            this.IsFinished = isFinishedInit;
            OnEndOfDuration += callback;
        }

        public void SetDuration(float duration)
        { 
            this.duration = duration; 
        }

        public void UpdateTimer(float deltaTime)
        {
            if(IsFinished) { return; }

            timer += deltaTime;


            if (timer >= duration) 
            {
                IsFinished = true;
                OnEndOfDuration?.Invoke();
            }
        }
    
        public void Restart()
        {
            timer = 0f;

            if(duration > 0)
                IsFinished = false;
        }
    }
}

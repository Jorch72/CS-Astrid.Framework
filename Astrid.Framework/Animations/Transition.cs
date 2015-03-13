﻿using System;
using Astrid.Framework.Extensions;

namespace Astrid.Framework.Animations
{
    public abstract class Transition<T> : Transition
    {
        protected Transition(T initialValue, T targetValue, Action<T> setValueAction, float duration)
            : base(duration)
        {
            InitialValue = initialValue;
            TargetValue = targetValue;
            _setValueAction = setValueAction;
        }

        public T InitialValue { get; private set; }
        public T TargetValue { get; private set; }

        private readonly Action<T> _setValueAction;

        protected abstract T CalculateNewValue(float multiplier);

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            var newValue = CalculateNewValue(CurrentValue);
            _setValueAction(newValue);
        }
    }

    public abstract class Transition
    {
        protected Transition(float duration)
        {
            CurrentValue = 0.0f;
            Duration = duration;
            EasingFunction = EasingFunctions.Linear;
            IsComplete = false;
            IsPaused = false;
        }

        public float CurrentTime { get; private set; }
        public float CurrentValue { get; private set; }
        public float Duration { get; private set; }
        public EasingFunction EasingFunction { get; set; }

        private bool _isComplete;
        public bool IsComplete
        {
            get { return _isComplete; }
            private set
            {
                if (_isComplete != value)
                {
                    _isComplete = value;
                    TransitionComplete.Raise(this, EventArgs.Empty);
                }
            }
        }

        public bool IsPaused { get; private set; }

        public event EventHandler TransitionComplete;

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Stop()
        {
            IsComplete = true;
        }

        public virtual void Update(float deltaTime)
        {
            if (!IsComplete && !IsPaused)
            {
                CurrentTime += deltaTime;
                CurrentValue = EasingFunction(CurrentTime / Duration);

                if (CurrentTime >= Duration)
                {
                    CurrentTime = Duration;
                    CurrentValue = EasingFunction(1.0f);
                    IsComplete = true;
                }
            }
        }
    }
}

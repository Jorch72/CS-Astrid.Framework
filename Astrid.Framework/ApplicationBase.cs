﻿using System;

namespace Astrid
{
    public abstract class ApplicationBase : IDisposable
    {
        protected ApplicationBase()
        {
        }

        public abstract AssetManager CreateAssetManager(IDeviceManager deviceManager);
        public abstract GraphicsDevice CreateGraphicsDevice();
        public abstract InputDevice CreateInputDevice(IInputDeviceContext context);
        public abstract AudioDevice CreateAudioDevice();
        public abstract void Run(IApplicationListener game);

        private bool _isDisposed;

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                // Free any other managed objects here. 
            }

            // Free any unmanaged objects here. 
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

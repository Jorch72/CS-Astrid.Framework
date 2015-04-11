﻿using System.Collections.Generic;
using Astrid.Animations;
using Astrid.Core;

namespace Astrid.Gui
{
    public interface IScreenManager : IDeviceManager
    {
        AssetManager AssetManager { get; }
        void SetScreen(Screen screen);
    }

    public abstract class Screen : IDeviceManager
    {
        protected Screen(IScreenManager screenManager)
        {
            _layers = new List<ScreenLayer>();

            ScreenManager = screenManager;
            ClearColor = Color.CornflowerBlue;
            Viewport = new StretchViewport(GraphicsDevice, GraphicsDevice.Width, GraphicsDevice.Height);
            Animations = new AnimationSystem();
        }

        protected IScreenManager ScreenManager { get; private set; }
        
        public Color ClearColor { get; set; }
        public Viewport Viewport { get; set; }
        public AnimationSystem Animations { get; private set; }

        private readonly List<ScreenLayer> _layers;
        public IList<ScreenLayer> Layers
        {
            get { return _layers; }
        }

        public AssetManager AssetManager
        {
            get { return ScreenManager.AssetManager; }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return ScreenManager.GraphicsDevice; }
        }

        public InputDevice InputDevice
        {
            get { return ScreenManager.InputDevice; }
        }

        public AudioDevice AudioDevice
        {
            get { return ScreenManager.AudioDevice; }
        }

        public void SetScreen(Screen screen)
        {
            ScreenManager.SetScreen(screen);
        }

        public virtual void Show() { }
        public virtual void Hide() { }
        public virtual void Resize(int width, int height) { }
        public virtual void Pause() { }
        public virtual void Resume() { }

        public virtual void Update(float deltaTime)
        {
            Animations.Update(deltaTime);

            foreach (var layer in Layers)
                layer.Update(deltaTime, InputDevice);
        }

        public virtual void Render(float deltaTime)
        {
            GraphicsDevice.Clear(ClearColor);

            foreach (var layer in _layers)
                layer.Render(deltaTime);
        }
    }
}
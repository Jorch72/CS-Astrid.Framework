﻿using System;
using Astrid.Framework.Assets;

namespace Astrid.Framework.Audio
{
    public abstract class SoundEffect : IAsset, IEquatable<SoundEffect>, IDisposable
    {
        protected SoundEffect(string key, string name)
        {
            Key = key;
            Name = name;
        }

        public string Key { get; private set; }
        public string Name { get; private set; }

        public static bool operator ==(SoundEffect x, SoundEffect y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(SoundEffect x, SoundEffect y)
        {
            return !Equals(x, y);
        }

        public override bool Equals(object obj)
        {
            var other = obj as SoundEffect;

            if (obj != null)
                return Equals(other);

            return false;
        }

        public bool Equals(SoundEffect other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return Key == other.Key;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public abstract void Dispose();
    }
}

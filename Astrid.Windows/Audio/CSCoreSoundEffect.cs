﻿using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;

namespace Astrid.Windows.Audio
{
    public class CSCoreSoundEffect : SoundEffect
    {
        public CSCoreSoundEffect(AudioDevice audioDevice, string filePath, string name)
            : base(filePath, name)
        {
            _audioDevice = audioDevice;
            _waveSource = CodecFactory.Instance.GetCodec(filePath);
        }

        private readonly AudioDevice _audioDevice;
        private readonly IWaveSource _waveSource;

        public override void Dispose()
        {
            _waveSource.Dispose();
        }

        private static ISoundOut CreateSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();

            return new DirectSoundOut();
        }

        public override SoundEffectInstance CreateInstance()
        {
            var soundOut = CreateSoundOut();
            soundOut.Initialize(_waveSource);
            return new CSCoreSoundEffectInstance(_audioDevice, soundOut);
        }
    }
}

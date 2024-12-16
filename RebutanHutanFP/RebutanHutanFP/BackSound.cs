using System.Media;
using System.IO;

namespace RebutanHutanFP
{
    public class BackSound
    {
        private static BackSound _instance; // Singleton instance
        private SoundPlayer _soundPlayer;

        // Constructor private agar hanya bisa diakses dari dalam kelas
        private BackSound()
        {
            _soundPlayer = new SoundPlayer(new MemoryStream(Properties.Resources.backsound));
        }

        // Method untuk mendapatkan instance tunggal
        public static BackSound Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BackSound();
                }
                return _instance;
            }
        }

        // Method untuk mulai memainkan backsound
        public void Play()
        {
            if (_soundPlayer != null)
            {
                _soundPlayer.PlayLooping();
            }
        }

        // Method untuk menghentikan backsound
        public void Stop()
        {
            if (_soundPlayer != null)
            {
                _soundPlayer.Stop();
            }
        }
    }
}
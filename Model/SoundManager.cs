using StudyTimer.Interfaces;
using System.Media;

namespace StudyTimer.Model
{
    public class SoundManager : ISoundManager
    {
        private const string _notificationsPath = @"C:\Users\zajac\Desktop\C# Projects\StudyTimer\Sounds\Notifications\SessionFinishedBell.wav";
        public SoundPlayer Player { get; }
        private bool _isPlaying = false;

        public SoundManager()
        {
            Player = new SoundPlayer(_notificationsPath);
            Player.Load();     // Loads the notification sound
        }

        public void PlayMusic()
        {
            if (_isPlaying) return;     // Doesn't play if true
            _ = PlaySoundAsync();       // Plays the sound but doesn't await, so it doesn't block the UI
        }
        public void PlayNotification()
        {
            if (_isPlaying) return;
            _ = PlaySoundAsync();
        }

        private async Task PlaySoundAsync()
        {
            _isPlaying = true;
            Player.Play();
            _isPlaying = false;     // Once the sound is done playing
        }
    }
}

using StudyTimer.Interfaces;
using System.Media;

namespace StudyTimer.Model
{
    public class SoundManager : ISoundManager
    {
        private const string _notificationsPath = @"C:\Users\zajac\Desktop\C# Projects\StudyTimer\Sounds\Notifications\SessionFinishedBell.wav";
        private readonly SoundPlayer _player;
        private bool _isPlaying = false;

        public SoundManager()
        {
            _player = new SoundPlayer(_notificationsPath);
            _player.Load();     // Loads the notification sound
        }

        public void PlaySound()
        {
            if (_isPlaying) return;     // Doesn't play if true
            _ = PlaySoundAsync();       // Plays the sound but doesn't await, so it doesn't block the UI
        }

        private async Task PlaySoundAsync()
        {
            _isPlaying = true;
            _player.Play();
            _isPlaying = false;     // Once the sound is done playing
        }
    }
}

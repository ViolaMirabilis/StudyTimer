using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace StudyTimer.Model
{
    public class SessionManager
    {
        private const string _fileName = "Sessions.json";
        public ObservableCollection<Session> Sessions { get; } = new ObservableCollection<Session>();       // Readonly, just adding contents to it

        // to be done
        public void ReadSessionsFromJson()
        {
            Sessions.Clear();
            
            foreach (string line in File.ReadLines(_fileName))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    try
                    {
                        var session = JsonConvert.DeserializeObject<Session>(line);
                        if (session != null)
                            Sessions.Add(session);
                    }
                    catch (JsonException)
                    {

                    }
                }
            }
        }

        public void WriteSessionsToJson(Session session)
        {
            string jsonString = JsonConvert.SerializeObject(session);

            using (StreamWriter sw = new StreamWriter(_fileName, append: true))
            {
                sw.WriteLine(jsonString);
            }

            Sessions.Add(session);
        }
    }
}

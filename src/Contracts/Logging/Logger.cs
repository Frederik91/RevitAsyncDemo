using System.IO;

namespace Contracts.Logging
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            if (CheckDatabaseAviliablity())
            {
                // Implement posting to table in database.
            }
            else
            {
                var logPath = @"C:\CAD\Revit\NO\ErrorLog.txt";
                if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                }

                if (File.Exists(logPath))
                {
                    File.WriteAllText(logPath, message);
                }
                else
                {
                    var writer = File.AppendText(logPath);
                    writer.WriteLine(message);
                    writer.Close();
                }                
            }
        }

        private bool CheckDatabaseAviliablity()
        {
            // implement logic to check if database is accessable.

            return false;
        }
    }
}

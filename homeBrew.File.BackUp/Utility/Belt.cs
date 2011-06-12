using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace homeBrew.File.BackUp.Utility
{
    public class Belt
    {
        public string RemoveRootDir(string FilePath, string WatchRootDir)
        {
            return FilePath.Replace(WatchRootDir.Trim(), "");
        }

        public void WriteToFile(string message)
        {
            string logDirectory = @"C:\homeBrew\";
            string logFile = "log.txt";
            string curDate = this.GetCurrentDate(true);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            logDirectory += curDate + @"\";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            if (System.IO.File.Exists(logDirectory + logFile))
            {
                using (StreamWriter wr = System.IO.File.AppendText(logDirectory + logFile))
                {
                    wr.WriteLine(message);
                    wr.Close();
                }
            }
            else
            {
                using (StreamWriter wr = System.IO.File.CreateText(logDirectory + logFile))
                {
                    wr.Write(message);
                    wr.Close();
                }
            }
        }

        public string GetCurrentDate(bool yyyymmdd)
        {
            DateTime date = DateTime.Now;
            string year = date.Year.ToString();
            string month = date.Month.ToString();
            string day = date.Day.ToString();

            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            return year + month + day;
        }
    }
}

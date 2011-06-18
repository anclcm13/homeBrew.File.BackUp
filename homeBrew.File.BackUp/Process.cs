using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace homeBrew.File.BackUp
{
    public class Process
    {
        #region Private Members

        private string _origDir;
        private string _copyDir;

        #endregion

        #region Public Properties

        public string OrigDirectory
        {
            set
            {
                this._origDir = value;
            }
        }

        public string CopyDirectory
        {
            set
            {
                this._copyDir = value;
            }
        }

        #endregion

        #region Constructors

        public Process()
        {
            this.Init();
        }

        #endregion

        #region Initializer

        private void Init()
        {
            this._origDir = String.Empty;
            this._copyDir = String.Empty;
        }

        #endregion

        public void ReadAndCompairDirectories()
        {
            Utility.Belt utilBelt = new Utility.Belt();

            try
            {
                utilBelt.WriteToFile("BackUp Start: " + Convert.ToString(DateTime.Now));

                try
                {
                    this.RecursiveSearch_NewAndUpdated(this._origDir, "*.*");
                }
                catch (Exception e)
                {
                    utilBelt.WriteToFile(Environment.NewLine + "Error Occured 'NewAndUpdated' With Exception: " + e.ToString());
                }

                try
                {
                    this.RecursiveSearch_Deleted(this._copyDir, "*");
                }
                catch (Exception x)
                {
                    utilBelt.WriteToFile(Environment.NewLine + "Error Occured 'Deleted' With Exception: " + x.ToString());
                }
            }
            finally
            {
                utilBelt.WriteToFile("BackUp Completed: " + Convert.ToString(DateTime.Now));
            }
        }

        private void RecursiveSearch_NewAndUpdated(string path, string pattern)
        {
            string[] folders = System.IO.Directory.GetDirectories(path);
            string[] patternFiles = System.IO.Directory.GetFiles(path, pattern);

            Utility.Belt utilBelt = new Utility.Belt();

            if (path.Substring(path.Length - 1, 1) != @"\")
            {
                path += @"\";
            }

            string minusRootDir = utilBelt.RemoveRootDir(path, this._origDir);

            if (!Directory.Exists(this._copyDir + minusRootDir))
            {
                Directory.CreateDirectory(this._copyDir + minusRootDir);
                utilBelt.WriteToFile("Created Directory: " + this._copyDir + minusRootDir);
            }

            foreach (string file in patternFiles)
            {
                FileInfo fileInfo = new FileInfo(file);

                if (!System.IO.File.Exists(this._copyDir + minusRootDir + fileInfo.Name.ToString()))
                {
                    fileInfo.CopyTo(this._copyDir + minusRootDir + fileInfo.Name.ToString());
                    utilBelt.WriteToFile("Copied: " + file);
                }
                else
                {
                    FileInfo copyFileInfo = new FileInfo(this._copyDir + minusRootDir + fileInfo.Name.ToString());
                    if ((copyFileInfo.Length != fileInfo.Length) | (copyFileInfo.LastWriteTime != fileInfo.LastWriteTime))
                    {
                        fileInfo.CopyTo(this._copyDir + minusRootDir + fileInfo.Name.ToString(), true);
                        utilBelt.WriteToFile("Updated: " + file);
                    }
                }
            }
            foreach (string folder in folders)
            {

                try
                {
                    this.RecursiveSearch_NewAndUpdated(folder, "*.*");
                }
                catch (Exception ex)
                {
                    utilBelt.WriteToFile("Error Occured Opening Folder With Exception: " + ex.ToString());
                }
            }
        }

        private void RecursiveSearch_Deleted(string path, string pattern)
        {
            bool okayToProceed = true;
            string[] folders = System.IO.Directory.GetDirectories(path);
            string[] patternFiles = System.IO.Directory.GetFiles(path, pattern);

            Utility.Belt utilBelt = new Utility.Belt();

            if (path.Substring(path.Length - 1, 1) != @"\")
            {
                path += @"\";
            }

            string minusRootDir = utilBelt.RemoveRootDir(path, this._copyDir);

            if (!Directory.Exists(this._origDir + minusRootDir))
            {
                Directory.Delete(this._copyDir + minusRootDir, true);
                okayToProceed = false;
                utilBelt.WriteToFile("Delted Directory: " + this._copyDir + minusRootDir);
            }

            if (okayToProceed)
            {
                foreach (string file in patternFiles)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    if (!System.IO.File.Exists(this._origDir + minusRootDir + fileInfo.Name.ToString()))
                    {
                        fileInfo.Delete();
                        utilBelt.WriteToFile("Deleted: " + file);
                    }
                }
                foreach (string folder in folders)
                {
                    try
                    {
                        this.RecursiveSearch_Deleted(folder, "*.*");
                    }
                    catch (Exception ex)
                    {
                        utilBelt.WriteToFile("Error Occured Opening Folder For Deleted Search With Exception: " + ex.ToString());
                    }
                }
            }
        }
    }
}

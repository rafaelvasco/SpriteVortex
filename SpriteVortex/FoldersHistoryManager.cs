#region License

/**
     * Copyright (C) 2010 Rafael Vasco (rafaelvasco87@gmail.com)
     * 
     *
     * This program is free software; you can redistribute it and/or
     * modify it under the terms of the GNU General Public License
     * as published by the Free Software Foundation; either version 2
     * of the License, or (at your option) any later version.

     * This program is distributed in the hope that it will be useful,
     * but WITHOUT ANY WARRANTY; without even the implied warranty of
     * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
     * GNU General Public License for more details.

     * You should have received a copy of the GNU General Public License
     * along with this program; if not, write to the Free Software
     * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
     */

#endregion

using System;
using System.IO;
using System.Text;
using System.Xml;


namespace SpriteVortex
{
    public static class FoldersHistoryManager
    {
        private static string historyFile = "FoldersHistory.xml";

        public static int MaxFoldersToRemember = 5;

        public static void WriteFolderPath(string path)
        {
            if (!File.Exists(historyFile))
            {
                XmlTextWriter writer = new XmlTextWriter(historyFile, Encoding.UTF8);
                writer.WriteStartDocument();
                writer.WriteStartElement("Folders");
                writer.WriteEndElement();
                writer.Close();
            }

            XmlDocument historyDoc = new XmlDocument();
            historyDoc.Load(historyFile);

            var currentFolders = historyDoc.GetElementsByTagName("Folder");

            var folderPathToAdd = Path.GetDirectoryName(path);

            var alreadyExists = false;

            if (currentFolders.Count < MaxFoldersToRemember)
            {
                foreach (XmlNode currentFolder in currentFolders)
                {
                    if (currentFolder.Attributes["Path"].Value.Equals(folderPathToAdd))
                    {
                        alreadyExists = true;
                        break;
                    }
                }
                if (!alreadyExists)
                {
                    XmlElement folderElement = historyDoc.CreateElement("Folder");
                    folderElement.SetAttribute("Path", folderPathToAdd);
                    if (historyDoc.DocumentElement != null) historyDoc.DocumentElement.AppendChild(folderElement);
                    historyDoc.Save(historyFile);
                }
            }
        }

        public static string[] GetHistoryFolders()
        {
            if (!File.Exists(historyFile))
            {
                return null;
            }

            XmlDocument historyDoc = new XmlDocument();
            historyDoc.Load(historyFile);

            XmlNodeList folderNodes = historyDoc.GetElementsByTagName("Folder");

            string[] folderPaths = new string[folderNodes.Count];

            for (int i = 0; i < folderNodes.Count; i++)
            {
                XmlNode node = folderNodes[i];

                folderPaths[i] = node.Attributes["Path"].Value;
            }

            return folderPaths;
        }

        public static bool EraseHistory()
        {
            try
            {
                if (File.Exists(historyFile))
                {
                    File.Delete(historyFile);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Messager.ShowMessage(Messager.Mode.Exception,
                                     string.Format("Error on removing history file: {0}", ex.Message));
            }
            return false;
        }
    }
}
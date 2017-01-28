using System.Text;
using System.Xml;

namespace SpriteVortex.Helpers
{
    public class XMLWriter
    {
        #region  Private Variables 

        private XmlTextWriter xtw;

        #endregion

        #region  Public Writer Methods 

        public void CreateDocWithRoot(string rootName, string commentTitle)
        {
            xtw.Formatting = Formatting.Indented;
            xtw.Indentation = 3;
            xtw.WriteStartDocument(true);
            xtw.WriteComment(commentTitle);
            xtw.WriteStartElement(rootName);
        }


        public void AddAttribute(string attrName, string attrValue)
        {
            xtw.WriteAttributeString(attrName, attrValue);
        }

        public void WriteComment(string text)
        {
            xtw.WriteComment(text);
        }

        public void OpenNewElement(string eltName)
        {
            xtw.WriteStartElement(eltName);
        }

        public void CloseCurrentElement()
        {
            xtw.WriteEndElement();
        }

        public void AddCompleteElementWithAttribute(string eltName, string attrName, string attrValue)
        {
            xtw.WriteStartElement(eltName);
            xtw.WriteAttributeString(attrName, attrValue);
            xtw.WriteEndElement();
        }

        public void AddCompleteElmentWithInnerText(string eltName, string innerText)
        {
            xtw.WriteElementString(eltName, innerText);
        }

        public void Save()
        {
            xtw.WriteEndElement();
            xtw.WriteEndDocument();
            xtw.Flush();

            xtw.Close();
        }

        #endregion

        #region  Constructor 

        // Constructor opens memorystream and textwriter.
        public XMLWriter(string fileName)
        {
            xtw = new XmlTextWriter(fileName, Encoding.UTF8);
        }

        #endregion
    }
}
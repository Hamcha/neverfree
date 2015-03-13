using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("save")]
public class SaveTable<TValue>
    : Dictionary<string, TValue>, IXmlSerializable {
    #region IXmlSerializable Members
    public System.Xml.Schema.XmlSchema GetSchema() {
        return null;
    }

    public void ReadXml(System.Xml.XmlReader reader) {
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        bool wasEmpty = reader.IsEmptyElement;
        reader.Read();

        if (wasEmpty)
            return;

        while (reader.NodeType != System.Xml.XmlNodeType.EndElement) {
            string key = reader.GetAttribute("name");
            reader.ReadStartElement("item");
            TValue value = (TValue)valueSerializer.Deserialize(reader);
            this.Add(key, value);
            reader.ReadEndElement();
            reader.MoveToContent();
        }
        reader.ReadEndElement();
    }

    public void WriteXml(System.Xml.XmlWriter writer) {
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        foreach (string key in this.Keys) {
            writer.WriteStartElement("item");
            writer.WriteAttributeString("name", key);
            TValue value = this[key];
            valueSerializer.Serialize(writer, value);
            writer.WriteEndElement();
        }
    }
    #endregion
}
/*Helper.cs
 * Created By: Phillip Buckreis 10/4/17
 * 
 * This script contains useful functions that are used throughout the game.
 * 
 */ 

using System.IO;
using System.Xml.Serialization;

public static class Helper
{
    // Serializes data
    public static string Serialize<T>(this T toSerilaize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerilaize);
        return writer.ToString();
    }

    // Desrializes data
    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDeserialize);
        return (T)xml.Deserialize(reader);
    }

    //converts a string to an enumerated type
    public static T ParseEnum<T>(string aText)
    {
        return (T)System.Enum.Parse(typeof(T), aText);
    }
}

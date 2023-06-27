using System.Xml.Serialization;
namespace DictionaryAPI;
[XmlRoot("LoginData")]
public class LoginData
{
    [XmlElement("AppId")]
    public string? AppId { get; set; }
    [XmlElement("AppKey")]
    public string? AppKey { get; set; }
}

public static class Login
{
    public static LoginData GetLogin()
    {
        XmlSerializer serializer = new(typeof(LoginData));
        using StringReader sr = new(File.ReadAllText("LoginData.xml"));

        return (LoginData)serializer.Deserialize(sr)! ?? throw new NullReferenceException();
    } 
}
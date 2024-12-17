using System;
using System.ServiceModel;
using System.Runtime.Serialization;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IJwtService" in both code and config file together.
[ServiceContract]
public interface IJwtService
{
    [OperationContract]
    JwtTokenResponse GetJwtToken(LoginDetails loginDetails);
}

[DataContract]
public class LoginDetails
{
    [DataMember]
    public string Username { get; set; }

    internal static string ToUpper()
    {
        throw new NotImplementedException();
    }
}

[DataContract]
public class JwtTokenResponse
{
    [DataMember]
    public string Token { get; set; }

    [DataMember]
    public DateTime TokenExpire { get; set; }
}

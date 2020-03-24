<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="AplikacijaDemoWeb.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aplikacija Demo Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Aplikacija Demo</h1>
            <asp:Login ID="Login" runat="server" OnAuthenticate="Login_Authenticate" UserNameLabelText="Korisničko ime" PasswordLabelText="Zaporka" TitleText="Prijava" UserNameRequiredErrorMessage="Obavezno upišite korisničko ime" PasswordRequiredErrorMessage="Obavezno upišite zaporku" LoginButtonText="Prijava" FailureText="Pokušaj prijave nije uspio. Pokušajte ponovo." RememberMeText="Zapamti podatke.">
            </asp:Login>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmMain.aspx.cs" Inherits="AplikacijaDemoWeb.frmMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Application Demo Main</title>
    <script type="text/javascript">

        function LimitBox(elementId, threshold, outputId) {
            this.elementId = elementId;
            this.threshold = threshold;
            this.outputId = outputId;
            this.messageOut = "SMS Poruka (Max {0} znakova, preostalo {1} znakova).";
            this.messageErr = "Poruka je veća od {0} znakova!";
            this.init();
        }

        LimitBox.prototype.init = function () {
            var ref = this;
            var elem = document.getElementById(this.elementId);
            elem.onkeypress = function () { ref.updateMessage(); };
            elem.onkeyup = function () { ref.updateMessage(); };
            elem.onchange = function () { ref.updateMessage(); };
            elem.onkeydown = function () { ref.updateMessage(e); }
            elem = null;
            this.updateMessage();
        }

        LimitBox.prototype.updateMessage = function () {
            var elem = document.getElementById(this.elementId);
            var output = document.getElementById(this.outputId);
            if (elem.hidden)
            {
                output.style.color = "Red";
                output.style.fontWeight = "bold";
            }
            else if (this.threshold < elem.value.length) {
                output.innerHTML = this.messageErr.Format(this.threshold);
                output.style.color = "Red";
                output.style.fontWeight = "bold";
            }
            else {
                output.innerHTML = this.messageOut.Format(this.threshold, this.threshold - elem.value.length);
                output.style.color = "black";
                output.style.fontWeight = "normal";
            }
            elem = null;
            output = null;
        }

        String.prototype.Format = function () {
            var str = this;
            for (var i = 0; i < arguments.length; i++) {
                str = str.replace("{" + i + "}", arguments[i]);
            }
            return str;
        }

        window.onload = function () {
            var smsMsg = new LimitBox("txtSMSMessage", 160, "lblCharCount");
        }
    </script>
    <style type="text/css">
        .col1_width {
            width: 100px;
        }

        .col2_width {
            width: 850px;
        }

        .sms_box {
            height: 200px;
            width: 800px;
        }

        .ime_box {
            width: 400px;
        }
    </style>
</head>
<body>
    <form id="root" runat="server">
        <div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <h1>Demo</h1>
                    </td>
                    <td>
                        <asp:LoginStatus ID="LoginStatus1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="col1_width">
                        <asp:Label ID="lblImePrezime" runat="server" Text="Ime i prezime"></asp:Label>
                    </td>

                    <td class="col2_width">
                        <asp:TextBox ID="txtImePrezime" runat="server" MaxLength="50" class="ime_box"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="col1_width">
                        <asp:Label ID="lblBrojMobitela" runat="server" Text="Broj mobitela"></asp:Label>
                    </td>
                    <td class="col2_width">
                        <asp:TextBox ID="txtBrojMobitela" runat="server" MaxLength="12" TextMode="Phone"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revPhone" runat="server" ErrorMessage="Nije ispravan telefonski broj. Treba imati 11 ili 12 znamenaka (npr. 385991234567)." ControlToValidate="txtBrojMobitela"
                            ValidationExpression="^\d{11,12}$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td class="col2_width">
                        <asp:Button ID="btnDodajPrimatelja" runat="server" Text="Dodaj Primatelja" CommandName="dodajPrimatelja" OnClick="btnDodajPrimatelja_Click" />
                        <asp:Label ID="lblDodajPrimateljaError" runat="server" ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>

            <br />
            <asp:GridView ID="GridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" PageSize="15" EmptyDataText="Nema upisanih primatelja" Caption="Primatelji">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Poslati">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ImePrezime" HeaderText="Ime i prezime" ReadOnly="True" SortExpression="ImePrezime" />
                    <asp:BoundField DataField="BrojMobitela" HeaderText="Broj mobitela" ReadOnly="True" SortExpression="BrojMobitela" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblCharCount" runat="server"></asp:Label>
            <br />
            <asp:TextBox class="sms_box" ID="txtSMSMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button ID="btnPosalji" runat="server" Text="Pošalji" OnClick="btnPosalji_Click" />
            <br />
            <asp:Label ID="lblSentSuccess" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>

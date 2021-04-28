<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="flowerpot.aspx.cs" Inherits="Flowerpot.flowerpot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0; height: 100%">
    <form id="form1" runat="server">
        <div style="background-image: url('Bristle Grass.jpg'); background-size: cover;
                    background-attachment:fixed;"  class="height" >
            <div class="set_label">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000"></asp:Timer>
                        <asp:Label ID="Label1" runat="server" Text="Moisture:"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Luminance:" Height="40px"></asp:Label>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="center">
                <asp:Button ID="Button1" runat="server" BackColor="Black" Font-Bold="True" ForeColor="White" OnClick="Button1_Click" Text="Water" CssClass="roundCorner" BorderStyle="None" />
                <br />
                <br />
                <asp:Button ID="Button2" runat="server" BackColor="Black" Font-Bold="True" ForeColor="White" OnClick="Button2_Click" Text="Light Auto" CssClass="roundCorner" BorderStyle="None"/>
            </div>
         </div>
        <style>
            .roundCorner {  
                border-radius: 25px;  
                text-align :center;
                color:#FFFFFF;
                font-weight:bold;  
                font-family:'Trebuchet MS';
                font-size:medium;
                width:200px;  
                height:45px;  
            }
            .height {
                height: 100vh;
            }
            .center {
                margin: 0;
                position: absolute;
                top: 80%;
                left: 50%;
                -ms-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);
            }
                        
            .set_label {
                margin: 0;
                border-radius: 25px;  
                position: absolute;
                top: 55%;
                left: 50%;
                padding: 25px;
                -ms-transform: translate(-50%, -50%);
                transform: translate(-50%, -50%);
                font-family:'Trebuchet MS';
                font-size:26px;
                font-weight:bold;
                background-color: rgba(248, 255, 246, 0.5);
                width:250px;  
                height:100px;  
            }
        </style>
    </form>
</body>
</html>
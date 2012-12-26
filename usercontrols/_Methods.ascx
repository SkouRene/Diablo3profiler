<%@ Control Language="C#" AutoEventWireup="true" CodeFile="_Methods.ascx.cs" Inherits="usercontrols_Methods" %>
<asp:Label ID="lblTestMethods" runat="server" Text="Test Methods" style="color:White;font-size:larger;"></asp:Label>
<br />
<asp:TextBox ID="txtbBattletag" runat="server" Width="300"></asp:TextBox>
<br />
<asp:Button ID="btnLoad" runat="server" Text="Load battletag" 
    onclick="btnLoad_Click" />
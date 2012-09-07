<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BilkaDubai.ascx.cs" Inherits="BilkaDubai" %>
<style type="text/css">
    #landingpage {font-family:Arial,Trebuchet MS,Georgia;font-size:14px;margin:0px;padding:0px;width:582px;height:800px;background-image:url('comp/images/konkurrenceside_bg.jpg')}
    .container {padding:16px 18px 14px 30px;}
    body {margin:0px;padding:0px;}
    .linkimg {float:right;}
    .section1 {width:100%;height:260px;}
    .section2 p:first-child {margin-right:147px;padding-top:10px;}
    .section2 p {width:36%;float:left;font-weight:bold;margin:0px;}
    .section2 {width:100%;}
    .section3 {clear:both;}
    #landingpage h3 {font-size:20px;margin:10px 0px;}
    #landingpage h4 {font-size:16px;font-weight:bold;margin:0px 0px 5px;}
    #landingpage img {border:none;}
    #landingpage p {font-size:11px;}
    .section4 {width:40%; float:left;}
    .section5 {width:60%; float:right;}
    .section5 span {width:100px;font-weight:bold;
        font-size:16px;}
        .section4 label {font-weight:bold;
        font-size:16px;}
    .lbl {float:left;width:30%;}
    .txtb {float:right;width:70%;margin:0px 0px 6px 0px;}
    #landingpage input[type=text]{
        width:80%;
        -moz-box-shadow: 10px 10px 5px #888;
        -webkit-box-shadow: 10px 10px 5px #888;
        box-shadow: 10px 10px 5px #888;
        font-weight:bold;
        font-size:16px;
        }
    .section6 span {float:left;}
    .imgbutton {margin-left:30px;}
    .cbbilka {margin-left: 30px;}
    #ContentPlaceHolderDefault_bilkaDubai1_cbTerms {border: 3px soild #000;}
</style>
<div id="landingpage">
<div class="container">
    <div class="section1"><a href="http://www.falklauritsen.dk" target="_blank"><img alt="link" src="/comp/images/link.png" class="linkimg"/></a></div>
    <div class="section2">    <p>Dubai, en verden af oplevelser. Dubai er det 
n&#230;stst&#248;rste af de syv Arabiske Emirater, 
og er med sine mange muligheder for oplevelser 
det ideelle sted at holde ferie, for jer der b&#229;de 
vil have godt vejr, strandture, shopping og 
oplevelser p&#229; jeres ferie.</p><p>4 overnatninger p&#229; Hotel Hilton Resort &amp; Spa i Ras Al Khaimah
3 overnatninger p&#229; M&#246;venpick Hotel Deira i Dubai<br /><br />

Fly t/r med Emirates Airlines inkl. mad p&#229; flyet og transfer til og fra lufthavn/hotel
+ Overnatning p&#229; Hilton i Kastrup lufthavn inden afrejse den 11. januar 2013</p>

<div style="clear:both;"></div>
</div>
<div class="section3"><h3>Deltag i konkurrencen ved at svare på spørgsmålet:</h3></div>
    <div><h4><asp:Label ID="lblRadioButtonlst" runat="server" Text="Hvor kan du vinde en rejse til i Bilka denne uge?"></asp:Label></h4></div>
    <div class="section4"><asp:RadioButtonList ID="rblstQuestion" runat="server">
        <asp:ListItem  Value="istanbul">Istanbul</asp:ListItem>
        <asp:ListItem Value="dubai">Dubai</asp:ListItem>
        <asp:ListItem Value="rom">Rom</asp:ListItem>
    </asp:RadioButtonList>
    </div>
    <div class="section5">
    <asp:RequiredFieldValidator ID="rvRadioButtons" runat="server" ErrorMessage="Vælge venlist et svar" Display="None" ControlToValidate="rblstQuestion"></asp:RequiredFieldValidator>
    <div><div class="lbl"><asp:Label ID="lblName" runat="server" Text="Fornavn"></asp:Label></div>
    <div class="txtb"><asp:TextBox ID="txtbName" runat="server"></asp:TextBox></div>
    <asp:RequiredFieldValidator ID="rvName" runat="server" ErrorMessage="-Indtast venligst fornavn." Display="None" ControlToValidate="txtbName"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Minimum 2 karater i fornavn" ValidationExpression="^.{2,}$" Display="None" ControlToValidate="txtbName"></asp:RegularExpressionValidator></div>
    <div style="clear:both;"></div>
    <div><div class="lbl"><asp:Label ID="lblLastname" runat="server" Text="Efternavn"></asp:Label></div>
    <div class="txtb"><asp:TextBox ID="txtbLastname" runat="server"></asp:TextBox></div>
    <asp:RequiredFieldValidator ID="rvLastname" runat="server" ErrorMessage="-Indtast venligst efternavn." Display="None" ControlToValidate="txtbLastname"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Minimum 2 karater i efternavn" ValidationExpression="^.{2,}$" Display="None" ControlToValidate="txtbLastname"></asp:RegularExpressionValidator></div>
    <div style="clear:both;"></div>
    <div><div class="lbl"><asp:Label ID="lblMobil" runat="server" Text="Mobil"></asp:Label></div>
    <div class="txtb"><asp:TextBox ID="txtbMobil" runat="server"></asp:TextBox></div>
    <asp:RequiredFieldValidator ID="RrvMobil" runat="server" ErrorMessage="-Indtast venligst mobilnummer med tal uden mellemrum." Display="None" ControlToValidate="txtbMobil"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RregexMobil" runat="server" ErrorMessage="Kun tal, min 8 tal i mobil" ValidationExpression="^[0-9]{8,}" Display="None" ControlToValidate="txtbMobil"></asp:RegularExpressionValidator></div>
    <div style="clear:both;"></div>
    <div><div class="lbl"><asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></div>
    <div class="txtb"><asp:TextBox ID="txtbEmail" runat="server"></asp:TextBox></div>
    <asp:RequiredFieldValidator ID="rvEmail" runat="server" ErrorMessage="-Indtast venligst en gyldig e-mail" Display="None" ControlToValidate="txtbEmail"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regexvEmail" runat="server" ErrorMessage="-Indtast venligst en gyldig e-mail" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtbEmail"></asp:RegularExpressionValidator></div>
    </div>
    <div style="clear:both;"></div>
    <div><h4>Deltag  senest d. 8. september.</h4></div>
    <div class="section6"><h4><asp:Label ID="lblCheckbox" runat="server" Text="Ønsker du fremover at modtage nyhedsbrev fra"></asp:Label></h4><asp:ImageButton ID="ImageButton1" runat="server" OnClick="btnSubmit_Click" ImageUrl="/comp/images/button.png" CssClass="imgbutton"/></div>
    
    
    <div><asp:CheckBox ID="cbFalkNyhedsbrev" runat="server" Text="Falk Lauritsen Rejser" />
    

   <asp:CheckBox ID="cbBilkaNyhedsbrev" runat="server" Text="Bilka" CssClass="cbbilka"/>
    </div>

    <div><asp:CheckBox ID="cbTerms" runat="server" Text="Jeg har læst og er indforstået med " Checked="false"/><a href="http://www.bilka.dk/Mere-Bilka/Dig-og-Bilka/Konkurrencebetingelser" target="_blank">konkurrencebetingelserne</a>
    </div>
    <div><p style="font-size:10px;">Vinderen får direkte besked og bliver offentliggjort i varehuset og på bilka.dk  Ansatte i Dansk Supermarked Gruppen, Falk Lauritsen  Rejser og deres husstande kan ikke deltage i konkurrencen.  Gevinsten kan ikke ombyttes til kontanter.</p></div>
    <asp:ValidationSummary
        ID="ValidationSummary1" runat="server" ShowSummary="false" ShowMessageBox="true" DisplayMode="BulletList"/>
</div>
</div>

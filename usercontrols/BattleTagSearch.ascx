<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BattleTagSearch.ascx.cs" Inherits="usercontrols_BattleTagSearch" %>
<asp:ScriptManager ID="smBattleTagSearch" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="upBattleTagSearch" runat="server">
<ContentTemplate>
<asp:Panel ID="pBattleTagSearch" runat="server">
<div class="row top">
    <div class="one columns"></div>
    <div class="ten columns"><h1 style="text-align:center;">Diablo 3 profiler</h1></div>
    <div class="one columns"></div>
</div>
<div class="row">
    <div class="one columns"></div>
    <div class="ten columns">
        <div class="row">
            <div class="nine columns"></div><div class="three columns"><h4 style="color:#f1be0e;float:left;">Beta</h4></div>
        </div>
    </div>
    <div class="one columns"></div>
</div>
<div id="battletag-search" class="row">
    <div class="one columns"></div>
    <div class="ten columns">
        <div class="row collapse">
                  <div class="eight mobile-four columns">
        <asp:TextBox ID="TxtbSearchBattleTag" runat="server" placeholder="Input a battletag here!"></asp:TextBox>
        </div>
        <div class="four mobile-two columns">
            <asp:Button ID="btnSearchBattleTag" runat="server" Text="Search" 
                CssClass="button radius primary medium postfix expand" 
                onclick="btnSearchBattleTag_Click" ValidationGroup="battletagsearch"/>
        </div>
        </div>
        <div class="row">
            <div class="twelve columns validationtext">
                <asp:RequiredFieldValidator ID="RequiredBattletagValidator" runat="server" ErrorMessage="Please provide a battletag" ControlToValidate="TxtbSearchBattleTag" Display="Dynamic" ValidationGroup="battletagsearch"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegexBattletag" runat="server" ErrorMessage="Your battletag is not valid" ControlToValidate="TxtbSearchBattleTag" Display="Dynamic" ValidationExpression="^[\D]*#[0-9]+$" ValidationGroup="battletagsearch"></asp:RegularExpressionValidator>
            </div>
        </div>
    </div>
    <div class="one columns"></div>
</div>
<div id="battletag-status" class="row first">
    <div class="twelve columns status-text"><asp:Label ID="lblSearchStatus" runat="server" Text="Label"></asp:Label></div>
</div>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="uppBattleTagSearch"  runat="server">
    <ProgressTemplate>
    <div id="progress" class="row first">
        <div class="one columns"></div>
        <div class="ten columns">
            <div class="row">
                <div class="six mobile-four columns update-text"><h5>Loading please wait.</h5></div>
                <div class="six mobile-two columns"><img alt="spinner" src="/images/ajax-loader.gif" /></div>
            </div>
            </div>
        <div class="one columns"></div>
    </div>
    </ProgressTemplate>
</asp:UpdateProgress>

<div id="battletag-footer" class="row">
    <div class="three columns"></div>
    <div class="six columns">
        <div class="row">
            <div class="twelve columns sponsored-link">
                <a href="http://juhlsen.com">Sponsored by Juhlsen.com</a>
            </div>
        </div>
    </div>
    <div class="three columns"></div>
</div>

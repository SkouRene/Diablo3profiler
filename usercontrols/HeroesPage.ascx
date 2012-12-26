<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeroesPage.ascx.cs" Inherits="usercontrols_HeroesPage" %>
<asp:ScriptManager ID="smHeroes" runat="server"></asp:ScriptManager>
<div id="heroes" class="row">
    <div class="twelve columns">
<asp:Panel ID="viewHeroes" ClientIDMode="Static" runat="server">
    <asp:Repeater ID="rViewHeroes" runat="server" OnItemDataBound="rViewHeroes_ItemDataBound">
        <HeaderTemplate></HeaderTemplate>
        <ItemTemplate>
            <div class="row hero-name"><asp:HyperLink ID="hplHero" runat="server">
                <div class="six mobile-three columns hero-na"><h1><asp:Label ID="lblHeroName" runat="server"></asp:Label></h1></div>
                <div class="two mobile-one columns hero-level"><h1><asp:Literal ID="lblHeroLevel" runat="server"></asp:Literal></h1></div>
                <div class="two mobile-one columns hero-dif"><asp:Image ID="imgHeroDificulty" runat="server" AlternateText="hero dificulty" /></div>
                <div class="two mobile-one columns hero-icon"><asp:Image ID="imgHeroIcon" runat="server" AlternateText="hero icon" /></div></asp:HyperLink>
              </div>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
    </asp:Repeater>

    <div class="row">
        <div class="six centered columns">

            <div id="btnGetMoreHeroes" Class="button primary radius full-width first" >Get more heroes</div>
        </div>
    </div>


</asp:Panel>

<asp:Panel ID="getHeroes" ClientIDMode="Static" runat="server" style="display:none;">
    <div style="color:#FFF;">Hallo world and get me more heroes!</div>
</asp:Panel>
    </div>
</div>
<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="Counters.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Reports.Counters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">

    <asp:Repeater ID="tblCounterReport" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th></th>
                    <th></th>
                </tr>
                <tr>
                    <th>ИТОГО</th>

                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

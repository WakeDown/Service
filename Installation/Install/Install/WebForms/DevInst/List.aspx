<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Install.WebForms.DevInst.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
        <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, FormUrl) %>'>новая заявка</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    test
</asp:Content>

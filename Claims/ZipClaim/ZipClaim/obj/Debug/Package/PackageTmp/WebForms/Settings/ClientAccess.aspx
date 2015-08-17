<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="ClientAccess.aspx.cs" Inherits="ZipClaim.WebForms.Settings.ClientAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    Настройка доступа для клиентов
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <blockquote class='bg-warning'>
        <h5 class="text-warning">Изменения вступят в силу в течение 10 минут после сохранения изменений</h5>
    </blockquote>
    <p>
        <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary btn-sm" OnClick="btnSave_OnClick" ValidationGroup="vgFilter"><i class="fa fa-save"></i>&nbsp;сохранить</asp:LinkButton>
    </p>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>--%>
    <asp:Repeater ID="rtrAccessList" runat="server" OnItemDataBound="rtrAccessList_OnItemDataBound">
        <HeaderTemplate>
            <table class="table table-striped">
                <tr>
                    <th>Наименование</th>
                    <th>ЗИП</th>
                    <th>Счетчик</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:HiddenField ID="hfIdContractor" runat="server" Value='<%#Eval("login") %>' />
                    <asp:Label ID="contractorName" runat="server" Text='<%#Eval("ContractorName") %>'></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkZip" runat="server" Value='<%#Eval("sid") %>' Checked='<%# UserHaveZipGroup(Eval("login").ToString()) %>' OnCheckedChanged="chkZip_OnCheckedChanged"/>
                </td>
                <td>
                    <asp:CheckBox ID="chkCounter" runat="server" Value='<%#Eval("sid") %>' Checked='<%# UserHaveCounterGroup(Eval("login").ToString()) %>' OnCheckedChanged="chkCounter_OnCheckedChanged" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
<%--        </ContentTemplate></asp:UpdatePanel>--%>
</asp:Content>

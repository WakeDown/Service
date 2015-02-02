<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="ManagerOperator.aspx.cs" Inherits="ZipClaim.WebForms.Settings.ManagerOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    Сопоставление менеджеров и операторов
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <asp:Repeater ID="tblManager2OperatorList" runat="server">
            <HeaderTemplate>
                <div class="row dropdown-header">
                    <div class="col-sm-2 align-right pad-r-md">Менеджер</div>
                    <div class="col-sm-3">Оператор</div>
                </div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="form-group">
                    <label class="col-sm-2 control-label nowrap"><%#Eval("name") %></label>
                    <div class="col-sm-3">
                        <asp:HiddenField ID="hfIdManager" runat="server" Value='<%#Eval("id") %>'></asp:HiddenField>
                        <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
         <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить" OnClick="btnSave_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

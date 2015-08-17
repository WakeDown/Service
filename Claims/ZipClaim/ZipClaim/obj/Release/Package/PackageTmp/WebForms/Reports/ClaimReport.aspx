<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="ClaimReport.aspx.cs" Inherits="ZipClaim.WebForms.Reports.ClaimReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtContractorInn" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                    <span class="help-block">
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateBegin.ClientID %>' class="col-sm-2 control-label">Период</label>
            <div class="row">
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateBegin" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата начала"></asp:TextBox>
                        <%--<span class="input-group-иет">
                        <span class="btn btn-default" onclick="$(this).datepicker('#<%=txtDateBegin.ClientID %>');"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                        <%--<span class="input-group-btn">
                        <i class="glyphicon glyphicon-minus"></i>
                    </span>--%>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateBegin" runat="server" ErrorMessage="Введите дату начала" CssClass="text-danger" ControlToValidate="txtDateBegin" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateEnd" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата окончания"></asp:TextBox>
                        <%--<span class="input-group-addon">
                        <span class="btn btn-default datepicker-btn"><i class="glyphicon glyphicon-calendar"></i></span>
                    </span>--%>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateEnd" runat="server" ErrorMessage="Введите дату окончания" CssClass="text-danger" ControlToValidate="txtDateEnd" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnLoadExcel" runat="server" class="btn btn-primary btn-sm" OnClick="btnLoadExcel_OnClick" ValidationGroup="vgFilter"><i class="fa fa-file-excel-o"></i>&nbsp;выгрузить в Excel</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

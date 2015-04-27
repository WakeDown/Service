<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="Install.WebForms.DevInst.Editor" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <%=FormTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtEtNumber.ClientID %>' class="col-sm-2 control-label">№ счета в Эталон</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtEtNumber" runat="server" class="form-control required-mark" MaxLength="20"></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtEtNumber" runat="server" ErrorMessage="Заполните поле &laquo;№ счета в Эталон&raquo;" ControlToValidate="txtEtNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtContractNumber.ClientID %>' class="col-sm-2 control-label">№ договора</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtContractNumber" runat="server" class="form-control required-mark" MaxLength="50"></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtContractNumber" runat="server" ErrorMessage="Заполните поле &laquo;№ договора&raquo;" ControlToValidate="txtContractNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
       <div class="form-group">
            <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtContractorInn" runat="server" class="form-control width-20" placeholder="поиск" MaxLength="30" AutoPostBack="True" OnTextChanged="txtContractorInn_OnTextChanged"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control required-mark">
                    </asp:DropDownList>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlContractor" runat="server" ErrorMessage="Выберите контрагента" ControlToValidate="ddlContractor" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDdlContractorEmpty" runat="server" ErrorMessage="Выберите контрагента" ControlToValidate="ddlContractor" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue=""></asp:RequiredFieldValidator>
                    </span>

                </div>
                <small>наберите часть имени и нажмите Enter
                </small>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtPlanDate.ClientID %>' class="col-sm-2 control-label">Планируемая дата инсталляции</label>
            <div class="row">
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtPlanDate" runat="server" CssClass="form-control datepicker-btn required-mark"></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvTxtPlanDate" runat="server" ErrorMessage="Заполните поле &laquo;Планируемая дата инсталляции&raquo;" ControlToValidate="txtPlanDate" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvTxtPlanDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtPlanDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>
         <div class="form-group">
            <label for='<%=txtDeviceModel.ClientID %>' class="col-sm-2 control-label">Модель аппарата</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtDeviceModel" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;№ договора&raquo;" ControlToValidate="txtNumber" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=ddlCity.ClientID %>'>Город</label>
                <div class="col-sm-10">
                    <div class="input-group full-width">
                        <span class="input-group-btn width-20">
                            <asp:TextBox ID="txtCityFilter" runat="server" class="form-control width-20" placeholder="поиск" MaxLength="30"></asp:TextBox>
                        </span>
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control required-mark">
                        </asp:DropDownList>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvDdlCity" runat="server" ErrorMessage="Выберите город" ControlToValidate="ddlCity" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        </span>
                    </div>

                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=txtAddress.ClientID %>'>Адрес</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtAddress" runat="server" class="form-control"></asp:TextBox>
                    <%--<div class="input-group full-width">
                        <span class="input-group-btn width-20">
                            <asp:TextBox ID="txtAddressFilter" runat="server" class="form-control width-20" placeholder="поиск" MaxLength="50"></asp:TextBox>
                        </span>
                        <asp:DropDownList ID="ddlAddress" runat="server" CssClass="form-control required-mark">
                        </asp:DropDownList>
                        <span class="help-block">
                            <asp:RequiredFieldValidator ID="rfvDdlAddress" runat="server" ErrorMessage="Выберите адрес" ControlToValidate="ddlAddress" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        </span>
                    </div>--%>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=txtObjectName.ClientID %>'>Наименование объекта</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtObjectName" runat="server" class="form-control"></asp:TextBox>
                </div>
            </div>
        <div class="form-group">
            <label for='<%=txtContactName.ClientID %>' class="col-sm-2 control-label">Контактное лицо</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtContactName" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtAddDevices.ClientID %>' class="col-sm-2 control-label">Доп. оборудование</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtAddDevices" runat="server" class="form-control" MaxLength="150"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label">Менеджер договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control required-mark">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlManager" runat="server" ErrorMessage="Выберите менеджера договора" ControlToValidate="ddlManager" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
                <label class="col-sm-2 control-label" for='<%=ddlServiceAdmin.ClientID %>'>Сервисный администратор</label>
                <div class="col-sm-10">
                    <div class="input-group full-width">
                        <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control required-mark">
                        </asp:DropDownList>
                    </div>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlServiceAdmin" runat="server" ErrorMessage="Выберите сервисного администратора" ControlToValidate="ddlServiceAdmin" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    </span>
                </div>
            </div>
         <div class="form-group">
            <label for='<%=txtComment.ClientID %>' class="col-sm-2 control-label">Комментарий</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtComment" runat="server" class="form-control" MaxLength="5000" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-offset-2 col-sm-10">
                <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                    <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку договоров" href='<%= FriendlyUrl.Href(ListUrl)  %>'><i class="fa fa-mail-reply"></i></a>
                </div>
            </div>
    </div>
</asp:Content>

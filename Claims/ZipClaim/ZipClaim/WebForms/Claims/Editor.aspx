<%@ Page Title="Заявки - редактор" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.Master" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="ZipClaim.WebForms.Claims.Editor" Culture="RU-ru" UICulture="RU-ru" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <asp:Literal ID="lFormTitle" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <%-- <ul class="nav nav-tabs">
  <li class="active"><a href='<%= %>'>Договор</a></li>
  <li><a href="#">Оборудование</a></li>
</ul>
     <div>--%>
    <asp:HiddenField ID="hfServSheetId" runat="server" />
     <asp:HiddenField ID="hfIdServClaim" runat="server" />
    <asp:HiddenField ID="hfContractNumAmdDate" runat="server" />
    <asp:HiddenField ID="hfIdDevice" runat="server" />
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtServiceDeskNum.ClientID %>' class="col-sm-2 control-label">№ SD UN1T</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtServiceDeskNum" runat="server" class="form-control" MaxLength="20"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Серийный номер&raquo;" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtServiceDeskNum.ClientID %>' class="col-sm-2 control-label">№ SD Контрагента</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtContractorSdNum" runat="server" class="form-control" MaxLength="20"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Серийный номер&raquo;" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSerialNum" runat="server" CssClass="form-control required-mark" MaxLength="20" AutoPostBack="True" OnTextChanged="txtSerialNum_OnTextChanged" ValidationGroup="vgForm"></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtSerialNum" runat="server" ErrorMessage="Заполните поле &laquo;Серийный номер&raquo;" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    <asp:RegularExpressionValidator ID="revTxtSerialNum" runat="server" ControlToValidate="txtSerialNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" ErrorMessage="Допустимы только латинские буквы и цифры и символы &laquo;-&raquo;,&laquo;\&raquo;,&laquo;/&raquo;,&laquo; &raquo;(пробел)" ValidationExpression="^[A-Za-z0-9\ \-\\\/]+$"></asp:RegularExpressionValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtInvNum.ClientID %>' class="col-sm-2 control-label">Инвентарный номер</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtInvNum" runat="server" CssClass="form-control required-mark" MaxLength="20" AutoPostBack="True" OnTextChanged="txtInvNum_OnTextChanged" ValidationGroup="vgForm"></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtInvNum" runat="server" ErrorMessage="Заполните поле &laquo;Инвентарный номер&raquo;" ControlToValidate="txtInvNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    <asp:RegularExpressionValidator ID="revTxtInvNum" runat="server" ControlToValidate="txtInvNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" ErrorMessage="Допустимы только латинские буквы и цифры и символы &laquo;-&raquo;,&laquo;\&raquo;,&laquo;/&raquo;,&laquo; &raquo;(пробел)" ValidationExpression="^[A-Za-z0-9\ \-\\\/]+$"></asp:RegularExpressionValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDeviceModel.ClientID %>' class="col-sm-2 control-label">Модель</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtDeviceModel" runat="server" CssClass="form-control required-mark txt" MaxLength="150" noautocopml></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtDeviceModel" runat="server" ErrorMessage="Заполните поле &laquo;Модель&raquo;" ControlToValidate="txtDeviceModel" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtPrice" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtPrice" Type="Currency" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCity.ClientID %>' class="col-sm-2 control-label">Город/Нас. пункт</label>
            <div class="col-sm-10">
                <asp:HiddenField ID="hfIdCity" runat="server" />
                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control required-mark txt" MaxLength="150" noautocopml></asp:TextBox>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtCity" runat="server" ErrorMessage="Заполните поле &laquo;Город&raquo;" ControlToValidate="txtCity" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtAddress.ClientID %>' class="col-sm-2 control-label">Адрес</label>
            <div class="col-sm-10">
                <div class="input-group">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control txt" MaxLength="150" noautocopml></asp:TextBox>
                    <div class="input-group-addon no-padding-top-bottom">
                        <asp:LinkButton ID="btnAddressChangeNote" runat="server" class=" btn btn-default btn-xs" data-toggle="tooltip" title="Уведомление об изменение адреса" OnClick="btnAddressChangeNote_Click"><i class="fa fa-envelope"></i></asp:LinkButton>
                    </div>
                </div>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="rfvTxtAddress" runat="server" ErrorMessage="Заполните поле &laquo;Адрес&raquo;" ControlToValidate="txtAddress" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div id="pnlAddressChangeNote" runat="server" visible="False" class="form-group">
            <div class="col-sm-10 col-sm-push-2">
                <div class="input-group">
                    <asp:TextBox ID="txtAddressChangeNote" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" MaxLength="500" placeholder="Введите информацию о новом местоположении" noautocopml></asp:TextBox>
                    <div class="input-group-addon">
                        <asp:LinkButton ID="btnSendNote" runat="server" class="btn btn-default" data-toggle="tooltip" title="Отправить уведомление" OnClick="btnSendNote_Click"><i class="fa fa-mail-forward"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtObjectName.ClientID %>' class="col-sm-2 control-label">Название объекта</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtObjectName" runat="server" CssClass="form-control txt" MaxLength="150" noautocopml></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Название объекта&raquo;" ControlToValidate="txtObjectName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                        <ContentTemplate>--%>
        <div class="form-group">
            <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
            <div class="col-sm-10">
                <div class="input-group full-width">

                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtContractorInn" runat="server" CssClass="form-control width-20" placeholder="поиск" MaxLength="100" OnTextChanged="txtContractorInn_OnTextChanged" AutoPostBack="True"></asp:TextBox>

                    </span>

                    <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control required-mark">
                    </asp:DropDownList>

                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlContractor" runat="server" ErrorMessage="Выберите контрагента" ControlToValidate="ddlContractor" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="rfvDdlContractorEmpty" runat="server" ErrorMessage="Выберите контрагента" ControlToValidate="ddlContractor" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue=""></asp:RequiredFieldValidator>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
                <small>наберите часть имени и нажмите Enter
                </small>
            </div>
        </div>
        <%--    </ContentTemplate>
                    </asp:UpdatePanel>--%>
        <div id="divContractNumber" runat="server" visible="False" class="form-group">
            <label for='<%=lblContractNumber.ClientID %>' class="col-sm-2 control-label">№ договора</label>
            <div class="col-sm-10">
                <asp:Label ID="lblContractNumber" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div id="divContractType" runat="server" visible="False" class="form-group">
            <label for='<%=lblContractType.ClientID %>' class="col-sm-2 control-label">Тип договора</label>
            <div class="col-sm-10">
                <asp:Label ID="lblContractType" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-10 col-sm-push-2">
                <asp:Label ID="lblCommentHistoryHeader" runat="server" CssClass="h4" Text="Предыдущие комментарии" Visible="False"></asp:Label>
                <asp:Repeater ID="rtrCommentHistory" runat="server" DataSourceID="sdsCommentHistory" OnItemDataBound="rtrCommentHistory_ItemDataBound">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="row h6">
                            <div class="col-sm-2">
                                <%#Eval("creator") %>
                            </div>
                            <div class="col-sm-5">
                                <%#Eval("descr") %>
                            </div>
                            <div class="col-sm-2">
                                <%#Eval("date_create") %>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="sdsCommentHistory" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" UpdateCommand="ui_zip_claims" UpdateCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="getClaimCommentHistory" Name="action" />
                        <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDescr.ClientID %>' class="col-sm-2 control-label">Комментарий</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtDescr" runat="server" class="form-control" MaxLength="500" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <span class="help-block">
                    <%--                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Заполните поле &laquo;Город&raquo;" ControlToValidate="txtAddress" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCounter.ClientID %>' class="col-sm-2 control-label">Счетчик</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtCounter" runat="server" class="form-control" MaxLength="20"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="rfvDdlContractStatus" runat="server" ErrorMessage="Выберите статус договора" ControlToValidate="ddlContractStatus" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="cvTxtCounter" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounter" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCounterColour.ClientID %>' class="col-sm-2 control-label">Счетчик (цветной)</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtCounterColour" runat="server" class="form-control" MaxLength="20"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:RequiredFieldValidator ID="rfvDdlContractStatus" runat="server" ErrorMessage="Выберите статус договора" ControlToValidate="ddlContractStatus" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>--%>
                    <asp:CompareValidator ID="cvTxtCounterColour" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCounterColour" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlEngeneerConclusion.ClientID %>' class="col-sm-2 control-label">Состояние оборудования</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlEngeneerConclusion" runat="server" CssClass="form-control required-mark">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlEngeneerConclusion" runat="server" ErrorMessage="Выберите заключение" ControlToValidate="ddlEngeneerConclusion" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlEngeneer.ClientID %>' class="col-sm-2 control-label">Инженер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlEngeneer" runat="server" CssClass="form-control required-mark">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlEngeneer" runat="server" ErrorMessage="Выберите инженера" ControlToValidate="ddlEngeneer" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceAdmin.ClientID %>' class="col-sm-2 control-label">Сервисный администратор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control required-mark">
                </asp:DropDownList>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvDdlServiceAdmin" runat="server" ErrorMessage="Выберите сервисного администратора" ControlToValidate="ddlServiceAdmin" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <asp:HiddenField ID="hfIdManager" runat="server" />
        <asp:Panel ID="pnlUsers" runat="server">
            <div class="form-group">
                <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label">Менеджер</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control required-mark">
                    </asp:DropDownList>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlManager" runat="server" ErrorMessage="Выберите менеджера" ControlToValidate="ddlManager" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <%--<asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label for='<%=ddlOperator.ClientID %>' class="col-sm-2 control-label">Оператор</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control required-mark">
                    </asp:DropDownList>
                    <span class="help-block">
                        <asp:RequiredFieldValidator ID="rfvDdlOperator" runat="server" ErrorMessage="Выберите оператора" ControlToValidate="ddlOperator" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" InitialValue="-1"></asp:RequiredFieldValidator>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label for='<%=txtRequestNum.ClientID %>' class="col-sm-2 control-label">№ требования</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtRequestNum" runat="server" class="form-control" MaxLength="9"></asp:TextBox>
                    <span class="help-block">
                        <%--<asp:RequiredFieldValidator ID="rfvTxtRequestNum" runat="server" ErrorMessage="Заполните поле &laquo;№ требования&raquo;" ControlToValidate="txtRequestNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                        <asp:RegularExpressionValidator ID="revTxtRequestNum" runat="server" ErrorMessage="введите 9 цифр номера требования" ValidationExpression="(^$)|(^\d{9}$)" CssClass="text-danger" ControlToValidate="txtRequestNum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RegularExpressionValidator>
                    </span>
                </div>
            </div>
            <div class="form-group">
                <label for='<%=txtWaybillNum.ClientID %>' class="col-sm-2 control-label">№ трансп. заявки</label>
                <div class="col-sm-10">
                    <asp:TextBox ID="txtWaybillNum" runat="server" class="form-control" MaxLength="20"></asp:TextBox>
                    <span class="help-block">
                        <%--<asp:RequiredFieldValidator ID="rfvTxtRequestNum" runat="server" ErrorMessage="Заполните поле &laquo;№ требования&raquo;" ControlToValidate="txtRequestNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>--%>
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
                        <%--                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="введите 9 цифр номера требования" ValidationExpression="(^$)|(^\d{9}$)" CssClass="text-danger" ControlToValidate="txtRequestNum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RegularExpressionValidator>--%>
                    </span>
                </div>
            </div>
        </asp:Panel>
        <div class="col-sm-offset-2 col-sm-10" id="9">
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <div id="pnlCopyInfo" runat="server">
                    <asp:Literal ID="lInfo" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSave" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить" OnClick="btnSave_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                <%--<asp:LinkButton ID="btnSaveAndAddNew" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="сохранить и очистить" OnClick="btnSaveAndAddNew_Click" ValidationGroup="vgForm"><i class="fa fa-save fa-lg"></i>&nbsp;<i class="fa fa-plus fa-sm"></i></asp:LinkButton>--%>
                <asp:LinkButton ID="btnAddNewClaim" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="новая заявка" OnClick="btnAddNewClaim_Click"><i class="fa fa-plus fa-sm"></i></asp:LinkButton>
                <a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку заявок" href='<%=  FriendlyUrl.Href("~/Claims") %>'><i class="fa fa-mail-reply"></i></a>
                <%--<a id="aCopyInfo" runat="server" type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="Информация" href='#'><i class="fa fa-copy"></i></a>--%>
                <asp:LinkButton ID="btnCopyInfo" runat="server" class="btn btn-default btn-lg" data-toggle="tooltip" title="информация" OnClick="btnCopyInfo_Click"><i class="fa fa-copy"></i></asp:LinkButton>
                <div class="pull-right">
                    <asp:HiddenField ID="hfIdContract" runat="server" />
                    <div id="counterReportError" runat="server" Visible="False" class="text-danger">Аппарата нет в базе!</div>
<%--                    <asp:LinkButton ID="btnCountersReport" runat="server" class="btn btn-primary" data-toggle="tooltip" title="" OnClick="btnCountersReport_Click"><i class="fa fa-clock-o"></i> История счетчиков</asp:LinkButton>--%>
                    <a id="btnCountersReport" runat="server" target="_blank" class="btn btn-primary"><i class="fa fa-clock-o"></i> История счетчиков</a>
                </div>
            </div>
        </div>

        <div id="pnlDevicesListWarning" runat="server">
            <div class="col-sm-10  col-sm-offset-2">
                <blockquote class='bg-warning'>
                    <h5 class="text-warning">Для добавления ЗИПов сохраните заявку</h5>
                </blockquote>
            </div>
        </div>
        <asp:Panel ID="pnlClaimUnits" runat="server" Visible="false">
            <div class="col-sm-12">
                <asp:HiddenField ID="hfLastHistDate" runat="server" />
                <blockquote class='bg-warning'>
                    <h5 class="text-warning">История изменений</h5>
                    <h6>
                        <table>
                            <asp:Repeater ID="rtrHistory" runat="server" DataSourceID="sdsHistory" OnItemDataBound="rtrHistory_OnItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="nowrap"><%#Eval("claim_state") %>&nbsp;&nbsp;&nbsp;</td>
                                        <td class="nowrap"><%#Eval("change_date") %></td>
                                        <td>&nbsp;&nbsp;<asp:Literal ID="lDateDiff" runat="server"></asp:Literal></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="2">
                                    <h4 class="text-danger">
                                        <asp:Label ID="lblCancelComment" runat="server" Text=""></asp:Label>
                                    </h4>
                                </td>
                            </tr>
                        </table>
                    </h6>
                </blockquote>
                <asp:SqlDataSource ID="sdsHistory" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="getClaimStateHistory" Name="action" />
                        <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
            <asp:UpdatePanel ID="upClaimUnits" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <h4>Список ЗИПов</h4>

                            <div>
                                <div class="panel-group" id="accordion">
                                    <div class="panel panel-default" id="pnl-history">
                                        <div id="historyHead" class="panel-heading">
                                            <div class="panel-title collapsed" data-toggle="collapse" data-target="#historyPanel">
                                                <a class="title"><i class="fa fa-clock-o"></i></a>
                                            </div>
                                        </div>
                                        <div id="historyPanel" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <asp:Repeater ID="rtrClimUnitsHistory" runat="server" DataSourceID="sdsClimUnitsHistory">
                                                    <HeaderTemplate>
                                                        <table class="table table-striped text-smaller">
                                                            <tr>
                                                                <td colspan="9">Последние 50 записей
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>№
                                                                </th>
                                                                <th>Каталожный номер
                                                                </th>
                                                                <th>Наименование
                                                                </th>
                                                                <th>Количество
                                                                </th>
                                                                <th>Дата создания заявки
                                                                </th>
                                                                <th>Статус заявки
                                                                </th>
                                                                <th>Счетчик
                                                                </th>
                                                                <th>Счетчик (цветной)
                                                                </th>
                                                                <th>ФИО инженера
                                                                </th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="nowrap"><%#Eval("row_num") %></td>
                                                            <td class="nowrap"><%#Eval("catalog_num") %></td>
                                                            <td class="nowrap"><%#Eval("name") %></td>
                                                            <td class="nowrap"><%#Eval("count") %></td>
                                                            <td class="nowrap"><%#Eval("date_create") %></td>
                                                            <td class="nowrap"><%#Eval("claim_state") %></td>
                                                            <td class="nowrap"><%#Eval("counter") %></td>
                                                            <td class="nowrap"><%#Eval("counter_colour") %></td>
                                                            <td class="nowrap"><%#Eval("service_engeneer") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <asp:SqlDataSource ID="sdsClimUnitsHistory" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="getClaimUnitHistoryList" Name="action" />
                                                        <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div id="pnlTop" runat="server" visible='False'>
                                <div class="panel-group" id="oftenSelected">
                                    <div class="panel panel-default" id="pnl-oftenSelected">
                                        <div id="oftenSelectedHead" class="panel-heading">
                                            <div class="panel-title" data-toggle="collapse" data-target="#oftenSelectedPanel">
                                                <a class="title"><i class="fa fa-retweet"></i></a>
                                            </div>
                                        </div>
                                        <div id="oftenSelectedPanel" class="panel-collapse collapse in">
                                            <div class="panel-body">
                                                <asp:Label ID="lblNoDataOpportunity" runat="server" Text="Недоступно для данного аппарата" Visible="False"></asp:Label>
                                                <asp:Panel ID="pnlOftenSelectedList" runat="server">
                                                    <div class="pull-left">
                                                        <asp:Repeater ID="rtrOftenSelected" runat="server" DataSourceID="sdsOftenSelected">
                                                            <HeaderTemplate>
                                                                <table class="table text-smaller auto-width">
                                                                    <tr>
                                                                        <th class="min-width"></th>
                                                                        <th class="min-width">№
                                                                        </th>
                                                                        <th class="min-width">Каталожный номер
                                                                        </th>
                                                                        <th class="min-width">Наименование
                                                                        </th>
                                                                        <th class="min-width">Количество
                                                                        </th>
                                                                        <th></th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style='<%# String.Format("background-color: #{0}", Eval("zip_group_color"))%>' data-toggle="tooltip" title='<%#Eval("comment") %>'>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOftenSelectedSet" runat="server" /></td>
                                                                    <td class="nowrap">
                                                                        <%#Eval("row_num") %></td>
                                                                    <td class="nowrap">
                                                                        <asp:Label ID="lblOftenSelectedCatalogNum" runat="server" Text='<%#Eval("catalog_num") %>'></asp:Label></td>
                                                                    <td class="nowrap">
                                                                        <asp:Label ID="lblOftenSelectedName" runat="server" Text='<%#Eval("name") %>'></asp:Label></td>
                                                                    <td class="nowrap">
                                                                        <asp:TextBox ID="txtOftenSelectedCount" runat="server" CssClass="txt-zip-count" Text='<%#Eval("count") %>'>1</asp:TextBox>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator ID="rfvTxtOftenSelectedCount" runat="server" ErrorMessage="Укажите количество" ControlToValidate="txtOftenSelectedCount" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgAddNew"></asp:RequiredFieldValidator>
                                                                            <asp:CompareValidator ID="cvTxtOftenSelectedCount" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtOftenSelectedCount" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                                                        </div>
                                                                    </td>

                                                                    <td></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                        <%--                                                    <asp:Label ID="lblOftenSelectedNoData" runat="server" Text="Нет данных по этой модели" Visible="False"></asp:Label>--%>
                                                        <asp:SqlDataSource ID="sdsOftenSelected" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                                            <SelectParameters>
                                                                <asp:Parameter DefaultValue="getOftenSelectedList" Name="action" />
                                                                <asp:ControlParameter ControlID="txtDeviceModel" Name="device_model" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                                <asp:Parameter DefaultValue="1" Name="is_top" DbType="Int16" />
                                                                <asp:Parameter DefaultValue="0" Name="start_row" />
                                                                <asp:Parameter DefaultValue="10" Name="end_row" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                    <div class="pull-left marg-l-sm">
                                                        <asp:Repeater ID="rtrOftenSelected2" runat="server" DataSourceID="sdsOftenSelected2">
                                                            <HeaderTemplate>
                                                                <table class="table text-smaller auto-width">
                                                                    <tr>
                                                                        <th class="min-width"></th>
                                                                        <th class="min-width">№
                                                                        </th>
                                                                        <th class="min-width">Каталожный номер
                                                                        </th>
                                                                        <th class="min-width">Наименование
                                                                        </th>
                                                                        <th class="min-width">Количество
                                                                        </th>
                                                                        <th></th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style='<%# String.Format("background-color: #{0}", Eval("zip_group_color"))%>' data-toggle="tooltip" title='<%#Eval("comment") %>'>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOftenSelectedSet" runat="server" /></td>
                                                                    <td class="nowrap">
                                                                        <%#Eval("row_num") %></td>
                                                                    <td class="nowrap">
                                                                        <asp:Label ID="lblOftenSelectedCatalogNum" runat="server" Text='<%#Eval("catalog_num") %>'></asp:Label></td>
                                                                    <td class="nowrap">
                                                                        <asp:Label ID="lblOftenSelectedName" runat="server" Text='<%#Eval("name") %>'></asp:Label></td>
                                                                    <td class="nowrap">
                                                                        <asp:TextBox ID="txtOftenSelectedCount" runat="server" CssClass="txt-zip-count" Text='<%#Eval("count") %>'>1</asp:TextBox>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator ID="rfvTxtOftenSelectedCount" runat="server" ErrorMessage="Укажите количество" ControlToValidate="txtOftenSelectedCount" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgAddNew"></asp:RequiredFieldValidator>
                                                                            <asp:CompareValidator ID="cvTxtOftenSelectedCount" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtOftenSelectedCount" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                                                        </div>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                        <asp:SqlDataSource ID="sdsOftenSelected2" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                                            <SelectParameters>
                                                                <asp:Parameter DefaultValue="getOftenSelectedList" Name="action" />
                                                                <asp:ControlParameter ControlID="txtDeviceModel" Name="device_model" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                                <asp:Parameter DefaultValue="1" Name="is_top" DbType="Int16" />
                                                                <asp:Parameter DefaultValue="11" Name="start_row" />
                                                                <asp:Parameter DefaultValue="20" Name="end_row" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                    <div class="pull-left marg-l-sm">
                                                        <asp:Repeater ID="rtrOftenSelected3" runat="server" DataSourceID="sdsOftenSelected3">
                                                            <HeaderTemplate>
                                                                <table class="table text-smaller auto-width">
                                                                    <tr>
                                                                        <th class="min-width"></th>
                                                                        <th class="min-width">№
                                                                        </th>
                                                                        <th class="min-width">Каталожный номер
                                                                        </th>
                                                                        <th class="min-width">Наименование
                                                                        </th>
                                                                        <th class="min-width">Количество
                                                                        </th>
                                                                        <th></th>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr style='<%# String.Format("background-color: #{0}", Eval("zip_group_color"))%>' data-toggle="tooltip" title='<%#Eval("comment") %>'>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkOftenSelectedSet" runat="server" /></td>
                                                                    <td class="nowrap">
                                                                        <%#Eval("row_num") %></td>
                                                                    <td class="nowrap">
                                                                        <asp:Label ID="lblOftenSelectedCatalogNum" runat="server" Text='<%#Eval("catalog_num") %>'></asp:Label></td>
                                                                    <td class="nowrap">
                                                                        <asp:Label ID="lblOftenSelectedName" runat="server" Text='<%#Eval("name") %>'></asp:Label></td>
                                                                    <td class="nowrap">
                                                                        <asp:TextBox ID="txtOftenSelectedCount" runat="server" CssClass="txt-zip-count" Text='<%#Eval("count") %>'>1</asp:TextBox>
                                                                        <div>
                                                                            <asp:RequiredFieldValidator ID="rfvTxtOftenSelectedCount" runat="server" ErrorMessage="Укажите количество" ControlToValidate="txtOftenSelectedCount" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgAddNew"></asp:RequiredFieldValidator>
                                                                            <asp:CompareValidator ID="cvTxtOftenSelectedCount" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtOftenSelectedCount" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                                                        </div>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                        <asp:SqlDataSource ID="sdsOftenSelected3" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                                                            <SelectParameters>
                                                                <asp:Parameter DefaultValue="getOftenSelectedList" Name="action" />
                                                                <asp:ControlParameter ControlID="txtDeviceModel" Name="device_model" DefaultValue="" ConvertEmptyStringToNull="True" />
                                                                <asp:Parameter DefaultValue="1" Name="is_top" DbType="Int16" />
                                                                <asp:Parameter DefaultValue="21" Name="start_row" />
                                                                <asp:Parameter DefaultValue="30" Name="end_row" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div>
                                                        <asp:Button ID="btnAddNewOftenSelected" runat="server" CssClass="btn btn-primary btn-lg" Text="добавить к заявке" OnClick="btnAddNewOftenSelected_OnCLick" />
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hfZipStateSysName" runat="server" />
                            <h4 id="pnlZipState" runat="server">&nbsp;&nbsp;&nbsp;</h4>
                            <h5>
                                <span id="pnlSumCount" runat="server" visible="False">
                                    <span class="label label-default">Общая сумма вход:
        <asp:Literal ID="lSummCountIn" runat="server" Text="0"></asp:Literal></span>
                                    <span class="label label-default">Общая сумма выход:
        <asp:Literal ID="lSummCountOut" runat="server" Text="0"></asp:Literal></span>
                                </span>
                            </h5>
                            <asp:GridView ID="tblClaimUnitList" runat="server" CssClass="table table-striped no-margin-bottom" DataSourceID="sdsList" AutoGenerateColumns="false" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowDataBound="tblClaimUnitList_OnRowDataBound" OnPreRender="tblClaimUnitList_OnPreRender" OnDataBound="tblClaimUnitList_DataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnClaimUnitDelete" runat="server" OnClick="btnClaimUnitDelete_OnClick" CommandArgument='<%#Eval("id_claim_unit") %>' OnClientClick="return DeleteConfirm('ЗИП')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить" Visible='<%# !String.IsNullOrEmpty(Eval("id_claim_unit").ToString()) && ((Eval("active_state").ToString() == "0" && Eval("id_engeneer").ToString() == User.Id.ToString()) || (UserIsTech && hfDisplayZipConfirmState.Value.Equals("True")) || UserIsSysAdmin) %>' TabIndex="1000"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="id_claim_unit" HeaderText="ID" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblIdClaimUnit" runat="server" Text='<%#Bind("id_claim_unit") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdClaimUnit" runat="server" Text='<%#Bind("id_claim_unit") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="catalog_num" HeaderText="Каталожный номер" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblCatalogNum" runat="server" Text='<%#Bind("catalog_num") %>' Visible='<%# !UserIsSysAdmin %>'></asp:Label>
                                            <asp:TextBox ID="txtCatalogNum" runat="server" Text='<%#Bind("catalog_num") %>' noautocopml Visible='<%# UserIsSysAdmin %>' CssClass="txt-zip-cat-num form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCatalogNum" runat="server" Text='<%#Bind("catalog_num") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCatalogNum" runat="server" CssClass="txt-zip-cat-num form-control" Text='<%#Bind("catalog_num") %>' OnTextChanged="txtCatalogNum_OnTextChanged" noautocopml AutoPostBack="True"></asp:TextBox>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rfvTxtCatalogNum" runat="server" ErrorMessage="Укажите каталожный номер" ControlToValidate="txtCatalogNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgAddNew"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revTxtCatalogNum" runat="server" ControlToValidate="txtCatalogNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm" ErrorMessage="Допустимы только латинские буквы и цифры и символы &laquo;-&raquo;,&laquo;\&raquo;,&laquo;/&raquo;,&laquo; &raquo;(пробел)" ValidationExpression="^[A-Za-z0-9\ \-\\\/]+$"></asp:RegularExpressionValidator>
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="name" HeaderText="Наименование" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Bind("name") %>' Visible='<%# !UserIsSysAdmin %>'></asp:Label>
                                            <asp:TextBox ID="txtName" runat="server" Text='<%#Bind("name") %>' noautocopml Visible='<%# UserIsSysAdmin %>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="txt-zip-name form-control" Text='<%#Bind("name") %>' noautocopml></asp:TextBox>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rfvTxtName" runat="server" ErrorMessage="Укажите наименование" ControlToValidate="txtName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgAddNew"></asp:RequiredFieldValidator>
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="count" HeaderText="Количество" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblCount" runat="server" Text='<%#Bind("count") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCount" runat="server" Text='<%#Bind("count") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtCount" runat="server" CssClass="txt-zip-count form-control" Text='<%#Bind("count") %>'>1</asp:TextBox>
                                            <div>
                                                <asp:RequiredFieldValidator ID="rfvTxtCount" runat="server" ErrorMessage="Укажите количество" ControlToValidate="txtCount" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgAddNew"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvtxtCount" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtCount" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="nomenclature_num" HeaderText="Номенклатурный номер" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNomenclatureNum" runat="server" Text='<%#Bind("nomenclature_num") %>' CssClass='txt-zip-nomnum form-control' Enabled='<%# String.IsNullOrEmpty(Eval("nomenclature_num").ToString()) || !String.IsNullOrEmpty(Eval("nomenclature_claim_num").ToString()) || UserIsSysAdmin %>'></asp:TextBox>
                                            <div>
                                                <asp:CheckBoxList ID="chklNoNomNum" runat="server" CssClass="form-control nowrap" SelectedValue='<%# Bind("no_nomenclature_num") %>' AutoPostBack="True" OnSelectedIndexChanged="chklNoNomNum_OnSelectedIndexChanged" RowIndex='<%# Container.DataItemIndex %>'>
                                                    <asp:ListItem Text=" Новый ЗИП" Value="1" />
                                                    <asp:ListItem Text=" присутствует" Value="0" style="display: none" />
                                                </asp:CheckBoxList>
                                            </div>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNomenclatureNum" runat="server" Text='<%#Bind("nomenclature_num") %>'></asp:Label>
                                            <div>
                                                <asp:Label ID="lblNomenclatureClaimNum" runat="server" Text='<%#Eval("nomenclature_claim_num") %>' CssClass="nomenclature-claim-num"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_OnCLick" CommandArgument='<%#Eval("id_claim") %>' CssClass="btn btn-link" data-toggle="tooltip" title="сохранить новый" ValidationGroup="vgAddNew"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Кол-во на складе" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEtCount" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Цена за ед. на складе" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEtPrice" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="delivery_time" HeaderText="Срок поставки" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDeliveryTime" runat="server" Text='<%#Bind("delivery_time") %>' CssClass="txt-zip-deliv form-control" noautocopml></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryTime" runat="server" Text='<%#Bind("delivery_time") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="price_in" HeaderText="Цена вход (за ед.)" Visible="false" FooterStyle-CssClass="text-right" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPriceIn" runat="server" CssClass="txt-zip-price form-control" Text='<%#Bind("price_in", "{0:D}") %>' OnTextChanged="txtPriceIn_OnTextChanged"></asp:TextBox>
                                            <div>
                                                <asp:CompareValidator ID="cvTxtPriceIn" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtPriceIn" Type="Currency" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                            </div>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriceIn" runat="server" Text='<%#Bind("price_in") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="" HeaderText="Цена вход (сумма)" Visible="false" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriceInSum" runat="server" Text='<%#Eval("price_in_sum") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--//Колонки price_in_sum и price_out_sum должны быть с этим индексом потому что они показываются определенным группам --%>
                                    <asp:TemplateField SortExpression="price_out" HeaderText="Цена выход (за ед.)" Visible="false" FooterStyle-CssClass="text-right" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPriceOut" runat="server" CssClass="txt-zip-price form-control" Text='<%#Bind("price_out", "{0:D}") %>'></asp:TextBox>
                                            <div>
                                                <asp:CompareValidator ID="cvTxtPriceOut" runat="server" ErrorMessage="Введите число (разделитель запятая)" CssClass="text-danger" ControlToValidate="txtPriceOut" Type="Currency" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True"></asp:CompareValidator>
                                            </div>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%#Eval("price_out") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField SortExpression="" HeaderText="Цена выход (сумма)" Visible="false" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriceOutSum" runat="server" Text='<%#Eval("price_out_sum") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-CssClass="min-width" FooterStyle-CssClass="min-width" HeaderStyle-CssClass="middle">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="btnSave" runat="server" CommandName="Update" RowIndex='<%#Container.DataItemIndex %>' OnClick="btnSave_OnClick" CommandArgument='<%#Eval("id_claim_unit") %>' CssClass="btn btn-link" data-toggle="tooltip" title="сохранить"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("id_claim_unit") %>' CssClass="btn btn-link" data-toggle="tooltip" title="изменить" Visible='<%# !String.IsNullOrEmpty(Eval("id_claim_unit").ToString()) && ((Eval("active_state").ToString() == "0" && Eval("id_engeneer").ToString() == User.Id.ToString()) || (Eval("active_state").ToString() == "1" && Eval("manager_can_edit").ToString().Equals("1") && (UserIsManager || UserIsOperator))|| (UserIsTech && hfDisplayZipConfirmState.Value.Equals("True")) || UserIsSysAdmin) %>' ValidationGroup="vgEdit"><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" UpdateCommand="ui_zip_claims" UpdateCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="getClaimUnitList" Name="action" />
                                    <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter DefaultValue="" Name="action" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                            <div class="col-sm-5 text-danger">
                                <asp:Literal ID="lLastClaim" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                
            <div class="form-group">
                <div class="pull-right">
                    <blockquote class='bg-warning'>
                        <h5 class="text-warning">Убедитесь что все данные сохранены</h5>
                    </blockquote>
                </div>
            </div>
            <div>
                <div class="pull-right">
                    <asp:HiddenField ID="hfDisplaySendState" runat="server" />
                    <asp:LinkButton ID="btnSetStateSend" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Передать в работу" OnClick="btnSetStateSend_Click"><i class="fa fa-share fa-2x"></i></asp:LinkButton>
                    <asp:HiddenField ID="hfDisplayZipConfirmState" runat="server" />
                    <asp:LinkButton ID="btnZipConfirm" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Утвердить ЗИП" OnClick="btnZipConfirm_Click"><i class="fa fa-check fa-2x"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Удалить заявку" OnClick="btnDelete_Click" OnClientClick="return DeleteConfirm('заявку')"><i class="fa fa-trash-o fa-2x"></i></asp:LinkButton>
                    <asp:HiddenField ID="hfDisplayPriceSet" runat="server" />
                    <asp:LinkButton ID="btnSetStatePriceSet" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Цены указаны" OnClick="btnSetStatePriceSet_Click"><i class="fa fa-money fa-2x"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnRequestPrice" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Запросить цены" OnClick="btnRequestPrice_Click"><i class="fa fa-comments-o fa-2x"></i></asp:LinkButton>
                    <asp:HiddenField ID="hfDisplayPriceStates" runat="server" />
                    <asp:LinkButton ID="btnMacthPrice" runat="server" class="btn btn-success btn-lg" data-toggle="tooltip" title="Согласовать цены" OnClick="btnMacthPrice_Click"><i class="fa fa-check-circle-o fa-2x"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnFailPrice" runat="server" class="btn btn-danger btn-lg" data-toggle="tooltip" title="Отклонить цены" OnClick="btnFailPrice_Click"><i class="fa fa-ban fa-2x"></i></asp:LinkButton>
                    <asp:HiddenField ID="hfDisplayDoneState" runat="server" />
                    <asp:LinkButton ID="btnSetStateDone" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Закрыть заявку" OnClick="btnSetStateDone_Click"><i class="fa fa-thumbs-o-up fa-2x"></i></asp:LinkButton>
                    <%--<div>
                        <asp:Label ID="lbl" runat="server" Text="Label"></asp:Label>
                    </div>--%>
                </div>
            </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
            <div id="pnlCancelComment" runat="server" class="pull-left" visible="False">
                <asp:HiddenField ID="hfDisplayCancelState" runat="server" />
                <%--                <div class="row">--%>
                <div class="pull-left" id="vgCancel">
                    <asp:TextBox ID="txtCancelComment" runat="server" CssClass="form-control txt-cancel" MaxLength="500" Rows="3" TextMode="MultiLine" placeholder="Причина отклонения заявки"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTxtCancelComment" runat="server" ErrorMessage="Укажите причину отклонения заявки" ControlToValidate="txtCancelComment" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgCancel"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="cvTxtCancelComment" runat="server" ErrorMessage="Длина причины должна быть не менее 10 символов" ControlToValidate="txtCancelComment" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgCancel" Operator="GreaterThan" ValidationExpression="^[\s\S]{10,500}$"></asp:RegularExpressionValidator>
                </div>
                <div class="pull-right">
                    <asp:LinkButton ID="btnCancelState" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Отклонить заявку" OnClick="btnCancelState_Click" ValidationGroup="vgCancel"><i class="fa fa-times fa-2x"></i></asp:LinkButton>
                </div>
                <%--                </div>--%>
            </div>
        </asp:Panel>
    </div>

</asp:Content>

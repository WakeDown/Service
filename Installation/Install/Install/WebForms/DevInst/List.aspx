<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Install.WebForms.DevInst.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
        <a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, FormUrl) %>'>новая заявка</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtNumber.ClientID %>' class="col-sm-2 control-label">№ договора</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtNumber" runat="server" class="form-control input-sm" MaxLength="150"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtFilterSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер аппарата</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtFilterSerialNum" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlContractor.ClientID %>' class="col-sm-2 control-label">Контрагент</label>
            <div class="col-sm-10">
                <div class="input-group full-width">
                    <span class="input-group-btn width-20">
                        <asp:TextBox ID="txtContractorInn" runat="server" class="form-control width-20 input-sm" placeholder="поиск" MaxLength="30"></asp:TextBox>
                    </span>
                    <asp:DropDownList ID="ddlContractor" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlClaimState.ClientID %>' class="col-sm-2 control-label">Статус заявки</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlClaimState" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label">Менеджер договора</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateBegin.ClientID %>' class="col-sm-2 control-label">Период</label>
            <div class="row">
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateBegin" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата начала"></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateBegin" runat="server" ErrorMessage="Введите дату начала" CssClass="text-danger" ControlToValidate="txtDateBegin" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
                <div class="col-sm-2">
                    <div class="input-group">
                        <asp:TextBox ID="txtDateEnd" runat="server" CssClass="form-control datepicker-btn input-sm" placeholder="Дата окончания"></asp:TextBox>
                    </div>
                    <span class="help-block">
                        <asp:CompareValidator ID="cvTxtDateEnd" runat="server" ErrorMessage="Введите дату окончания" CssClass="text-danger" ControlToValidate="txtDateEnd" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtRowsCount.ClientID %>' class="col-sm-2 control-label">Показывать записей</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtRowsCount" runat="server" class="form-control input-sm"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-primary btn-sm" OnClick="btnSearch_OnClick" ValidationGroup="vgFilter"><i class="glyphicon glyphicon-search"></i>&nbsp;найти</asp:LinkButton>
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>
    </h5>
    <asp:Repeater ID="tblList" runat="server" DataSourceID="sdsList">
        <HeaderTemplate>
            <table class="table table-striped">
                <tr>
                    <th></th>
                    <th>ID</th>
                    <th>№ счета в Эталон</th>
                    <th>№ договора</th>
                    <th>Контрагент</th>
                    <th>Планируемая дата инсталляции</th>
                    <th>Модель аппарата</th>
                    <th>Местонахождение</th>
                    <th>
                        <div>Менеджер</div>
                        <div class="text-nowrap">Серв. администратор</div>
                    </th>
                    <th class="text-nowrap">Конт. лицо</th>
                    <th>Доп. оборудование</th>
                    <th>Комментарий</th>
                    <th></th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <a runat="server" href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_device_install")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="изменить"><i class="fa fa-edit fa-lg"></i></a>
                </td>
                <td><%#Eval("id_device_install") %></td>
                <td><%#Eval("et_number") %></td>
                    <td><%#Eval("contract_number") %></td>
                    <td><%#Eval("contractor") %></td>
                    <td><%#Eval("plan_date", "{0:D}") %></td>
                    <td><%#Eval("device_model") %></td>
                    <td><%#Eval("device_place") %></td>
                    <td>
                        <div><%#Eval("manager") %></div>
                        <div><%#Eval("service_admin") %></div>
                    </td>
                    <td><%#Eval("contact_name") %></td>
                    <td><%#Eval("add_devices") %></td>
                    <td><%#Eval("descr") %></td>
                <td>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_device_install") %>' OnClientClick="return DeleteConfirm('заявку')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_device_install" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getDeviceInstallList" Name="action" />
            <asp:QueryStringParameter QueryStringField="number" Name="contract_number" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="rcn" Name="rows_count" DefaultValue="10" ConvertEmptyStringToNull="True" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

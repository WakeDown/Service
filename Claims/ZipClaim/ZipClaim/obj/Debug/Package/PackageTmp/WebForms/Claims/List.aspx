<%@ Page Title="Заявки - список" Language="C#" MasterPageFile="~/WebForms/Masters/List.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ZipClaim.WebForms.Claims.List" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <asp:Panel ID="pnlNewClaim" runat="server">
        <%--<a class="btn btn-primary btn-lg" type="button" href='<%= GetRedirectUrlWithParams(String.Empty, false, FormUrl) %>'>новая заявка</a>--%>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtId.ClientID %>' class="col-sm-2 control-label">ID</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtId" runat="server" class="form-control input-sm" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtId.ClientID %>' class="col-sm-2 control-label">№ SD UN1T</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSdNum" runat="server" class="form-control input-sm" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtId.ClientID %>' class="col-sm-2 control-label">№ SD Контрагента</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtContractorSdNum" runat="server" class="form-control input-sm" MaxLength="20"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSerialNum" runat="server" class="form-control input-sm" MaxLength="50"></asp:TextBox>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
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
                    <span class="help-block">
                        <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=chklClaimState.ClientID %>' class="col-sm-2 control-label">Статус заявки</label>
            <div class="col-sm-10">
                <%--<asp:DropDownList ID="ddlClaimState" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>--%>
                <span id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                    <input id="chkTristate" runat="server" type="hidden" />
                </span><span>все</span>
                <asp:CheckBoxList ID="chklClaimState" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm chk-lst disp-table" RepeatLayout="Flow"></asp:CheckBoxList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
<%--        <div class="form-group">
            <label for='<%=chklEtClaimState.ClientID %>' class="col-sm-2 control-label">Статус требования</label>
            <div class="col-sm-10">
                <span id="pnlTristate1" runat="server" data-toggle="tooltip" title="выделить все">
                    <input id="chkTristate1" runat="server" type="hidden" />
                </span><span>все</span>
                <asp:CheckBoxList ID="chklEtClaimState" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm chk-lst disp-table" RepeatLayout="Flow"></asp:CheckBoxList>
            </div>
        </div>--%>
        <div class="form-group">
            <label for='<%=chklWaybillClaimState.ClientID %>' class="col-sm-2 control-label">Статус транспортной заявки</label>
            <div class="col-sm-10">
                <%--<asp:DropDownList ID="ddlClaimState" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>--%>
                <span id="pnlTristate2" runat="server" data-toggle="tooltip" title="выделить все">
                    <input id="chkTristate2" runat="server" type="hidden" />
                </span><span>все</span>
                <asp:CheckBoxList ID="chklWaybillClaimState" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm chk-lst disp-table" RepeatLayout="Flow"></asp:CheckBoxList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlEngeneer.ClientID %>' class="col-sm-2 control-label">Инженер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlEngeneer" runat="server" CssClass="form-control input-sm" Enabled="false">
                </asp:DropDownList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlServiceAdmin.ClientID %>' class="col-sm-2 control-label">Сервисный администратор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceAdmin" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlOperator.ClientID %>' class="col-sm-2 control-label">Оператор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label">Менеджер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblNotInSystem.ClientID %>' class="col-sm-2 control-label">Устройство заведено в систему</label>
            <div class="col-sm-10">
                <asp:RadioButtonList ID="rblNotInSystem" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                    <asp:ListItem Text="все" Value="-13"></asp:ListItem>
                    <asp:ListItem Text="да" Value="1"></asp:ListItem>
                    <asp:ListItem Text="нет" Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <span class="help-block">
                    <%--                    <asp:CompareValidator ID="cvTxtSpeed" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtSpeed" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>--%>
                </span>
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
            <label for='<%=txtRowsCount.ClientID %>' class="col-sm-2 control-label">Показывать записей</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtRowsCount" runat="server" class="form-control input-sm" MaxLength="5"></asp:TextBox>
                <span class="help-block">
                    <asp:CompareValidator ID="cvTxtRowsCount" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtRowsCount" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-warning btn-sm" OnClick="btnSearch_OnClick" ValidationGroup="vgFilter"><i class="glyphicon glyphicon-search"></i>&nbsp;найти</asp:LinkButton>
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
                <asp:LinkButton ID="btnSaveUserFilter" runat="server" class="btn btn-primary btn-sm" OnClick="btnSaveUserFilter_OnClick" ValidationGroup="vgFilter"><i class="fa fa-save"></i>&nbsp;сохранить</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>
        <span  id="pnlSumCount" runat="server" Visible="False">
        <span class="label label-default">Общая сумма вход:
        <asp:Literal ID="lSummCountIn" runat="server" Text="0"></asp:Literal></span>
            <span class="label label-default">Общая сумма выход:
        <asp:Literal ID="lSummCountOut" runat="server" Text="0"></asp:Literal></span>
            </span>
    </h5>
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false"  GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnRowDataBound="tblList_RowDataBound" OnDataBound="tblList_DataBound">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                    <span class='row-mark <%# Eval("fault_state") %>'>&nbsp;</span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id_claim" SortExpression="id_claim" HeaderText="ID" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />
            <asp:TemplateField ItemStyle-CssClass="min-width" HeaderText="№ ServiceDesk" HeaderStyle-CssClass="sorted-header">
                <ItemTemplate>
                    <div class="nowrap">
                    <%# Eval("service_desk_num") %></div>
                    <div class="nowrap">
                    <%# Eval("contractor_sd_num") %></div>
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:BoundField DataField="service_desk_num" SortExpression="service_desk_num" HeaderText="№ ServiceDesk" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                    <a runat="server" href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_claim")), false, FormUrl) %>' class="btn btn-link" data-toggle="tooltip" title="изменить"><i class="fa fa-edit fa-lg"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <%# Eval("device")  %>
                    <div>
                        <%# Eval("contractor_col_name")  %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <%--            <asp:BoundField DataField="device" SortExpression="device" HeaderText="Аппарат" HeaderStyle-CssClass="sorted-header" />--%>

            <%--//Колонки price_in_sum и price_out_sum должны быть с этим индексом потому что они показываются определенным группам --%>
            <asp:BoundField DataField="price_in_sum" SortExpression="price_in_sum" HeaderText="Сумма цена вход" HeaderStyle-CssClass="sorted-header" Visible="false" />
            <asp:BoundField DataField="price_out_sum" SortExpression="price_out_sum" HeaderText="Сумма цена выход" HeaderStyle-CssClass="sorted-header" Visible="false" />


            <asp:BoundField DataField="zip_unit_count" SortExpression="zip_unit_count" HeaderText="Количество ЗИПов" HeaderStyle-CssClass="sorted-header" />
            <%-- <asp:BoundField DataField="engeneer" SortExpression="engeneer" HeaderText="ФИО инженера" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:TemplateField ItemStyle-CssClass="min-width" HeaderText="Инженер Администратор Оператор Менеджер">
                <ItemTemplate>
                    <div class="nowrap" data-toggle="tooltip" title='<%#String.Format("{0}, {1}, {2}", Eval("engeneer_city"), Eval("engeneer_org"), Eval("engeneer_pos")) %>'><%# Eval("engeneer")  %></div>
                    <div class="nowrap"><%# Eval("service_admin") %></div>
                    <div class="nowrap"><%# Eval("operator")  %></div>
                    <div class="nowrap"><%# Eval("manager")  %></div>
                </ItemTemplate>
            </asp:TemplateField>
            <%--            <asp:BoundField DataField="claim_state" SortExpression="claim_state" HeaderText="Статус" HeaderStyle-CssClass="sorted-header" />--%>
            <asp:TemplateField HeaderText="Статус">
                <ItemTemplate>
                    <%# Eval("claim_state") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                    <div class="lightlow nowrap">Создано</div><div>
                    <%# Eval("date_create") %>
                        </div>
                    <div class="lightlow nowrap">Приход план</div><div>
                    <%# GetDateText(Eval("et_plan_came_date").ToString()) %>
                        </div>
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:BoundField DataField="date_create" SortExpression="date_create" HeaderText="Дата создания" HeaderStyle-CssClass="sorted-header" ItemStyle-CssClass="min-width nowrap" />--%>
            <%--<asp:TemplateField HeaderText="Оборудование">
                <ItemTemplate>
                   <ul class="list-unstyled">
                        <li>
                            <a class="btn btn-link" data-toggle="tooltip" title="перейти к списку" href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_contract")), false, "~/Contracts/Devices/Editor") %>'><%# String.Format("{0} шт.:", Eval("device_count")) %></a>
                            <asp:Repeater ID="rtrContract2DevicesList" runat="server" DataSourceID="sdsContract2DevicesList">
                                <ItemTemplate>
                                    <li>
                                        <h5 class="small nomargin pad-l-sm"><%#String.Format("{0} - {1}", Eval("count"), Eval("service_interval")) %></h5>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ul>
                    <asp:SqlDataSource ID="sdsContract2DevicesList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="getContract2DevicesIntervalGroupList" Name="action" DbType="String" />
                            <asp:Parameter DefaultValue="-1" Name="id_contract" DbType="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField ItemStyle-CssClass="min-width nowrap">
                <ItemTemplate>
                    <asp:HiddenField ID="hfDisplaySendState" runat="server" Value='<%#Eval("display_send_state") %>' />
                    <asp:LinkButton ID="btnMacthPrice" runat="server" OnClick="btnMacthPrice_OnClick" CommandArgument='<%#Eval("id_claim") %>' CssClass="btn btn-link success" data-toggle="tooltip" title="согласовать цены"><i class="fa fa-check-circle-o fa-lg"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnFailPrice" runat="server" OnClick="btnFailPrice_OnClick" CommandArgument='<%#Eval("id_claim") %>' CssClass="btn btn-link danger" data-toggle="tooltip" title="отклонить цены"><i class="fa fa-ban fa-lg"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnSetStateDone" runat="server" class="btn btn-link btn-lg" CommandArgument='<%#Eval("id_claim") %>' data-toggle="tooltip" title="Закрыть заявку" OnClick="btnSetStateDone_Click"><i class="fa fa-thumbs-o-up fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-CssClass="min-width">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_claim") %>' OnClientClick="return DeleteConfirm('заявку')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelecting="sdsList_Selecting">
        <SelectParameters>
            <asp:Parameter DefaultValue="getClaimList" Name="action" />
            <asp:QueryStringParameter QueryStringField="sdnum" Name="service_desk_num" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="csdnum" Name="contractor_sd_num" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="snum" Name="serial_num" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="mngr" Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="engr" Name="id_engeneer" DefaultValue='' ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="oper" Name="id_operator" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="sadm" Name="id_service_admin" DefaultValue='' ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="ctrtr" Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="state" Name="lst_claim_states" DefaultValue="1,3,4,5,6,8,9,10,11,12,13,21,22" ConvertEmptyStringToNull="True" />
             <asp:QueryStringParameter QueryStringField="etste" Name="lst_et_claim_states" DefaultValue="" ConvertEmptyStringToNull="True" />
             <asp:QueryStringParameter QueryStringField="wayst" Name="lst_waybill_claim_states" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="DateTime" />
            <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="DateTime"/>
            <asp:QueryStringParameter QueryStringField="nins" Name="not_in_system" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
            <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
            <asp:QueryStringParameter QueryStringField="rcn" Name="rows_count" DefaultValue="30" ConvertEmptyStringToNull="True" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

<%@ Page Title="Личный кабинет" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ZipClaim.WebForms.Client.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <h4>
        <asp:Label ID="lblContractorName" runat="server" Text=""></asp:Label>
    </h4>
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
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSerialNum" runat="server" class="form-control input-sm" MaxLength="50"></asp:TextBox>
                <span class="help-block"></span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=chklClaimState.ClientID %>' class="col-sm-2 control-label">Статус заявки</label>
            <div class="col-sm-10">
                <span id="pnlTristate" runat="server" data-toggle="tooltip" title="выделить все">
                    <input id="chkTristate" runat="server" type="hidden" />
                </span><span>все</span>
                <asp:CheckBoxList ID="chklClaimState" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm chk-lst disp-table" RepeatLayout="Flow"></asp:CheckBoxList>
                <span class="help-block"></span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=chklWaybillClaimState.ClientID %>' class="col-sm-2 control-label">Статус транспортной заявки</label>
            <div class="col-sm-10">
                <span id="pnlTristate2" runat="server" data-toggle="tooltip" title="выделить все">
                    <input id="chkTristate2" runat="server" type="hidden" />
                </span><span>все</span>
                <asp:CheckBoxList ID="chklWaybillClaimState" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm chk-lst disp-table" RepeatLayout="Flow"></asp:CheckBoxList>
                <span class="help-block"></span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlEngeneer.ClientID %>' class="col-sm-2 control-label">Инженер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlEngeneer" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block"></span>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label">Менеджер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlManager" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
                <span class="help-block"></span>
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
                <asp:TextBox ID="txtRowsCount" runat="server" class="form-control input-sm" MaxLength="5"></asp:TextBox>
                <span class="help-block">
                    <asp:CompareValidator ID="cvTxtRowsCount" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtRowsCount" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgFilter"></asp:CompareValidator>
                </span>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-primary btn-sm" OnClick="btnSearch_OnClick" ValidationGroup="vgFilter"><i class="glyphicon glyphicon-search"></i>&nbsp;найти</asp:LinkButton>
                <%--                <asp:LinkButton ID="btnSaveUserFilter" runat="server" class="btn btn-primary btn-sm" OnClick="btnSaveUserFilter_OnClick" ValidationGroup="vgFilter"><i class="fa fa-save"></i>&nbsp;сохранить</asp:LinkButton>--%>
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <div id="pnlNoData" runat="server"></div>
            <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>
            </h5>
            <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="False" AllowPaging="False" PageSize="30" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnDataBound="tblList_DataBound">
                <Columns>
                    <asp:BoundField DataField="id_claim" SortExpression="id_claim" HeaderText="ID" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />
                    <asp:TemplateField ItemStyle-CssClass="min-width" HeaderText="№ ServiceDesk" HeaderStyle-CssClass="sorted-header">
                        <ItemTemplate>
                            <div class="nowrap">
                                <%# Eval("service_desk_num") %>
                            </div>
                            <div class="nowrap">
                                <%# Eval("contractor_sd_num") %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="min-width nowrap" HeaderText="Оборудование">
                        <ItemTemplate>
                            <a runat="server" href='<%# GetRedirectUrlWithParams(String.Format("id={0}", Eval("id_claim")), false, DetailUrl) %>' class="btn btn-link">
                                <%#Eval("device") %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <%--            <asp:BoundField DataField="price_sum" SortExpression="price_sum" HeaderText="Стоимость ЗИП" HeaderStyle-CssClass="sorted-header" />--%>


                    <asp:BoundField DataField="zip_unit_count" SortExpression="zip_unit_count" HeaderText="Количество ЗИПов" HeaderStyle-CssClass="sorted-header" />
                    <asp:TemplateField ItemStyle-CssClass="min-width" HeaderText="Инженер Менеджер">
                        <ItemTemplate>
                            <div class="nowrap"><%# Eval("engeneer")  %></div>
                            <div class="nowrap"><%# Eval("manager")  %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Статус">
                        <ItemTemplate>
                            <%# Eval("claim_state") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="date_create" SortExpression="date_create" HeaderText="Дата создания" HeaderStyle-CssClass="sorted-header" ItemStyle-CssClass="min-width nowrap" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelecting="sdsList_Selecting">
                <SelectParameters>
                    <asp:Parameter DefaultValue="getClientZipViewClaimList" Name="action" />
                    <asp:Parameter Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="sdnum" Name="service_desk_num" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="csdnum" Name="contractor_sd_num" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="snum" Name="serial_num" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="mngr" Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="engr" Name="id_engeneer" DefaultValue='' ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="state" Name="lst_claim_states" DefaultValue="1,3,4,5,6,8,9,10,11,12,13" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="etste" Name="lst_et_claim_states" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="wayst" Name="lst_waybill_claim_states" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="DateTime" />
                    <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="DateTime" />
                    <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
                    <asp:QueryStringParameter QueryStringField="rcn" Name="rows_count" DefaultValue="30" ConvertEmptyStringToNull="True" />
                </SelectParameters>
            </asp:SqlDataSource>
</asp:Content>

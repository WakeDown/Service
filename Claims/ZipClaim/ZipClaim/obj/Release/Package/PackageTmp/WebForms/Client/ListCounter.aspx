<%@ Page Title="Личный кабинет" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="ListCounter.aspx.cs" Inherits="ZipClaim.WebForms.Client.ListCounter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
    <h4>
        <asp:Label ID="lblContractorName" runat="server" Text=""></asp:Label>
    </h4>
<%--    <asp:LinkButton ID="btnDownloadSettings" runat="server" OnClick="btnDownloadSettings_OnClick">Скачать настройки</asp:LinkButton>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlContract.ClientID %>' class="col-sm-2 control-label">Договор</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlContract" runat="server" CssClass="form-control input-sm">
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
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <h5>
        <span class="label label-primary">Всего аппаратов:
        <asp:Literal ID="lDevicesCount" runat="server" Text="0"></asp:Literal>
        </span>
        <span class="label label-info">из них показано:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>
    </h5>
    <div id="pnlNoData" runat="server"></div>
    <div id="pnlList" runat="server"></div>
<%--    <asp:PlaceHolder ID="phList" runat="server"></asp:PlaceHolder>--%>
    <%--<asp:Repeater ID="tblList" runat="server" DataSourceID="sdsList" OnLoad="tblList_OnLoad" OnItemDataBound="tblList_OnItemDataBound">
        <HeaderTemplate>
            <table class="table table-striped">
                <tr class="total-counter-row">
                    <td colspan="3" class="text-right bold">ВСЕГО</td>
                    <asp:Repeater ID="rtrClientTotalCounterMonthes" runat="server" OnItemDataBound="rtrClientTotalCounterMonthes_OnItemDataBound">
                        <ItemTemplate>
                            <asp:ListView ID="lvVolumeSum" runat="server">
                            <ItemTemplate>
                                <td class="bold text-right">
                                    <%# String.Format("{0:### ### ### ### ###}", Eval("volume_sum")) %>
                                </td>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <td class="bold">-</td>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        </ItemTemplate>
                    </asp:Repeater>
                    <td></td>
                </tr>
                <tr>
                    <th>&nbsp;</th>
                    <th>№ договора</th>
                    <th class="text-right curr-counter-col">Текущий</th>
                    <asp:Repeater ID="rtrClientCounterMonthes" runat="server">
                        <ItemTemplate>
                            <th class="text-right"><%# String.Format("{0:MMM yyyy}",Eval("Date")) %></th>
                        </ItemTemplate>
                    </asp:Repeater>
                    <th class="text-right total-counter-col">ИТОГО</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="min-width">
                    <div data-toggle="tooltip" title='<%#String.Format("{0}{3} {1}{4} {2}", Eval("city"), Eval("address"), Eval("object_name"), !String.IsNullOrEmpty(Eval("address").ToString()) ? "," : String.Empty, !String.IsNullOrEmpty(Eval("object_name").ToString()) ? "," : String.Empty) %>' class="nowrap">
                    <a runat="server" href='<%# GetRedirectUrlWithParams(String.Format("id={0}&cid={1}", Eval("id_device"), Eval("id_contract")), false, DetailUrl) %>' target="_blank" class="btn btn-link">
                        <%#Eval("device") %>
                    </a>
                        </div>
                </td>
                <td class="min-width nowrap">
                    <%# String.Format("{0:### ### ### ### ###}", Eval("contract_num")) %>
                </td>
                <td class="min-width nowrap text-right curr-counter-col">
                    <%#String.Format("{0:### ### ### ### ###}", Eval("last_counter")) %>
                </td>
                <asp:Repeater ID="rtrClientCounterMonthes" runat="server" OnItemDataBound="rtrClientCounterMonthes_OnItemDataBound">
                    <ItemTemplate>
                        <asp:ListView ID="lvDeviceCounter" runat="server">
                            <ItemTemplate>
                                <td class="text-right">
                                    <%# String.IsNullOrEmpty(Eval("volume").ToString()) ? "-" : String.Format("{0:### ### ### ### ###}", Eval("volume")) %>
                                </td>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <td class="text-right">-</td>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </ItemTemplate>
                </asp:Repeater>
                <td class="text-right bold">
                    <asp:Label ID="lVolRowTotal" runat="server" Text="0" CssClass="total-counter-col"></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsList_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getContract2DevicesList" Name="action" />
            <asp:Parameter Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="ctr" Name="id_contract" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="rcn" Name="rows_count" DefaultValue="50" ConvertEmptyStringToNull="True" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
</asp:Content>

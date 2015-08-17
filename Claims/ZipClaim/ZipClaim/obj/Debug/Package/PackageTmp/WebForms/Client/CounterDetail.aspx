<%@ Page Title="Личный кабинет" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="CounterDetail.aspx.cs" Inherits="ZipClaim.WebForms.Client.CounterDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
        <asp:Literal ID="lFormTitle" runat="server"></asp:Literal>
    <h5>
        <asp:Literal ID="lDeviceAddress" runat="server"></asp:Literal>
    </h5>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=rblDataSource.ClientID %>' class="col-sm-1 control-label">Источник</label>
            <div class="col-sm-4">
                <asp:RadioButtonList ID="rblDataSource" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst input-sm" AutoPostBack="True">
                    <asp:ListItem Text="все" Value="all" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="только UN1T Счетчик" Value="un1t_cnt"></asp:ListItem>
                    <asp:ListItem Text="только Инженер" Value="eng"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblRowsCount.ClientID %>' class="col-sm-1 control-label">Показывать записей</label>
            <div class="col-sm-2">
                <asp:RadioButtonList ID="rblRowsCount" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst input-sm" AutoPostBack="True">
                    <asp:ListItem Text="все" Value="0"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
    </div>
    <div class="clearfix"></div>
    <asp:Repeater ID="tblDeviceCounterHistory" runat="server" DataSourceID="sdsDeviceCounterHistory" OnItemDataBound="tblDeviceCounterHistory_OnItemDataBound">
        <HeaderTemplate>
            <table class="table table-striped text-smaller auto-width">
                <tr>
                    <th class="min-width">Дата
                    </th>
                    <th class="min-width text-right">Счетчик
                    </th>
                    <th class="text-right">Разница</th>
                    <th></th>
                    <th></th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="nowrap text-right"><%#String.Format("{0:d}", Eval("date_counter")) %></td>
                <td id="tdCounter" runat="server" class="nowrap text-right"><%# String.Format("{0:### ### ### ### ###}", Eval("counter")) %></td>
                <td id="tdDiff" runat="server" class="nowrap text-right"></td>
                <td class="nowrap"><%#Eval("name") %></td>
                <td class="nowrap">
                    <a id="fileLink" runat="server" target="_blank" href='<%# Eval("full_path") %>' visible='<%# !String.IsNullOrEmpty(Eval("id_akt_scan").ToString()) %>'>скан акта обслуживания №<%#Eval("id_akt_scan") %></a>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="sdsDeviceCounterHistory" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
        <SelectParameters>
            <asp:Parameter DefaultValue="getDeviceCounterHistory" Name="action" />
            <asp:QueryStringParameter QueryStringField="id" Name="id_device" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
            <asp:QueryStringParameter QueryStringField="cid" Name="id_contract" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />
            <asp:ControlParameter DefaultValue="" ControlID="rblRowsCount" Name="rows_count" />
            <asp:ControlParameter DefaultValue="" ControlID="rblDataSource" Name="data_source" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

<%@ Page Title="История заказа ЗИП" Language="C#" MasterPageFile="~/WebForms/Masters/Site.Master" AutoEventWireup="true" CodeBehind="ZipHistory.aspx.cs" Inherits="ZipClaim.WebForms.Claims.ZipHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="server">

    <%-- Основные кнопки управления --%>
    <div class="btn-toolbar " role="toolbar">
        <asp:LinkButton ID="btnAddClaim" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnAddClaim_Click">новая заявка</asp:LinkButton>
    </div>
    <hr class="smallmargin teeny" />
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtSerialNum" runat="server" class="form-control input-sm" MaxLength="50" AutoPostBack="True" OnTextChanged="txtSerialNum_OnTextChanged"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=rblFirstFif.ClientID %>' class="col-sm-2 control-label">Показать строк</label>
            <div class="col-sm-10">
                <asp:RadioButtonList ID="rblFirstFif" runat="server" RepeatDirection="Horizontal" CssClass="form-control chk-lst">
                    <asp:ListItem Text="первые 50" Value="-13"></asp:ListItem>
                    <asp:ListItem Text="все" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-primary btn-sm" OnClick="btnSearch_OnClick" ValidationGroup="vgFilter"><i class="glyphicon glyphicon-search"></i>&nbsp;найти</asp:LinkButton>
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlNoRows" runat="server" Visible="False">
     <blockquote class='bg-warning'>
                        <h5 class="text-warning">По вашему запросу ничего не найдено</h5>
                    </blockquote>
    </asp:Panel>
    <asp:GridView ID="tblClimUnitsHistory" runat="server" CssClass="table table-striped text-smaller" DataSourceID="sdsClimUnitsHistory" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="False" AllowPaging="False" PageSize="30" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" ShowFooter="True" ShowHeaderWhenEmpty="False">
        <Columns>
            <asp:BoundField DataField="row_num" SortExpression="row_num" HeaderText="№" HeaderStyle-CssClass="sorted-header" ItemStyle-CssClass="min-width nowrap" />
            <asp:BoundField DataField="catalog_num" SortExpression="catalog_num" HeaderText="Каталожный номер" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="name" SortExpression="name" HeaderText="Наименование" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="count" SortExpression="count" HeaderText="Количество" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="date_create" SortExpression="date_create" HeaderText="Дата создания заявки" HeaderStyle-CssClass="sorted-header" ItemStyle-CssClass="nowrap" />
            <asp:BoundField DataField="claim_state" SortExpression="claim_state" HeaderText="Статус заявки" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="counter" SortExpression="counter" HeaderText="Счетчик" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="counter_colour" SortExpression="counter_colour" HeaderText="Счетчик (цветной)" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="service_engeneer" SortExpression="service_engeneer" HeaderText="ФИО инженера" HeaderStyle-CssClass="sorted-header" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsClimUnitsHistory" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false" OnSelected="sdsClimUnitsHistory_OnSelected">
        <SelectParameters>
            <asp:Parameter DefaultValue="getClaimUnitHistoryList" Name="action" />
            <asp:QueryStringParameter QueryStringField="snum" Name="serial_num" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="all" Name="get_all" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int16" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

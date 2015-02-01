<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="PriceImport.aspx.cs" Inherits="ZipClaim.WebForms.Settings.Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
                <div class="form-group">
            <label for='<%=txtCatalogNum.ClientID %>' class="col-sm-2 control-label">ID</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtCatalogNum" runat="server" class="form-control input-sm" MaxLength="30"></asp:TextBox>
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
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <%--    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=FileUploader.ClientID %>' class="col-sm-2 control-label">Загрузка файла</label>
            <div class="col-sm-10">
                <asp:FileUpload ID="FileUploader" runat="server" />
                <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" />
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </div>
        </div>
    </div>--%>
    <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>
    </h5>
    <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnDataBound="tblList_DataBound">
        <Columns>
            <asp:BoundField DataField="catalog_num" SortExpression="catalog_num" HeaderText="Каталожный №" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header nowrap" />
            <asp:BoundField DataField="name" SortExpression="name" HeaderText="Наименование" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="price_usd" SortExpression="price_usd" HeaderText="Цена, USD" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="price_eur" SortExpression="price_eur" HeaderText="Цена, EUR" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="price_in" SortExpression="price_in" HeaderText="Цена вход, руб." ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="price_out" SortExpression="price_out" HeaderText="Цена выход, руб." ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="load_date" SortExpression="load_date" HeaderText="Дата загрузки" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" DataFormatString="{0:D}" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
        <SelectParameters>
            <asp:Parameter DefaultValue="getNomenclaturePrice" Name="action" />
             <asp:QueryStringParameter QueryStringField="catn" Name="catalog_num" DefaultValue="" ConvertEmptyStringToNull="True" />
             <asp:QueryStringParameter QueryStringField="rcn" Name="rows_count" DefaultValue="30" ConvertEmptyStringToNull="True" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

<%@ Page Title="Личный кабинет" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="ZipClaim.WebForms.Client.Detail" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    <asp:Literal ID="lFormTitle" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <asp:HiddenField ID="hfIdDevice" runat="server" />
    <asp:HiddenField ID="hfContractNumAmdDate" runat="server" />
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=txtServiceDeskNum.ClientID %>' class="col-sm-2 control-label">Номер заявки ServiceDesk UN1T</label>
            <div class="col-sm-10">
                <asp:Label ID="txtServiceDeskNum" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtSerialNum.ClientID %>' class="col-sm-2 control-label">Серийный номер</label>
            <div class="col-sm-10">
                <asp:Label ID="txtSerialNum" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDeviceModel.ClientID %>' class="col-sm-2 control-label">Модель</label>
            <div class="col-sm-10">
                <asp:Label ID="txtDeviceModel" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCity.ClientID %>' class="col-sm-2 control-label">Город/Нас. пункт</label>
            <div class="col-sm-10">
                <asp:Label ID="txtCity" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtAddress.ClientID %>' class="col-sm-2 control-label">Адрес</label>
            <div class="col-sm-10">
                <asp:Label ID="txtAddress" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtObjectName.ClientID %>' class="col-sm-2 control-label">Название объекта</label>
            <div class="col-sm-10">
                <asp:Label ID="txtObjectName" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div id="divContractNumber" runat="server" visible="False" class="form-group">
            <label for='<%=lblContractNumber.ClientID %>' class="col-sm-2 control-label">№ договора</label>
            <div class="col-sm-10">
                <asp:Label ID="lblContractNumber" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCounter.ClientID %>' class="col-sm-2 control-label">Счетчик</label>
            <div class="col-sm-10">
                <asp:Label ID="txtCounter" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtCounterColour.ClientID %>' class="col-sm-2 control-label">Счетчик (цветной)</label>
            <div class="col-sm-10">
                <asp:Label ID="txtCounterColour" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlEngeneerConclusion.ClientID %>' class="col-sm-2 control-label">Состояние оборудования</label>
            <div class="col-sm-10">
                <asp:Label ID="ddlEngeneerConclusion" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=ddlEngeneer.ClientID %>' class="col-sm-2 control-label">Инженер</label>
            <div class="col-sm-10">
                <asp:Label ID="ddlEngeneer" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <asp:HiddenField ID="hfIdManager" runat="server" />
        <div class="form-group">
            <label for='<%=ddlManager.ClientID %>' class="col-sm-2 control-label">Менеджер</label>
            <div class="col-sm-10">
                <asp:Label ID="ddlManager" runat="server" Text="" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <asp:HiddenField ID="hfManagerMail" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <%--<a type="button" class="btn btn-default btn-lg" data-toggle="tooltip" title="к списку заявок" href='<%=  FriendlyUrl.Href("~/Client") %>'><i class="fa fa-mail-reply"></i></a>--%>
                        <asp:LinkButton ID="btnShowPnlMail2Manager" runat="server" class="btn btn-danger btn-lg" OnClick="btnShowPnlMail2Manager_Click"><i class="fa fa-envelope"></i> сообщение менеджеру</asp:LinkButton>
                    </div>
                </div>
                <div id="pnlMail2Manager" runat="server" visible="False" class="form-group">
                    <div class="col-sm-10 col-sm-push-2">
                        <div class="input-group">
                            <asp:TextBox ID="txtMail2Manager" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" MaxLength="5000" placeholder="Введите сообщение для менеджера" noautocopml></asp:TextBox>
                            <div class="input-group-addon">
                                <asp:LinkButton ID="btnSendMail2Manager" runat="server" class="btn btn-primary btn-lg" data-toggle="tooltip" title="Отправить сообщение менеджеру" OnClick="btnSendMail2Manager_Click"><i class="fa fa-mail-forward"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Panel ID="pnlClaimUnits" runat="server">
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
                                        <asp:UpdatePanel ID="upClaimUnits" runat="server">
                                            <ContentTemplate>

                                                <asp:Label ID="lblShowLast" runat="server" Text="Показаны последние 50 записей"></asp:Label>
<%--                                                <asp:LinkButton ID="btnShowAll" runat="server" class="btn btn-link" OnClick="btnShowAll_Click">показать все</asp:LinkButton>--%>
                                                <asp:Repeater ID="rtrClimUnitsHistory" runat="server" DataSourceID="sdsClimUnitsHistory">
                                                    <HeaderTemplate>
                                                        <table class="table table-striped text-smaller">
                                                            <%--<tr>
                                                                <td colspan="9">
                                                                    
                                                                </td>
                                                            </tr>--%>
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
<%--                                                        <asp:ControlParameter DefaultValue="0" ControlID="hfShowAll" Name="get_all" />--%>
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
<%--                                                <asp:HiddenField ID="hfShowAll" runat="server" Value="0" />--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <asp:GridView ID="tblClaimUnitList" runat="server" CssClass="table table-striped no-margin-bottom" DataSourceID="sdsList" AutoGenerateColumns="false" PagerSettings-PageButtonCount="5" AllowSorting="False" AllowPaging="False" PageSize="30" PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast" PagerSettings-LastPageText="&lt;i class=&quot;fa fa-angle-double-right&quot;&gt;&lt;/&gt;" PagerSettings-FirstPageText="&lt;i class=&quot;fa fa-angle-double-left&quot;&gt;&lt;/&gt;" PagerSettings-NextPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" PagerSettings-PreviousPageText="&lt;i class=&quot;fa fa-angle-left&quot;&gt;&lt;/&gt;" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" ShowFooter="True" ShowHeaderWhenEmpty="True">
                        <Columns>
                            <%--                                    <asp:TemplateField SortExpression="id_claim_unit" HeaderText="ID">
                                        <ItemTemplate>
                                            <%#Eval("id_claim_unit") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:TemplateField SortExpression="catalog_num" HeaderText="Каталожный номер" ItemStyle-CssClass="nowrap min-width">
                                <ItemTemplate>
                                    <%#Eval("catalog_num") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="name" HeaderText="Наименование" ItemStyle-CssClass="nowrap min-width">
                                <ItemTemplate>
                                    <%#Eval("name") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="count" HeaderText="Количество" ItemStyle-CssClass="nowrap min-width">
                                <ItemTemplate>
                                    <%#Eval("count") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField SortExpression="price_out" HeaderText="Цена выход (за ед.)" FooterStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                        <ItemTemplate>
                                            <%#Eval("price_out") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="" HeaderText="Цена выход (сумма)" ItemStyle-CssClass="text-right">
                                        <ItemTemplate>
                                            <%#Eval("price_out_sum") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    &nbsp;
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" UpdateCommand="ui_zip_claims" UpdateCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="getClaimUnitList" Name="action" />
                            <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" />
                            <asp:Parameter Name="id_contractor" DefaultValue="" ConvertEmptyStringToNull="True" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <div class="col-sm-5 text-danger">
                        <asp:Literal ID="lLastClaim" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>
</asp:Content>

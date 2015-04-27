<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="Counters.aspx.cs" Inherits="ServicePlaningWebUI.WebForms.Reports.Counters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <div class="form-group">
            <label for='<%=ddlServiceManager.ClientID %>' class="col-sm-2 control-label">Менеджер</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlServiceManager" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for='<%=txtDateMonth.ClientID %>' class="col-sm-2 control-label">Месяц</label>
            <div class="col-sm-3">
                <div class="input-group">
                    <asp:TextBox ID="txtDateMonth" runat="server" CssClass="form-control datepicker-btn-month"></asp:TextBox>
                </div>
                <span class="help-block">
                    <asp:RequiredFieldValidator ID="rfvTxtPlaningDate" runat="server" ErrorMessage="Заполните поле &laquo;Месяц&raquo;" Display="Dynamic" ControlToValidate="txtDateMonth" InitialValue='' SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTxtPlaningDate" runat="server" ErrorMessage="Введите дату (месяц, год) в формате '01.2014'" ControlToValidate="txtDateMonth" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgFilter" ValidationExpression="(0?[1-9]|1[012]).((19|20)[0-9]{2})"></asp:RegularExpressionValidator>
                    <%--<asp:CompareValidator ID="cvTxtPlaningDate" runat="server" ErrorMessage="Введите дату" CssClass="text-danger" ControlToValidate="txtPlaningDate" Type="Date" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>--%>
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
    <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
    <div class="table">
        <div class="row row-total">
            <div class="col-sm-3 bold align-right">ИТОГО</div>
            <div class="col-sm-1 align-right"><span id="sDevCountTotal" runat="server"></span></div>
            <div class="col-sm-1 align-right"><span id="sCurVolTotal" runat="server"></span></div>
            <div class="col-sm-1 align-right"><span id="sPrevVolTotal" runat="server"></span></div>
            <div class="col-sm-1 align-right"><span id="sPrevPrevVolTotal" runat="server"></span></div>
            <div class="col-sm-1 align-right"></div>
            <div class="col-sm-1 align-right"></div>
            <div class="col-sm-1 align-right"></div>
            <div class="col-sm-1 align-right"></div>
            <div class="col-sm-1 align-right"></div>
        </div>
    </div>
    <asp:Repeater ID="tblCounterReport" runat="server" DataSourceID="sdsCounterReport" OnItemDataBound="tblCounterReport_OnItemDataBound" OnDataBinding="tblCounterReport_OnDataBinding">
        <HeaderTemplate>
            <div class="table table-striped">
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-1 bold align-right">Количество аппаратов</div>
                    <div class="col-sm-1 bold align-right">Объем<br /><%# GetCurMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right">Объем<br /><%# GetPrevMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right">Объем<br /><%# GetPrevPrevMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right">Загрузка<br />
                        <%# GetCurMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right">Загрузка<br />
                        <%# GetPrevMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right">Загрузка<br />
                        <%# GetPrevPrevMonth().ToString("MMM yyyy") %></div>
                    <div class="col-sm-1 bold align-right">Загрузка<br />
                        средняя</div>
                    <div class="col-sm-1 bold align-right">Текущий<br />
                        счетчик</div>
                </div>
        </HeaderTemplate>
        <ItemTemplate>
            <asp:HiddenField ID="hfIdContractor" runat="server" Value='<%#Eval("Id") %>' />
            <div class="row bg collapsed row-action" data-toggle="collapse" data-target='<%# String.Format("#contractList{0}", Container.ItemIndex) %>'>
                <div class="col-sm-3">
                    <%#Eval("name") %>
                </div>
                <div class="col-sm-1 align-right"><span id="sDevCount" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorCurVol" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorPrevVol" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorPrevPrevVol" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorCurLoading" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorPrevLoading" runat="server"></span></div>
                <div class="col-sm-1 align-right"><span id="sContractorPrevPrevLoading" runat="server"></span></div>
                <div class="col-sm-1"></div>
                <div class="col-sm-1"></div>
            </div>
            <div id='<%# String.Format("contractList{0}", Container.ItemIndex) %>' class="collapse">
                <asp:Repeater ID="rtrContractorContractList" runat="server" OnItemDataBound="rtrContractorContractList_OnItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfIdContract" runat="server" Value='<%#GetItem(Container.DataItem ,5) %>' />
                        <div class="row collapsed row-action" data-toggle="collapse" data-target='<%#String.Format("#deviceList{1}Ctr{0}", GetItem(Container.DataItem ,4), GetItem(Container.DataItem ,5)) %>'>
                            <div class="col-sm-3">
                                <span class="pad-l-sm"><%#GetItem(Container.DataItem ,0) %></span>
                            </div>
                            <div class="col-sm-1 align-right"><%# GetItem(Container.DataItem ,6) %></div>
                            <div class="col-sm-1 align-right"><span id="sCurVol" runat="server"></span></div>
                            <div class="col-sm-1 align-right"><span id="sPrevVol" runat="server"></span></div>
                            <div class="col-sm-1 align-right"><span id="sPrevPrevVol" runat="server"></span></div>
                            <div class="col-sm-1 align-right"><span id="sCurLoading" runat="server"></span></div>
                            <div class="col-sm-1 align-right"><span id="sPrevLoading" runat="server"></span></div>
                            <div class="col-sm-1 align-right"><span id="sPrevPrevLoading" runat="server"></span></div>
                            <div class="col-sm-1"></div>
                            <div class="col-sm-1"></div>
                        </div>
                        <div id='<%# String.Format("deviceList{1}Ctr{0}", GetItem(Container.DataItem ,4), GetItem(Container.DataItem ,5)) %>' class="collapse">
                            <div id="phContractorContractsDevicesList" runat="server"></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="sdsCounterReport" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_service_planing" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
        <SelectParameters>
            <asp:Parameter DefaultValue="getCounterReportContractorList" Name="action" />
            <asp:QueryStringParameter QueryStringField="mgr" Name="id_manager" DefaultValue="" ConvertEmptyStringToNull="True" />
            <asp:QueryStringParameter QueryStringField="mth" Name="date_month" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Date" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

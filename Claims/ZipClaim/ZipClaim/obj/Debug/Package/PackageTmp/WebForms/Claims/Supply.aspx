<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/Masters/List.master" AutoEventWireup="true" CodeBehind="Supply.aspx.cs" Inherits="ZipClaim.WebForms.Claims.Supply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphControlButtons" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFilterBody" runat="server">
    <div class="form-horizontal val-form" role="form">
        <%--        <div class="form-group">
            <label for='<%=txtId.ClientID %>' class="col-sm-2 control-label">ID</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtId" runat="server" class="form-control input-sm" MaxLength="20"></asp:TextBox>
            </div>
        </div>--%>
        <div class="form-group">
            <label for='<%=ddlSupplyMan.ClientID %>' class="col-sm-2 control-label">Снабженец</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="ddlSupplyMan" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
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
        <%--        <div class="form-group">
            <label for='<%=txtDateBegin.ClientID %>' class="col-sm-2 control-label">Период установки цены</label>
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
        </div>--%>
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
                <%--                <asp:LinkButton ID="btnSaveUserFilter" runat="server" class="btn btn-primary btn-sm" OnClick="btnSaveUserFilter_OnClick" ValidationGroup="vgFilter"><i class="fa fa-save"></i>&nbsp;сохранить</asp:LinkButton>
                <a type="button" class="btn btn-default btn-sm" href='javascript:void(0)' onclick="FilterClear();"><i class="glyphicon glyphicon-repeat"></i>&nbsp;очистить</a>--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphList" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
            <h5><span class="label label-default">Показано записей:
        <asp:Literal ID="lRowsCount" runat="server" Text="0"></asp:Literal></span>
            </h5>
            <asp:GridView ID="tblList" runat="server" CssClass="table table-striped" DataSourceID="sdsList" AutoGenerateColumns="false" GridLines="None" SortedAscendingHeaderStyle-CssClass="header-asc" SortedDescendingHeaderStyle-CssClass="header-desc" OnDataBound="tblList_DataBound">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="min-width" FooterStyle-CssClass="min-width" HeaderStyle-CssClass="middle">
                        <%--                <EditItemTemplate>
                    <asp:LinkButton ID="btnSave" runat="server" CommandName="Update" CommandArgument='<%#Eval("id_claim_unit") %>' CssClass="btn btn-link" data-toggle="tooltip" title="сохранить"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:HiddenField ID="hfIdClaimUnit" runat="server" Value='<%#Eval("id_claim_unit") %>' />
                            <asp:HiddenField ID="hfIdClaim" runat="server" Value='<%#Eval("id_claim") %>' />
                            <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_OnClick" CommandArgument='<%#Eval("id_claim_unit") %>' CssClass="btn btn-link" data-toggle="tooltip" title="отмена" Visible="False"><i class="fa fa-times fa-lg"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_OnClick" CommandArgument='<%#Eval("id_claim_unit") %>' CssClass="btn btn-link" data-toggle="tooltip" title="изменить" Visible='<%# UserIsSupplyMan || UserIsSysAdmin %>'><i class="fa fa-edit fa-lg"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="id_claim" SortExpression="id_claim" HeaderText="ID" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />
                    <asp:BoundField DataField="date_price_request" SortExpression="date_price_request" HeaderText="Дата передачи" HeaderStyle-CssClass="sorted-header" />
                    <asp:BoundField DataField="catalog_num" SortExpression="catalog_num" HeaderText="Каталожный №" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
                    <asp:TemplateField ItemStyle-CssClass="text-right" HeaderText="Номенклатурный №">
                        <ItemTemplate>
                            <asp:Label ID="lblNomenclatureNum" runat="server" Text='<%#Eval("nomenclature_num") %>'></asp:Label>
                            <asp:HiddenField ID="hfNoNomenclatureNum" runat="server" Value='<%#Eval("no_nomenclature_num") %>' />

                            <asp:TextBox ID="txtNomenclatureNum" runat="server" CssClass="txt-zip-nomnum" Text='<%#Bind("nomenclature_num") %>' Visible="False"></asp:TextBox>
                            <div>
                                <asp:Label ID="lblNomenclatureClaimNum" CssClass="nomenclature-claim-num" runat="server" Text='<%#Eval("nomenclature_claim_num") %>'></asp:Label>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="rfvTxtNomenclatureNum" runat="server" ErrorMessage="Укажите номер" ControlToValidate="txtNomenclatureNum" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgSupply" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="name" SortExpression="name" HeaderText="Наименование" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
                    <asp:BoundField DataField="device" SortExpression="device" HeaderText="Модель аппарата" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
                    <asp:BoundField DataField="manager" SortExpression="manager" HeaderText="Менеджер" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
                    <asp:TemplateField ItemStyle-CssClass="text-right" HeaderText="Цена вход (за ед.)">
                        <ItemTemplate>
                            <asp:Label ID="lblPriceIn" runat="server" Text='<%#Eval("price_in") %>'></asp:Label>
                            <asp:TextBox ID="txtPriceIn" runat="server" CssClass="txt-zip-price" Text='<%#Bind("price_in", "{0:D}") %>' Visible="False"></asp:TextBox>
                            <div>
                                <asp:CompareValidator ID="cvTxtPriceIn" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtPriceIn" Type="Currency" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" Enabled="False" ValidationGroup="vgSupply"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="rfvTxtPriceIn" runat="server" ErrorMessage="Укажите цену" ControlToValidate="txtPriceIn" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgSupply" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="" HeaderText="Срок поставки">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryTime" runat="server" Text='<%#Eval("delivery_time") %>'></asp:Label>
                            <asp:TextBox ID="txtDeliveryTime" runat="server" Text='<%#Bind("delivery_time") %>' CssClass="txt-zip-deliv" noautocopml Visible="False"></asp:TextBox>
                            <div>
                                <asp:RequiredFieldValidator ID="rfvTxtDeliveryTime" runat="server" ErrorMessage="Укажите срок поставки" ControlToValidate="txtDeliveryTime" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgSupply" Enabled="false"></asp:RequiredFieldValidator>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:BoundField DataField="price_in" SortExpression="price_in" HeaderText="Цена" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />
            <asp:BoundField DataField="delivery_time" SortExpression="delivery_time" HeaderText="Срок" ItemStyle-CssClass="min-width bold" HeaderStyle-CssClass="sorted-header" />--%>
                    <%--                    <asp:BoundField DataField="claim_state" SortExpression="claim_state" HeaderText="Статус" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />
                    <asp:BoundField DataField="supply_man" SortExpression="supply_man" HeaderText="ФИО снабженца" ItemStyle-CssClass="" HeaderStyle-CssClass="sorted-header" />--%>
                    <asp:TemplateField ItemStyle-CssClass="min-width" FooterStyle-CssClass="min-width" HeaderStyle-CssClass="middle">
                        <%--                <EditItemTemplate>
                    <asp:LinkButton ID="btnSave" runat="server" CommandName="Update" CommandArgument='<%#Eval("id_claim_unit") %>' CssClass="btn btn-link" data-toggle="tooltip" title="сохранить"><i class="fa fa-save fa-lg"></i></asp:LinkButton>
                </EditItemTemplate>--%>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_OnClick" CommandArgument='<%#Eval("id_claim_unit") %>' CssClass="btn btn-link" data-toggle="tooltip" title="отправить" Visible="False" ValidationGroup="vgSupply"><i class="fa fa-reply fa-lg"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnReturn" runat="server" CssClass="btn btn-link" OnClick="btnReturn_OnClick" data-toggle="tooltip" title="вернуть" Visible='<%# UserIsSupplyMan || UserIsSysAdmin %>'><i class="fa fa-recycle fa-lg"></i></asp:LinkButton>
                            <asp:Panel ID="pnlReturnDescr" runat="server" Visible="false">
                                <div class="supply-return-pnl">
                                    <asp:TextBox ID="txtReturnDescr" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Комментарий к возврату"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTxtReturnDescr" runat="server" ErrorMessage="Укажите причину отклонения позиции" ControlToValidate="txtReturnDescr" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger" ValidationGroup="vgCancel"></asp:RequiredFieldValidator>
                                    <div>
                                        <asp:LinkButton ID="btnSendReturn" runat="server" CssClass="btn btn-success" Text="Вернуть" OnClick="btnSendReturn_OnClick" CommandArgument='<%#Eval("id_claim_unit") %>' ValidationGroup="vgCancel" />
                                        <asp:LinkButton ID="btnReturnCancel" runat="server" CssClass="btn btn-danger" Text="Отмена" OnClick="btnReturnCancel_OnClick" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="sdsList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                <SelectParameters>
                    <asp:Parameter DefaultValue="getClaimUnitCommonList" Name="action" />
                    <asp:Parameter DefaultValue="1" Name="supply_list" />
                    <asp:QueryStringParameter QueryStringField="mgr" Name="id_manager" DefaultValue='' ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="supm" Name="id_resp_supply" DefaultValue='' ConvertEmptyStringToNull="True" />
                    <%--                    <asp:QueryStringParameter QueryStringField="state" Name="lst_claim_states" DefaultValue="" ConvertEmptyStringToNull="True" />
                    <asp:QueryStringParameter QueryStringField="dst" Name="date_begin" DefaultValue="" ConvertEmptyStringToNull="True" DbType="DateTime" />
                    <asp:QueryStringParameter QueryStringField="den" Name="date_end" DefaultValue="" ConvertEmptyStringToNull="True" DbType="DateTime" />
                    <asp:QueryStringParameter QueryStringField="id" Name="id_claim" DefaultValue="" ConvertEmptyStringToNull="True" DbType="Int32" />--%>
                    <asp:QueryStringParameter QueryStringField="rcn" Name="rows_count" DefaultValue="30" ConvertEmptyStringToNull="True" />
                </SelectParameters>
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
        CancelControlID="btnReturnCancel" OkControlID="btnSendReturn"
        TargetControlID="btnFakePopup" PopupControlID="pnlReturnDescr"
        BackgroundCssClass="ModalPopupBG">
    </asp:ModalPopupExtender>--%>
    <%--    <asp:Button ID="btnFakePopup" runat="server" Text="test" />--%>
</asp:Content>

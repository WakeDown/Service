<%@ Page Title="Настройка - ТОП" Language="C#" MasterPageFile="~/WebForms/Masters/Editor.master" AutoEventWireup="true" CodeBehind="ZipGroups.aspx.cs" Inherits="ZipClaim.WebForms.Settings.ZipGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphFormTitle" runat="server">
    Настройка групп для ТОП
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphFormBody" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfIdZipGroup" runat="server" />
            <div>
                <asp:PlaceHolder ID="phServerMessage" runat="server"></asp:PlaceHolder>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <div class="input-group full-width">
                        <asp:TextBox ID="txtZipGroupName" runat="server" CssClass="form-control" MaxLength="50" placeholder="Название группы"></asp:TextBox>
                        <span class="input-group-btn width-120px">
                            <asp:TextBox ID="txtZipGroupColour" runat="server" CssClass="form-control" MaxLength="6" placeholder="Цвет 000FFF"></asp:TextBox>
                        </span>
                        <span class="input-group-btn width-80px">
                            <asp:TextBox ID="txtZipGroupOrderNum" runat="server" CssClass="form-control" MaxLength="2" placeholder="Порядок"></asp:TextBox>
                        </span>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="btnZipGroupAdd" runat="server" OnClick="btnZipGroupAdd_Click" class="btn btn-primary" data-toggle="tooltip" title="добавить группу" ValidationGroup="vgForm"><i class="fa fa-plus"></i></asp:LinkButton>
                        </span>
                    </div>
                    <div class="help-block">
                        <asp:RequiredFieldValidator ID="rfvTxtZipGroupName" runat="server" ErrorMessage="Заполните поле &laquo;Название группы&raquo;" ControlToValidate="txtZipGroupName" Display="Dynamic" CssClass="text-danger" SetFocusOnError="True" ValidationGroup="vgForm"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvTxtZipGroupOrderNum" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtZipGroupOrderNum" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgForm"></asp:CompareValidator>
                    </div>
                    <%--<asp:ListBox ID="lbZipGroupList" runat="server" CssClass="form-control"></asp:ListBox>--%>
                    <%-- <asp:Repeater ID="rtrZipGroupList" runat="server" DataSourceID="sdsZipGroupList">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("row_num") %></td>
                        <td><%#Eval("name") %>
                            <asp:HiddenField ID="idZipGroup" runat="server" Value='<%#Eval("id_zip_group") %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_zip_group") %>' OnClientClick="return DeleteConfirm('группу ЗИПов')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o fa-lg"></i></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>--%>
                    <asp:GridView ID="tblZipGroupList" runat="server" CssClass="table" DataSourceID="sdsZipGroupList" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="tblZipGroupList_RowDataBound" ShowHeader="False">
                        <Columns>
                            <asp:BoundField DataField="row_num" SortExpression="row_num" ItemStyle-CssClass="min-width" />
                            <asp:TemplateField ItemStyle-CssClass="min-width">
                                <ItemTemplate>
                                    <div class="colour-mark" style='<%# String.Format("background-color: #{0}", Eval("colour"))%>'>&nbsp;</div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfIdZipGroup" runat="server" Value='<%# Eval("id_zip_group") %>' />
                                    <%--<a href='<%# GetRedirectUrlWithParams(String.Format("{1}={0}", Eval("id_zip_group"), qsKeyZipGroup)) %>' class="zip-grp-lnk"><%#Eval("name") %></a>--%>
                                    <asp:LinkButton ID="btnSelectGroup" runat="server" OnClick="btnSelectGroup_OnClick" CommandArgument='<%#Eval("id_zip_group") %>' CssClass="btn btn-link zip-grp-lnk"><%#Eval("name") %></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="min-width">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtZipGroupColour" runat="server" CssClass="form-control width-120px" MaxLength="6" Text='<%#Eval("colour") %>' OnTextChanged="txtZipGroupColour_OnTextChanged" AutoPostBack="True"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="min-width">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtZipGroupOrderNum" runat="server" CssClass="form-control width-80px" MaxLength="3" Text='<%#Eval("order_num") %>' OnTextChanged="txtZipGroupOrderNum_OnTextChanged" AutoPostBack="True" ValidationGroup="vgZipGroup"></asp:TextBox>
                                    <asp:CompareValidator ID="cvTxtZipGroupOrderNum" runat="server" ErrorMessage="Введите число" CssClass="text-danger" ControlToValidate="txtZipGroupOrderNum" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" SetFocusOnError="True" ValidationGroup="vgZipGroup"></asp:CompareValidator>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="min-width">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDeleteGroup" runat="server" OnClick="btnDeleteGroup_OnClick" CommandArgument='<%#Eval("id_zip_group") %>' OnClientClick="return DeleteConfirm('группу ЗИПов')" CssClass="btn btn-link" data-toggle="tooltip" title="удалить"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="sdsZipGroupList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="getZipGroupList" Name="action" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
                <div class="col-sm-6">
                    <div class="form-control">
                        <div class="col-sm-6">
                            <asp:CheckBox ID="chkOnlySelected" runat="server" AutoPostBack="True" />&nbsp;только отмеченные
                        </div>
                        <div class="col-sm-6">
                            <asp:CheckBox ID="chkOnlyFree" runat="server" AutoPostBack="True" />&nbsp;только свободные
                        </div>
                    </div>
                    <div class="help-block">
                    </div>
                    <asp:Repeater ID="rtrZipOftSelList" runat="server" DataSourceID="sdsZipOftSelList">
                        <HeaderTemplate>
                            <table class="table">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class='<%# Eval("show_selected").ToString().Equals("1") && IdZipGroup > 0 ?  IdZipGroup.ToString() == Eval("id_zip_group").ToString() ? "zip-grp-current" : "zip-grp-other" : "zip-grp-nomark" %>'>
                                <td>
                                    <asp:LinkButton ID="btnAdd" runat="server" Visible='<%# Eval("show_add").ToString().Equals("1") && IdZipGroup > 0 %>' OnClick="btnAdd_OnClick" CommandArgument='<%#Eval("catalog_num") %>' CssClass="btn btn-link" data-toggle="tooltip" title="привязать"><i class="fa fa-plus"></i></asp:LinkButton>
                                </td>
                                <td><%#Eval("row_num") %></td>
                                <td><div class="pull-left"><%#Eval("catalog_num") %></div>
                                <%--</td>
                                <td>--%>
                                    <div class="pull-right">
                                    <%#Eval("name") %></div>
                                    <div class="clearfix"></div>
                                    <div class="pull-left">
                                        <asp:TextBox ID="txtClaimUnitComment" runat="server" CssClass="form-control" MaxLength="50" Text='<%#Eval("comment") %>' OnTextChanged="txtClaimUnitComment_OnTextChanged" AutoPostBack="True" cat_num='<%#Eval("catalog_num") %>'></asp:TextBox>
                                    </div>
                                    <div class="h6 nomargin pull-right paddingsm" style='<%# String.Format("background-color: #{0}", Eval("zip_group_color"))%>'><%#Eval("zip_group") %></div>
                                    <%--                            <asp:HiddenField ID="idZipGroup" runat="server" Value='<%#Eval("id_zip_group") %>' />--%>
                                </td>
                                <td><%#Eval("cnt") %></td>
                                <td>
                                    <asp:LinkButton ID="btnDelete" runat="server" Visible='<%# Eval("show_delete").ToString().Equals("1") && IdZipGroup > 0 %>' OnClick="btnDelete_OnClick" CommandArgument='<%#Eval("id_zip_group2cat_num") %>' OnClientClick="return DeleteConfirm('ЗИП из группы')" CssClass="btn btn-link" data-toggle="tooltip" title="исключить"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <asp:SqlDataSource ID="sdsZipOftSelList" runat="server" ConnectionString="<%$ ConnectionStrings:unitConnectionString %>" SelectCommand="ui_zip_claims" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="getOftenSelectedList" Name="action" />
                            <asp:Parameter DefaultValue="1" Name="get_all" />
                            <asp:ControlParameter ControlID="chkOnlySelected" Name="in_group" />
                            <asp:ControlParameter ControlID="chkOnlyFree" Name="no_group" />
                            <asp:ControlParameter ControlID="hfIdZipGroup" Name="id_zip_group" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

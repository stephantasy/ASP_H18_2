<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tp2.aspx.vb" Inherits="Tp2" %>
        
<!-- 
    IFT1148 - TP2
    Stéphane Barthélemy
    20084771 - barthste
 -->

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IFT1148 - TP2</title>
    <!-- Javascript (fichier externe) -->	
    <script type="text/javascript" src="javascript.js?v=2"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Bordure noire autour de la page -->
        <div style="padding: 5px; border-style: solid; border-width: 1px;">
           
            <!-- Titre -->
            <asp:Table ID="Tb_Titre" runat="server"  Width="100%" BackColor="#FFCC66" CellPadding="20">
                <asp:TableRow ID="TbTitreRow1" runat="server">
                    <asp:TableCell ID="TbTitreCell1" runat="server" HorizontalAlign="Center" VerticalAlign="Middle">
                        <asp:Label ID="Lb_TitrePage" runat="server" Font-Bold="True" Text="Travail pratique #2" Font-Size="XX-Large"></asp:Label><br />
                        <asp:Label ID="Lb_SousTitrePage" runat="server" Font-Bold="True" Text="Calcul de la note finale pour le cours IFT1148" Font-Size="X-Large"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br />
            
            <!-- Nom de l'étudiant -->            
            <div style="padding: 5px;">
                <asp:Label ID="Lb_NomEtudiant" runat="server" Text="Nom de l'étudiant : "></asp:Label>
                <asp:TextBox ID="Tb_NomEtudiant" runat="server" style="vertical-align:middle" Width="200px" ToolTip="Entrez votre nom"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Rfv_NomEtudiant" runat="server" ErrorMessage="Veuillez entrer le nom de l'étudiant" ControlToValidate="Tb_NomEtudiant" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>
            <br />

            <!-- Table contenant les champs pour entrer les notes -->
            <!-- (5 lignes avec 3 cellules) -->
            <!-- Chaque bloc contient un Label pour la désignation, un TextBox pour entrer la valeur, un Label pour la note max. et 2 validations (Required et Custom) -->
            <table>
                <!-- TP1 -->
                <tr>
                    <td>
                        <asp:Label ID="Lb_NameTp1" runat="server" Text="TP 1 : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Tb_NoteTp1" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Lb_NoteMax1" runat="server" Text="" >/ <%= TP1_NOTE_MAX %></asp:Label>
                        <asp:RequiredFieldValidator ID="Rfv_NoteTp1" runat="server" ErrorMessage="Veuillez entrer une note" ControlToValidate="Tb_NoteTp1" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="Cv_NoteTp1" runat="server" ControlToValidate="Tb_NoteTp1" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True" OnServerValidate="CustomServerValidation" ClientValidationFunction="customClientValidation"></asp:CustomValidator>
                    </td>
                </tr>
                <!-- TP2 -->
                <tr>
                    <td>
                        <asp:Label ID="Lb_NameTp2" runat="server" Text="TP 2 : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Tb_NoteTp2" runat="server"></asp:TextBox>                        
                    </td>
                    <td>
                        <asp:Label ID="Lb_NoteMax2" runat="server" Text="">/ <%= TP2_NOTE_MAX %></asp:Label>
                        <asp:RequiredFieldValidator ID="Rfv_NoteTp2" runat="server" ErrorMessage="Veuillez entrer une note" ControlToValidate="Tb_NoteTp2" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="Cv_NoteTp2" runat="server" ControlToValidate="Tb_NoteTp2" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True" OnServerValidate="CustomServerValidation" ClientValidationFunction="customClientValidation"></asp:CustomValidator>
                    </td>
                </tr>
                <!-- TP3 -->
                <tr>
                    <td>
                        <asp:Label ID="Lb_NameTp3" runat="server" Text="TP 3 : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Tb_NoteTp3" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Lb_NoteMax3" runat="server" Text="">/ <%= TP3_NOTE_MAX %></asp:Label>
                        <asp:RequiredFieldValidator ID="Rfv_NoteTp3" runat="server" ErrorMessage="Veuillez entrer une note" ControlToValidate="Tb_NoteTp3" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="Cv_NoteTp3" runat="server" ControlToValidate="Tb_NoteTp3" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True" OnServerValidate="CustomServerValidation" ClientValidationFunction="customClientValidation"></asp:CustomValidator>
                    </td>
                </tr>
                <!-- Examen Intra -->
                <tr>
                    <td>
                        <asp:Label ID="Lb_NameIntra" runat="server" Text="Examen intra : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Tb_NoteIntra" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Lb_NoteMaxIntra" runat="server" Text="">/ <%= INTRA_NOTE_MAX %> (compte pour <%= INTRA_NOTE_PERCENT %>)</asp:Label>
                        <asp:RequiredFieldValidator ID="Rfv_NoteIntra" runat="server" ErrorMessage="Veuillez entrer une note" ControlToValidate="Tb_NoteIntra" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="Cv_NoteIntra" runat="server" ControlToValidate="Tb_NoteIntra" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True" OnServerValidate="CustomServerValidation" ClientValidationFunction="customClientValidation"></asp:CustomValidator>
                    </td>
                </tr>
                <!-- Examen Final -->
                <tr>
                    <td>
                        <asp:Label ID="Lb_NameFinal" runat="server" Text="Examen final : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Tb_NoteFinal" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="Lb_NoteMaxFinal" runat="server" Text="">/ <%= FINAL_NOTE_MAX %> (compte pour <%= FINAL_NOTE_PERCENT %>)</asp:Label>
                        <asp:RequiredFieldValidator ID="Rfv_NoteFinal" runat="server" ErrorMessage="Veuillez entrer une note" ControlToValidate="Tb_NoteFinal" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="Cv_NoteFinal" runat="server" ControlToValidate="Tb_NoteFinal" Display="Dynamic" ForeColor="#CC0000" SetFocusOnError="True" OnServerValidate="CustomServerValidation" ClientValidationFunction="customClientValidation"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
            
            <!-- Bouton -->             
            <br />
            <asp:Button ID="Bt_Calculer" runat="server" Text="Calculer" OnClick="CalculerResultats" />
            <br />

            <!-- Résultats des contrôles (TP et examens) -->
            <br />
            <asp:Label ID="Lb_ResultNotes" runat="server" Text=""></asp:Label>
            <br />

            <!-- Résultats du cours -->
            <asp:Literal ID="Lt_ResultCours" runat="server" Text=""></asp:Literal>
            <asp:Label ID="Lb_Seuil" runat="server" Text="" ForeColor="Red"></asp:Label>
                        
        </div>
    </form>    
    <p>Par Stéphane Barthélemy, BARS11017704</p>
</body>
</html>
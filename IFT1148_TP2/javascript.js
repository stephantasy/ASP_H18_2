/*
   IFT1148 -TP2
   Stéphane Barthélemy
   20084771 - barthste
*/


/* ===== VALIDATION CLIENT ===== */
	

//Constantes
var TP1_NOTE_MAX = 10;
var TP2_NOTE_MAX = 15;
var TP3_NOTE_MAX = 15;
var INTRA_NOTE_MAX = 100;
var FINAL_NOTE_MAX = 100;

//Messages d'erreurs
var ERROR_MESSAGE_NOTE_DEFAUT = "Veuillez entrer une note valide";
var ERROR_MESSAGE_NOTE_RANGE = "La note doit être dans l'interval prévue";
var ERROR_MESSAGE_R_FINAL = "Le code R ne peut pas être utilisée ici";

// Validation Client des champs
function customClientValidation(source, args){
    	
    var RawValue = args.Value;                      // Valeur brute du champ
    var NumberValue = parseFloat(args.Value);       // Valeur numérique du champ
    var textBoxTraite = source.controltovalidate;   // Champ traité

    //Non valide par défaut
    args.IsValid = false;

    //Est-ce un "R"
    if (RawValue.toUpperCase() == "R"){
        // Sauf pour l'examen final
        if (textBoxTraite == document.getElementById("Tb_NoteFinal").id) {
            source.innerHTML  = ERROR_MESSAGE_R_FINAL;
        }else{
            //Sinon, c'est valide
            args.IsValid = true;
        }
        return;
    }

    // Est-ce un nombre ?
    if(isNaN(NumberValue)){
        //Ce n'est pas un nombre
        source.innerHTML  = ERROR_MESSAGE_NOTE_DEFAUT;
    }else{

        //Le nombre est dans le bon interval
        if (isInRange(textBoxTraite, NumberValue)) {
            //Validation Ok
            args.IsValid = true;
        }else{
            //Hors interval
            source.innerHTML  = ERROR_MESSAGE_NOTE_RANGE;
        }
    }
}

//Renvoie si la note est dans l'interval prévu
function isInRange(textBoxId, value){
    switch(textBoxId){
        case "Tb_NoteTp1":
            return value >= 0 && value <= TP1_NOTE_MAX;
        case "Tb_NoteTp2":
            return value >= 0 && value <= TP2_NOTE_MAX;
        case "Tb_NoteTp3":
            return value >= 0 && value <= TP3_NOTE_MAX;
        case "Tb_NoteIntra":
            return value >= 0 && value <= INTRA_NOTE_MAX;
        case "Tb_NoteFinal":
            return value >= 0 && value <= FINAL_NOTE_MAX;
        default:
            return;
    }
}
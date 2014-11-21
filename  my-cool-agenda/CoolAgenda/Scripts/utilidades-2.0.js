/**
* Tem dependencia com <jQuery 1.10.2> e <jQuery UI 1.10.4> e <Boostrap> e <jquery-toastmessage-plugin> 
*/


// Mostra uma caixa de dialogo com titulo, mensagem, modal personalizado
function JSMostraDialog(stringTitulo, stringMensagem, boolModal) {
    $("body").append("<div id='dialogError' title='" + stringTitulo + "' hidden ><div id='dialogErrorMensagem'> </div></div>'");
    $("#dialogErrorMensagem").html(stringMensagem);
    $("#dialogError").dialog({ modal: boolModal });

}



// Mostra uma caixa de dialogo com confirmação e executa o callback passado
function JSMostraDialogConfirmacao(stringTitulo, stringMensagem, mostraModal, callback) {
    var $dialogConfirmacao = $('<div></div>')
       .html(stringMensagem)
       .dialog({
           autoOpen: true,
           modal: mostraModal,
           title: stringTitulo,
           buttons: {
               "OK": function () {
                   $(this).dialog("close");
                   callback();
               },
               "Cancel": function () {
                   $(this).dialog("close");
               }
           }
       });
}


// Mostra uma notificação de sucesso
// stringMensagem: Mensagem que será exibida
// boolPersistir: indica se a notificação ficará fixa até o usuário fechá-la por um botão
// fnCallback: função que será chamada quando a notificação fechar
function JSMostraNotificacaoSucesso(stringMensagem, boolPersistir, fnCallback) {
    $().toastmessage('showToast', {
        text: stringMensagem,
        sticky: boolPersistir,
        type: 'success',
        close: fnCallback
    });
}

// Mostra uma notificação de erro
// stringMensagem: Mensagem que será exibida
// boolPersistir: indica se a notificação ficará fixa até o usuário fechá-la por um botão
// fnCallback: função que será chamada quando a notificação fechar
function JSMostraNotificacaoErro(stringMensagem, boolPersistir, fnCallback) {
    $().toastmessage('showToast', {
        text: stringMensagem,
        sticky: boolPersistir,
        type: 'error',
        close: fnCallback
    });
}

// Mostra uma notificação de erro
// stringMensagem: Mensagem que será exibida
// boolPersistir: indica se a notificação ficará fixa até o usuário fechá-la por um botão
function JSMostraNotificacaoErro(stringMensagem, boolPersistir) {
    $().toastmessage('showToast', {
        text: stringMensagem,
        sticky: boolPersistir,
        type: 'error',
    });
}

// Mostra uma notificação de informação
// stringMensagem: Mensagem que será exibida
// boolPersistir: indica se a notificação ficará fixa até o usuário fechá-la por um botão
// fnCallback: função que será chamada quando a notificação fechar
function JSMostraNotificacaoInfo(stringMensagem, boolPersistir, fnCallback) {
    $().toastmessage('showToast', {
        text: stringMensagem,
        sticky: boolPersistir,
        type: 'notice',
        close: fnCallback
    });
}



$(document).ready(function () {

    // Componente do jQuery UI para possibilitar a escolha de uma data a partir de um calendário
    // Está configurado no idioma PT_BR
    $(".datepicker-pt-br").datepicker({
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior'
    });


    // Configuração para exibir o loading do ajax
    $(document).ajaxStart(function () {
        $("#carregamento").show();
    });

    $(document).ajaxComplete(function () {
        $("#carregamento").hide()
    });
});



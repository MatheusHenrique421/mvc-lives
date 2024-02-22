$(document).ready(function () {
    $('#cmbLiveNome').change(function () {
        var selectedLiveID = $(this).val();
        if (selectedLiveID) {
            console.log(selectedLiveID);
            // Fazer uma requisição AJAX para obter o valor da inscrição com base no LiveID
            $.ajax({
                url: '/Inscricao/GetValorInscricao',
                type: 'GET',
                data: { liveID: selectedLiveID },
                success: function (data) {
                    $('#txtLiveValorInscricao').val(data);
                },
                error: function () {
                    console.error('Erro ao obter o valor da inscrição.');
                }
            });
        } else {
            $('#txtLiveValorInscricao').val('');
        }
    });
});
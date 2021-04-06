function SendMail() {
    var GetId = document.getElementsByName("Email")[0].value;
    var GetHtml = document.getElementById("SendMail").innerHTML;
    $.ajax({
        url: "/DomainUser/DoneResults",
        type: "Post",
        data: {
            Mail: GetId,
            İcerik: GetHtml
        },
        beforeSend: function(aa) {
            Swal.fire({
                title: "Gönderiliyor",
                imageUrl: "/loading.gif",
                imageWidth: 100,
                imageHeight: 100,
                allowEscapeKey: false,
                allowOutsideClick: false,
                showConfirmButton: false
            });
        },
        complete: function(aa) {
            hideSwall();
            Swal.fire({
                title: "Mail Gönderildi",
                icon: "success",
                allowEscapeKey: false,
                allowOutsideClick: false,
                showConfirmButton: false,
                timer: 700
            });
            TableReload();

        },
        error: function(request, error) {
            Swal.fire({
                title: "Hata Oluştu",
                text: "Ayrıntılar için hata kayıtlarına bakın",
                icon: "success"
            });
        }
    });
}
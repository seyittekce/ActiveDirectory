function showLoading() {
    Swal.fire({
        title: "Yükleniyor",
        imageUrl: "/loading.gif",
        imageWidth: 100,
        imageHeight: 100,
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: false
    });
}

function ShowLoadWithUrl(url, title) {
    history.pushState({}, title, url);
    Swal.fire({
        title: "Yükleniyor",
        imageUrl: "/loading.gif",
        imageWidth: 100,
        imageHeight: 100,
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: false
    });
}

function hideSwall() {
    Swal.fire({
        title: "Yüklendi",
        icon: "success",
        allowEscapeKey: false,
        allowOutsideClick: false,
        showConfirmButton: false,
        timer: 700
    });
}

function ErrorSwall() {
    Swal.fire(
        "Hata!",
        "Hata meydana geldi. Sistem yöneticinizle iletişime geçin",
        "error"
    );
}

function Create() {
    $.ajax({
        url: "/DomainUser/Create",
        type: "Get",
        beforeSend: function() {
            showLoading();
        },
        success: function(result) {
            $("#RenderCrate").html(result);
            $("#GlobalModal").modal("show");
        },
        complete: function(aa) {
            hideSwall();
        },
        error: function(request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}
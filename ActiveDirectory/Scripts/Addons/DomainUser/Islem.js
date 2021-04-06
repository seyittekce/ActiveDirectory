function Duzenle(ID) {
    $.ajax({
        url: "/DomainUser/Create/" + ID,
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

function IslemYapS(ID, EmpNumber) {
    var aa = document.getElementById("durum");
    $.ajax({
        url: "/DomainUser/IslemYap",
        type: "Get",
        data: {
            ID: ID,
            durum: aa.value
        },
        beforeSend: function() {
            showLoading();
        },
        success: function(result) {
            Done(EmpNumber);
        },
        complete: function(aa) {
            hideSwall();
        },
        error: function(request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });
}

function DuzeltButton() {
    var selectBox = document.getElementById("durum");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;
    var OnaylaBtn = document.getElementById("OnaylaButton");
    var DuzeltBtn = document.getElementById("Duzenles");
    if (selectedValue == 3) {
        OnaylaBtn.style.display = "none";
        DuzeltBtn.style.display = "block";
    }
    if (selectedValue == 1 || selectedValue == 2) {
        OnaylaBtn.style.display = "block";
        DuzeltBtn.style.display = "none";
    }
}

function Done(GetId) {
    $.ajax({
        url: "/DomainUser/DoneResult",
        type: "Get",
        data: {
            ID: GetId,
        },
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
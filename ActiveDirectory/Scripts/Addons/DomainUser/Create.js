FormValidation.formValidation(
    document.getElementById("kt_form"),
    {
        fields: {
            FirstName: {
                validators: {
                    notEmpty: {
                        message: "First Name is required"
                    }
                }
            },
            LastName: {
                validators: {
                    notEmpty: {
                        message: "Last Name is required"
                    }
                }
            },
            EmployeNumber: {
                validators: {
                    notEmpty: {
                        message: "Employee Number is required"
                    }
                }
            },
            OtherPhoneNumber: {
                validators: {
                    notEmpty: {
                        message: "phone number is required"
                    }
                }
            }
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            bootstrap: new FormValidation.plugins.Bootstrap()
        }
    }
);

function GetCity() {
    var selectBox = document.getElementById("Company");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;
    $.ajax({
        url: "../../DomainData/GetCity",
        type: "Get",
        data: {
            OU: selectedValue
        },
        beforeSend: function() {
            document.getElementById("City").innerHTML = "";
            document.getElementById("Manager").innerHTML = "";
            document.getElementById("Group").innerHTML = "";
            //document.getElementById("CitySpinner").classList.add("spinner");
        },
        success: function(value) {
            $.each(value,
                function(i, item) {
                    var option = new Option(item.City, item.City);
                    $(option).html(item.City);
                    $("#City").append(
                        option
                    );
                });
            GetManager();
        },
        complete: function() {
            document.getElementById("CitySpinner").classList.remove("spinner");
        }
    });
}

function GetManager() {
    var selectBox2 = document.getElementById("Company");
    var selectedValue2 = selectBox2.options[selectBox2.selectedIndex].value;
    var selectBox1 = document.getElementById("City");
    var selectedValue1 = selectBox1.options[selectBox1.selectedIndex].value;
    $.ajax({
        url: "../../DomainData/Manager",
        type: "Get",
        data: {
            City: selectedValue1,
            Company: selectedValue2
        },
        beforeSend: function() {
            document.getElementById("Manager").innerHTML = "";
            document.getElementById("Group").innerHTML = "";
            document.getElementById("SimilarSpinner").classList.add("spinner");
            document.getElementById("ManagerSpinner").classList.add("spinner");
        },
        success: function(value) {
            $.each(value,
                function(i, item) {
                    var option1 = new Option(item.Display, item.DName);
                    var option2 = new Option(item.Display, item.DName);
                    $(option1).html(item.Display);
                    $(option2).html(item.Display);
                    $("#Manager").append(
                        option1
                    );
                    $("#Group").append(
                        option2
                    );
                });
        },
        complete: function(val) {
            document.getElementById("SimilarSpinner").classList.remove("spinner");
            document.getElementById("ManagerSpinner").classList.remove("spinner");
        }
    });
}

jQuery(document).ready(function() {
    GetCity();
});
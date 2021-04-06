"use strict";
// Class definition
var KTWizard2 = function () {
    // Base elements
    var _formEl;
    var _validations = [];
    var initValidation = function () {
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        // Step 1

            {
                
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        ));
      
        // Step 5
    }
    return {
        // public functions
        init: function () {
            _wizardEl = KTUtil.getById('kt_wizard_v2');
            _formEl = KTUtil.getById('kt_form');
            initWizard();
            initValidation();
        }
    };
}();
jQuery(document).ready(function () {
    KTWizard2.init();
});

define(['jquery'], function ($) {
    'use strict';

    var formElem = $('#select-payment-type-form');
    var cardDetailsWrapper = $('#card-details');
    var paymentTypeSelectElem = $('select[name="SelectedPaymentMethod"]', formElem);

    function showHideCardDetailsCollectionForm() {
        if ($('option:selected', paymentTypeSelectElem).text().toLowerCase() === 'credit card') {
            cardDetailsWrapper.show();
        } else {
            cardDetailsWrapper.hide();
        }
    }

    function initCardDetailCollectionForm() {
        paymentTypeSelectElem.on('change', function() {
            showHideCardDetailsCollectionForm();
        });

        showHideCardDetailsCollectionForm();
    }

    var module = {

        init: function () {
            $(document).ready(function () {
                initCardDetailCollectionForm();
            });
        }

    };

    return module;

});

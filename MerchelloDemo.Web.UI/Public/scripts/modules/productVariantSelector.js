define(['jquery'], function ($) {
    'use strict';

    var formElem = $('#add-to-basket-form');
    var optionSelectElems = $('select', formElem);
    var priceDisplayElem = $('#product-price');

    var productKey = $('input[name="ProductKey"]', formElem).val();

    function updatePriceOnOptionSelection() {
        $('select', formElem).on('change', function () {
            updatePriceForVariant();
        });
    }

    function updatePriceForVariant() {
        var optionKeys = [];
        optionSelectElems.each(function () {
            optionKeys.push($(this).val());
        });

        var url = '/umbraco/surface/ProductPage/GetPriceForProductVariant';
        $.ajaxSettings.traditional = true;
        $.getJSON(url, {
            productKey: productKey,
            optionKeys: optionKeys
        })
        .done(function (data) {
            priceDisplayElem.text('$' + data.price.toFixed(2));
        });
    }

    var module = {

        init: function () {
            $(document).ready(function () {
                updatePriceForVariant();
                updatePriceOnOptionSelection();
            });
        }

    };

    return module;

});

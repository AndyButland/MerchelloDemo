define(['jquery'], function ($) {
    'use strict';

    var formElem = $('#add-to-basket-form');
    var optionSelectElems = $('select', formElem);
    var priceDisplayElem = $('#product-detail .product-price');
    var currentPriceDisplayElem = $('.current-price', priceDisplayElem);
    var salePriceDisplayElem = $('.sale-price', priceDisplayElem);
    var variantPrefixElem = $('.variant-prefix', priceDisplayElem);

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
            currentPriceDisplayElem.text('$' + data.price.toFixed(2));
            if (data.onSale) {
                currentPriceDisplayElem.addClass('current-price-on-sale');
            } else {
                currentPriceDisplayElem.removeClass('current-price-on-sale');
            }

            salePriceDisplayElem.text('$' + data.salePrice.toFixed(2));
            if (data.onSale) {
                salePriceDisplayElem.addClass('sale-price-on-sale');
            } else {
                salePriceDisplayElem.removeClass('sale-price-on-sale');
            }

            variantPrefixElem.hide();   // Displaying details for variant, so no need for the "From: " prefix
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

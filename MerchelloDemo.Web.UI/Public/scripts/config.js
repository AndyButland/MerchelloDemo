require.config({
    paths: {
        'jquery': 'libs/jquery-2.1.1',
        'jquery.validate': 'libs/jquery.validate',
        'jquery.validate.unobtrusive': 'libs/jquery.validate.unobtrusive',

        'paymentMethodSelector': 'modules/paymentMethodSelector',
        'productVariantSelector': 'modules/productVariantSelector'
    },

    shim: {
        'jquery.validate': ['jquery'],
        'jquery.validate.unobtrusive': ['jquery', 'jquery.validate']
    }

});

require(['jquery', 'jquery.validate', 'jquery.validate.unobtrusive'],
    function ($) {
        'use strict';

        if ($('#add-to-basket-form').length > 0) {
            require(['productVariantSelector'], function (module) {
                module.init();
            });
        }

        if ($('#select-payment-type-form').length > 0) {
            require(['paymentMethodSelector'], function (module) {
                module.init();
            });
        }

    }
);

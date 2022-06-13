
var onloadCallback = function () {
    grecaptcha.render('divcaptcha', {
        'sitekey': '6LeDmCYgAAAAABeq5a6Tb2N8FB9KfeTxoHJjBnKW',
        'callback': function (response) {
            console.log('ASD')
            $('#txtCaptcha').val(window.grecaptcha.getResponse());
        }
    })
}

setTimeout(function () {
    $('.alert').slideUp();
}, 5000);


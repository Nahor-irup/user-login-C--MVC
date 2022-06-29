
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

window.onload = function () {
    $('#password-strength-meter').hide();

}


function passStrength(val) {
    var newPassword = document.getElementById('pass').value;
    let passLength = $('#pass').val().length;
    console.log(passLength);
    console.log(newPassword);

    var strongRegex = new RegExp("^(?=.{8,15})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g"); // special char 2 ! W
    var mediumRegex = new RegExp("^(?=.{6,15})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");// 6 char A Z az 09
    var lowRegex = new RegExp("(?=.{5,}).*", "g"); //less than 5 char

    if (strongRegex.test(newPassword)) {
        $('#password-strength-meter').val(4);
        $('#password-strength-meter').show();
    }
    else if (mediumRegex.test(newPassword)) {
        $('#password-strength-meter').val(2);
        $('#password-strength-meter').show();
    }
    else if (lowRegex.test(newPassword)) {
        $('#password-strength-meter').show();
        $('#password-strength-meter').val(1);
    }
    else {
        $('#password-strength-meter').val(1);
        $('#password-strength-meter').hide();
    }
}
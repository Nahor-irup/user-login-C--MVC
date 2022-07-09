

setTimeout(function () {
    $('.alert').slideUp();
}, 5000);

window.onload = function () {
    $('#password-strength-meter').hide();
}


function passStrength(val) {
    var newPassword = document.getElementById('pass').value
    var strongRegex = new RegExp("^(?=.{8,15})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g"); // special char 2 ! W
    var mediumRegex = new RegExp("^(?=.{6,15})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");// 6 char A Z az 09
    var lowRegex = new RegExp("(?=.{5,}).*", "g"); //less than 5 char
    if (strongRegex.test(newPassword)) {
        $('#password-strength-meter').val(4);
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
        $('#password-strength-meter').show();
    }
}


function showPassword() {
    var pass = document.getElementById("pass");
    var repass = document.getElementById("repass");
    if (pass.type == "password" && repass.type == "password") {
        pass.type = "text";
        repass.type = "text";
        document.getElementById('eye').className = "fas fa-eye-slash";
    } else {
        pass.type = "password";
        repass.type = "password";
        document.getElementById('eye').className = "fas fa-eye";
    }
}

function showPasswordSignin() {
    var pass = document.getElementById("pass");
    if (pass.type == "password") {
        pass.type = "text";
        document.getElementById('eye').className = "fas fa-eye-slash";
    } else {
        pass.type = "password";
        document.getElementById('eye').className = "fas fa-eye";
    }
}
$(document).ready(function () {
    //Khai báo biến
    var $email = $('#email');
    var $BtnSend = $('#BtnSend');
    var $BtnOtp = $('#BtnOtp');
    var $emailErrorMessage = $email.next('p');
    var $otp1 = $('#otp1');
    var $ServerOTP = $('#ServerOTP');
    var $password = $('#password');
    var $rePassword = $('#rePassword');
    var $submit = $('#submit');

    var otpSentTime; 
//Vô hiệu hoá nút gửi OTP đến emai và nút Kiểm tra mã OTP
    $BtnSend.prop("disabled", true);
    $BtnOtp.prop("disabled", true);

    //Chuyển từ form email sang form nhập OTP
    $("#BtnSend").click(function () {
        $("#EntryOTP").addClass('showform');
        $("#Entryemail").removeClass('showform');
        $('#btnreSend').css('pointer-events', 'none');
        updateCountdown(30);
    });


    //Chuyển từ form OTP về form email
    $(".arrow").click(function () {
        $("#EntryOTP").removeClass("showform");
        $("#Entryemail").addClass("showform");
    });

    //Chuyển từ form nhập pass về OTP
    $(".arrow2").click(function () {
        $("#Resetpass").removeClass("showform");
        $("#EntryOTP").addClass("showform");
    });

//Kiểm tra email hợp lệ hay không
    $email.on('change', function () {
        var email = $email.val();
        var noError = true;

        const emailPattern = /^[a-zA-Z][a-zA-Z0-9._%+-]+@[^\s@]+\.[^\s@]{2,}$/;

        if (email === '') {
            $emailErrorMessage.text("Please enter your email.");
            noError = false;
        } else if (!emailPattern.test(email)) {
            $emailErrorMessage.text("Email address is invalid.");
            noError = false;
        } else {
            $emailErrorMessage.text("");
        }
//Cập nhật trạng thái nút tương ứng với noError
        $BtnSend.prop("disabled", !noError);

        if (noError) {
            var request = $.ajax({
                type: 'POST',
                data: {
                    email: email,
                },
                url: '/Account/CheckEmailAsync'
            });

            request.done(function (result) {
                var isValid = result === "true"; 
                if (!isValid) {
                    console.log("result là: " + result);
                    $emailErrorMessage.text("Email does not exist");
                    $BtnSend.prop("disabled", true);
                } else {
                    $BtnSend.prop("disabled", false);
                }
            });
        }
        $('#emailSend').val(email);
        $(".emailmsg").html("<span>" + email + "</span>");
    });

//Đảm bảo OTP được nhập
    $otp1.on('change', function () {
        var otp1 = $otp1.val();
        if (otp1 !== null) {
            $BtnOtp.prop("disabled", false);
        }
    });
    
    //Khi nút Send được nhấn thì sẽ gửi mã OTP đến email tương ứng
    $BtnSend.click(function () {
        var email = $email.val();
        var request = $.ajax({
            type: 'POST',
            data: {
                email: email
            },
            url: '/Account/SendOTP'
        });

        request.done(function (result) {
         
            console.log("Server response: ", result);
            if (result.otp !== null) {
                console.log("result là: " + result.otp);
                $ServerOTP.val(result.otp);
                console.log("mã server là: " + $ServerOTP.val());
                otpSentTime = new Date().getTime(); // Lưu thời gian gửi OTP
               
            }
        });
      

        request.fail(function (jqXHR, textStatus) {
            console.log("Request failed: " + textStatus);
        });
    });

//Lấy giá trị của mã OTP người dùng nhập và so sánh với mã OTP của server xem hợp lệ không
    $BtnOtp.click(function () {
        var OTP = "";
        for (var i = 1; i <= 6; i++) {
            OTP += $('#otp' + i).val();
        }
        var currentTime = new Date().getTime(); 
        var timeLimit = 1.5 * 60 * 1000;
        var ServerOtp = $ServerOTP.val();
        console.log("JS OTP is: " + OTP);
        console.log("JS ServerOtp is: " + ServerOtp);

        if (otpSentTime && (currentTime - otpSentTime > timeLimit)) {
            $('#err-otp-msg').text('OTP has expired! Please request a new one.');
            return;
        }
        if (OTP === ServerOtp) {
            $("#Resetpass").addClass("showform");
            $("#EntryOTP").removeClass("showform");
            $('#err-otp-msg').text('');
        } else {
            $('#err-otp-msg').text('OTP is incorrect!');
        }
    });
    
//Kiểm tra pass hợp lệ không
    $password.on('blur', function () {
        var password = $password.val();
        $submit.prop("disabled", true);
        if (password === "") {
            $password.next('p').text("Please enter password");
        } else if (password.length < 8) { //đổi chỗ này
            $password.next('p').text("Password must be at least 8 characters!");
        } else if (!/(?=.*[A-Z])(?=.*[!@#$%^&*])(.{8,})/.test(password)) {
            $password.next('p').text("Password must have uppercase, special character!");
            noError = false;
        } else {
            $password.next('p').text("");
            $submit.prop("disabled", false);
        }
        console.log("pass" + $submit.prop("disabled"));
    });
    
//Kiểm tra repass hợp lệ không + đúng với pass
    $rePassword.on('blur', function () {
        $submit.prop("disabled", true);
        var rePassword = $rePassword.val();
        var password = $password.val();
        if (rePassword === "") {
            $rePassword.next('p').text("Please confirm your password");
        } else if (rePassword !== password) {
            $rePassword.next('p').text("The confirm password does not matches!");
        } else {
            $rePassword.next('p').text("");
            $submit.prop("disabled", false);
        }
        console.log("repass" + $submit.prop("disabled"));
    });
});



function ResendOTP() {
    var $email = $('#email');
    var email = $email.val();
    var $ServerOTP = $('#ServerOTP');


    if ($('#countDown').length === 0) {
        $("#btnreSend").append('<span id="countDown">(30)</span>');
        $('#btnreSend').css('pointer-events', 'none'); // Vô hiệu hóa nút gửi lại
        console.log("bam dc");

        var request = $.ajax({
            type: 'POST',
            data: {
                email: email
            },
            url: '/Account/SendOTP'
        });

        request.done(function (result) {
            if (result.otp !== null) {
                console.log("result là: " + result.otp);
                $ServerOTP.val(result.otp);
                console.log("mã server là: " + $ServerOTP.val());
                otpSentTime = new Date().getTime(); // Lưu thời gian gửi OTP
                updateCountdown(30); // Bắt đầu đếm ngược
            }
        });
    }
}


function updateCountdown(count) {
    $('#countDown').text('(' + count + ')'); // Cập nhật đếm ngược

    if (count <= 0) {
        // Xóa đếm ngược và cho phép gửi lại
        $('#countDown').remove();
        $('#btnreSend').css('pointer-events', 'auto'); // Kích hoạt nút gửi lại
    } else {
        setTimeout(function () {
            updateCountdown(count - 1); // Gọi lại với giá trị đếm ngược giảm đi 1
        }, 1000);
    }
}

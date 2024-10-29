$(document).ready(function () {
    // Khai báo biến
    var $email = $('#email');
    var $BtnSend = $('#BtnSend');
    var $BtnOtp = $('#BtnOtp');
    var $otp1 = $('#otp1');
    var $ServerOTP = $('#ServerOTP');

    var otpSentTime;
    sendMail();
    // Vô hiệu hoá nút gửi OTP đến email và nút Kiểm tra mã OTP
    $BtnSend.prop("disabled", true);
    $BtnOtp.prop("disabled", true);

    $otp1.on('change', function () {
        var otp1 = $otp1.val();
        if (otp1 !== null) {
            $BtnOtp.prop("disabled", false);
        }
    });

    // Khi nút Send được nhấn thì sẽ gửi mã OTP đến email tương ứng
   function sendMail () {
        // Lấy dữ liệu từ localStorage
        const registrationData = JSON.parse(localStorage.getItem('registrationData'));
        if (registrationData) {
            const email = registrationData.email;

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
                    updateCountdown(30);
                    console.log("mã server là: " + $ServerOTP.val());
                    otpSentTime = new Date().getTime(); // Lưu thời gian gửi OTP
                }
            });

            request.fail(function (jqXHR, textStatus) {
                console.log("Request failed: " + textStatus);
            });
        }
    }

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
            $('#err-otp-msg').text('');
            registerAccount(); // Gọi hàm đăng ký tài khoản nếu OTP đúng
        } else {
            $('#err-otp-msg').text('OTP is incorrect!');
        }
    });

    function registerAccount() {
        // Lấy dữ liệu từ localStorage
        const registrationData = JSON.parse(localStorage.getItem('registrationData'));
        if (registrationData) {
            const username = registrationData.username;
            const fullname = registrationData.fullname;
            const phone = registrationData.phone;
            const email = registrationData.email; // Địa chỉ email đã được gửi mã OTP
            const password = registrationData.password;
            const rePassword = registrationData.rePassword;

            $.ajax({
                url: '/Register/OnPostRegister',
                type: "POST",
                data: {
                    username: username,
                    fullname: fullname,
                    phone: phone,
                    email: email,
                    password: password,
                    rePassword: rePassword
                },
                success: function (data) {
                    if (data === "False") {
                        console.log("Registration failed");
                    } else {
                        window.location.href = '/Login';
                    }
                },
                error: function () {
                    $('#registError').text('An error occurred. Please try again later.').show();
                }
            });

            // Xóa dữ liệu khỏi localStorage nếu không cần thiết nữa
            localStorage.removeItem('registrationData');
        }
    }

    // Các hàm khác vẫn giữ nguyên
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
});

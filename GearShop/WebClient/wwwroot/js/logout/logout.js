const handleLogout = () => {

    $.ajax({
        url: '/Login/Logout',
        type: "POST",
        success: function (data) {
            sessionStorage.removeItem("username");
            sessionStorage.removeItem("ProId");
            window.location.href = "/";
        },
        error: function () {
            $('#loginError').text('An error occurred. Please try again later.').show();
        }
    });
}
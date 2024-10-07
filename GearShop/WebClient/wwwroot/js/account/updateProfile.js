const handleUpdateProfile = () => {
    var fullname = getValueById("fullnameUpdate");
    var email = getValueById("emailUpdate");
    var phone = getValueById("phoneUpdate");

    // Clear previous error messages
    document.getElementById("errorFullname").textContent = "";
    document.getElementById("errorEmail").textContent = "";
    document.getElementById("errorPhone").textContent = "";

    // Verify information
    let hasError = false;

    if (!fullname) {
        document.getElementById("errorFullname").textContent = "Full name is required.";
        hasError = true;
    }

    if (!email || !validateEmail(email)) {
        document.getElementById("errorEmail").textContent = "A valid email is required.";
        hasError = true;
    }

    if (!phone || !validatePhone(phone)) {
        document.getElementById("errorPhone").textContent = "A valid phone number is required.";
        hasError = true;
    }

    if (hasError) {
        return; // Stop execution if there are validation errors
    }

    $.ajax({
        url: '/Customers/UpdateProfile',
        type: "POST",
        data: {
            FullName: fullname,
            Email: email,
            Phone: phone
        },
        success: function (response) {
            if (response) {
                window.location.reload();
                console.log("Profile updated successfully");
            }
        },
        error: function (error) {
            console.error("Error updating profile", error);
        }
    });
}

let originalValues = {};
const toggleEditMode = () => {
    const isReadOnly = document.getElementById("fullnameUpdate").readOnly;
    const button = document.getElementById("editUpdateButton");

    if (isReadOnly) {
        // Save original values
        originalValues = {
            fullname: getValueById("fullnameUpdate"),
            email: getValueById("emailUpdate"),
            phone: getValueById("phoneUpdate")
        };

        // Enable editing
        document.getElementById("fullnameUpdate").readOnly = false;
        document.getElementById("emailUpdate").readOnly = false;
        document.getElementById("phoneUpdate").readOnly = false;
        button.textContent = "Update";
        button.onclick = handleUpdateProfile;

        // Add Cancel button
        const cancelButton = document.createElement("button");
        cancelButton.id = "cancelButton";
        cancelButton.textContent = "Cancel";
        cancelButton.className = "button-32";
        cancelButton.onclick = cancelEdit;
        document.querySelector(".col-md-12.form-group").appendChild(cancelButton);
    } else {
        // Call update function
        handleUpdateProfile();
    }
}

const cancelEdit = () => {
    // Revert to original values
    document.getElementById("fullnameUpdate").value = originalValues.fullname;
    document.getElementById("emailUpdate").value = originalValues.email;
    document.getElementById("phoneUpdate").value = originalValues.phone;

    // Set fields to read-only
    document.getElementById("fullnameUpdate").readOnly = true;
    document.getElementById("emailUpdate").readOnly = true;
    document.getElementById("phoneUpdate").readOnly = true;

    // Change button back to Edit
    const button = document.getElementById("editUpdateButton");
    button.textContent = "Edit";
    button.onclick = toggleEditMode;

    // Remove Cancel button
    const cancelButton = document.getElementById("cancelButton");
    if (cancelButton) {
        cancelButton.remove();
    }
}

function getValueById(id) {
    return $('#' + id).val();
}

function validateEmail(email) {
    // Simple email validation regex
    var re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

function validatePhone(phone) {
    // Phone validation to check if it starts with 0 and has a maximum of 11 digits
    var re = /^0\d{0,10}$/;
    return re.test(phone);
}